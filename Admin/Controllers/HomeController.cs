using Admin.Models;
using Generics;
using Generics.Db;
using Generics.HelperModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ShipStationApi;
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Admin.Controllers
{
    public class Tokens
    {
        string token { get; set; }


    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
        public IActionResult UpdateToken()
        {
            return View();
        }


        [HttpPost]
        public int Dummy()
        {
            using StreamReader reader = new StreamReader(Request.BodyReader.AsStream());
            string s =  reader.ReadToEnd();
            var sss = HttpUtility.UrlDecode(s);
            LayerDao.SiteMetaDAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
            {
                KEY = "HTML",
                VALUE = sss,
                LastUpdated = DateTime.Now,
            });
            return 1;
        }
        [HttpPost]
        public int Sams1()
        {
            using StreamReader reader = new StreamReader(Request.BodyReader.AsStream());
            string s = reader.ReadToEnd();
            var sss = HttpUtility.UrlDecode(s);

            var samsAccont = new SamsAccontDto(){
               Id = 1,
               Cookie = sss
            };
            var acc = LayerDao.SamsAccountDao.GetSamsAccontDto(samsAccont.Id);
            acc.Cookie = samsAccont.Cookie;
            LayerDao.SamsAccountDao.UpdateSamsAccountDao(acc);

         
            return 1;
        }

        [HttpPost]
        public IActionResult UpdateToken(string tok)
        {
            
            var url = tok.Replace("#", "?");
            var urr = new Uri(url);
            string access_token = HttpUtility.ParseQueryString(urr.Query).Get("access_token");
            string Idtoken = HttpUtility.ParseQueryString(urr.Query).Get("id_token");
            
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
            return RedirectToAction("Index");
        }

       

        [HttpPost]
        public async Task<int> RequestTokenBjs()
        {


            using StreamReader reader = new StreamReader(Request.BodyReader.AsStream());
            string s = await reader.ReadToEndAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(s);
            var element = doc.DocumentNode.SelectNodes("//h2");
            var code = element[1].InnerText;
            LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
            {
                KEY = "BJS_CODE",
                VALUE = code

            });
            return 1;
        }

        [HttpPost]
        public async Task<int> RequestToken()
        {


            using StreamReader reader = new StreamReader(Request.BodyReader.AsStream());
            string s = await reader.ReadToEndAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(s);
            var element = doc.GetElementbyId("backgroundWrapper");
            var nodes = element.SelectNodes("//table");
            var table = nodes[9];
            var tr = table.SelectNodes("//body //tr //td");
            var code = tr[12].InnerText.Trim();
            LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
            {
                KEY = "SAMS_CODE",
                VALUE = code

            });
            return 1;
        }
       
        public async Task<IActionResult> Index()
        {
            //using StreamReader reader = new StreamReader(Request.BodyReader.AsStream());
            //string s = await reader.ReadToEndAsync();
            //var doc = new HtmlDocument();
            //doc.LoadHtml(s);
            //var links = doc.DocumentNode.SelectNodes("//a");
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
            //    var ordrrr = orders.Orders.FirstOrDefault(o => o.InternalNotes != null && o.InternalNotes.RemovesAllCharactersFromString().Equals(thirdTable.Trim().RemovesAllCharactersFromString()));
            //    if(ordrrr == null)
            //    {
            //        return Ok("Order Not Found");
            //    }
            //    var trackerInfo = await ShipStationApi.ShipStationHandler.GetUpsTrackingInfo(Id);
            //    try
            //    {
            //        await ShipStationHandler.UpdateOrderTracking(new Generics.HelperModels.OrderShipperDto()
            //        {
            //            ShipstationOrderId = ordrrr.OrderId.ToString(),
            //            TrackingId = Id,
            //            TrackingUrl = link,
            //            CarrierCode = "UPSN",
            //            PlatformOrderId = thirdTable,
            //            TrackingDate = DateTime.Parse(trackerInfo.TrackDetails[0].AdditionalInformation.ShippedOrBilledDate),
                        
            //        });
            //        return Ok(true);
            //    }
            //    catch(Exception e)
            //    {
            //        return Ok(e.Message);
            //    }
            //    return Ok(false);
            //}
            //return Ok("Link Not Found");

            //var doc = new HtmlDocument();
            //doc.LoadHtml(s);
            //var element = doc.GetElementbyId("backgroundWrapper");
            //var nodes = element.SelectNodes("//table");
            //var table = nodes[9];
            //var tr = table.SelectNodes("//body //tr //td");
            //var code = tr[12].InnerText.Trim();
            //LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
            //{
            //    KEY = "SAMS_CODE",
            //    VALUE = code

            //});

            return View();
        }

        [HttpPost]
        public async Task<int> PushToken()
        {
            using StreamReader reader = new StreamReader(Request.BodyReader.AsStream());
            string s = await reader.ReadToEndAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(s);
            var element = doc.GetElementbyId("backgroundWrapper");
            var nodes = element.SelectNodes("//table");
            var table = nodes[9];
            var tr = table.SelectNodes("//body //tr //td");
            var code = tr[12].InnerText.Trim();
            LayerBao.SiteMetaBAO.InsertIfNotFound(new Generics.Db.SiteMetaDto()
            {
                KEY = "SAMS_CODE",
                VALUE = code

            });

            return 1;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
