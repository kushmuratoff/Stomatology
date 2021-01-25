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
    public class BemorController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Bemor/
        public ActionResult Index(int? Id)
        {
            var bemor = db.Bemor.Include(b => b.Userlar).Where(b => b.Id == Id);
            return View(bemor.ToList());
        }
        public ActionResult Kabinet(int? Id)
        {
            ViewBag.Id = Id;
            int soni=0;
            soni= db.DoktorVaqti.Include(d => d.Bemor).Where(d => d.BemorId == Id && d.holati == 1 && d.Sanasi.Value.Day - DateTime.Today.Day == 0 && d.Sanasi.Value.Month - DateTime.Today.Month == 0 && d.Sanasi.Value.Year - DateTime.Today.Year == 0).Count();
            soni += db.BemorgaXabar.Include(d => d.Bemor).Where(d => d.BemorId == Id && d.Holati == 0).Count();
            ViewBag.bugun = soni;
            return View();
        }
        public ActionResult Sozlamalar(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Bemor = db.Bemor.Include(st => st.Userlar).Where(d => d.Id == Id).ToList();
            // var doktorlar = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
            // s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(sto => sto.Id == Id).ToList();
            return View(s);
        }
        [HttpPost]

        public ActionResult Sozlamalar(int? Id, string Familya, string Ism, string Sharif, string YashManzil, string TelNomer, string Login, string Parol, int UsId, string EskiKasallari)
        {
            Bemor dok = db.Bemor.Find(Id);
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
            dok.EskiKasallari = EskiKasallari;
            db.SaveChanges();
            return RedirectToAction("Kabinet", "Bemor", new { Id = Id });
        }
     
        public ActionResult Xabar(int?Id)
        {
            Sahifa s = new Sahifa();
            s.DoktorVaqti = db.DoktorVaqti.Include(d => d.Bemor).Include(d=>d.Doktor.Stomatologiya).Where(d => d.BemorId == Id && d.holati == 1 && d.Sanasi.Value.Day - DateTime.Today.Day == 0 && d.Sanasi.Value.Month - DateTime.Today.Month == 0 && d.Sanasi.Value.Year - DateTime.Today.Year == 0).ToList();
            s.BemorgaXabar = db.BemorgaXabar.Include(d => d.Bemor).Where(d => d.BemorId == Id && d.Holati == 0).ToList();
          
            return View(s);
        }
          public ActionResult XabarniTas(int?Id)
        {
            BemorgaXabar bx = db.BemorgaXabar.Find(Id);
            bx.Holati = 1;
            db.SaveChanges();
            return RedirectToAction("Kabinet", "Bemor", new { Id = bx.BemorId });
        }
        public ActionResult AnketaT(int? Id)
        {
            return View();
        }

        // GET: /Bemor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bemor bemor = db.Bemor.Find(id);
            if (bemor == null)
            {
                return HttpNotFound();
            }
            return View(bemor);
        }

        // GET: /Bemor/Create
        public ActionResult Create()
        {
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini");
            return View();
        }

        // POST: /Bemor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Familya,Ism,Sharif,TugVaqti,PasSeria,PasNomer,KimTomBer,YashManzil,TelNomer,EskiKasallari,UserlarId")] Bemor bemor)
        {
            if (ModelState.IsValid)
            {
                db.Bemor.Add(bemor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", bemor.UserlarId);
            return View(bemor);
        }

        // GET: /Bemor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bemor bemor = db.Bemor.Find(id);
            if (bemor == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", bemor.UserlarId);
            return View(bemor);
        }

        // POST: /Bemor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Familya,Ism,Sharif,TugVaqti,PasSeria,PasNomer,KimTomBer,YashManzil,TelNomer,EskiKasallari,UserlarId")] Bemor bemor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bemor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", bemor.UserlarId);
            return View(bemor);
        }

        // GET: /Bemor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bemor bemor = db.Bemor.Find(id);
            if (bemor == null)
            {
                return HttpNotFound();
            }
            return View(bemor);
        }

        // POST: /Bemor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bemor bemor = db.Bemor.Find(id);
            db.Bemor.Remove(bemor);
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
