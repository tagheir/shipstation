
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Generics
{
    public static class Functions
    {
        public static string localFolderBasePath = null;

        public static string RemoveCharacters(this string s, params char[] unwantedCharacters)
        => s == null ? null : string.Join(string.Empty, s.Split(unwantedCharacters));
        public static string RemoveCharacterFromString(this string data,Char ch)
        {
            var ss = string.Join(string.Empty, data.Split(ch));
            
            return ss;
        }

        public static DateTime GetEasternDateTimeByRegion(this DateTime timeUtc )
        {
            
            
            TimeZoneInfo easternZone;
            TimeZoneInfo easternStandardTime = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                easternStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                easternStandardTime = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                easternStandardTime = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
            }

            easternZone = easternStandardTime;
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            return easternTime;
        }

        public static List<string> ReadFileFromFolder(this string folderPath)
        {
            List<string> files = new List<string>();
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.*"))
            {
                files.Add(file);
            }
            return files;
        }

        public static string GetHttpResponse(string url)
        {
            using var wc = new WebClient();
            try
            {
                return wc.DownloadString(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public static T FromJson<T>(this string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                return default;
            }
        }
        public static string ToJson<T>(this T obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static bool IsEmpty(this string str) => str == null || string.IsNullOrWhiteSpace(str);
        public static bool IsNotEmpty(this string str) => !str.IsEmpty();

        public static string GetClaimValue(this IEnumerable<System.Security.Claims.Claim> claims, string key)
            => claims?.FirstOrDefault(u => u.Type == key)?.Value;
        public static string GetIdFromClaim(this IEnumerable<System.Security.Claims.Claim> claims)
            => claims.GetClaimValue("UserId");

        public static bool ToBool(this string str)
        {
            try
            {
                return bool.Parse(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                Console.WriteLine(ex);
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
                Console.WriteLine(e);
                return 0;
            }
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
        public static string RemovesAllCharactersFromString(this string data)
        {
            if(data != null)
            {
                data = data.Trim();
                data = data.Replace("\t", "");
                data = data.Replace("\n", "");
            }
         
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                return 0;
            }
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static bool HasAttribute(this PropertyInfo propertyInfo, Type attribute)
        {
            if (propertyInfo == null || attribute == null) return false;
            var found = propertyInfo.CustomAttributes.FirstOrDefault(p => p.AttributeType == attribute);
            return found != null;
        }
        public static string GetAttributeColumnName(this Type type, Type attribute)
        {
            if (type == null || attribute == null) return null;
            var property = type.GetProperties().FirstOrDefault(p => p.HasAttribute(attribute));
            return property?.Name;
        }
        public static decimal ToDecimal(this string number)
        {
            try
            {
                return decimal.Parse(number);
            }
            catch (Exception)
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
            catch (Exception)
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
            decimal parsedNumber;
            try
            {
                var number = (Regex.Split(str, @"[^0-9\.]+")
                                  .FirstOrDefault(c => c != "." && c.Trim() != "") ?? "0").ToDecimal();
                parsedNumber = Math.Floor(number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }

            return parsedNumber;
        }

        public static List<List<T>> ListDivider<T>(this IEnumerable<T> enumerable, int nSize = 3)
        {
            if (enumerable == null) return new List<List<T>>();

            var list = enumerable.ToList();
            if (nSize < 1)
                return new List<List<T>>() { list.ToList() };
            var listOfList = new List<List<T>>();
            while (list.Any())
            {
                listOfList.Add(list.Take(nSize).ToList());
                list = list.Skip(nSize).ToList();
            }
            return listOfList;
        }

    }
}
