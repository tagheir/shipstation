
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

using Generics.Common;

using Generics.HelperModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using ShipStationApi;


using Google.Apis.Gmail.v1;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Util.Store;
using Google.Apis.Gmail.v1.Data;
using System.Collections.Generic;
using Google.Apis.Services;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using HtmlAgilityPack;
using Generics;
using System.Net;
using System.Web;

using System.Globalization;


using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Net.Mail;

using ShipStationApi.Models.Tracking;
using OpenQA.Selenium.DevTools.V101.Network;
using HttpRequester;
using Org.BouncyCastle.Utilities;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Runtime;

namespace JobWorker
{
    public class GoogleBase
    {
        const string PrivateKey =
            "-----BEGIN PRIVATE KEY-----\n\n-----END PRIVATE KEY-----\n";

        public static string StatusPrivateKey = "-----BEGIN PRIVATE KEY-----\n\n-----END PRIVATE KEY-----\n";
        public static string StatusEmail = "status@euphoric-coast-346418.iam.gserviceaccount.com";
        public static string StatusOfficialEmail = "status@shopimo.co";

        public static string SAMSPrivateKey = "-----BEGIN PRIVATE KEY-----\n\n-----END PRIVATE KEY-----\n";
        public static string SAMSEmail = "sasmclub@beaming-ion-347814.iam.gserviceaccount.com";
        public static string SAMSOfficialEmail = "sams8@shopimo.co";

        const string BJSEmail = "admin-274@gmail-access-for-shipping.iam.gserviceaccount.com";
        public static GmailService GetMailService(string email = "bjs@shopimo.co")
        {
            var privateKey = PrivateKey;
            var acc = BJSEmail;
            if(email == StatusOfficialEmail)
            {
                privateKey = StatusPrivateKey;
                acc = StatusEmail;
            }
            else if(email == SAMSEmail)
            {
                privateKey = SAMSPrivateKey;
                acc = SAMSEmail;
            }
            var credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(acc)
                {
                    User = email,
                    Scopes = new[]
                    {
                        "https://mail.google.com/",

                    }
                }.FromPrivateKey(privateKey));
            if (credential.RequestAccessTokenAsync(CancellationToken.None).Result)
            {
                var initializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gmail Access"
                };
                return new GmailService(initializer);
            }
            return null;

        }


        public static GmailService GetMailService(string email,string PriavateKey,string token)
        {
            var privateKey = "-----BEGIN PRIVATE KEY-----\n\n-----END PRIVATE KEY-----\n";


            var credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(token)
                {
                    User = email,
                    Scopes = new[]
                    {
                        "https://mail.google.com/",

                    }
                }.FromPrivateKey(privateKey));
            if (credential.RequestAccessTokenAsync(CancellationToken.None).Result)
            {
                var initializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gmail Access"
                };
                return new GmailService(initializer);
            }
            return null;

        }
    }
    public class EmailDto
    {
        public static string LabelBatchSubject(string txt, string easternTime,string batchName  = "") => $" {batchName} Label Batch - {txt} Printed  on {easternTime}";
        public static string LabelBatchBody(string txt, string failed, string success,string printerName,string batchName = "") => $"{batchName} Label Batch # {txt}  has been printed on station {printerName} :  \n - Success Labels {success} \n - Failed Labels {failed} \n  Shopimo.com";

        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class PrintingService
    {
        
        
        public string Name { get; set; }    
        public string PrinterName { get; set; }
        public string PrinterId { get; set; }
        public  List<string> Emails { get; set; }


        public static PrintingService GetPrintingServiceInfo(int type)
        {

            if(type == 0)
            {
                return new PrintingService()
                {
                    Name = "R2P Filter",
                    PrinterName = "VM-Station",
                    PrinterId = "f490bf28-5e06-4f65-99ac-394af898b470",
                    Emails = new List<string>()
                    {

                    }
                };
            }
            else 
            {
                return new PrintingService()
                {
                    Name = "Amazon R2P",
                    PrinterName = "Marcus Printer",
                    PrinterId = "149eda3e-696e-420b-a45a-29b86b139e2c",
                    Emails = new List<string>()
                    {
                        "marcus@shopimo.co"
                    }
                };
            }

        }

    }
    internal class Program
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static async Task LabelPrinting(int type = 0)
        {
            while (true)
            {


                var driver = GetDriver("https://ss7.shipstation.com/#/orders");
                //var wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

                driver.Manage().Window.Maximize();
                //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("[Id=username]")));

                try
                {
                    System.Threading.Thread.Sleep(5000);

                    var elemnt = driver.FindElement(By.CssSelector("[Id=username]"));
                    elemnt.SendKeys("arijghan");

                    elemnt = driver.FindElement(By.Id("password"));
                    elemnt.SendKeys("Shopimo2022");

                    var inputElement = driver.FindElement(By.Id("btn-login"));
                    inputElement.SendKeys(Keys.Enter);
                    inputElement.Submit();

                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));




                    var pr = PrintingService.GetPrintingServiceInfo(type);
                    driver.FindElement(By.ClassName("filter-toggle")).Click();
                    //var aa = driver.FindElement(By.ClassName("filter-toggle"));
                    var cd = driver.FindElement(By.XPath("//*[@id='orders']//div//div[2]//div[2]//div//div//div[1]//div[10]//ul//li[14]//a"));
                    var eles = driver.FindElements(By.XPath("//*[@id='orders']//div//div[2]//div[2]//div//div//div[1]//div[10]//ul//li"));
                    foreach(var el in eles)
                    {
                        try
                        {
                            var ell = el.FindElement(By.TagName("a"));
                            if(ell != null && ell.Text.Contains(pr.Name))
                            {

                                cd = ell;
                            }
                        }
                        catch(Exception e)
                        {

                        }
                        

                        
                    }
                    var idd = cd.GetAttribute("data-filterid");
                    cd.Click();
                    System.Threading.Thread.Sleep(35000);
                    try
                    {
                        var lttt = driver.FindElements(By.XPath("//*[@id='orderlist-body']//tr"));
                        if (lttt.Count() < 2)
                        {
                            driver.Close();
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        driver.Close();
                    }
                    
                    System.Threading.Thread.Sleep(5000);
                    var icon = driver.FindElements(By.ClassName("selector"));
                    System.Threading.Thread.Sleep(5000);
                    icon[0].Click();

                    elemnt = driver.FindElement(By.ClassName("btn-ship"));
                    elemnt.Click();
                    System.Threading.Thread.Sleep(15000);
                    try
                    {
                        elemnt = driver.FindElement(By.ClassName("btn-continue"));
                        elemnt.Click();
                    }
                    catch (Exception e)
                    {

                    }
                    System.Threading.Thread.Sleep(15000);


                    bool click = false;
                    int attempt = 0;
                    int no = 5;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[3]/button[1]"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    }

                    //elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[3]/button[1]"));
                    //elemnt.Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));

                    elemnt = driver.FindElement(By.ClassName("track-PrintLabels"));
                    elemnt.Click();



                    System.Threading.Thread.Sleep(15000);
                    no = 7;
                    click = false;
                    attempt = 0;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    }

                    System.Threading.Thread.Sleep(10000);
                    var elements = driver.FindElements(By.ClassName("track-PrintShipStationConnectPrinter"));  // 
                    bool isPrinterFound = false;
                    foreach (var el in elements)
                    {
                        
                        if (el.GetAttribute("data-workstation") != null && el.GetAttribute("data-workstation").Equals(pr.PrinterId ))
                        {
                            el.Click();
                            isPrinterFound = true;
                            break;
                        }


                    }
                    if(!isPrinterFound)
                    {
                        elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[2]/div/div/div[1]/em")); // batch no
                        var bno = elemnt.Text;
                        var FailedSubject = $"URGENT: Batch # {bno} Failed to print! ";
                        var FailedBody = $"Batch # {bno} Has been created but failed to print. \n - Print Manually \n Shopimo.co";
                        GmailHelper.Mailer.SendEmail(FailedSubject, FailedBody, "lior@shopimo.co", 
                            new List<string>() { "tiara@shopimo.co", "tia@shopimo.co", "marcus@shopimo.co"}, 
                            GmailHelper.GoogleBase.StatusOfficialEmail);
                        return;
                    }
                    var successLabels = driver.FindElement(By.ClassName("success-count"));
                    var totalFailed = 0;
                    try
                    {
                        var failedLabels = driver.FindElement(By.ClassName("batch-status-link"));
                        totalFailed = int.Parse(failedLabels.Text.Replace("Label(s)",""));
                        
                    }
                    catch (Exception e)
                    {

                    }
                    

                    System.Threading.Thread.Sleep(15000);
                    no = 5;
                    click = false;
                    attempt = 0;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[2]/div/div/div[1]/em"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    } // batch no
                    var txt = elemnt.Text;
                    var timeUtc = DateTime.UtcNow;
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
                    var subject = EmailDto.LabelBatchSubject(txt,easternTime.ToString(),pr.Name);
                    var body = EmailDto.LabelBatchBody(txt, totalFailed.ToString(), successLabels.Text, pr.PrinterName, pr.Name);
                    var dic = new Dictionary<string, string>();
                    dic.Add("subject", subject);
                    dic.Add("from", GoogleBase.StatusOfficialEmail);
                    dic.Add("to", "lior@shopimo.co");
                    dic.Add("body", body);

                    SendItTwo(GoogleBase.GetMailService(GoogleBase.StatusOfficialEmail), dic, pr.Emails.FirstOrDefault());


                    
                    try
                    {
                        driver.FindElement(By.ClassName("batch-printed")).Click();
                        driver.Close();
                        return;
                    }
                    catch(Exception ex)
                    {

                    }
                    no = 5;
                    click = false;
                    attempt = 0;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[1]/a"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    }

                    System.Threading.Thread.Sleep(10000);
                    driver.Navigate().GoToUrl("https://ss7.shipstation.com/#/track/batches");
                    System.Threading.Thread.Sleep(10000);

                    try
                    {
                        elemnt = driver.FindElement(By.XPath("//*[@id='track']//div//div[2]//div[4]//div[3]//table//tbody//tr[1]//td[1]"));
                        elemnt.Click();

                        //elemnt = driver.FindElement(By.ClassName("selector"));
                        //elemnt.Click();
                    }
                    catch (Exception e)
                    {

                    }



                    elemnt = driver.FindElement(By.XPath("//*[@id='track']/div/div[2]/div[4]/div[1]/div/div/a[4]"));
                    elemnt.Click();
                    driver.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(10));
                    driver.Close();
                }


            }

            
            //"ship-window"
            //    "btn btn-print-labels btn-success track-PrintLabels"
            //    "/html/body/div[8]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"
            //    "ssc-printer"
            //    "p-zebra_technologies_ztc_zt230_200dpi_zpl"


        }
        static async Task AmazonLabelPrinting()
        {
            while (true)
            {


                var driver = GetDriver("https://ss7.shipstation.com/#/orders");
                //var wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

                driver.Manage().Window.Maximize();
                //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("[Id=username]")));

                try
                {
                    System.Threading.Thread.Sleep(5000);

                    var elemnt = driver.FindElement(By.CssSelector("[Id=username]"));
                    elemnt.SendKeys("arijghan");

                    elemnt = driver.FindElement(By.Id("password"));
                    elemnt.SendKeys("Shopimo2022");

                    var inputElement = driver.FindElement(By.Id("btn-login"));
                    inputElement.SendKeys(Keys.Enter);
                    inputElement.Submit();

                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));





                    driver.FindElement(By.ClassName("filter-toggle")).Click();
                    var aa = driver.FindElement(By.ClassName("filter-toggle"));

                    var eleeess = driver.FindElements(By.XPath("//*[@id='orders']//div//div[2]//div[2]//div//div//div[1]//div[10]//ul//li"));

                    foreach (var filters in eleeess)
                    {
                        try
                        {
                            Console.WriteLine(filters.FindElement(By.TagName("a")).Text);
                            if (filters.FindElement(By.TagName("a")).Text.Contains("Amazon R2P"))
                            {
                                filters.FindElement(By.TagName("a")).Click();
                                break;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //var cd = driver.FindElement(By.XPath("//*[@id='orders']//div//div[2]//div[2]//div//div//div[1]//div[10]//ul//li[5]//a"));
                    //var idd = cd.GetAttribute("data-filterid");
                    //cd.Click();
                    System.Threading.Thread.Sleep(5000);
                    var icon = driver.FindElements(By.ClassName("selector"));
                    System.Threading.Thread.Sleep(5000);
                    icon[0].Click();

                    elemnt = driver.FindElement(By.ClassName("btn-ship"));
                    elemnt.Click();
                    System.Threading.Thread.Sleep(15000);
                    try
                    {
                        elemnt = driver.FindElement(By.ClassName("btn-continue"));
                        elemnt.Click();
                    }
                    catch (Exception e)
                    {

                    }
                    System.Threading.Thread.Sleep(15000);
                    bool click = false;
                    int attempt = 0;
                    int no = 5;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[3]/button[1]"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch(Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    }
                    
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));

                    elemnt = driver.FindElement(By.ClassName("track-PrintLabels"));
                    elemnt.Click();



                    System.Threading.Thread.Sleep(15000);
                    no = 7;
                    click = false;
                    attempt = 0;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    }

                    try
                    {
                        
                    }
                    catch (Exception e)
                    {
                        elemnt = driver.FindElement(By.XPath("/html/body/div[8]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"));
                        elemnt.Click();
                    }

                    System.Threading.Thread.Sleep(10000);
                    var elements = driver.FindElements(By.ClassName("track-PrintShipStationConnectPrinter"));
                    foreach (var el in elements)
                    {
                        if (el.GetAttribute("data-workstation") != null && el.GetAttribute("data-workstation").Equals("149eda3e-696e-420b-a45a-29b86b139e2c"))
                        {
                            el.Click();
                            break;
                        }


                    }
                    System.Threading.Thread.Sleep(15000);

                    no = 5;
                    click = false;
                    attempt = 0;
                    while (!click && attempt < 3)
                    {
                        try
                        {
                            elemnt = driver.FindElement(By.XPath($"/html/body/div[{no}]/div/div/div[2]/div/div/div[1]/em"));
                            elemnt.Click();
                            click = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            no++;
                            attempt++;
                        }
                    }
                    //elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[2]/div/div/div[1]/em"));
                    var txt = elemnt.Text;
                    var timeUtc = DateTime.UtcNow;
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
                    var subject = $"Amazon Label Batch - {txt} Printed  on {easternTime}";
                    var body = $"- Amazon Label Batch # {txt}  has been printed  : \n Shopimo.com";
                    var dic = new Dictionary<string, string>();
                    dic.Add("subject", subject);
                    dic.Add("from", GoogleBase.StatusOfficialEmail);
                    dic.Add("to", "lior@shopimo.co");
                    dic.Add("body", body);

                    SendItTwo(GoogleBase.GetMailService(GoogleBase.StatusOfficialEmail), dic, "marcus@shopimo.co");

                    elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[1]/a"));
                    elemnt.Click();

                    System.Threading.Thread.Sleep(10000);
                    driver.Navigate().GoToUrl("https://ss7.shipstation.com/#/track/batches");
                    System.Threading.Thread.Sleep(10000);

                    try
                    {
                        elemnt = driver.FindElement(By.XPath("//*[@id='track']/div/div[2]/div[4]/div[3]/table/thead/tr/th[1]"));
                        elemnt.Click();
                    }
                    catch (Exception e)
                    {

                    }



                    elemnt = driver.FindElement(By.XPath("//*[@id='track']/div/div[2]/div[4]/div[1]/div/div/a[4]"));
                    elemnt.Click();
                    driver.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(10));
                    driver.Close();
                }


            }


            //"ship-window"
            //    "btn btn-print-labels btn-success track-PrintLabels"
            //    "/html/body/div[8]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"
            //    "ssc-printer"
            //    "p-zebra_technologies_ztc_zt230_200dpi_zpl"


        }
        static async Task PickList()
        {
            while (true)
            {


                var driver = GetDriver("https://ss7.shipstation.com/#/orders");
                //var wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

                driver.Manage().Window.Maximize();
                //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("[Id=username]")));

                try
                {
                    System.Threading.Thread.Sleep(40000);

                    var elemnt = driver.FindElement(By.CssSelector("[Id=username]"));
                    elemnt.SendKeys("arijghan");

                    elemnt = driver.FindElement(By.Id("password"));
                    elemnt.SendKeys("Shopimo2022");

                    var inputElement = driver.FindElement(By.Id("btn-login"));
                    inputElement.SendKeys(Keys.Enter);
                    inputElement.Submit();

                    System.Threading.Thread.Sleep(40000);



                    var printelement = driver.FindElement(By.XPath("//*[@id='topnav']//div//div//div[4]//div[2]//button"));
                    printelement.Click();

                    var batchList = driver.FindElements(By.XPath("//div[@class='batch-ct']//div"));
                    
                    var bt = batchList.Where(e => e.GetAttribute("class").Contains("done")).ToList();
                    var data = LayerBao.SiteMetaBAO.GetKey("lastbatch");
                    List<string> batches = new List<string>();
                    foreach(var batch in bt)
                    {
                        try
                        {
                            var batchno = batch.FindElement(By.ClassName("batch-number")).Text;
                            batchno = batchno.Trim().Replace("Batch","").RemoveCharacterFromString('#');
                            if(batchno.Trim().Equals(data.VALUE.Trim()))
                            {
                                break;
                            }
                            Console.WriteLine(batch.GetAttribute("class"));
                            batch.FindElement(By.ClassName("row-check")).Click();
                            batches.Add(batchno);
                            Console.WriteLine(batch.FindElement(By.ClassName("batch-number")).Text);
                        }
                        catch(Exception e)
                        {

                        }
                        
                    }
                    


                    driver.FindElement(By.XPath("/html//body//div[1]//div[2]//div[1]//div//div[2]//nav//ul[1]//li[2]//a")).Click();

                   // driver.FindElement(By.ClassName("filter-toggle")).Click();

                    var printli = driver.FindElement(By.ClassName("print-menu"));

                    var li = printli.FindElements(By.TagName("li"));

                    foreach(var l in li)
                    {
                        try
                        {
                            Console.WriteLine(l.FindElement(By.ClassName("btn-print")).Text);

                            if(l.FindElement(By.ClassName("btn-print")).Text.Contains("Pick"))
                            {
                                l.FindElement(By.ClassName("btn-print")).Click();

                                var dw = driver.FindElement(By.ClassName("print-options"));
                                var dwlist = dw.FindElements(By.TagName("li"));
                                foreach(var dwo in dwlist)
                                {
                                    var aaa = dwo.FindElement(By.TagName("a"));
                                    var pro = aaa.GetAttribute("data-printer");
                                    if(pro != null && pro.Contains("download"))
                                    {
                                        aaa.Click();

                                        System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));

                                        string subPath = AppDomain.CurrentDomain.BaseDirectory+"picklist"; // Your code goes here

                                        var filename = subPath.ReadFileFromFolder();
                                        if(filename != null && filename.Count() > 0)
                                        {
                                            GmailHelper.Mailer.SendEmail("Pick List Ready "+DateTime.UtcNow.GetEasternDateTimeByRegion(),
                                                "<p> Pick list for batches #"+ String.Join(",",batches.Select(b => "<strong>"+b.Replace("Batch","")+"</strong>").ToList())+"</p> <h3> Shopimo.com </h3>", 
                                                "Lior@shopimo.co", 
                                                new List<string>()
                                                {
                                                   
                                                        "Tiara@shopimo.co",
                                                        "Tia@shopimo.co",
                                                        "Marcus@shopimo.co",
                                                        "Dontai@shopimo.co"
                                                }, 
                                                "status@shopimo.co",
                                                filename.FirstOrDefault());
                                            LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
                                            {
                                                VALUE = batches.FirstOrDefault(),
                                                LastUpdated = DateTime.Now,
                                                KEY = "lastbatch"
                                            });
                                            var dt = DateTime.UtcNow;
                                            TimeSpan ts = new TimeSpan(5, 0, 0);

                                            dt = dt.Date + ts;
                                            dt = dt.AddDays(1);
                                            LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
                                            {
                                                KEY = "picktime",
                                                LastUpdated = DateTime.UtcNow,
                                                VALUE = dt.ToString(),
                                            });
                                            driver.Close();
                                            return;
                                        }
                                        //GmailHelper.Mailer.SendEmail(GmailService.Equals)
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                        
                    }

                    //var li = printli.FirstOrDefault(e => e.FindElement(By.ClassName("btn-print")).Text.Contains("Pick"));

                    var aa = driver.FindElement(By.ClassName("filter-toggle"));
                    var cd = driver.FindElement(By.XPath("//*[@id='orders']//div//div[2]//div[2]//div//div//div[1]//div[10]//ul//li[14]//a"));
                    var idd = cd.GetAttribute("data-filterid");
                    cd.Click();
                    System.Threading.Thread.Sleep(5000);
                    var icon = driver.FindElements(By.ClassName("selector"));
                    System.Threading.Thread.Sleep(5000);
                    icon[0].Click();

                    elemnt = driver.FindElement(By.ClassName("btn-ship"));
                    elemnt.Click();
                    System.Threading.Thread.Sleep(15000);
                    try
                    {
                        elemnt = driver.FindElement(By.ClassName("btn-continue"));
                        elemnt.Click();
                    }
                    catch (Exception e)
                    {

                    }
                    System.Threading.Thread.Sleep(15000);
                    elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[3]/button[1]"));
                    elemnt.Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));

                    elemnt = driver.FindElement(By.ClassName("track-PrintLabels"));
                    elemnt.Click();



                    System.Threading.Thread.Sleep(15000);

                    try
                    {
                        elemnt = driver.FindElement(By.XPath("/html/body/div[7]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"));
                        elemnt.Click();
                    }
                    catch (Exception e)
                    {
                        elemnt = driver.FindElement(By.XPath("/html/body/div[8]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"));
                        elemnt.Click();
                    }

                    System.Threading.Thread.Sleep(10000);
                    var elements = driver.FindElements(By.ClassName("track-PrintShipStationConnectPrinter"));
                    foreach (var el in elements)
                    {
                        if (el.GetAttribute("data-workstation") != null && el.GetAttribute("data-workstation").Equals("f490bf28-5e06-4f65-99ac-394af898b470"))
                        {
                            el.Click();
                            break;
                        }


                    }
                    System.Threading.Thread.Sleep(15000);
                    elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[2]/div/div/div[1]/em"));
                    var txt = elemnt.Text;
                    var timeUtc = DateTime.UtcNow;
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
                    var subject = $"Label Batch - {txt} Printed  on {easternTime}";
                    var body = $"- Label Batch # {txt}  has been printed on station VMStation : \n Shopimo.com";
                    var dic = new Dictionary<string, string>();
                    dic.Add("subject", subject);
                    dic.Add("from", GoogleBase.StatusOfficialEmail);
                    dic.Add("to", "lior@shopimo.co");
                    dic.Add("body", body);

                    SendItTwo(GoogleBase.GetMailService(GoogleBase.StatusOfficialEmail), dic, "Tia@shopimo.co");

                    elemnt = driver.FindElement(By.XPath("/html/body/div[5]/div/div/div[1]/a"));
                    elemnt.Click();

                    System.Threading.Thread.Sleep(10000);
                    driver.Navigate().GoToUrl("https://ss7.shipstation.com/#/track/batches");
                    System.Threading.Thread.Sleep(10000);

                    try
                    {
                        elemnt = driver.FindElement(By.XPath("//*[@id='track']/div/div[2]/div[4]/div[3]/table/thead/tr/th[1]"));
                        elemnt.Click();
                    }
                    catch (Exception e)
                    {

                    }



                    elemnt = driver.FindElement(By.XPath("//*[@id='track']/div/div[2]/div[4]/div[1]/div/div/a[4]"));
                    elemnt.Click();
                    driver.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(10));
                    driver.Close();
                }


            }


            //"ship-window"
            //    "btn btn-print-labels btn-success track-PrintLabels"
            //    "/html/body/div[8]/div/div/div[2]/div[1]/div/div[1]/ul/li[1]/a"
            //    "ssc-printer"
            //    "p-zebra_technologies_ztc_zt230_200dpi_zpl"


        }
        public static void SendItTwo(GmailService gmail, Dictionary<string, string> dict,string emailAddresses = null)
        {
            MailMessage mailmsg = new MailMessage();
            {
                mailmsg.Subject = dict["subject"];
                mailmsg.Body = dict["body"];
                mailmsg.From = new MailAddress(dict["from"]);
                mailmsg.To.Add(new MailAddress(dict["to"]));
               
                mailmsg.IsBodyHtml = false;
            }
            if(emailAddresses != null)
            {
                mailmsg.To.Add(emailAddresses);
            }

            ////add attachment if specified
            if (dict.ContainsKey("attachement"))
            {
                if (File.Exists(dict["attachment"]))
                {
                    Attachment data = new Attachment(dict["attachment"]);
                    mailmsg.Attachments.Add(data);

                }
                else
                {
                    Console.WriteLine("Error: Invalid Attachemnt");
                }
            }
            //Make mail message a Mime message
            MimeKit.MimeMessage mimemessage = MimeKit.MimeMessage.CreateFromMailMessage(mailmsg);
            Google.Apis.Gmail.v1.Data.Message finalmessage = new Google.Apis.Gmail.v1.Data.Message();
            finalmessage.Raw = Base64UrlEncode(mimemessage.ToString());
            var result = gmail.Users.Messages.Send(finalmessage, "me").Execute();
        }
        public static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
        static async Task BJSGmailTrackingService()
        {


            // Create Gmail API service.
            var service = GoogleBase.GetMailService();

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            service.Users.Messages.Send(new Message() {

                
            }, "me");
            // List labels.
            var messages = service.Users.Messages.List("me");

            messages.Q = "your order has been shipped";
            messages.MaxResults = 500;
            int i = 0;
            while (i < 3)
            {
                try
                {
                    Console.WriteLine(i);
                    var mm = messages.Execute();
                    var orders = await ShipStationHandler.GetOrdererdOrders(0);
                    foreach (var m in mm.Messages)
                    {


                        var ssd = service.Users.Messages.Get("me", m.Id).Execute();

                        var gmail_date = ssd.InternalDate.Value;

                        //Get DateTime of epoch ms
                        var to_date = DateTimeOffset.FromUnixTimeMilliseconds(gmail_date).DateTime;


                        var part = ssd.Payload.Parts.FirstOrDefault(f =>
                            f.MimeType == "text/html"
                        );
                        if (part == null || part.Body.Data == null)
                        {
                            continue;
                        }
                        var decodedStr = Base64UrlEncoder.Decode(part.Body.Data);
                        var doc = new HtmlDocument();
                        doc.LoadHtml(decodedStr);
                        var links = doc.DocumentNode.SelectNodes("//a");
                        var packageLink = links[0];
                        var orderId = doc.DocumentNode.SelectNodes("//table//table//table[4]");
                        var thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[26]).InnerText.Trim().Replace("#", "");

                        if (packageLink.InnerText.Trim().ToLower().Contains("track"))
                        {
                            var trackLink = packageLink.GetAttributeValue("href", "").ToString();
                            var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
                            if (ordrrr == null)
                            {
                                thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[25]).InnerText.Trim().Replace("#", "");
                                ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
                                if (ordrrr == null)
                                {
                                    continue;
                                }

                            }
                            if(string.IsNullOrWhiteSpace(trackLink))
                            {
                                Console.WriteLine("OrderNumber EmptyLink: " + ordrrr.OrderNumber);
                                continue;
                            }
                            var redirectUrl = HttpRequester.HttpHandler.GetFinalRedirect(trackLink);

                            var resp = await HttpRequester.HttpHandler.Request(new HttpRequester.HttpRequester()
                            {
                                Url = trackLink,
                                Method = HttpRequester.HttpMethod.GET,
                                IsAuthorized = false

                            });
                            doc.LoadHtml(resp.Response);
                            var Id = "";
                            var date = DateTime.Now;
                            var carrierCode = "UPSN";
                            var link = "";
                            if (redirectUrl.Contains("usps"))
                            {
                                var dd = doc.DocumentNode.SelectSingleNode("//span[@class='tracking-number']");
                                var status = doc.DocumentNode.SelectSingleNode("//div[@class='delivery_status']//strong");

                                Uri myUri = new Uri(redirectUrl);
                                Id = HttpUtility.ParseQueryString(myUri.Query).Get("qtc_tLabels1");
                                date = to_date;
                                carrierCode = "usps";
                            }
                            else
                            {

                                var trackingInformation = doc.DocumentNode.SelectNodes("//div[@id='carrier']//a")[0];
                                Id = trackingInformation.InnerText.Trim();
                                link = trackingInformation.GetAttributeValue("href", "");

                                var trackerInfo = await ShipStationApi.ShipStationHandler.GetUpsTrackingInfo(Id);
                                if (trackerInfo.TrackDetails[0].AdditionalInformation == null || String.IsNullOrWhiteSpace(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate))
                                {
                                    continue;
                                }
                                try
                                {
                                    date = DateTime.Parse(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate);

                                }
                                catch (Exception e)
                                {
                                    date = DateTime.ParseExact(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate, "MM/dd/yyyy", CultureInfo.CurrentCulture);

                                }

                            }

                            try
                            {
                                Console.WriteLine(ordrrr.OrderNumber);
                                await ShipStationHandler.UpdateOrderTracking(new OrderShipperDto()
                                {
                                    ShipstationOrderId = ordrrr.OrderId.ToString(),
                                    TrackingId = Id,
                                    TrackingUrl = link,
                                    CarrierCode = carrierCode,
                                    PlatformOrderId = thirdTable,
                                    TrackingDate = date,

                                });
                                continue;
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }





                    }


                    messages.PageToken = mm.NextPageToken;
                    i++;
                }
                catch(Exception e)
                {

                }

            }

        }
        static async Task OLDSAMSGmailTrackingService()
        {


            // Create Gmail API service.
            var service = GoogleBase.GetMailService(GoogleBase.SAMSOfficialEmail);

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            service.Users.Messages.Send(new Message()
            {


            }, "me");
            // List labels.
            var messages = service.Users.Messages.List("me");

            messages.Q = "Your SamsClub.com order has shipped";
            messages.MaxResults = 500;
            int i = 0;
            while (i < 5)
            {
                Console.WriteLine(i);
                var mm = messages.Execute();
                var orders = await ShipStationHandler.GetOrdererdOrders(0);
                foreach (var m in mm.Messages)
                {

                    System.Threading.Thread.Sleep(10000);
                    var doc = new HtmlDocument();
                    var to_date = DateTime.Now;
                    try
                    {

                        var ssd = service.Users.Messages.Get("me", m.Id).Execute();

                        var gmail_date = ssd.InternalDate.Value;

                        //Get DateTime of epoch ms
                        to_date = DateTimeOffset.FromUnixTimeMilliseconds(gmail_date).DateTime;



                        var decodedStr = Base64UrlEncoder.Decode(ssd.Payload.Body.Data);
                        doc.LoadHtml(decodedStr);
                    }
                    catch (Exception e)
                    {


                    }





                    var links = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//table[1]//tr//td//table[3]//tr//td//table[1]//tr[1]//td");
                    ;
                    var samsOrderId = links.InnerText.Trim().Replace("Order", "").Trim();
                    var tid = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//td/table[1]//tr/td//table[3]//tr//td//table[5]//tr//td//table//tr//td//table//tr//td[2]//p//a");
                    var tr = "";
                    if (tid == null)
                    {
                        var l = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//td/table[1]//tr//td//table[3]//tr//td//table[3]//tr//td//a");
                        if (l != null)
                        {
                            var samsLink = l.Attributes.FirstOrDefault(a => a.Name == "href").Value;
                            var redirectUrl = HttpRequester.HttpHandler.GetFinalRedirect(samsLink);
                            Uri myUri = new Uri(redirectUrl);
                            var abs = myUri.AbsolutePath.Split('/')[2];
                            tr = abs;
                        }
                    }
                    else
                    {
                        tr = tid.InnerText.Trim();
                    }
                    var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(samsOrderId));
                    if (ordrrr != null)
                    {
                        var tinfo = await OrderPlacer.SamsClub.SamsClubWorker.GetSamsTrackingInfo(tr);
                        if (tinfo == null)
                        {
                            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                            tinfo = await OrderPlacer.SamsClub.SamsClubWorker.GetSamsTrackingInfo(tr);
                        }
                        if(tinfo == null)
                        {
                            Console.WriteLine("Some Issue");
                            continue;
                        }
                        Console.WriteLine(ordrrr.OrderNumber);
                        try
                        {
                            await ShipStationHandler.UpdateOrderTracking(new OrderShipperDto()
                            {
                                ShipstationOrderId = ordrrr.OrderId.ToString(),
                                TrackingId = tr,
                                TrackingUrl = tinfo.Payload.CarrierTrackingUrl.OriginalString,
                                CarrierCode = "",
                                PlatformOrderId = samsOrderId,
                                TrackingDate = to_date,

                            });
                        }
                        catch (Exception ex)
                        {

                        }

                        System.Threading.Thread.Sleep(10000);
                    }
                    //var links = doc.DocumentNode.SelectNodes("//a");
                    //var packageLink = links[0];
                    //var orderId = doc.DocumentNode.SelectNodes("//table//table//table[4]");
                    //var thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[26]).InnerText.Trim().Replace("#", "");

                    //if (packageLink.InnerText.Trim().ToLower().Contains("track"))
                    //{
                    //    var trackLink = packageLink.GetAttributeValue("href", "").ToString();
                    //    var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
                    //    if (ordrrr == null)
                    //    {
                    //        thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[25]).InnerText.Trim().Replace("#", "");
                    //        ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
                    //        if (ordrrr == null)
                    //        {
                    //            continue;
                    //        }

                    //    }

                    //    var redirectUrl = HttpRequester.HttpHandler.GetFinalRedirect(trackLink);

                    //    var resp = await HttpRequester.HttpHandler.Request(new HttpRequester.HttpRequester()
                    //    {
                    //        Url = trackLink,
                    //        Method = HttpRequester.HttpMethod.GET,
                    //        IsAuthorized = false

                    //    });
                    //    doc.LoadHtml(resp.Response);
                    //    var Id = "";
                    //    var date = DateTime.Now;
                    //    var carrierCode = "UPSN";
                    //    var link = "";
                    //    if (redirectUrl.Contains("usps"))
                    //    {
                    //        var dd = doc.DocumentNode.SelectSingleNode("//span[@class='tracking-number']");
                    //        var status = doc.DocumentNode.SelectSingleNode("//div[@class='delivery_status']//strong");

                    //        Uri myUri = new Uri(redirectUrl);
                    //        Id = HttpUtility.ParseQueryString(myUri.Query).Get("qtc_tLabels1");
                    //        date = to_date;
                    //        carrierCode = "usps";
                    //    }
                    //    else
                    //    {

                    //        var trackingInformation = doc.DocumentNode.SelectNodes("//div[@id='carrier']//a")[0];
                    //        Id = trackingInformation.InnerText.Trim();
                    //        link = trackingInformation.GetAttributeValue("href", "");

                    //        var trackerInfo = await ShipStationApi.ShipStationHandler.GetUpsTrackingInfo(Id);
                    //        if (trackerInfo.TrackDetails[0].AdditionalInformation == null)
                    //        {
                    //            continue;
                    //        }
                    //        try
                    //        {
                    //            date = DateTime.Parse(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate);

                    //        }
                    //        catch (Exception e)
                    //        {
                    //            date = DateTime.ParseExact(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate, "MM/dd/yyyy", CultureInfo.CurrentCulture);
                    //        }







                    //    }

                    //    try
                    //    {
                    //        Console.WriteLine(ordrrr.OrderNumber);
                    //        await ShipStationHandler.UpdateOrderTracking(new OrderShipperDto()
                    //        {
                    //            ShipstationOrderId = ordrrr.OrderId.ToString(),
                    //            TrackingId = Id,
                    //            TrackingUrl = link,
                    //            CarrierCode = carrierCode,
                    //            PlatformOrderId = thirdTable,
                    //            TrackingDate = date,

                    //        });
                    //        continue;
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        continue;
                    //    }
                    //}





                }


                messages.PageToken = mm.NextPageToken;
                i++;
            }

        }
        static async Task SAMSGmailTrackingService()
        {

            var accounts = LayerDao.SamsAccountDao.GetAllAccountInfo();
            foreach(var account in accounts)
            {
                if(string.IsNullOrWhiteSpace(account.Token) || string.IsNullOrWhiteSpace(account.PrivateKey))
                {
                    continue;
                }
                var service = GoogleBase.GetMailService(account.Email,account.PrivateKey,account.EmailID);

                // Define parameters of request.
                UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

                service.Users.Messages.Send(new Message()
                {


                }, "me");
                // List labels.
                var messages = service.Users.Messages.List("me");

                messages.Q = "Your SamsClub.com order has shipped";
                messages.MaxResults = 500;
                int i = 0;
                while (i < 5)
                {
                    Console.WriteLine(i);
                    var mm = messages.Execute();
                    var orders = await ShipStationHandler.GetOrdererdOrders(0);
                    foreach (var m in mm.Messages)
                    {

                        System.Threading.Thread.Sleep(10000);
                        var doc = new HtmlDocument();
                        var to_date = DateTime.Now;
                        try
                        {

                            var ssd = service.Users.Messages.Get("me", m.Id).Execute();

                            var gmail_date = ssd.InternalDate.Value;

                            //Get DateTime of epoch ms
                            to_date = DateTimeOffset.FromUnixTimeMilliseconds(gmail_date).DateTime;



                            var decodedStr = Base64UrlEncoder.Decode(ssd.Payload.Body.Data);
                            doc.LoadHtml(decodedStr);
                        }
                        catch (Exception e)
                        {
                            continue;

                        }





                        var links = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//table[1]//tr//td//table[3]//tr//td//table[1]//tr[1]//td");
                        ;
                        var samsOrderId = links.InnerText.Trim().Replace("Order", "").Trim();
                        var tid = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//td/table[1]//tr/td//table[3]//tr//td//table[5]//tr//td//table//tr//td//table//tr//td[2]//p//a");
                        var tr = "";
                        if (tid == null)
                        {
                            var l = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//td/table[1]//tr//td//table[3]//tr//td//table[3]//tr//td//a");
                            if (l != null)
                            {
                                var samsLink = l.Attributes.FirstOrDefault(a => a.Name == "href").Value;
                                var redirectUrl = HttpRequester.HttpHandler.GetFinalRedirect(samsLink);
                                Uri myUri = new Uri(redirectUrl);
                                var abs = myUri.AbsolutePath.Split('/')[2];
                                tr = abs;
                            }
                        }
                        else
                        {
                            tr = tid.InnerText.Trim();
                        }
                        var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(samsOrderId));
                        if (ordrrr != null)
                        {
                            var tinfo = await OrderPlacer.SamsClub.SamsClubWorker.GetSamsNewTracker(tr);
                            if (tinfo == null)
                            {
                                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                                tinfo = await OrderPlacer.SamsClub.SamsClubWorker.GetSamsTrackingInfo(tr);
                            }
                            Console.WriteLine(ordrrr.OrderNumber);
                            try
                            {
                                await ShipStationHandler.UpdateOrderTracking(new OrderShipperDto()
                                {
                                    ShipstationOrderId = ordrrr.OrderId.ToString(),
                                    TrackingId = tr,
                                    TrackingUrl = tinfo.Payload.CarrierTrackingUrl.OriginalString,
                                    CarrierCode = "",
                                    PlatformOrderId = samsOrderId,
                                    TrackingDate = to_date,

                                });
                            }
                            catch (Exception ex)
                            {

                            }

                            System.Threading.Thread.Sleep(10000);
                        }
                        //var links = doc.DocumentNode.SelectNodes("//a");
                        //var packageLink = links[0];
                        //var orderId = doc.DocumentNode.SelectNodes("//table//table//table[4]");
                        //var thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[26]).InnerText.Trim().Replace("#", "");

                        //if (packageLink.InnerText.Trim().ToLower().Contains("track"))
                        //{
                        //    var trackLink = packageLink.GetAttributeValue("href", "").ToString();
                        //    var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
                        //    if (ordrrr == null)
                        //    {
                        //        thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[25]).InnerText.Trim().Replace("#", "");
                        //        ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
                        //        if (ordrrr == null)
                        //        {
                        //            continue;
                        //        }

                        //    }

                        //    var redirectUrl = HttpRequester.HttpHandler.GetFinalRedirect(trackLink);

                        //    var resp = await HttpRequester.HttpHandler.Request(new HttpRequester.HttpRequester()
                        //    {
                        //        Url = trackLink,
                        //        Method = HttpRequester.HttpMethod.GET,
                        //        IsAuthorized = false

                        //    });
                        //    doc.LoadHtml(resp.Response);
                        //    var Id = "";
                        //    var date = DateTime.Now;
                        //    var carrierCode = "UPSN";
                        //    var link = "";
                        //    if (redirectUrl.Contains("usps"))
                        //    {
                        //        var dd = doc.DocumentNode.SelectSingleNode("//span[@class='tracking-number']");
                        //        var status = doc.DocumentNode.SelectSingleNode("//div[@class='delivery_status']//strong");

                        //        Uri myUri = new Uri(redirectUrl);
                        //        Id = HttpUtility.ParseQueryString(myUri.Query).Get("qtc_tLabels1");
                        //        date = to_date;
                        //        carrierCode = "usps";
                        //    }
                        //    else
                        //    {

                        //        var trackingInformation = doc.DocumentNode.SelectNodes("//div[@id='carrier']//a")[0];
                        //        Id = trackingInformation.InnerText.Trim();
                        //        link = trackingInformation.GetAttributeValue("href", "");

                        //        var trackerInfo = await ShipStationApi.ShipStationHandler.GetUpsTrackingInfo(Id);
                        //        if (trackerInfo.TrackDetails[0].AdditionalInformation == null)
                        //        {
                        //            continue;
                        //        }
                        //        try
                        //        {
                        //            date = DateTime.Parse(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate);

                        //        }
                        //        catch (Exception e)
                        //        {
                        //            date = DateTime.ParseExact(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate, "MM/dd/yyyy", CultureInfo.CurrentCulture);
                        //        }







                        //    }

                        //    try
                        //    {
                        //        Console.WriteLine(ordrrr.OrderNumber);
                        //        await ShipStationHandler.UpdateOrderTracking(new OrderShipperDto()
                        //        {
                        //            ShipstationOrderId = ordrrr.OrderId.ToString(),
                        //            TrackingId = Id,
                        //            TrackingUrl = link,
                        //            CarrierCode = carrierCode,
                        //            PlatformOrderId = thirdTable,
                        //            TrackingDate = date,

                        //        });
                        //        continue;
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        continue;
                        //    }
                        //}





                    }


                    messages.PageToken = mm.NextPageToken;
                    i++;
                }
            }
            // Create Gmail API service.


        }
        static async Task  PickListWorker()
        {
            while (true)
            {


                var nextPickTime = LayerBao.SiteMetaBAO.GetKey("picktime");
                var tt = DateTime.Parse(nextPickTime.VALUE);

                var currentEasternTime = DateTime.UtcNow.GetEasternDateTimeByRegion();
                Console.WriteLine("Current Eastern Time is: " + currentEasternTime.ToString());
                Console.WriteLine("NEXT PICKUP Time is: " + nextPickTime.ToString());
                if (currentEasternTime.CompareTo(tt) > 0)
                {
                    Console.WriteLine("PICK LIST TIME");
                    await PickList();
                }
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(5));
            }
        }
        static async Task SamsGmailTrackingService()
        {
            var doc = new HtmlDocument();
            doc.Load("ht.html");
            var links = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//table[1]//tr//td//table[3]//tr//td//table[1]//tr[1]//td");
            ;
            var samsOrderId = links.InnerText.Trim().Replace("Order", "").Trim();
            var tid = doc.DocumentNode.SelectSingleNode("//table[@id='backgroundWrapper']//tr//td/table[1]//tr/td//table[3]//tr//td//table[5]//tr//td//table//tr//td//table//tr//td[2]//p//a");
            var tinfo = await OrderPlacer.SamsClub.SamsClubWorker.GetSamsTrackingInfo(tid.InnerText);
        }
        static void ResponseReceivedHandler(object sender, ResponseReceivedEventArgs e)
        {
            if(e.Response.MimeType.Equals("application/json"))
            {
                
            }
            
            Console.WriteLine($"Status: { e.Response.Status } : {e.Response.StatusText} | File: { e.Response.MimeType } | Url: { e.Response.Url }");
        }
        public static FedexTrackingInfoDto FedExFetch()
        {



            var driver = GetDriver("https://www.fedex.com/fedextrack/?trknbr=61290980824620190323&trkqual=20220415005000~61290980824620190323~FXSP");
            //var wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
            //var devTools = SetdevTools(driver);





            //var idt = devTools.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V96.DevToolsSessionDomains>();
            //idt.Network.Enable(new EnableCommandSettings());
            //string json = null;
            //idt.Network.ResponseReceived += async (sender, e) =>
            //{

            //    if (e.Response.MimeType.Equals("application/json") && e.Response.Url.Contains("shipments"))
            //    {
            //       json = await getResponsesAsync(e.RequestId, idt);
            //        //System.Threading.Thread.Sleep(5000);
            //    }

            //    Console.WriteLine($"Status: { e.Response.Status } : {e.Response.StatusText} | File: { e.Response.MimeType } | Url: { e.Response.Url }");
            //};
            //driver.Navigate().GoToUrl("https://www.fedex.com/fedextrack/?trknbr=61290980824620190323&trkqual=20220415005000~61290980824620190323~FXSP");

            //while(json == null)
            //{
            //    System.Threading.Thread.Sleep(10000);
            //}
            //Console.WriteLine(json);
            return null;

            //return FedexTrackingInfoDto.FromJson(json);
        }
        static async Task<string> getResponsesAsync(string rid, OpenQA.Selenium.DevTools.V101.DevToolsSessionDomains idt)
        {
            var rcs = new GetResponseBodyCommandSettings();
            rcs.RequestId = rid;

            
            var respp = await idt.Network.GetResponseBody(rcs);
            Console.WriteLine(respp.Body);
            return respp.Body;
        }

        static async Task Main(string[] args)
        {
            //await OrderPlacer.SamsClub.SamsClubWorker.GenerateContractAndPlaceOrder("");
            //await OrderPlacer.SamsClub.SamsClubWorker.GetStores();
            //await OrderPlacer.SamsClub.SamsClubWorker.SelectClubAsStoreId(8166);
            //await LayerBao.OrderPlaceBao.PlaceOrderAsync((await ShipStationHandler.FetchOrdersByOrderNumber("4871457260281")).OrderId.Value);

            //HttpHandler.GetDriver("https://www.fedex.com");
            Generics.Services.DatabaseService.AdoNet.Constants.ConnectionString = "Server=shipstation.database.windows.net;Initial Catalog=shipstation;Persist Security Info=True;User ID=sams8;Password=Helloworld123;MultipleActiveResultSets=True;";
            //FedExFetch();
            //await OrderPlacer.SamsClub.SamsClubWorker.UpdateUserCart();
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Exception.ToString());

                //SendItTwo(GmailService.)
                //GmailHelper.Mailer.SendEmail("Exception: " + DateTime.Now.ToString(), eventArgs.Exception.Message + "\n" + eventArgs.Exception.StackTrace, "admin@tayyabtariq.me");
            };
            //while (true)
            //{
            //    Console.WriteLine("Refresh Store");
            //    ShipStationApi.ShipStationHandler.RefreshStore();
            //    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(5));
            //}
            //await ShipStationApi.RateGeneratorHelper.DoForOrder(await ShipStationHandler.FetchOrdersByOrderNumber("113-3226619-2741000"));
            //try
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            await ShipStationApi.RateGeneratorHelper.GetAllRateOrdersAsync();
            //        }
            //        catch (Exception e)
            //        {

            //        }

            //    }

            //}
            //catch (Exception e)
            //{

            //}



            //var packageLink = links[0];
            //var orderId = doc.DocumentNode.SelectNodes("//table//table//table[4]");
            //var thirdTable = (orderId[0].SelectNodes("//table[2]")[2].SelectNodes("//tr//td")[25]).InnerText.Trim();
            //if (packageLink.InnerText.Trim().ToLower().Contains("track"))
            //{
            //    var trackLink = packageLink.GetAttributeValue("href", "").ToString();
            //    var resp = await HttpRequester.HttpHandler.Request(new HttpRequester.HttpRequester()
            //    {
            //        Url = trackLink,
            //        Method = HttpRequester.HttpMethod.GET,
            //        IsAuthorized = false

            //    });
            //    doc.LoadHtml(resp.Response);
            //    var trackingInformation = doc.DocumentNode.SelectNodes("//div[@id='carrier']//a")[0];
            //    var Id = trackingInformation.InnerText.Trim();
            //    var link = trackingInformation.GetAttributeValue("href", "");
            //    var orders = await ShipStationHandler.GetOrdererdOrders(0);
            //    var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes.RemovesAllCharactersFromString().Equals(Id.Trim().RemovesAllCharactersFromString()));
            //}
            //await AmazonLabelPrinting();

            int defaultCase = 0;
            //await CreateOrderBJS("9870867647974");
            Console.WriteLine("PRESS 1 FOR SAMS CLUB ORDER JOBS \n"+
                "PRESS 2 FOR BJS ORDERING JOBS \n " +
                "PRESS 3 For SAMS TRACKING \n " +
                "PRESS 4 FOR Sams Out Of Stock Checking \n PRESS 5 For BJS Tracking \n Press 6 for BJS Out Of Stock");

            defaultCase = int.Parse(Console.ReadLine());

            switch (defaultCase)
            {
                case 1:
                    await RunSamsClubOrderingJob();
                    break;
                case 2:
                    await DoAsync("https://www.bjs.com/signIn");
                    break;
                case 3:
                    while (true)
                    {
                        Console.WriteLine("JOB STARTED " + DateTime.Now.ToString());
                        await OLDSAMSGmailTrackingService();
                        System.Threading.Thread.Sleep(TimeSpan.FromHours(4));
                    }
                    break;
                case 4:
                         await RunSamsOutOfStockJob();
                    break;
                case 5:
                    while (true)
                    {
                        Console.WriteLine("JOB STARTED " + DateTime.Now.ToString());
                        await BJSGmailTrackingService();
                        System.Threading.Thread.Sleep(TimeSpan.FromHours(4));
                    } 
                    break;
                case 6:
                    await BJSOutOfStock();
                    break;
                case 7:
                    await ShipStationApi.OrderEmailProcessing.MuliTaskEmailerAsync();
                    break;
                case 8:
                    BackUpJob();
                    break;
                case 10:
                    await PickListWorker();
                    break;
                case 9:
                   await LabelPrinterAsync();
                    break;
            }
        }

        public static async Task LabelPrinterAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Current Time: " + DateTime.UtcNow.GetEasternDateTimeByRegion());
                    while (DateTime.UtcNow.GetEasternDateTimeByRegion().Hour > 7 && DateTime.UtcNow.GetEasternDateTimeByRegion().Hour < 17)
                    {
                        Console.Clear();
                        await LabelPrinting();
                        Console.Clear();
                        await LabelPrinting(1);
                        Console.Clear();
                        System.Threading.Thread.Sleep(TimeSpan.FromHours(1));
                    }
                    await Task.Delay(TimeSpan.FromHours(0.5));
                }
                catch(Exception e)
                {
                    var FailedSubject = $"URGENT: Failed to print! ";
                    var FailedBody = $"failed to print. \n - Print Manually \n Shopimo.co";
                    GmailHelper.Mailer.SendEmail(FailedSubject, FailedBody, "lior@shopimo.co",
                        new List<string>() { "tiara@shopimo.co", "tia@shopimo.co", "marcus@shopimo.co" },
                        GmailHelper.GoogleBase.StatusOfficialEmail);
                    return;
                }
                
            }
            
            
        }
        public static IWebDriver GetDriver(string urlll)
        {
            var IOSDRIVERPATH  = @"/Users/a/Downloads";
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
                options.AddArgument("--disable-gpu"); // applicable to windows os only
                options.AddArgument("--user-data-dir=~/chromeTemp");
                options.AddExcludedArgument("enable-automation");
                options.AddAdditionalCapability("useAutomationExtension", false);
                driver = new ChromeDriver(IOSDRIVERPATH);

               
            }
            return driver;
        }
        public static IDevToolsSession SetdevTools(IWebDriver driver)
        {
            var tools = driver as IDevTools;
            IDevToolsSession session = tools.GetDevToolsSession();
            return session;
        }
        public static SamsTokenDto BackUpJob()
        {
            while(true)
            {
                var token = LayerBao.SiteMetaBAO.GetKey(Generics.SiteMetaConstants.ACCESS_TOKEN_SAMSCLUB);
                if(token.LastUpdated != null)
                {
                    var dt = DateTime.Now;
                    var diff = (DateTime.Now - token.LastUpdated);
                    if(diff.Minutes > 45)
                    {
                        SamsLogin();
                    }
                }
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(30));
            }
        }
        public static SamsTokenDto SamsLogin()
        {
            IWebDriver driver = null;

            driver = GetDriver("https://www.samsclub.com/?xid=domain_sams");

            while (true)
            {
                try
                {
                    var url = LayerBao.SiteMetaBAO.GetKey(Generics.SiteMetaConstants.SAMSCLUB_LOGIN_URL).VALUE;


                     driver = GetDriver(url);





                    System.Threading.Thread.Sleep(10000);
                    var elemnt = driver.FindElement(By.CssSelector("[Id=signInName]"));
                    elemnt.SendKeys("Sams8@shopimo.co");

                    elemnt = driver.FindElement(By.Id("password"));
                    elemnt.SendKeys("LSHOPIMO2017");

                    var inputElement = driver.FindElement(By.Id("next"));
                    inputElement.SendKeys(Keys.Enter);
                    inputElement.Submit();

                    System.Threading.Thread.Sleep(10000);







                    System.Threading.Thread.Sleep(10000);

                    if (driver.Url.Contains("access_token"))
                    {

                        var pr = ProcessToken(driver.Url);
                        driver.Close();
                        return pr;
                    }

                    var pe = driver.FindElement(By.Id("userSelectedMfaMode_email"));
                    pe.Click();

                    System.Threading.Thread.Sleep(5000);
                    inputElement = driver.FindElement(By.Id("continue"));
                    inputElement.SendKeys(Keys.Enter);
                    //inputElement.Submit();

                    System.Threading.Thread.Sleep(30000);


                    inputElement = driver.FindElement(By.Id("emailVerificationControl-readOnly_but_send_code"));
                    inputElement.Click();

                    System.Threading.Thread.Sleep(1000);

                    inputElement = driver.FindElement(By.Id("verificationCode"));
                    System.Threading.Thread.Sleep(160000);
                    var val = LayerBao.SiteMetaBAO.GetKey("SAMS_CODE").VALUE;
                    inputElement.SendKeys(val);

                    System.Threading.Thread.Sleep(3000);

                    inputElement = driver.FindElement(By.Id("emailVerificationControl-readOnly_but_verify_code"));
                    inputElement.Click();

                    System.Threading.Thread.Sleep(10000);

                    inputElement = driver.FindElement(By.Id("continue"));
                    inputElement.SendKeys(Keys.Enter);
                    //inputElement.Submit();


                    System.Threading.Thread.Sleep(10000);




                    Console.Write(driver.Url);
                    //Puts an Implicit wait, Will wait for 10 seconds before throwing exception
                    var urll = driver.Url.Replace("#", "?");
                    var urr = new Uri(urll);
                    string access_token = HttpUtility.ParseQueryString(urr.Query).Get("access_token");
                    string Idtoken = HttpUtility.ParseQueryString(urr.Query).Get("id_token");
                    driver.Close();
                    var token = new SamsTokenDto()
                    {
                        AccessToken = access_token,
                        IdToken = Idtoken
                    };



                    if (token.AccessToken != null)
                    {
                        LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
                        {
                            KEY = Generics.SiteMetaConstants.ACCESS_TOKEN_SAMSCLUB,
                            VALUE = token.AccessToken
                        });
                    }


                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(18));
                }
                catch(Exception e )
                {
                    if(driver != null)
                    {
                        driver.Close();
                    }
                    Console.WriteLine(e.Message);
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(25));

                }
                
            }

            
        }
        public static SamsTokenDto ProcessToken(string urll)
        {
            urll = urll.Replace("#", "?");
            var url = new Uri(urll);
            string access_token = HttpUtility.ParseQueryString(url.Query).Get("access_token");
            string Idtoken = HttpUtility.ParseQueryString(url.Query).Get("id_token");
            return new SamsTokenDto()
            {
                AccessToken = access_token,
                IdToken = Idtoken
            };
        }
        public static void Login(IWebDriver driver)
        {

            var elemnt = driver.FindElement(By.CssSelector("[Id=emailLogin]"));
            elemnt.SendKeys("bjs@shopimo.co");
            elemnt = driver.FindElement(By.Id("inputPassword"));
            elemnt.SendKeys("Lshopimo2017");
            var inputElement = driver.FindElement(By.ClassName("sign-in-submit-btn"));
            inputElement.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(10000);
            try
            {

                var pe = driver.FindElement(By.ClassName("next-btn"));
                pe.Click();
                System.Threading.Thread.Sleep(5000);
                System.Threading.Thread.Sleep(1000);
                inputElement = driver.FindElement(By.Id("verificationCode"));
                System.Threading.Thread.Sleep(160000);
                var val = LayerBao.SiteMetaBAO.GetKey("BJS_CODE").VALUE;
                inputElement.SendKeys(val);
                System.Threading.Thread.Sleep(3000);
                inputElement = driver.FindElement(By.ClassName("red-btn"));
                inputElement.Click();

                System.Threading.Thread.Sleep(10000);
            }
            catch (Exception ex)
            {

                System.Threading.Thread.Sleep(10000);
            }


        }
        public static async Task CreateOrderBJS(List<string> orders)
        {
            var vall = ShipStationApi.ShipTag.Ordered.GetEnumValue();
            var outofstock = ShipStationApi.ShipTag.OutOfStock.GetEnumValue();
            var ds = ShipStationApi.ShipTag.DsProblems.GetEnumValue();
            var bjs = ShipStationApi.ShipTag.BJS.GetEnumValue();
            var sams = ShipStationApi.ShipTag.SamsDropShipping.GetEnumValue();
           

                try
                {

                   
                  
                       
                        var driver = GetDriver("https://www.bjs.com/signIn");
                        System.Threading.Thread.Sleep(10000);
                        var session = SetdevTools(driver);
                        var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V101.DevToolsSessionDomains>();
                        var cookies = await domains.Network.GetAllCookies();
                        Login(driver);
                        cookies = await domains.Network.GetAllCookies();
                        var container = new CookieContainer();
                        var cook = new CookieContainer();
                        var valll = cookies.Cookies.FirstOrDefault(f => f.Name.Equals("_b2c_jwt_token-live"));
                        foreach (var ck in cookies.Cookies)
                        {
                            try
                            {
                                cook.Add(new System.Net.Cookie()
                                {
                                    Name = ck.Name,
                                    Value = ck.Value,
                                    Domain = ck.Domain
                                });
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                      

                            OrderPlacer.BJS.BjsWorker.cookie = cook;


                            foreach(var orderNumber in orders )
                             {

                    var order = await ShipStationApi.ShipStationHandler.FetchOrdersByOrderNumber(orderNumber);
                    Console.WriteLine(order.OrderNumber + "|" + order.OrderTotal);

                    if (order.Items.Where(e => e.FulfillmentSku == null || e.FulfillmentSku.Length == 0).ToList().Count > 0)
                    {
                        await ShipStationHandler.AddTagToOrder(order?.OrderId ?? 0,
                                ShipTag.FulfillmentSKU.GetEnumValue());
                        continue;
                    }
                    if (order.TagIds.Contains(vall.ToString()) || order.TagIds.Contains(sams.ToString())

                        || order.TagIds.Contains(ds.ToString())
                        )
                    {
                        continue;
                    }


                    var pp = LayerBao.OrderPlaceBao.GetPlaceOrderDtoFromOrder(order);


                    var orderResponse = await OrderPlacer.BJS.BjsWorker.Do(pp);
                    //await OrderPlacer.BJS.BjsWorker.AddItemToCart(0);
                    driver.Navigate().GoToUrl("https://www.bjs.com/checkout");
                    System.Threading.Thread.Sleep(10000);

                    try
                    {
                        var inputElement = driver.FindElement(By.Id("#secureCheckoutBtn"));
                        inputElement.Click();
                        System.Threading.Thread.Sleep(30000);
                        try
                        {
                            inputElement = driver.FindElement(By.ClassName("add-new"));
                            inputElement.Click();
                            System.Threading.Thread.Sleep(10000);
                            inputElement = driver.FindElement(By.ClassName("addNew"));
                            inputElement.Click();
                            System.Threading.Thread.Sleep(10000);
                            var elemnt = driver.FindElement(By.Id("firstName"));
                            elemnt.SendKeys(pp.Address.FirstName);
                            elemnt = driver.FindElement(By.Id("lastname"));
                            elemnt.SendKeys(pp.Address.LastName);
                            elemnt = driver.FindElement(By.Id("Address1"));
                            elemnt.SendKeys(pp.Address.LineOne);
                            elemnt = driver.FindElement(By.Id("Address2"));
                            elemnt.SendKeys(pp.Address.LineTwo);
                            elemnt = driver.FindElement(By.Id("city"));
                            elemnt.SendKeys(pp.Address.City);
                            elemnt = driver.FindElement(By.Id("zipCode"));
                            elemnt.SendKeys(pp.Address.PostalCode);
                            elemnt = driver.FindElement(By.Id("phone"));
                            elemnt.SendKeys(pp.Address.PhoneNumber);

                            elemnt = driver.FindElement(By.Id("state"));
                            var select = new OpenQA.Selenium.Support.UI.SelectElement(elemnt);
                            select.SelectByText(pp.Address.State, true);

                            elemnt = driver.FindElement(By.XPath("//label[contains(@class,'save-check')]//child::input"));
                            elemnt.Click();
                            var vttt = elemnt.TagName;

                            inputElement = driver.FindElement(By.Id("saveNewAddrBtn"));
                            inputElement.Click();
                            System.Threading.Thread.Sleep(10000);
                            inputElement = driver.FindElement(By.ClassName("continue-btn"));
                            inputElement.Click();
                        }
                        catch (Exception ex)
                        {
                            orderResponse.IsSuccessfull = false;
                            orderResponse.IsAddressError = true;
                            await LayerBao.OrderPlaceBao.UpdateOrderReponseOnShipStation(order, orderResponse);

                            return;
                        }

                        System.Threading.Thread.Sleep(20000);
                        var frame = driver.SwitchTo().Frame(driver.FindElement(By.Id("eProtect-iframe")));
                        System.Threading.Thread
                            .Sleep(10000);
                        var eles = driver.FindElements(By.TagName("input"));
                        foreach (var el in eles)
                        {
                            Console.WriteLine(el.Text);
                            var tt = el.GetDomAttribute("name");
                            var idd = el.GetDomAttribute("id");

                            var stt = el.GetAttribute("placeholder");
                            if (idd.Equals("cvv"))
                            {
                                el.SendKeys("7055");
                            }

                        }


                        driver.SwitchTo().DefaultContent();
                        inputElement = driver.FindElement(By.ClassName("continue-btn"));
                        inputElement.Click();
                        orderResponse.IsSuccessfull = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        orderResponse.IsSuccessfull = false;
                        orderResponse.IsAddressError = false;
                        orderResponse.ErrorType = OrderResponseErrorType.OutOfStock;


                    }
                    if (orderResponse.IsSuccessfull)
                    {
                        try
                        {

                            System.Threading.Thread.Sleep(5000);
                            var inputElement = driver.FindElement(By.ClassName("order-id"));
                            var val = inputElement.Text;
                            orderResponse.OrderNumber = (val.Split(":")[1]).Trim();
                        }
                        catch (Exception e)
                        {
                            orderResponse.IsSuccessfull = false;
                        }
                    }

                    await LayerBao.OrderPlaceBao.UpdateOrderReponseOnShipStation(order, orderResponse);
                }

                        
                        driver.Close();
                    
                }
                catch (Exception ex)
                {
                   
                }
            
        }
        public static async Task<SamsTokenDto> DoAsync(string urlll)
        {
            var vall = ShipStationApi.ShipTag.Ordered.GetEnumValue();
            var outofstock = ShipStationApi.ShipTag.OutOfStock.GetEnumValue();
            var ds = ShipStationApi.ShipTag.DsProblems.GetEnumValue();
            var bjs = ShipStationApi.ShipTag.BJS.GetEnumValue();
            var sams = ShipStationApi.ShipTag.SamsDropShipping.GetEnumValue();
            int PageNo = 1;
            while (true)
            {

                try
                {

                    //var response = driver.ExecuteJavaScript<dynamic>("var req = new XMLHttpRequest();req.open('GET', document.location, false);req.send(null);return req.getAllResponseHeaders()");   
                    var orders = await ShipStationApi.ShipStationHandler.GetOrdererdOrders(1);
                    var orderss = orders.Orders.OrderBy(e => e.OrderDate).ToList().Where(f => f.TagIds.Contains(bjs.ToString())).ToList();

                    orderss = orderss.Where(f => !f.TagIds.Contains(vall.ToString())).ToList();
                    orderss = orderss.Where(f => !f.TagIds.Contains(ds.ToString())).ToList();
                    orderss = orderss.Where(f => !f.TagIds.Contains(outofstock.ToString())).ToList();
                    var dl = Generics.Functions.ListDivider<ShipStationApi.Models.Order>(orderss, 12);
                    foreach (var d in dl)
                    {
                        int i = 0;
                        while (i < 4)
                        {
                            if (LayerBao.SiteMetaBAO.GetKey("BJSLOGIN").Equals("WORKING"))
                            {
                                System.Threading.Thread.Sleep(60000);
                            }
                            i++;
                        }

                        LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
                        {
                            KEY = "BJSLOGIN",
                            VALUE = "WORKING"
                        });
                        var driver = GetDriver(urlll);
                        System.Threading.Thread.Sleep(10000);
                        var session = SetdevTools(driver);
                        var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V101.DevToolsSessionDomains>();
                        var cookies = await domains.Network.GetAllCookies();
                        Login(driver);
                        LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
                        {
                            KEY = "BJSLOGIN",
                            VALUE = ""
                        });
                        cookies = await domains.Network.GetAllCookies();
                        var container = new CookieContainer();

                        var cook = new CookieContainer();
                        var valll = cookies.Cookies.FirstOrDefault(f => f.Name.Equals("_b2c_jwt_token-live"));
                        foreach (var ck in cookies.Cookies)
                        {
                            try
                            {
                                cook.Add(new System.Net.Cookie()
                                {
                                    Name = ck.Name,
                                    Value = ck.Value,
                                    Domain = ck.Domain
                                });
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        foreach (var orderr in d)
                        {

                            OrderPlacer.BJS.BjsWorker.cookie = cook;




                            var order = await ShipStationApi.ShipStationHandler.FetchOrdersByOrderNumber(orderr.OrderNumber);
                            Console.WriteLine(order.OrderNumber + "|" + order.OrderTotal);

                            if (order.Items.Where(e => e.FulfillmentSku == null || e.FulfillmentSku.Length == 0).ToList().Count > 0)
                            {
                                await ShipStationHandler.AddTagToOrder(order?.OrderId ?? 0,
                                        ShipTag.FulfillmentSKU.GetEnumValue());
                                continue;
                            }
                            if (order.TagIds.Contains(vall.ToString()) || order.TagIds.Contains(sams.ToString())

                                || order.TagIds.Contains(ds.ToString())
                                )
                            {
                                continue;
                            }


                            var pp = LayerBao.OrderPlaceBao.GetPlaceOrderDtoFromOrder(order);


                            var orderResponse = await OrderPlacer.BJS.BjsWorker.Do(pp);
                            //await OrderPlacer.BJS.BjsWorker.AddItemToCart(0);
                            driver.Navigate().GoToUrl("https://www.bjs.com/checkout");
                            System.Threading.Thread.Sleep(10000);

                            try
                            {
                                var inputElement = driver.FindElement(By.Id("#secureCheckoutBtn"));
                                inputElement.Click();
                                System.Threading.Thread.Sleep(30000);
                                try
                                {
                                    inputElement = driver.FindElement(By.ClassName("add-new"));
                                    inputElement.Click();
                                    System.Threading.Thread.Sleep(10000);
                                    inputElement = driver.FindElement(By.ClassName("addNew"));
                                    inputElement.Click();
                                    System.Threading.Thread.Sleep(10000);
                                    var elemnt = driver.FindElement(By.Id("firstName"));
                                    elemnt.SendKeys(pp.Address.FirstName);
                                    elemnt = driver.FindElement(By.Id("lastname"));
                                    elemnt.SendKeys(pp.Address.LastName);
                                    elemnt = driver.FindElement(By.Id("Address1"));
                                    elemnt.SendKeys(pp.Address.LineOne);
                                    elemnt = driver.FindElement(By.Id("Address2"));
                                    elemnt.SendKeys(pp.Address.LineTwo);
                                    elemnt = driver.FindElement(By.Id("city"));
                                    elemnt.SendKeys(pp.Address.City);
                                    elemnt = driver.FindElement(By.Id("zipCode"));
                                    elemnt.SendKeys(pp.Address.PostalCode);
                                    elemnt = driver.FindElement(By.Id("phone"));
                                    elemnt.SendKeys(pp.Address.PhoneNumber);

                                    elemnt = driver.FindElement(By.Id("state"));
                                    var select = new OpenQA.Selenium.Support.UI.SelectElement(elemnt);
                                    select.SelectByText(pp.Address.State, true);

                                    elemnt = driver.FindElement(By.XPath("//label[contains(@class,'save-check')]//child::input"));
                                    elemnt.Click();
                                    var vttt = elemnt.TagName;

                                    inputElement = driver.FindElement(By.Id("saveNewAddrBtn"));
                                    inputElement.Click();
                                    System.Threading.Thread.Sleep(10000);
                                    inputElement = driver.FindElement(By.ClassName("continue-btn"));
                                    inputElement.Click();
                                }
                                catch (Exception ex)
                                {
                                    orderResponse.IsSuccessfull = false;
                                    orderResponse.IsAddressError = true;
                                    await LayerBao.OrderPlaceBao.UpdateOrderReponseOnShipStation(order, orderResponse);
                                    continue;
                                }

                                System.Threading.Thread.Sleep(20000);
                                var frame = driver.SwitchTo().Frame(driver.FindElement(By.Id("eProtect-iframe")));
                                System.Threading.Thread
                                    .Sleep(10000);
                                var eles = driver.FindElements(By.TagName("input"));
                                foreach (var el in eles)
                                {
                                    Console.WriteLine(el.Text);
                                    var tt = el.GetDomAttribute("name");
                                    var idd = el.GetDomAttribute("id");

                                    var stt = el.GetAttribute("placeholder");
                                    if (idd.Equals("cvv"))
                                    {
                                        el.SendKeys("7055");
                                    }

                                }


                                driver.SwitchTo().DefaultContent();
                                inputElement = driver.FindElement(By.ClassName("continue-btn"));
                                inputElement.Click();
                                orderResponse.IsSuccessfull = true;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                orderResponse.IsSuccessfull = false;
                                orderResponse.IsAddressError = false;
                                orderResponse.ErrorType = OrderResponseErrorType.OutOfStock;


                            }
                            if (orderResponse.IsSuccessfull)
                            {
                                try
                                {

                                    System.Threading.Thread.Sleep(5000);
                                    var inputElement = driver.FindElement(By.ClassName("order-id"));
                                    var val = inputElement.Text;
                                    orderResponse.OrderNumber = (val.Split(":")[1]).Trim();
                                }
                                catch (Exception e)
                                {
                                    orderResponse.IsSuccessfull = false ;
                                }
                            }

                            await LayerBao.OrderPlaceBao.UpdateOrderReponseOnShipStation(order, orderResponse);
                        }
                        driver.Close();
                    }
                    if (orders.Pages > PageNo)
                    {
                        PageNo++;
                    }
                    else
                    {
                        PageNo = 1;
                    }
                    System.Threading.Thread.Sleep(1600000);
                }
                catch (Exception ex)
                {

                }
            }

            return null;
        }
        public static async Task<bool> MuliTaskEmailerAsync()
        {
            var Awaitingorders = await ShipStationHandler.GetOrdererdOrders(1);
            
            var OutOfStockOrders = Awaitingorders.Orders.Where(e => e.TagIds.Contains(ShipTag.OutOfStock.GetEnumValue().ToString())
                && (DateTime.Now - e.OrderDate.Value).TotalDays >= 7).ToList();
            
            
            var OrderedOrders = Awaitingorders.Orders.Where(e => e.TagIds.Contains(ShipTag.Ordered.GetEnumValue().ToString())
               && (DateTime.Now - e.OrderDate.Value).TotalDays >= 7).ToList();


            var Shippedorders = await ShipStationHandler.GetShippedOrders(DateTime.Now.AddDays(-10));


            return true;

        }
        public static async Task BJSOutOfStock()
        {

            while (true)
            {

                try
                {

                    //var response = driver.ExecuteJavaScript<dynamic>("var req = new XMLHttpRequest();req.open('GET', document.location, false);req.send(null);return req.getAllResponseHeaders()");   
                    var orders = await ShipStationApi.ShipStationHandler.GetOutOfStockOrders(ShipTag.BJS);

                    Console.WriteLine("JOb Started At: " + DateTime.Now);

                    foreach (var orderr in orders)
                    {

                        
                        var placeOrder = await OrderPlacer.BJS.BjsWorker.Verifyoutofstock(LayerBao.OrderPlaceBao.GetPlaceOrderDtoFromOrder(orderr));

                        
                        if(placeOrder)
                        {
                           await LayerBao.OrderPlaceBao.UpdateOutOfStock(orderr.OrderId.Value,true);
                        }


                        Console.WriteLine();
                    }
                    System.Threading.Thread.Sleep(190000);

                }
                catch (Exception e)
                {

                }
            }
        }
        public static async Task RunSamsClubOrderingJob()
        {
            while (true)
            {
                try{
                    var orders = await ShipStationApi.ShipStationHandler.GetOrdererdOrders(1);
                    var orderss = orders.Orders.OrderBy(e => e.OrderDate).ToList();
                    orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.BJS.GetEnumValue().ToString())).ToList();
                    orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.Ordered.GetEnumValue().ToString())).ToList();
                    orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.DsProblems.GetEnumValue().ToString())).ToList();
                    orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.OutOfStock.GetEnumValue().ToString())).ToList();
                    orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.INProcess.GetEnumValue().ToString())).ToList();

                    var stores = await OrderPlacer.SamsClub.SamsClubWorker.GetStores();
                    foreach(var st in stores)
                    {
                        await OrderPlacer.SamsClub.SamsClubWorker.SelectClubAsStoreId(Convert.ToInt32(st.Id));
                        List<PlaceOrderDto> placeOrders = new List<PlaceOrderDto>();
                        foreach (var order in orderss)
                        {
                            
                            try
                            {
                                OrderPlacer.SamsClub.SamsClubWorker.CurrentId = 1;
                                placeOrders.Add(LayerBao.OrderPlaceBao.GetPlaceOrderDtoFromOrder(order));
                                
                            }
                            catch (Exception e)
                            {

                            }


                            

                        }
                        var modelsUpdated = await OrderPlacer.SamsClub.SamsClubWorker.DoWork(placeOrders);
                        foreach(var model in modelsUpdated.Where(e => e.IsOrdered).ToList())
                        {

                            var orderssss = ShipStationHandler.FetchOrder(model.OrderId).ConfigureAwait(true);
                            Console.WriteLine(orderssss.GetAwaiter().GetResult().OrderNumber.ToString());
                            await ShipStationHandler.AddTagToOrder(Convert.ToInt64(model.OrderId),
                        ShipTag.INProcess.GetEnumValue());

                        }
                        System.Threading.Thread.Sleep(9000);
                    }

                    

                }
                catch(Exception e)
                {
                    Console.Write(e);
                }

            }
        }
        public static async Task RunSamsOutOfStockJob()
        {
            while (true)
            {
                var orders = await ShipStationApi.ShipStationHandler.GetOrdererdOrders(1);
                var orderss = orders.Orders.OrderBy(e => e.OrderDate).ToList();
                orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.BJS.GetEnumValue().ToString())).ToList();
                orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.Ordered.GetEnumValue().ToString())).ToList();
                orderss = orderss.Where(o => !o.TagIds.Contains(ShipTag.DsProblems.GetEnumValue().ToString())).ToList();
                orderss = orderss.Where(o => o.TagIds.Contains(ShipTag.OutOfStock.GetEnumValue().ToString())).ToList();

                orderss = orderss.OrderByDescending(e => e.OrderDate).ToList();
                Console.WriteLine("JOb Started At: " + DateTime.Now);
                Dictionary<string, bool> itemStatus = new Dictionary<string, bool>();
                foreach (var order in orderss.ToList())
                {
                    //if(order.OrderNumber.Equals("4870610897954"))
                    //{
                    //    Console.WriteLine("DUPLICATE ORDER");
                    //}

                    try
                    {
                        var pl = LayerBao.OrderPlaceBao.GetPlaceOrderDtoFromOrder(order);
                        var result = await OrderPlacer.SamsClub.SamsClubWorker.OutOfStockWorker(pl, itemStatus);


                        if (result)
                        {

                            await LayerBao.OrderPlaceBao.UpdateOutOfStock(order.OrderId.Value, result);

                        }

                        bool itemFound = false;
                        foreach (var item in pl.Items)
                        {
                            if (itemStatus.Get(item.Sku, false))
                            {
                                itemFound = true;
                            }
                        }
                        if (itemFound)
                        {
                            Console.WriteLine("Searching in other orders");
                            var odList = orderss.Select(o => LayerBao.OrderPlaceBao.GetPlaceOrderDtoFromOrder(o)).ToList();
                            foreach (var od in odList)
                            {
                                bool outOfStock = false;
                                foreach (var od2 in od.Items)
                                {
                                    if (itemStatus.Get(od2.Sku, false))
                                    {
                                        outOfStock = outOfStock && true;
                                    }
                                }
                                if (outOfStock)
                                {
                                    Console.WriteLine(od.OrderId);
                                    await LayerBao.OrderPlaceBao.UpdateOutOfStock(Convert.ToInt64(od.OrderId), result);
                                    orderss.Remove(orderss.FirstOrDefault(o => o.OrderId.ToString() == od.OrderId));
                                }
                            }
                        }
                    
                          
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    System.Threading.Thread.Sleep(50000);

                }
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(30));
            }
        }
        public static async Task TrackSamsOrder(string orderNumber)
        {
            var order = await ShipStationHandler.FetchOrdersByOrderNumber(orderNumber);
            var resp = await OrderPlacer.SamsClub.SamsClubWorker.GetTrackingInfo(order.InternalNotes);
            if(resp != null)
            {
                Console.WriteLine(order.OrderNumber + " Tracking:");
                resp.ShipstationOrderId = order.OrderId.ToString();
                await ShipStationHandler.UpdateOrderTracking(resp);
            }

        }
        public static async Task RunTrackingForSamsClub()
        {
            while (true)
            {
                int pageNo = 1;
                var orders = await ShipStationApi.ShipStationHandler.GetOrdererdOrders(pageNo);
                var orderss = orders.Orders.OrderBy(e => e.OrderDate).ToList();



                orderss = orderss.Where(e => e.TagIds.Contains(ShipTag.Ordered.GetEnumValue().ToString())).ToList();
                if (orderss == null || orderss.Count == 0)
                {
                    pageNo = 1;
                    continue;

                }
                foreach (var order in orderss)
                {
                    var val = ShipStationApi.ShipTag.Ordered.GetEnumValue();
                    var outofstock = ShipStationApi.ShipTag.OutOfStock.GetEnumValue();
                    var ds = ShipTag.DsProblems.GetEnumValue();
                    var bjs = ShipStationApi.ShipTag.BJS.GetEnumValue();
                    var sams = ShipTag.SamsDropShipping.GetEnumValue();
                    if (order.TagIds.Contains(val.ToString()) && order.TagIds.Contains(sams.ToString()))
                    {

                        if (order.InternalNotes != "" || order.InternalNotes != null)
                        {
                            System.Threading.Thread.Sleep(10000);
                            var resp = await OrderPlacer.SamsClub.SamsClubWorker.GetTrackingInfo(order.InternalNotes);
                            if (resp != null)
                            {
                                try
                                {
                                    Console.WriteLine(order.OrderNumber + " Tracking:");
                                    resp.ShipstationOrderId = order.OrderId.ToString();
                                    await ShipStationHandler.UpdateOrderTracking(resp);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                            }

                        }
                    }
                }
                pageNo++;
            }
        }

                        
        

    }
}

