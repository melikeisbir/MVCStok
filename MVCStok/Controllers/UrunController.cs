using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;


namespace MVCStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MVCDbStokEntities1 db = new MVCDbStokEntities1();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLERR.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()  //Kategorileirn içerisinden tblkategorilerin listesini i'ye ata,
                                                                                   //yeni listeyi seç 
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,   //Bu seçmiş olduğumuz listin değeri i'den geln KATEGORIAD
                                                 Value = i.KATEGORIID.ToString()  //bunun valuesu da i'nin KATEGORIID'si

                                             }).ToList();
            ViewBag.dgr = degerler; //sorguları diğer sayfaya değerleri taşımak için
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLERR p1)
        {
            //Yeni bir kategori ekleme işlemi-> ürün içerisine kategoriyi dropdowndan seç
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault(); //Katogoriid si p1den gelen tblkategorilerdeki kategoriideye eşit olan değeri getirir.
            p1.TBLKATEGORILER = ktg;  //p1.kategorilere ktg'den gelen değeri ata
           
            db.TBLURUNLERR.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index"); //kaydetme işleminden sonra bizi index sayfasına yönlendirsin.
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLERR.Find(id); //tblurunler içerisinde id bul 
            db.TBLURUNLERR.Remove(urun); //urunu kaldır
            db.SaveChanges();
            return RedirectToAction("Index"); //Index sayfasına yönlendir
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLERR.Find(id);
            return View("UrunGetir", urun);
        }



    }
}