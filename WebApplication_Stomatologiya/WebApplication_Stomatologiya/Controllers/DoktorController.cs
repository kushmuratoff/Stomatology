using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication_Stomatologiya.Models;

namespace WebApplication_Stomatologiya.Controllers
{
    public class DoktorController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Doktor/
        public ActionResult Index()
        {
            Sahifa s = new Sahifa();
           s.Doktor= db.Doktor.Include(d => d.Stomatologiya).Include(d => d.Userlar).ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult Qidirish(string nom)
        {
            Sahifa s = new Sahifa();
            s.Doktor = db.Doktor.Include(d => d.Stomatologiya).Include(d => d.Userlar).Where(d=>d.Familya.Contains(nom)||d.Ism.Contains(nom)||d.Sharif.Contains(nom)||d.TelNomer.Contains(nom)||d.YashManzil.Contains(nom)).ToList();
            return View(s);
        }
        public ActionResult Sozlamalar(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Doktor = db.Doktor.Include(st => st.Userlar).Where(d => d.Id == Id).ToList();
           // var doktorlar = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
           // s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(sto => sto.Id == Id).ToList();
            return View(s);
        }
        [HttpPost]

        public ActionResult Sozlamalar(int? Id, string Familya, string Ism, string Sharif, string YashManzil, string TelNomer, string Login, string Parol, int UsId)
        {
            Doktor dok = db.Doktor.Find(Id);
           // stom = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(s => s.Id == Id).FirstOrDefault();
            Userlar us = db.Userlar.Find(UsId);
            //us = db.Userlar.Where(u => u.Id == UsId).FirstOrDefault();
            us.Logini = Login;
            us.Paroli = Parol;
            db.SaveChanges();
            dok.Familya = Familya;
            dok.Ism = Ism;
            dok.Sharif = Sharif;
            dok.TelNomer = TelNomer;
            dok.YashManzil = YashManzil;
            db.SaveChanges();
            return RedirectToAction("Oyna", "Doktor", new { Id = Id });
        }
     

        public ActionResult Qabul(int? Id)
        {
            ViewBag.id = Id;
            return View();
        }

        public ActionResult QabulVaqti(int? D_Id,DateTime? vaqt)
        {
           string kun = vaqt.Value.DayOfWeek.ToString();
           ViewBag.vaqt = vaqt;
           ViewBag.D_Id = D_Id;
           int haftakuni = 0;
           switch (kun)
           {
               case "Monday": haftakuni = 1; break;
               case "Tuesday": haftakuni = 2; break;
               case "Wednesday": haftakuni = 3; break;
               case "Thursday": haftakuni = 4; break;
               case "Friday": haftakuni = 5; break;
               case "Saturday": haftakuni = 6; break;
               case "Sunday": haftakuni = 7; break;
              
           }
           
          string kelish = db.IshVaqti.Include(i => i.Doktor).Include(i => i.Kun).Where(i => i.DoktorId == D_Id && i.KunId==haftakuni).FirstOrDefault().KelishV;
          string ketish = db.IshVaqti.Include(i => i.Doktor).Include(i => i.Kun).Where(i => i.DoktorId == D_Id && i.KunId == haftakuni).FirstOrDefault().KetishV;
          ViewBag.kel = kelish;
          ViewBag.ket = ketish;
          int soat = 0; int min = 0;
          int a = Convert.ToInt16(kelish);
          int b = Convert.ToInt16(ketish);
          string summ = "";
          for (int i = a; i < b;i++ )
          {
              for (int j = 0; j < 60; j = j + 30)
              {
                  string sss = i.ToString() + "." + j.ToString();
                  int borligi = 0;
                   borligi = db.DoktorVaqti.Where(d => d.Sanasi == vaqt && d.DoktorId == D_Id && string.Equals(d.vaqti, sss)).Count();
                  if(borligi==0)
                  {
                      summ += i.ToString() + "." + j.ToString() + " "; 
                  }
                  
                 
              }
          }
          ViewBag.sum = summ;
          ViewBag.kuni = haftakuni;
          string summ1 = "";
          
            return View();
        }
        [HttpPost]
        public ActionResult VaqtSaqlash(int? D_Id,DateTime? sana,string vaqti)
        {
            ViewBag.vaqt = vaqti;
            DoktorVaqti dv = new DoktorVaqti();
            dv.DoktorId = D_Id;
            var idd = db.Userlar.Where(u => u.Logini == User.Identity.Name).FirstOrDefault().Id;
            dv.BemorId = db.Bemor.Where(b => b.UserlarId == idd).FirstOrDefault().Id;
            dv.Sanasi = sana;
            dv.vaqti = vaqti;
            dv.holati = 0;
            db.DoktorVaqti.Add(dv);
            db.SaveChanges();
            return View();
        }
        public ActionResult Oyna(int? Id)
        {
            
            ViewBag.Id = Id;
            ViewBag.soni = db.DoktorVaqti.Where(d => d.DoktorId == Id && d.holati == 0).Count();
            ViewBag.bugun = db.DoktorVaqti.Include(d => d.Bemor).Where(d => d.DoktorId == Id && (d.holati==0||d.holati==1) && d.Sanasi.Value.Day - DateTime.Today.Day == 0 && d.Sanasi.Value.Month - DateTime.Today.Month == 0 && d.Sanasi.Value.Year - DateTime.Today.Year == 0).Count();
            return View();
        }
        public ActionResult BugXabar(int Id)
        {
            Sahifa s = new Sahifa();
            s.DoktorVaqti = db.DoktorVaqti.Include(d => d.Bemor).Where(d => d.DoktorId == Id && (d.holati == 0 || d.holati == 1) && d.Sanasi.Value.Day - DateTime.Today.Day == 0 && d.Sanasi.Value.Month - DateTime.Today.Month == 0 && d.Sanasi.Value.Year - DateTime.Today.Year == 0).ToList();
            return View(s);
          
        }
        public ActionResult RemoveB(int Id)
        {
            DoktorVaqti dok = db.DoktorVaqti.Find(Id);
            db.DoktorVaqti.Remove(dok);
            db.SaveChanges();
            return RedirectToAction("Enter", "Home");          
        }
      
       public ActionResult YangiBemor(int? Id)
        {
            Sahifa s = new Sahifa();
            ViewBag.Did = Id;
            s.DoktorVaqti = db.DoktorVaqti.Include(d => d.Bemor).Where(d => d.DoktorId == Id && d.holati == 0).ToList();
            return View(s);
        }
        public ActionResult TashxisQ(int? Did,int?Bid)
       {
           ViewBag.d = Did;
           ViewBag.b = Bid;
           return View();
       }
        [HttpPost]
        public ActionResult TashxisQ(int? DoktorId, int? BemorId, string Tashxis, string Shikoyat, string KasRivoj, string LabNatija, string Tishlash, string OgizBosh, int Summa)
        {
            var idd = db.DoktorVaqti.Where(d => d.DoktorId == DoktorId && d.BemorId == BemorId && d.holati == 0).FirstOrDefault().Id;
            DoktorVaqti dv = db.DoktorVaqti.Find(idd);
            dv.holati = 1;
            db.SaveChanges();
            Korik k = new Korik();
            k.DoktorId = DoktorId;
            k.BemorId = BemorId;
            k.Tashxis = Tashxis;
            k.Shikoyat = Shikoyat;
            k.KasRivoj = KasRivoj;
            k.LabNatija = LabNatija;
            k.Tishlash = Tishlash;
            k.OgizBosh = OgizBosh;
            k.Summa = Summa;
            k.Holat = 0;
            db.Korik.Add(k);
            db.SaveChanges();

            return RedirectToAction("Oyna", "Doktor", new { Id = DoktorId });
        }

        // GET: /Doktor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doktor doktor = db.Doktor.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            return View(doktor);
        }

        // GET: /Doktor/Create
        public ActionResult Create()
        {
            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi");
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini");
            return View();
        }

        // POST: /Doktor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Familya,Ism,Sharif,TugVaqti,PasSeria,PasNomer,KimTomBer,Rasm,YashManzil,TelNomer,UserlarId,StomatologiyaId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                db.Doktor.Add(doktor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi", doktor.StomatologiyaId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", doktor.UserlarId);
            return View(doktor);
        }

        // GET: /Doktor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doktor doktor = db.Doktor.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi", doktor.StomatologiyaId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", doktor.UserlarId);
            return View(doktor);
        }

        // POST: /Doktor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Familya,Ism,Sharif,TugVaqti,PasSeria,PasNomer,KimTomBer,Rasm,YashManzil,TelNomer,UserlarId,StomatologiyaId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doktor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi", doktor.StomatologiyaId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", doktor.UserlarId);
            return View(doktor);
        }

        // GET: /Doktor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doktor doktor = db.Doktor.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            return View(doktor);
        }

        // POST: /Doktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doktor doktor = db.Doktor.Find(id);
            db.Doktor.Remove(doktor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
