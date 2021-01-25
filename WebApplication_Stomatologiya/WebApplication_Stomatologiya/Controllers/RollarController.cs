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
    public class RollarController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Rollar/
        public ActionResult Index()
        {
            return View(db.Rollar.ToList());
        }

        // GET: /Rollar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rollar rollar = db.Rollar.Find(id);
            if (rollar == null)
            {
                return HttpNotFound();
            }
            return View(rollar);
        }

        // GET: /Rollar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Rollar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nomi")] Rollar rollar)
        {
            if (ModelState.IsValid)
            {
                db.Rollar.Add(rollar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rollar);
        }

        // GET: /Rollar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rollar rollar = db.Rollar.Find(id);
            if (rollar == null)
            {
                return HttpNotFound();
            }
            return View(rollar);
        }

        // POST: /Rollar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nomi")] Rollar rollar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rollar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rollar);
        }

        // GET: /Rollar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rollar rollar = db.Rollar.Find(id);
            if (rollar == null)
            {
                return HttpNotFound();
            }
            return View(rollar);
        }

        // POST: /Rollar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rollar rollar = db.Rollar.Find(id);
            db.Rollar.Remove(rollar);
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
