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
    public class XulosaController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Xulosa/
        public ActionResult Index()
        {
            var xulosa = db.Xulosa.Include(x => x.Korik);
            return View(xulosa.ToList());
        }

        // GET: /Xulosa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xulosa xulosa = db.Xulosa.Find(id);
            if (xulosa == null)
            {
                return HttpNotFound();
            }
            return View(xulosa);
        }

        // GET: /Xulosa/Create
        public ActionResult Create()
        {
            ViewBag.KorikId = new SelectList(db.Korik, "Id", "Tashxis");
            return View();
        }

        // POST: /Xulosa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,KorikId,Summa,Vaqt")] Xulosa xulosa)
        {
            if (ModelState.IsValid)
            {
                db.Xulosa.Add(xulosa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KorikId = new SelectList(db.Korik, "Id", "Tashxis", xulosa.KorikId);
            return View(xulosa);
        }

        // GET: /Xulosa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xulosa xulosa = db.Xulosa.Find(id);
            if (xulosa == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorikId = new SelectList(db.Korik, "Id", "Tashxis", xulosa.KorikId);
            return View(xulosa);
        }

        // POST: /Xulosa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,KorikId,Summa,Vaqt")] Xulosa xulosa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xulosa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorikId = new SelectList(db.Korik, "Id", "Tashxis", xulosa.KorikId);
            return View(xulosa);
        }

        // GET: /Xulosa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xulosa xulosa = db.Xulosa.Find(id);
            if (xulosa == null)
            {
                return HttpNotFound();
            }
            return View(xulosa);
        }

        // POST: /Xulosa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Xulosa xulosa = db.Xulosa.Find(id);
            db.Xulosa.Remove(xulosa);
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
