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
    public class DoktorVaqtiController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /DoktorVaqti/
        public ActionResult Index()
        {
            var doktorvaqti = db.DoktorVaqti.Include(d => d.Bemor).Include(d => d.Doktor);
            return View(doktorvaqti.ToList());
        }

        // GET: /DoktorVaqti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorVaqti doktorvaqti = db.DoktorVaqti.Find(id);
            if (doktorvaqti == null)
            {
                return HttpNotFound();
            }
            return View(doktorvaqti);
        }

        // GET: /DoktorVaqti/Create
        public ActionResult Create()
        {
            ViewBag.BemorId = new SelectList(db.Bemor, "Id", "Familya");
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya");
            return View();
        }

        // POST: /DoktorVaqti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,DoktorId,BemorId,Sanasi,vaqti,holati")] DoktorVaqti doktorvaqti)
        {
            if (ModelState.IsValid)
            {
                db.DoktorVaqti.Add(doktorvaqti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BemorId = new SelectList(db.Bemor, "Id", "Familya", doktorvaqti.BemorId);
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya", doktorvaqti.DoktorId);
            return View(doktorvaqti);
        }

        // GET: /DoktorVaqti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorVaqti doktorvaqti = db.DoktorVaqti.Find(id);
            if (doktorvaqti == null)
            {
                return HttpNotFound();
            }
            ViewBag.BemorId = new SelectList(db.Bemor, "Id", "Familya", doktorvaqti.BemorId);
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya", doktorvaqti.DoktorId);
            return View(doktorvaqti);
        }

        // POST: /DoktorVaqti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DoktorId,BemorId,Sanasi,vaqti,holati")] DoktorVaqti doktorvaqti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doktorvaqti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BemorId = new SelectList(db.Bemor, "Id", "Familya", doktorvaqti.BemorId);
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya", doktorvaqti.DoktorId);
            return View(doktorvaqti);
        }

        // GET: /DoktorVaqti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorVaqti doktorvaqti = db.DoktorVaqti.Find(id);
            if (doktorvaqti == null)
            {
                return HttpNotFound();
            }
            return View(doktorvaqti);
        }

        // POST: /DoktorVaqti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoktorVaqti doktorvaqti = db.DoktorVaqti.Find(id);
            db.DoktorVaqti.Remove(doktorvaqti);
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
