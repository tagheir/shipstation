using HttpRequester;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using ShipStationApi.Models.Tracking;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Net.Mail;
using OpenQA.Selenium.DevTools.V101.Network;
using ShipStationApi.Models.Tracking;
namespace ShipStationApi
{
    public class TrackingHandlers
    {
        static void ResponseReceivedHandler(object sender, ResponseReceivedEventArgs e)
        {
            if (e.Response.MimeType.Equals("application/json"))
            {

            }

            Console.WriteLine($"Status: { e.Response.Status } : {e.Response.StatusText} | File: { e.Response.MimeType } | Url: { e.Response.Url }");
        }
        static IWebDriver driver;
        public static async Task<FedexTrackingInfoDto> FedExFetch(string trackingId)
        {


            if(driver == null)
            {
                driver = HttpHandler.GetDriver("https://www.fedex.com");
            }
            
            //var wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

            //driver.Manage().Window.Maximize();
            var devTools =  HttpHandler.SetdevTools(driver);





            var idt = devTools.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V101.DevToolsSessionDomains>();
            await idt.Network.Enable(new EnableCommandSettings());
            string json = null;
            idt.Network.ResponseReceived += async (sender, e) =>
            {

                if (e.Response.MimeType.Equals("application/json") && e.Response.Url.Contains("shipments"))
                {
                    while(json == null)
                    {
                        try
                        {
                            json = await getResponsesAsync(e.RequestId, idt);
                        }
                        catch (Exception ex)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    
                    
                    //System.Threading.Thread.Sleep(5000);
                }

                Console.WriteLine($"Status: { e.Response.Status } : {e.Response.StatusText} | File: { e.Response.MimeType } | Url: { e.Response.Url }");
            };
            driver.Navigate().GoToUrl("https://www.fedex.com/fedextrack/?trknbr="+trackingId);
            int i = 0;
            while (json == null)
            {
                System.Threading.Thread.Sleep(10000);
                i++;
                if(i > 3)
                {
                    driver.Navigate().GoToUrl("https://www.fedex.com/fedextrack/?trknbr=" + trackingId);
                }
            }
            Console.WriteLine(json);
            driver.Close();
            return FedexTrackingInfoDto.FromJson(json);
        }

        static async Task<string> getResponsesAsync(string rid, OpenQA.Selenium.DevTools.V101.DevToolsSessionDomains idt)
        {
            var rcs = new GetResponseBodyCommandSettings();
            rcs.RequestId = rid;


            var respp = await idt.Network.GetResponseBody(rcs);
            Console.WriteLine(respp.Body);
            return respp.Body;
        }
       
    }
}
