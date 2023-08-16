using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MVCDbStokEntities1 db= new MVCDbStokEntities1();
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORILER.ToList();
            return View(degerler);
        }
        [HttpGet] // Herhangi bir işlem yapmazsam sayfayı geri döndürme işlemi
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost] //Sayfaya herhangi bir işlem yapıldığında kaydet butonuna bastığımız zaman işlemi gerçekleştirir.
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if(!ModelState.IsValid) //Modelin durumunda doğrulama işlemi yapılmadıysa return bize yenikategori ekleme vieeini geri göndersim
            {
                return View("YeniKategori");
            }

            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges(); ;
            return View();

        }
        //Silme işlemi uygulamak için
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", ktgr); //kategori bilgilerinin olduğu viewi döndürsün ktgrden gelecek olan değerlerle
        }
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID); //pden göndermiş oldugumuz kategorinin idsini bulsun
            ktg.KATEGORIAD = p1.KATEGORIAD; //ilgili id ye göre ad değiştirecek
            db.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}