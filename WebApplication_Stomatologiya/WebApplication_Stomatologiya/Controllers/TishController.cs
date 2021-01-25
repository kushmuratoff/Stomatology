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
    public class TishController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Tish/
        public ActionResult Index()
        {
            return View(db.Tish.ToList());
        }

        // GET: /Tish/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tish tish = db.Tish.Find(id);
            if (tish == null)
            {
                return HttpNotFound();
            }
            return View(tish);
        }

        // GET: /Tish/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tish/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nomi,Malumot")] Tish tish)
        {
            if (ModelState.IsValid)
            {
                db.Tish.Add(tish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tish);
        }

        // GET: /Tish/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tish tish = db.Tish.Find(id);
            if (tish == null)
            {
                return HttpNotFound();
            }
            return View(tish);
        }

        // POST: /Tish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nomi,Malumot")] Tish tish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tish);
        }

        // GET: /Tish/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tish tish = db.Tish.Find(id);
            if (tish == null)
            {
                return HttpNotFound();
            }
            return View(tish);
        }

        // POST: /Tish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tish tish = db.Tish.Find(id);
            db.Tish.Remove(tish);
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
