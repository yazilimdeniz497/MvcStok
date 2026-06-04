using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            var satislar = db.Satislar.ToList();

            return View(satislar);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler = (from i in db.Urunler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Urunadi,
                                                 Value = i.UrunId.ToString(),
                                             }).ToList();
            List<SelectListItem> degerler1 = (from a in db.Musteriler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.MusteriAD + " " + a.MusteriSoyad,
                                                  Value = a.MüsteriId.ToString(),
                                              }).ToList();
            ViewBag.dgr1 = degerler;
            ViewBag.dgr2 = degerler1;
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Satislar s)
        {

            var urun = db.Urunler.Where(i => i.UrunId == s.Urunıd).FirstOrDefault();
           s.Urunler = urun;
            var musteri = db.Musteriler.Where(i => i.MüsteriId == s.MüsteriId).FirstOrDefault();
            s.Urunler = urun;
            s.Musteriler=musteri;
            db.Satislar.AddObject(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Sil(int id)
        {
            var ogesil = db.Satislar.FirstOrDefault(x => x.SatisId == id);
            db.Satislar.DeleteObject(ogesil);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> degerler = (from i in db.Urunler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Urunadi,
                                                 Value = i.UrunId.ToString(),
                                             }).ToList();
            List<SelectListItem> degerler1 = (from a in db.Musteriler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.MusteriAD + " " + a.MusteriSoyad,
                                                  Value = a.MüsteriId.ToString(),
                                              }).ToList();
            ViewBag.dgr1 = degerler;
            ViewBag.dgr2 = degerler1;
            var satis=db.Satislar.FirstOrDefault(d=>d.SatisId==id);
            return View(satis);
        }
        
        [HttpPost]
        public ActionResult Guncelle(Satislar s, int id)
        {

            var Urun = db.Urunler.Where(i => i.UrunId == s.Urunıd).FirstOrDefault();
            s.Urunler = Urun;
            var musteri = db.Musteriler.FirstOrDefault(i => i.MüsteriId == s.MüsteriId);
            s.Musteriler = musteri;
            var satis = db.Satislar.FirstOrDefault(i => i.SatisId == id);
            satis.Urunıd = s.Urunıd;
            satis.MüsteriId = s.MüsteriId;
            satis.ADET = s.ADET;
            satis.Fiyat=s.Fiyat;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}