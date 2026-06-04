using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var musteri = db.Musteriler.ToList();
            return View(musteri);
        }
        [HttpGet]
        public ActionResult Ekle() {

            return View();
        
        }
        [HttpPost]
        public ActionResult Ekle(Musteriler m)
        {
            db.Musteriler.AddObject(m);
            db.SaveChanges();


            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Sil(int id)
        {
            var ogesil=db.Musteriler.FirstOrDefault(u=>u.MüsteriId==id) ;
            db.Musteriler.DeleteObject(ogesil);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}