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
    public class IshVaqtiController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /IshVaqti/
        public ActionResult Index(int? Id)
        {
            Sahifa s = new Sahifa();

            var ishvaqti = db.IshVaqti.Include(i => i.Doktor).Include(i => i.Kun).Where(d=>d.Doktor.StomatologiyaId==Id).OrderBy(d=>d.Doktor.Familya);
            return View(ishvaqti.ToList());
        }
        public ActionResult Vaqtlar(int? Id)
        {
            ViewBag.Id = Id;
            var ishvaqti = db.IshVaqti.Include(i => i.Doktor).Include(i => i.Kun).Where(d => d.DoktorId == Id).OrderBy(d => d.Doktor.Familya);
            return View(ishvaqti.ToList());
        }
        public ActionResult YangiVaqt(int? Id)
        {
            Sahifa s = new Sahifa();
            ViewBag.sel = db.Kun.ToList();
            s.Kun = db.Kun.ToList();
            ViewBag.DokId = Id;
            return View(s);
        }
        [HttpPost]
        public ActionResult YangiVaqt(int? KunId,int?DoktorId,string KelishV,string KetishV)
        {
            IshVaqti iv = new IshVaqti();
            iv.DoktorId = DoktorId;
            iv.KunId = KunId;
            iv.KelishV = KelishV;
            iv.KetishV = KetishV;
            db.IshVaqti.Add(iv);
            db.SaveChanges();

            return RedirectToAction("Vaqtlar", "IshVaqti", new { Id = DoktorId });
        }
        public ActionResult Change(int ?Id,int?KunId)
        {
            ViewBag.kelv = db.IshVaqti.Where(d => d.KunId == KunId && d.DoktorId == Id).FirstOrDefault().KelishV;
            ViewBag.ketv = db.IshVaqti.Where(d => d.KunId == KunId && d.DoktorId == Id).FirstOrDefault().KetishV;

            ViewBag.Id = Id;
            ViewBag.KunId = KunId;

            return View();
        }
        [HttpPost]
        public ActionResult Change(int?KunId,int? DoktorId, string KelishV, string KetishV)
        {
            var idd = db.IshVaqti.Where(d => d.KunId == KunId && d.DoktorId == DoktorId).FirstOrDefault().Id;
            IshVaqti dv = db.IshVaqti.Find(idd);
            dv.KetishV = KetishV;
            dv.KelishV = KelishV;
            db.SaveChanges();
            return RedirectToAction("Vaqtlar", "IshVaqti", new { Id = DoktorId });
        }

        // GET: /IshVaqti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IshVaqti ishvaqti = db.IshVaqti.Find(id);
            if (ishvaqti == null)
            {
                return HttpNotFound();
            }
            return View(ishvaqti);
        }

        // GET: /IshVaqti/Create
        public ActionResult Create()
        {
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya");
            ViewBag.KunId = new SelectList(db.Kun, "Id", "Nomi");
            return View();
        }

        // POST: /IshVaqti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,KunId,DoktorId,KelishV,KetishV")] IshVaqti ishvaqti)
        {
            if (ModelState.IsValid)
            {
                db.IshVaqti.Add(ishvaqti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya", ishvaqti.DoktorId);
            ViewBag.KunId = new SelectList(db.Kun, "Id", "Nomi", ishvaqti.KunId);
            return View(ishvaqti);
        }

        // GET: /IshVaqti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IshVaqti ishvaqti = db.IshVaqti.Find(id);
            if (ishvaqti == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya", ishvaqti.DoktorId);
            ViewBag.KunId = new SelectList(db.Kun, "Id", "Nomi", ishvaqti.KunId);
            return View(ishvaqti);
        }

        // POST: /IshVaqti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,KunId,DoktorId,KelishV,KetishV")] IshVaqti ishvaqti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ishvaqti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoktorId = new SelectList(db.Doktor, "Id", "Familya", ishvaqti.DoktorId);
            ViewBag.KunId = new SelectList(db.Kun, "Id", "Nomi", ishvaqti.KunId);
            return View(ishvaqti);
        }

        // GET: /IshVaqti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IshVaqti ishvaqti = db.IshVaqti.Find(id);
            if (ishvaqti == null)
            {
                return HttpNotFound();
            }
            return View(ishvaqti);
        }

        // POST: /IshVaqti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IshVaqti ishvaqti = db.IshVaqti.Find(id);
            db.IshVaqti.Remove(ishvaqti);
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
