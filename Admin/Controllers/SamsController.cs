using Generics.Db;
using Generics.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;

namespace Admin.Controllers
{
    public class SamsController : Controller
    {
        [HttpPost]
        public IActionResult UpdateEmailToken(SamsAccontDto samsAccountDto)
        {

            var url = samsAccountDto.Token.Replace("#", "?");
            var urr = new Uri(url);
            string access_token = HttpUtility.ParseQueryString(urr.Query).Get("access_token");
            string Idtoken = HttpUtility.ParseQueryString(urr.Query).Get("id_token");

            var acc = LayerDao.SamsAccountDao.GetSamsAccontDto(samsAccountDto.Id);


            var token = new SamsTokenDto()
            {
                AccessToken = access_token,
                IdToken = Idtoken
            };



            if (token.AccessToken != null)
            {
                acc.Token = access_token;
                acc.Id = samsAccountDto.Id;
                LayerDao.SamsAccountDao.UpdateSamsAccountDao(acc);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult UpdateToken()
        {
            return View();
        }
        // GET: SamsController
        public ActionResult Index()
        {
            
            return View(LayerDao.SamsAccountDao.GetAllAccountInfo());
        }

        // GET: SamsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SamsController/Create
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult Token(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Cookie(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult Token(SamsAccontDto samsAccont)
        {
            var acc = LayerDao.SamsAccountDao.GetSamsAccontDto(samsAccont.Id);
            acc.Token = samsAccont.Token;
            LayerDao.SamsAccountDao.UpdateSamsAccountDao(acc);
            return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        public ActionResult Cookie(SamsAccontDto samsAccont)
        {
            var acc = LayerDao.SamsAccountDao.GetSamsAccontDto(samsAccont.Id);
            acc.Cookie = samsAccont.Cookie;
            LayerDao.SamsAccountDao.UpdateSamsAccountDao(acc);
            return RedirectToAction(nameof(Index));
        }
        // POST: SamsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamsAccontDto samsAccont)
        {
            try
            {
                LayerDao.SamsAccountDao.InsertSamsAccountDao(samsAccont);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SamsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SamsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SamsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SamsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
