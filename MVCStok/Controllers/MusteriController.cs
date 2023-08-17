using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MVCDbStokEntities1 db= new MVCDbStokEntities1();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d;  //değerleri tblmusteriler uzerinde cekip d üzerine attık
            if(!string.IsNullOrEmpty(p))//Eğer p değeri null değilse yap
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p)); // boş değilse müşteri adı içerisinde parametreye eşit olan değerleir getirecek.
            }
            return View(degerler.ToList());
            //var degerler=db.TBLMUSTERILER.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()

        { 
            return View();
        }
       
        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid) //Modelin durumunda doğrulama işlemi yapılmadıysa return bize yenimusteri ekleme vieeini geri göndersim
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var musteri = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}