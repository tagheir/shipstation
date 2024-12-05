using AutoMapper;

using Generics.Services.DatabaseService.AdoNet;

using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Generics.Common
{
    public static class Functions
    {
#pragma warning disable 0168
        private static IMapper _mapper;

        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool IsEmpty(this StringValues str) => string.IsNullOrWhiteSpace(str);

        public static bool IsAnyEmpty(this KeyValuePair<string, StringValues> str) => str.Key.IsEmpty() || str.Value.IsEmpty();

        public static bool IsAnyEmpty(this KeyValuePair<string, string> str) => str.Key.IsEmpty() || str.Value.IsEmpty();

        public static bool IsBothEmpty(this KeyValuePair<string, StringValues> str) => str.Key.IsEmpty() && str.Value.IsEmpty();

        public static bool IsBothEmpty(this KeyValuePair<string, string> str) => str.Key.IsEmpty() && str.Value.IsEmpty();

        public static bool IsListEmpty<T>(this List<T> list) => list == null && list.Count == 0;
        public static T Clone<T>(this T source) =>
        JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));


        public static string ToQueryString(this NameValueCollection collection)
        {
            if (collection == null) return null;
            var list = new List<string>();
            foreach (var c in collection.AllKeys)
            {
                var pair = $"{c}={collection[c]}";
                Console.WriteLine(pair);
                list.Add(pair);
            }
            if (list.Count <= 1)
                return list.FirstOrDefault();
            return list.Aggregate((c, n) => $"{c}&{n}");
        }
        public static NameValueCollection ToNameValueCollection<T>(this T obj, string prefix = "", bool preferJsonPropertyName = false)
        {
            var type = obj.GetType();
            var collection = new NameValueCollection();
            if (type.IsSimple())
            {
                collection.Add(prefix, obj.ToString());
            }
            else if (type.IsCollectionType())
            {
                var list = obj as IList;
                var i = 0;
                foreach (var item in list)
                {
                    if (item.GetType().IsSimple())
                    {
                        collection.Add($"{prefix}[{i++}]", item.ToString());
                    }
                    else
                    {
                        var tempCollection = item.ToNameValueCollection($"{prefix}[{i++}]", preferJsonPropertyName);
                        collection.Add(tempCollection);
                    }
                }
            }
            else if (!type.IsCollectionType() && !type.IsSimple())
            {
                var properties = type.GetProperties().ToList();
                foreach (var classProp in properties)
                {
                    object property = type.GetProperty(classProp.Name).GetValue(obj);
                    if (property == null || property.ToString().IsEmpty()) continue;
                    var propertyType = property.GetType();
                    var basePropertyName = classProp.Name;
                    if (preferJsonPropertyName)
                    {
                        var attributeName = classProp.CustomAttributes
                            .FirstOrDefault(a => a.AttributeType.Name.ToString() == "JsonPropertyAttribute" || a.AttributeType.ToString() == "JsonPropertyNameAttribute");
                        //if (attributeName != null)
                        //    basePropertyName = attributeName;
                        if (attributeName?.ConstructorArguments?.FirstOrDefault().Value != null)
                            basePropertyName = attributeName.ConstructorArguments.FirstOrDefault().Value.ToString();
                    }
                    var propertyName = prefix.IsEmpty() ? basePropertyName : $"{prefix}.{basePropertyName}";
                    if (propertyType.IsSimple())
                    {
                        collection.Add(propertyName, property.ToString());
                    }
                    else
                    {
                        var tempCollection = property.ToNameValueCollection(propertyName, preferJsonPropertyName);
                        collection.Add(tempCollection);
                    }

                }
            }
            return collection;
        }



        public static int GetEnumValue<T>(this T val)
        {

            var controltypes = new Dictionary<int, string>();
            foreach (var foo in Enum.GetValues(typeof(T)))
            {
                if (foo.Equals(val))
                {
                    return (int)foo;
                }
                controltypes.Add((int)foo, foo.ToString());
            }
            return -1;
        }
        public static Dictionary<int, string> GetEnumValues<T>()
        {

            var controltypes = new Dictionary<int, string>();
            foreach (var foo in Enum.GetValues(typeof(T)))
            {
                controltypes.Add((int)foo, foo.ToString());
            }
            return controltypes;
        }

        public static string BooleanToStringCheckBoxValue(this bool val)
        {
            return (val ? "True" : "");
        }
        public static T ConvertEnum<T>(this int i) where T : struct, IConvertible
        {

            return (T)(object)i;
        }



        public static T ConvertStringToEnum<T>(this string i) where T : struct, IConvertible
        {
            var enums = GetEnumValues<T>();
            foreach (var val in enums)
            {
                if (val.Value.Equals(i))
                {
                    return (T)(object)val.Key;
                }
            }
            return default;
        }



        public static To ToMapViaJson<From, To>(this From obj)
        {
            try
            {
                return obj.ToJson().FromJson<To>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return default;
            }
        }




        public static Dictionary<string, StringValues> CreateDictionaryFromKeyValuePairs(
            List<KeyValuePair<string, string>> form)
        {
            if (form == null) return null;
            var keyValuePairs = new Dictionary<string, StringValues>();
            foreach (var keyvalues in form)
            {
                if (!keyValuePairs.ContainsKey(keyvalues.Key))
                {
                    keyValuePairs.Add(keyvalues.Key, keyvalues.Value);
                }
            }
            return keyValuePairs;
        }

        public static string GetPhoneNumber(this string phoneNumberRaw)
        {
            phoneNumberRaw = new string(phoneNumberRaw.Where(char.IsDigit).ToArray());
            if (phoneNumberRaw.Length > 10)
                phoneNumberRaw = new string(phoneNumberRaw.Skip(phoneNumberRaw.Length - 10).ToArray());
            else if (phoneNumberRaw.Length < 10) return null;
            return "+92" + phoneNumberRaw;
        }
        public static bool CompareDate(this DateTime d1, DateTime d2)
        {
            return d1.Day == d2.Day && d1.Month == d2.Month && d1.Year == d2.Year;
        }
        public static string ReplaceMultiplesWithSingle(this string data, string replaceStr = " ")
        {
            if (data == null) return null;

            if (string.IsNullOrWhiteSpace(data)) return "";
            while (data.Contains(replaceStr + replaceStr))
                data = data.Replace(replaceStr + replaceStr, replaceStr);
            return data;
        }
        public static int ToInt(this int? number)
        {
            try
            {
                return number ?? 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static int ToInt(this bool? bit)
        {
            try
            {
                if (!bit.HasValue) return 0;
                return bit.Value ? 1 : 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static int ToInt(this bool bit)
        {
            try
            {
                return bit ? 1 : 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        public static Dictionary<T1, T2> ToCustomDictionary<T1, T2>(this List<KeyValuePair<T1, T2>> list)
        {
            var dictionary = new Dictionary<T1, T2>();
            foreach (var pair in list)
            {
                dictionary[pair.Key] = pair.Value;
            }
            return dictionary;
        }

        //public static Dictionary<string, StringValues> CreateDictionaryFromKeyValuePairs(List<KeyValuePair<string, string>> form)
        //{
        //    if (form == null) return null;
        //    Dictionary<string, StringValues> keyValuePairs = new Dictionary<string, StringValues>();
        //    foreach (var keyvalues in form)
        //    {
        //        if (!keyValuePairs.ContainsKey(keyvalues.Key))
        //        {
        //            keyValuePairs.Add(keyvalues.Key, keyvalues.Value);
        //        }
        //    }
        //    return keyValuePairs;
        //}
        public static int ToInt(this string number)
        {
            try
            {
                return int.Parse(number);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static bool ToBool(this string str)
        {
            try
            {
                return bool.Parse(str);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static int ToBoolInt(this string str) => str.ToBool().ToInt();
        public static bool IsSimple(this Type type) =>
            type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)) || type.Equals(typeof(decimal));
        public static bool IsCollectionType(this Type type)
        {
            try
            {

                return (type.GetInterface(nameof(ICollection)) != null);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static long ToLong(this string number)
        {
            try
            {
                return long.Parse(number);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static string SubStr(this string str, int start, int length)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";
            if (str.Length > (length - start))
            {
                return str.Substring(start, length);
            }
            else if (str.Length < Math.Abs(start))
            {
                return "";
            }
            else
            {
                return str;
            }
        }
        public static string ConvetObjectToJson<T>(T request)
        {
            var requestJson = JsonConvert.SerializeObject(request,
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            return requestJson;
        }
        public static T ConvertJsonToObject<T>(string request)
        {
            var requestJson = JsonConvert.DeserializeObject(request);
            return (T)requestJson;
        }
        public static decimal ToDecimal(this string number)
        {
            try
            {
                return decimal.Parse(number);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static double ToDouble(this string number)
        {
            try
            {
                return double.Parse(number);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static double ToDoubleFromAmountString(this string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return 0;
            if (number.Contains("$"))
                number = number.Replace("$", "");
            if (number.Contains(","))
                number = number.Replace(",", "");
            return number.ToDouble();
        }

        public static long Floor(this decimal number) => (long)Math.Floor(number);
        public static long Floor(this double number) => (long)Math.Floor(number);
        public static decimal ExtractNumberFromString(this string str)
        {
            decimal parsedNumber = 0;
            try
            {
                var number = (Regex.Split(str, @"[^0-9\.]+")
                                  .FirstOrDefault(c => c != "." && c.Trim() != "") ?? "0").ToDecimal();
                parsedNumber = Math.Floor(number);
            }
            catch (Exception ex)
            {
                return 0;
            }

            return parsedNumber;
        }
        public static List<List<T>> ListDividier<T>(this IEnumerable<T> list, int nsize = 3)
        {
            var listOfList = new List<List<T>>();
            while (list.Any())
            {
                listOfList.Add(list.Take(nsize).ToList());
                list = list.Skip(nsize).ToList();
            }
            return listOfList;
        }

        public static string SerializeXml<T>(this T obj)
        {
            try
            {

                var stringwriter = new StringWriter();
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(stringwriter, obj);
                return stringwriter.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static T DeserializeXml<T>(this string xml)
        {
            try
            {
                var stringReader = new System.IO.StringReader(xml);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        public static string ConvertXmlToJson(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return null;
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                return JsonConvert.SerializeXmlNode(doc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public static T FromXml<T>(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return default(T);
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                T response = default(T);
                using (XmlReader reader = XmlReader.Create((new StringReader(xml))))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    response = (T)serializer.Deserialize(reader);
                }
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default(T);
            }
        }
        public static string ConvertJsontoxml(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;
            try
            {
                var doc = JsonConvert.DeserializeXmlNode(json);
                return doc.InnerXml;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
        public static T Get<T>(this Dictionary<string, T> dictionary, string key, T value = default(T))
        {
            return !dictionary.ContainsKey(key) ? value : dictionary[key];
        }
        public static T2 Get<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value = default(T2))
        {
            return !dictionary.ContainsKey(key) ? value : dictionary[key];
        }
        public static string Get(this Dictionary<string, string> dictionary, string key, string value = "")
        {
            return !dictionary.ContainsKey(key) ? "" : dictionary[key];
        }
        public static string GetEnumDescription(this Enum value)
        {
            string description = value.ToString();

            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (null != attributes && attributes.Length > 0)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                description = attributes[0].Description;
            }

            return description;
        }


        public static void RunParallel<T1>(List<T1> list, Func<T1, bool> func, int noOfParallelThreads)
        {
            var pairList = ListDividier(list, noOfParallelThreads);
            var threads = list.Select(item => new Thread(() => { func(item); })).ToList();
            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
        }


        public static long GetLong(long? value, long defaultValue = 0)
        {
            try
            {
                return value ?? 0;
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static long GetLong(string value, long defaultValue = 0)
        {
            try
            {
                return long.Parse(value);
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static long GetLong(object value, long defaultValue = 0)
        {
            try
            {
                return (long)(value);
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }

        public static double GetDouble(double? value, double defaultValue = 0)
        {
            try
            {
                return value ?? 0;
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static double GetDouble(string value, double defaultValue = 0)
        {
            try
            {
                return double.Parse(value);
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static double GetDouble(object value, double defaultValue = 0)
        {
            try
            {
                return (double)(value);
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }

        public static int GetInt(int? value, int defaultValue = 0)
        {
            try
            {
                return value ?? 0;
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static int GetInt(string value, int defaultValue = 0)
        {
            try
            {
                return int.Parse(value);
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static int GetInt(object value, int defaultValue = 0)
        {
            try
            {
                return (int)(value);
            }

            catch (Exception e)
            {

                return defaultValue;
            }
        }
        public static T ParseJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public static string ConvertToJson<T>(this T json)
        {
            try
            {
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        // combination of values from Windows 7 Registry and 
        // from C:\Windows\System32\inetsrv\config\applicationHost.config
        // some added, including .7z and .dat
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.adobe.air-application-installer-package+zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-itunes-ipa"},
        {".ipg", "application/x-itunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-itunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-itunes-ite"},
        {".itlp", "application/x-itunes-itlp"},
        {".itms", "application/x-itunes-itms"},
        {".itpc", "application/x-itunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/java-archive"},
        {".java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-powerpoint"},
        {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-powerpoint"},
        {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-powerpoint"},
        {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-powerpoint"},
        {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-powerpoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-excel"},
        {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-excel"},
        {".xld", "application/vnd.ms-excel"},
        {".xlk", "application/vnd.ms-excel"},
        {".xll", "application/vnd.ms-excel"},
        {".xlm", "application/vnd.ms-excel"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-excel"},
        {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".zip", "application/x-zip-compressed"},
        #endregion
        
        };

        public static string GetMimeType(this string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }
#pragma warning restore 0168
    }



    public class GenericConverter<T> : JsonConverter
    {
        public static readonly GenericConverter<T> Singleton = new GenericConverter<T>();
        public override bool CanWrite { get { return false; } }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var readerValue = reader.Value?.ToString();
            if (string.IsNullOrWhiteSpace(readerValue)) return default(T);

            var type = typeof(T);

            if (type?.Name == Enums.DataType.Boolean.ToString())
            {
                return readerValue.ToBoolean();
            }
            else if (!string.IsNullOrEmpty(readerValue))
            {
                return readerValue.To(type);
            }
            else if (type.Name == Enums.DataType.String.ToString())
            {
                return readerValue.To(type);
            }
            return readerValue;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(String) || objectType == typeof(Boolean))
            {
                return true;
            }
            return false;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
    }
    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
