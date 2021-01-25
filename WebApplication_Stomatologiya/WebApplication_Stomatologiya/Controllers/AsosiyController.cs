using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication_Stomatologiya.Models;
using System.Data.Entity;
using System.IO;
namespace WebApplication_Stomatologiya.Controllers
{
    public class AsosiyController : Controller
    {

        public BazaContext db = new BazaContext();

        // GET: /Asosiy/
        public ActionResult Index()
        {
            Sahifa s = new Sahifa();
            s.Stomatologiya = db.Stomatologiya.ToList();
            return View(s);
        }
        public ActionResult Qushish()
        {
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.habar = "";
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Login, string Parol)
        {

            Userlar user = null;
            user = db.Userlar.Where(u => u.Logini == Login).FirstOrDefault();
            if (user != null)
            {
                ViewBag.habar = "Bunday loginli foydalanuvchi mavjud";
                return View();
            }
            else
            {
                return RedirectToAction("Saqlash", "Asosiy", new { Login = Login, Parol = Parol });
            }

        }
        public ActionResult Saqlash(string Login, string Parol)
        {
            Sahifa s = new Sahifa();
            s.Tuman = db.Tuman.ToList();
            ViewBag.Login = Login;
            ViewBag.Parol = Parol;
            return View(s);
        }
        [HttpPost]
        public ActionResult Saqlash(string Login, string Parol, string Nomi, string Manzil, string TelNomer, int? TumanId, HttpPostedFileBase Imagefile)
        {
            Userlar user = new Userlar();
            user.Logini = Login;
            user.Paroli = Parol;
            // user.XaridorId = db.Xaridor.Where(x => x.Id == idsi).FirstOrDefault().Id;
            user.RollarId = 2;
            db.Userlar.Add(user);
            db.SaveChanges();
            int Idd = db.Userlar.Where(u => u.Logini == Login).FirstOrDefault().Id;
            Stomatologiya stom = new Stomatologiya();
            stom.Manzil = Manzil;
                stom.TumanId=TumanId;
            stom.TelNomer=TelNomer;
            stom.UserlarId=Idd;
            stom.Nomi = Nomi;
            if (Imagefile != null)
            {
                string path = Server.MapPath("~/Rasmlar/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = Path.GetFileName(Imagefile.FileName);
                Imagefile.SaveAs(path + Path.GetFileName(Imagefile.FileName));
                stom.Logatip = filename;
            }
            db.Stomatologiya.Add(stom);
            db.SaveChanges();
            // var Id = db.doktor.Where(b => b.UserlarId == Idd).FirstOrDefault().Id;
            return RedirectToAction("Index", "Asosiy");
        }
     
	}
}