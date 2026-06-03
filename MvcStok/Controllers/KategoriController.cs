using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities db=new MvcDbStokEntities();
        public ActionResult Index()
        {
            var kategori=db.Kategoriler.ToList();
            return View(kategori);
        }
        [HttpGet]
        public ActionResult Ekle() {
            return View();
        
        }
        [HttpPost]
        public ActionResult Ekle(Kategoriler k)
        {
            db.Kategoriler.AddObject(k);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
      
    }
}