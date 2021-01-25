using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication_Stomatologiya.Models;
using System.Data.Entity;
namespace WebApplication_Stomatologiya.Controllers
{
    public class HomeController : Controller
    {
        public BazaContext db = new BazaContext();
        public ActionResult Index()
        {
            Sahifa s = new Sahifa();
            s.Yangilik = db.Yangilik.Include(y=>y.Stomatologiya).OrderByDescending(y=>y.Vaqti).ToList();
            return View(s);
        }
        public ActionResult Keng(int? Id)
        {
            Sahifa s = new Sahifa();
            s.Yangilik = db.Yangilik.Include(y => y.Stomatologiya).Where(t => t.Id == Id).ToList();
            return View(s);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
                return RedirectToAction("Saqlash", "Home", new { Login = Login, Parol = Parol });
            }

        }
        public ActionResult Saqlash(string Login, string Parol)
        {
            ViewBag.Sto = db.Stomatologiya.ToList();
            ViewBag.Login = Login;
            ViewBag.Parol = Parol;
            return View();
        }
        [HttpPost]
        public ActionResult Saqlash(string Login, string Parol, string Fam, string Ism, string Shar, string Manzil, string PasS, string PasN, string tel, string Ktb, string eskiK, DateTime tugyil)
        {
            Userlar user = new Userlar();
            user.Logini = Login;
            user.Paroli = Parol;
            // user.XaridorId = db.Xaridor.Where(x => x.Id == idsi).FirstOrDefault().Id;
            user.RollarId = 3;
            db.Userlar.Add(user);
            db.SaveChanges();
            int Idd = db.Userlar.Where(u => u.Logini == Login).FirstOrDefault().Id;
            Bemor bemor = new Bemor();
            bemor.Familya = Fam;
            bemor.Ism = Ism;
            bemor.Sharif = Shar;
            bemor.TugVaqti = tugyil;
            bemor.PasSeria = PasS;
            bemor.PasNomer = PasN;
            bemor.KimTomBer = Ktb;
            bemor.YashManzil = Manzil;
            bemor.TelNomer = tel;
            bemor.EskiKasallari = eskiK;
           // bemor.StomatologiyaId = StoId;
            bemor.UserlarId = Idd;
            db.Bemor.Add(bemor);
            db.SaveChanges();
            var Id = db.Bemor.Where(b => b.UserlarId == Idd).FirstOrDefault().Id;
            FormsAuthentication.SetAuthCookie(Login, true);
            return RedirectToAction("Kabinet", "Bemor", new { Id = Id });
        }

        public ActionResult Krish()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Krish(string Login, string Parol)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Userlar user = null;
                using (BazaContext db = new BazaContext())
                {
                    user = db.Userlar.Where(u => u.Logini == Login &&
                    u.Paroli == Parol).FirstOrDefault();
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(Login, true);
                    var rolee = user.RollarId;
                    BazaContext db = new BazaContext();
                    var nomi = db.Rollar.Where(r => r.Id == rolee).First().Nomi;
                    var useridd = user.Id;

                    switch (nomi)
                    {
                        case "Doktor": {
                            int doktorid = db.Doktor.Where(b => b.UserlarId == useridd).FirstOrDefault().Id;

                            return RedirectToAction("Oyna", "Doktor", new { Id = doktorid }); } break;
                        case "Bemor":
                            {
                                int bemorid = db.Bemor.Where(b => b.UserlarId == useridd).FirstOrDefault().Id;

                                return RedirectToAction("Kabinet", "Bemor", new { Id = bemorid });
                            } break;
                        case "Admin":
                            {
                                var d = db.Stomatologiya.Where(s => s.UserlarId == user.Id).FirstOrDefault().Id;

                                return RedirectToAction("Index", "Admin", new { Id = d });
                            } break;
                        case "Samariddin":
                            {
                              

                                return RedirectToAction("Index", "Asosiy");
                            } break;

                    } 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Bunday foydalanuvchi mavjud emas");
                }
            }
            return RedirectToAction("Index", "Home");


        }
        public ActionResult Enter()
        {
            var login = User.Identity.Name;


            var nomi = db.Userlar.Include(u=>u.Rollar).Where(r => r.Logini == login).FirstOrDefault().Rollar.Nomi;
            var useridd = db.Userlar.Include(u => u.Rollar).Where(r => r.Logini == login).FirstOrDefault().Id;

            switch (nomi)
            {
                case "Doktor":
                    {
                        int doktorid = db.Doktor.Where(b => b.UserlarId == useridd).FirstOrDefault().Id;

                        return RedirectToAction("Oyna", "Doktor", new { Id = doktorid });
                    } break;
                case "Bemor":
                    {
                        int bemorid = db.Bemor.Where(b => b.UserlarId == useridd).FirstOrDefault().Id;

                        return RedirectToAction("Kabinet", "Bemor", new { Id = bemorid });
                    } break;
                case "Admin":
                    {
                        var d = db.Stomatologiya.Where(s => s.UserlarId == useridd).FirstOrDefault().Id;

                        return RedirectToAction("Index", "Admin", new { Id = d });
                    } break;

            } 

            return View();
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}