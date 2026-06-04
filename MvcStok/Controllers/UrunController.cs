using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db=new MvcDbStokEntities();
        
        public ActionResult Index()
        {
            var urunler = db.Urunler.ToList();

            return View(urunler);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler=(from i in db.Kategoriler.ToList() select new SelectListItem
            {
                Text=i.KategoriAdi,
                Value=i.KategoriId.ToString(),
            }).ToList();
            ViewBag.dgr1 = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Urunler u)
        {

            var ktg = db.Kategoriler.Where(i => i.KategoriId == u.UrunKategori).FirstOrDefault();
            u.Kategoriler = ktg;
            db.Urunler.AddObject(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Sil(int id)
        {
            var ogesil=db.Urunler.FirstOrDefault(x=>x.UrunId==id);
            db.Urunler.DeleteObject(ogesil);
            db.SaveChanges();
          
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdi,
                                                 Value = i.KategoriId.ToString(),
                                             }).ToList();
            ViewBag.dgr1 = degerler;
            var urun = db.Urunler.FirstOrDefault(i => i.UrunId == id);
            return View(urun);
        }
        [HttpPost]
        public ActionResult Guncelle(Urunler u,int id)
        {

            var ktg = db.Kategoriler.Where(i => i.KategoriId == u.UrunKategori).FirstOrDefault();
            u.Kategoriler = ktg;
            var urun = db.Urunler.FirstOrDefault(i => i.UrunId == id);
            urun.Urunadi = u.Urunadi;
            urun.Urunstok = u.Urunstok;
            urun.UrunKategori=u.UrunKategori;
            urun.UrunFiyat = u.UrunFiyat;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}