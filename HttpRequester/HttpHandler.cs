using Generics.HelperModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Safari;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HttpRequester
{

    public static class HttpHandler
    {
        public static IWebDriver GetDriver(string urlll)
        {
            var IOSDRIVERPATH = @"/Users/a/Downloads";
            IWebDriver driver;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-blink-features=AutomationControlled"); // don't enforce the same-origin policy
                options.AddArgument("--disable-gpu"); // applicable to windows os only

                options.AddExcludedArgument("enable-automation");
                options.AddAdditionalCapability("useAutomationExtension", false);

                string subPath = AppDomain.CurrentDomain.BaseDirectory + "picklist"; // Your code goes here

                bool exists = System.IO.Directory.Exists(subPath);
                if (exists)
                {
                    System.IO.Directory.Delete(subPath, true);
                }
                exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                options.AddUserProfilePreference("download.default_directory", subPath);

                options.AddUserProfilePreference("disable-popup-blocking", "true");
                driver = new ChromeDriver(options)
                {
                    Url = urlll

                };
            }
            else
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-blink-features=AutomationControlled"); // don't enforce the same-origin policy
/*                options.AddArgument("--disable-gpu"); // applicable to windows os only
                options.AddArgument("--user-data-dir=~/chromeTemp");*/
                driver = new ChromeDriver(IOSDRIVERPATH)
                {
                    Url = urlll

                };
            }
            return driver;
        }
        public static IDevToolsSession SetdevTools(IWebDriver driver)
        {
            var tools = driver as IDevTools;
            IDevToolsSession session = tools.GetDevToolsSession();
            return session;
        }
        public static SamsTokenDto Do(string urlll)
        {
            IWebDriver driver;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                driver = new SafariDriver()
                {
                    Url = urlll

                };
            }
            else
            {
                driver = new ChromeDriver(@"/Users/a/Downloads")
                {
                    Url = urlll

                };
            }




            Thread.Sleep(10000);
            var elemnt = driver.FindElement(By.CssSelector("[Id=signInName]"));
            elemnt.SendKeys("Sams8@shopimo.co");

            elemnt = driver.FindElement(By.Id("password"));
            elemnt.SendKeys("LSHOPIMO2017");

            var inputElement = driver.FindElement(By.Id("next"));
            inputElement.SendKeys(Keys.Enter);
            inputElement.Submit();

            Thread.Sleep(10000);


            var pe = driver.FindElement(By.Id("userSelectedMfaMode_email"));
            pe.Click();

            Thread.Sleep(5000);
            inputElement = driver.FindElement(By.Id("continue"));
            inputElement.SendKeys(Keys.Enter);
            //inputElement.Submit();

            Thread.Sleep(10000);


            inputElement = driver.FindElement(By.Id("emailVerificationControl-readOnly_but_send_code"));
            inputElement.Click();

            Thread.Sleep(1000);

            inputElement = driver.FindElement(By.Id("verificationCode"));
            Thread.Sleep(20000);

            inputElement.SendKeys("2588");

            Thread.Sleep(5000);

            inputElement = driver.FindElement(By.Id("emailVerificationControl-readOnly_but_verify_code"));
            inputElement.Click();

            Thread.Sleep(10000);

            inputElement = driver.FindElement(By.Id("continue"));
            inputElement.SendKeys(Keys.Enter);
            inputElement.Submit();


            Thread.Sleep(10000);




            //Console.Write(driver.Url);
            //Puts an Implicit wait, Will wait for 10 seconds before throwing exception
            var urll = driver.Url.Replace("#", "?");
            var url = new Uri(urll);
            string access_token = HttpUtility.ParseQueryString(url.Query).Get("access_token");
            string Idtoken = HttpUtility.ParseQueryString(url.Query).Get("id_token");
            driver.Close();
            return new SamsTokenDto()
            {
                AccessToken = access_token,
                IdToken = Idtoken
            };
        }
        public static string GetFinalRedirect(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            int maxRedirCount = 8;  // prevent infinite loops
            string newUrl = url;
            do
            {
                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Method = "GET";
                    req.AllowAutoRedirect = true;
                    resp = (HttpWebResponse)req.GetResponse();
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return resp.ResponseUri.OriginalString;
                            return newUrl;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.MovedPermanently:
                        case HttpStatusCode.RedirectKeepVerb:
                        case HttpStatusCode.RedirectMethod:
                            newUrl = resp.Headers["Location"];
                            if (newUrl == null)
                                return url;

                            if (newUrl.IndexOf("://", System.StringComparison.Ordinal) == -1)
                            {
                                // Doesn't have a URL Schema, meaning it's a relative or absolute URL
                                Uri u = new Uri(new Uri(url), newUrl);
                                newUrl = u.ToString();
                            }
                            break;
                        default:
                            return newUrl;
                    }
                    url = newUrl;
                }
                catch (WebException e)
                {

                    // Return the last known good URL
                    return e.Response.Headers.Get(1);
                    return newUrl;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (resp != null)
                        resp.Close();
                }
            } while (maxRedirCount-- > 0);

            return newUrl;
        }
        public static async Task<HttpResponser> Request(HttpRequester requester)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(requester.Url);

                webRequest.Method = requester.Method.ToString();
                if (requester.IsAuthorized)
                {
                    webRequest.Headers.Add("AUTHORIZATION", GetAuthorization(requester));
                    webRequest.Headers.Add("CONNECTION", "keep-alive");
                }
                
                webRequest.CookieContainer = new CookieContainer();
                if (requester.CookieContainer != null)
                {
                    webRequest.CookieContainer = requester.CookieContainer;
                }

                if (requester.Params != null && requester.Params.Count > 0)
                {
                    foreach (var param in requester.Params)
                    {
                        if (!string.IsNullOrEmpty(param.Key))
                        {
                            webRequest.Headers.Add(param.Key, param.Value);
                        }
                    }
                }
                if (requester.Method == HttpMethod.Post || requester.Method == HttpMethod.Put || requester.Method == HttpMethod.Delete && requester.Body != null)
                {
                    using var streamWriter = new StreamWriter(webRequest.GetRequestStream());


                    streamWriter.Write(requester.Body);
                }
                webRequest.AllowAutoRedirect = true;
                var webResponse = (HttpWebResponse)webRequest.GetResponse();


                using StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string s = await reader.ReadToEndAsync();
                //Console.WriteLine(s);

                foreach (var ck in webResponse.Cookies)
                {
                    //  Console.WriteLine(ck);
                }

                reader.Close();

                return new HttpResponser()
                {
                    CookieContainer = webResponse.Cookies,
                    Response = s,
                    StatusCode = webResponse.StatusCode.ToString()
                };
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                String sst = "";
                Console.WriteLine("This program is expected to throw WebException on successful run." +
                        "\n\nException Message :" + ex.Message);
                string respp = "";
                if (ex.Response == null)
                {
                    return null;
                }
                var t = ReadFully(ex.Response.GetResponseStream());
                byte[] y;
                try
                {
                    y = Decompress(t);
                }
                catch (Exception e)
                {
                    return new HttpResponser()
                    {
                        StatusCode = webResponse.StatusCode.ToString(),
                        StatusMessage = webResponse.StatusDescription,
                        Response = respp
                    };
                }


                using (var ms = new MemoryStream(y))
                using (var streamReader = new StreamReader(ms))
                    respp = await streamReader.ReadToEndAsync();

                //var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

                //dynamic obj = JsonConvert.DeserializeObject(resp);

                //using (var stream = ex.Response.GetResponseStream())  
                //using (var reader = new StreamReader(stream))
                //{
                //    Console.WriteLine(reader.ReadToEnd());
                //    sst += reader.ReadToEnd();
                //}
                //Console.WriteLine(sst);

                return new HttpResponser()
                {
                    StatusCode = webResponse.StatusCode.ToString(),
                    StatusMessage = webResponse.StatusDescription,
                    Response = respp
                };
            }
            //catch (Exception e)
            //{
            //    var webResponse = e.Response as System.Net.HttpWebResponse;

            //    using (var stream = webResponse.GetResponseStream())
            //    using (var reader = new StreamReader(stream))
            //    {
            //        Console.WriteLine(reader.ReadToEnd());
            //    }
            //    string s = "";
            //    return new HttpResponser() {

            //        StatusCode = webResponse.StatusCode.ToString(),
            //        StatusMessage = webResponse.GetResponseStream().ToString(),
            //        Response = s

            //    };
            //}

        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
        public static string GetAuthorization(HttpRequester requester)
        {
            string autorization = $"{requester.UserName}" + ":" + $"{requester.Password}";
            byte[] binaryAuthorization = System.Text.Encoding.UTF8.GetBytes(autorization);
            autorization = Convert.ToBase64String(binaryAuthorization);
            autorization = "Basic " + autorization;
            return autorization;
        }
    }
}
