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
    public class BotController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Bot/
        public ActionResult Index()
        {
            return View(db.Bot.ToList());
        }

        // GET: /Bot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bot bot = db.Bot.Find(id);
            if (bot == null)
            {
                return HttpNotFound();
            }
            return View(bot);
        }

        // GET: /Bot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Bot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,BotId,StatusId,UserId")] Bot bot)
        {
            if (ModelState.IsValid)
            {
                db.Bot.Add(bot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bot);
        }

        // GET: /Bot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bot bot = db.Bot.Find(id);
            if (bot == null)
            {
                return HttpNotFound();
            }
            return View(bot);
        }

        // POST: /Bot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,BotId,StatusId,UserId")] Bot bot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bot);
        }

        // GET: /Bot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bot bot = db.Bot.Find(id);
            if (bot == null)
            {
                return HttpNotFound();
            }
            return View(bot);
        }

        // POST: /Bot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bot bot = db.Bot.Find(id);
            db.Bot.Remove(bot);
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
