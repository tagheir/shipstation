using Generics.Db;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admin.Controllers
{
    public class SiteMetasController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View(LayerBao.SiteMetaBAO.GetSiteMetas());
        }
        [Route("/sitemetas/edit/{id}")]
        public IActionResult Edit(string id)
        {

            return View(LayerBao.SiteMetaBAO.GetKey(id));
        }
        [Route("/sitemetas/create")]
        public IActionResult Create(string id)
        {

            return View();
        }
        [Route("/sitemetas/create")]
        [HttpPost]
        public IActionResult Edit(SiteMetaDto siteMetas)
        {
            LayerBao.SiteMetaBAO.InsertIfNotFound(siteMetas);
            return RedirectToAction("index");
        }

    }
}
