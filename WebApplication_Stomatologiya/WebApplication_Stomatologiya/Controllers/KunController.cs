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
    public class KunController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Kun/
        public ActionResult Index()
        {
            return View(db.Kun.ToList());
        }

        // GET: /Kun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kun kun = db.Kun.Find(id);
            if (kun == null)
            {
                return HttpNotFound();
            }
            return View(kun);
        }

        // GET: /Kun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Kun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nomi")] Kun kun)
        {
            if (ModelState.IsValid)
            {
                db.Kun.Add(kun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kun);
        }

        // GET: /Kun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kun kun = db.Kun.Find(id);
            if (kun == null)
            {
                return HttpNotFound();
            }
            return View(kun);
        }

        // POST: /Kun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nomi")] Kun kun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kun);
        }

        // GET: /Kun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kun kun = db.Kun.Find(id);
            if (kun == null)
            {
                return HttpNotFound();
            }
            return View(kun);
        }

        // POST: /Kun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kun kun = db.Kun.Find(id);
            db.Kun.Remove(kun);
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
