using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication_Stomatologiya.Models;
using System.IO;

namespace WebApplication_Stomatologiya.Controllers
{
    public class YangilikController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Yangilik/
        public ActionResult Index()
        {
            var yangilik = db.Yangilik.Include(y => y.Stomatologiya);
            return View(yangilik.ToList());
        }
        public ActionResult Umumiy(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Yangilik = db.Yangilik.Where(d => d.StomatologiyaId == Id).ToList();
           
            return View(s);
        }
        public ActionResult Saqlash(int?Id)
        {
            Sahifa s = new Sahifa();
            s.Tuman = db.Tuman.ToList();
            ViewBag.Id = Id;
            //ViewBag.Parol = Parol;
            return View(s);
        }
        [HttpPost]
        public ActionResult Saqlash(string Mavzu, string Batafsil, int? StoId, HttpPostedFileBase Imagefile)
        {
            Yangilik yan = new Yangilik();
            yan.StomatologiyaId = StoId;
            yan.Mavzu = Mavzu;
            yan.Batafsil = Batafsil;
            yan.Vaqti = DateTime.Now;
            yan.Holati = 0;
            if (Imagefile != null)
            {
                string path = Server.MapPath("~/Rasmlar/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = Path.GetFileName(Imagefile.FileName);
                Imagefile.SaveAs(path + Path.GetFileName(Imagefile.FileName));
                yan.Rasm = filename;
            }
            db.Yangilik.Add(yan);
            db.SaveChanges();
            // var Id = db.doktor.Where(b => b.UserlarId == Idd).FirstOrDefault().Id;
            return RedirectToAction("Umumiy", "Yangilik", new {Id=StoId });
        }
        public ActionResult Remove(int? Id)
        {
            Yangilik yangilik = db.Yangilik.Find(Id);
            var id = yangilik.StomatologiyaId;
            db.Yangilik.Remove(yangilik);
            db.SaveChanges();
            return RedirectToAction("Umumiy", "Yangilik", new { Id=id});
        }

        // GET: /Yangilik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yangilik yangilik = db.Yangilik.Find(id);
            if (yangilik == null)
            {
                return HttpNotFound();
            }
            return View(yangilik);
        }

        // GET: /Yangilik/Create
        public ActionResult Create()
        {
            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi");
            return View();
        }

        // POST: /Yangilik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,StomatologiyaId,Rasm,Mavzu,Batafsil,Vaqti,Holati")] Yangilik yangilik)
        {
            if (ModelState.IsValid)
            {
                db.Yangilik.Add(yangilik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi", yangilik.StomatologiyaId);
            return View(yangilik);
        }

        // GET: /Yangilik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yangilik yangilik = db.Yangilik.Find(id);
            if (yangilik == null)
            {
                return HttpNotFound();
            }
            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi", yangilik.StomatologiyaId);
            return View(yangilik);
        }

        // POST: /Yangilik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,StomatologiyaId,Rasm,Mavzu,Batafsil,Vaqti,Holati")] Yangilik yangilik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yangilik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StomatologiyaId = new SelectList(db.Stomatologiya, "Id", "Nomi", yangilik.StomatologiyaId);
            return View(yangilik);
        }

        // GET: /Yangilik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yangilik yangilik = db.Yangilik.Find(id);
            if (yangilik == null)
            {
                return HttpNotFound();
            }
            return View(yangilik);
        }

        // POST: /Yangilik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yangilik yangilik = db.Yangilik.Find(id);
            db.Yangilik.Remove(yangilik);
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
