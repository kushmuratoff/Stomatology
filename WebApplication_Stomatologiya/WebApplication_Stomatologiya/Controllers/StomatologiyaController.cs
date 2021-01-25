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
    public class StomatologiyaController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Stomatologiya/
        public ActionResult Index()
        {
            Sahifa s = new Sahifa();
            s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).ToList();
            //var stomatologiya = db.Stomatologiya.Include(s => s.Tuman).Include(s => s.Userlar);
            return View(s);
        }
        [HttpPost]
        public ActionResult Qidirish(string nom)
        {
            Sahifa s = new Sahifa();
            s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(d=>d.Nomi.Contains(nom)||d.Manzil.Contains(nom)||d.TelNomer.Contains(nom)||d.Tuman.Nomi.Contains(nom)).ToList();
            return View(s);
        }
        public ActionResult Kabinet(int? Id)
        {
            Sahifa s = new Sahifa();
            s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(st=>st.Id==Id).ToList();
            s.Doktor = db.Doktor.Include(d => d.Stomatologiya).Include(d => d.Userlar).Where(st => st.StomatologiyaId == Id).ToList();
            return View(s);
        }
        // GET: /Stomatologiya/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stomatologiya stomatologiya = db.Stomatologiya.Find(id);
            if (stomatologiya == null)
            {
                return HttpNotFound();
            }
            return View(stomatologiya);
        }

        // GET: /Stomatologiya/Create
        public ActionResult Create()
        {
            ViewBag.TumanId = new SelectList(db.Tuman, "Id", "Nomi");
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini");
            return View();
        }

        // POST: /Stomatologiya/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nomi,Logatip,TumanId,Manzil,TelNomer,UserlarId")] Stomatologiya stomatologiya)
        {
            if (ModelState.IsValid)
            {
                db.Stomatologiya.Add(stomatologiya);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TumanId = new SelectList(db.Tuman, "Id", "Nomi", stomatologiya.TumanId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", stomatologiya.UserlarId);
            return View(stomatologiya);
        }

        // GET: /Stomatologiya/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stomatologiya stomatologiya = db.Stomatologiya.Find(id);
            if (stomatologiya == null)
            {
                return HttpNotFound();
            }
            ViewBag.TumanId = new SelectList(db.Tuman, "Id", "Nomi", stomatologiya.TumanId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", stomatologiya.UserlarId);
            return View(stomatologiya);
        }

        // POST: /Stomatologiya/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nomi,Logatip,TumanId,Manzil,TelNomer,UserlarId")] Stomatologiya stomatologiya)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stomatologiya).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TumanId = new SelectList(db.Tuman, "Id", "Nomi", stomatologiya.TumanId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Logini", stomatologiya.UserlarId);
            return View(stomatologiya);
        }

        // GET: /Stomatologiya/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stomatologiya stomatologiya = db.Stomatologiya.Find(id);
            if (stomatologiya == null)
            {
                return HttpNotFound();
            }
            return View(stomatologiya);
        }

        // POST: /Stomatologiya/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stomatologiya stomatologiya = db.Stomatologiya.Find(id);
            db.Stomatologiya.Remove(stomatologiya);
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
