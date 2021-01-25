using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebApplication_Stomatologiya.Models;
using System.Data.Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace WebApplication_Stomatologiya.Controllers
{
    public class AdminController : Controller
    {
        BazaContext db = new BazaContext();
        //
        // GET: /Admin/
        public ActionResult Index(int Id)
        {
            Sahifa s = new Sahifa();
            ViewBag.Id = Id;
            s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(st => st.Id == Id).ToList();
            var stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(st => st.Id == Id);
            return View(stomatologiya.ToList());
        }
        public ActionResult DokorIshV(int? Id)
        {
            Sahifa s = new Sahifa();
           // s.Doktor = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
           // s.DoktorVaqti = db.DoktorVaqti.Where(d => d.Doktor.StomatologiyaId == Id).ToList();
            s.IshVaqti = db.IshVaqti.Include(i => i.Kun).Include(i => i.Doktor).Where(i => i.DoktorId == Id).ToList();

            return View(s);
        }
        public ActionResult Sozlamalar(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Doktor = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
            var doktorlar = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
            s.Stomatologiya = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(sto => sto.Id == Id).ToList();
            return View(s);
        }
        [HttpPost]
        
        public ActionResult Sozlamalar(int? Id, string Nomi, string Manzil, string TelN, string Login, string Parol, int UsId)
        {
            Stomatologiya stom = new Stomatologiya();
            stom = db.Stomatologiya.Include(st => st.Tuman).Include(st => st.Userlar).Where(s => s.Id == Id).FirstOrDefault();
            Userlar us = new Userlar();
            us = db.Userlar.Where(u => u.Id == UsId).FirstOrDefault();
            us.Logini = Login;
            us.Paroli = Parol;
            db.SaveChanges();
            stom.Nomi = Nomi;
            stom.Manzil = Manzil;
            stom.TelNomer = TelN;
            db.SaveChanges();
            return RedirectToAction("Index", "Admin", new { Id = Id });
        }
        public ActionResult Doktorlar(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Doktor = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
            var doktorlar = db.Doktor.Where(d => d.StomatologiyaId == Id).ToList();
            return View(s);
        }
        public ActionResult Bemorlar(int? Id)
        {
            Sahifa s = new Sahifa();
            s.DoktorVaqti = db.DoktorVaqti.Include(d => d.Bemor).Where(d => d.DoktorId == Id && d.holati==1).OrderBy(s1=>s1.Sanasi).ToList();
            return View(s);
        }
        public ActionResult BemorlarCon(int? Id)
        {
            Sahifa s = new Sahifa();
            s.DoktorVaqti = db.DoktorVaqti.Include(d => d.Bemor).Where(d => d.Doktor.StomatologiyaId == Id && d.holati == 1).ToList();
            return View(s);
        }
        public ActionResult Xisobot(int? Id)
        {
            Sahifa s = new Sahifa();
            s.DoktorVaqti = db.DoktorVaqti.Include(d => d.Bemor).Include(d => d.Doktor).Where(d => d.Doktor.StomatologiyaId == Id && d.holati == 3).ToList();
            return View(s);
        }
        public ActionResult Tulovlar(int?Id)
        {
            ViewBag.StoId = Id;
            Sahifa s = new Sahifa();
            s.Korik = db.Korik.Include(d => d.Doktor).Include(d => d.Bemor).Where(d=>d.Doktor.StomatologiyaId==Id).ToList();
            var tgan = db.Korik.Include(d => d.Doktor).Include(d => d.Bemor).Where(d => d.Doktor.StomatologiyaId == Id && d.Holat==1).ToList();
            var tmagan = db.Korik.Include(d => d.Doktor).Include(d => d.Bemor).Where(d => d.Doktor.StomatologiyaId == Id&& d.Holat==0).ToList();
            int sumt = 0;
            int sumtg = 0;

            foreach (var t in tgan)
            {
                sumt += t.Summa;
            }
            foreach (var t in tmagan)
            {
                sumtg += t.Summa;
            }
            ViewBag.s1 = sumt;
            ViewBag.s2 = sumtg;
            return View(s);
        }
        public ActionResult BemXab (int? Id)
        {
            Korik korik = db.Korik.Find(Id);
            var bemorid = db.Korik.Include(b => b.Bemor).Where(b => b.Id == Id).FirstOrDefault().BemorId;
            var stom = db.Korik.Include(b => b.Doktor.Stomatologiya).Where(b => b.Id == Id).FirstOrDefault().Doktor.Stomatologiya.Nomi;
            Bemor bem = db.Bemor.Find(bemorid);
            BemorgaXabar bx = new BemorgaXabar();
            bx.BemorId = bemorid;
            bx.Holati = 0;
            bx.Matni = "Xurmatli " + bem.Familya + " " + bem.Ism + " " + bem.Sharif + " sizdan " + stom + " mamuriyati "+korik.Summa.ToString()+" so'm miqdordagi to'lov pulini to'lashingizni talab qiladi. Xabar jo'natilgan vaqt("+DateTime.Now.ToString()+")";
            db.BemorgaXabar.Add(bx);
            db.SaveChanges();
            return View();
        }
        public ActionResult TulovT(int?Id,int?StoId)
        {
            Korik k = db.Korik.Find(Id);
            k.Holat = 1;
            db.SaveChanges();
            return RedirectToAction("Tulovlar", "Admin", new { Id = StoId });
        }
        public ActionResult Yakun(int?Did,int? Id)
        {
            DoktorVaqti dv = db.DoktorVaqti.Find(Id);
            dv.holati = 3;
            db.SaveChanges();
            return RedirectToAction("Oyna", "Doktor", new { Id = Did });
        }
        public ActionResult Anketasi(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Korik = db.Korik.Include(k => k.Bemor).Include(k => k.Doktor).Where(k=>k.BemorId==Id).ToList();
           
            return View(s);
        }
        public ActionResult BemorYV(int?Did,int Id)
        {
            ViewBag.Did = Did;
            ViewBag.Id = Id;

            return View();
        }
        public ActionResult QabulVaqtiD(int? Did, int?Id,DateTime? vaqt)
        {
            string kun = vaqt.Value.DayOfWeek.ToString();
            ViewBag.vaqt = vaqt;
            ViewBag.Did = Did;
            ViewBag.Id = Id;

            int haftakuni = 0;
            switch (kun)
            {
                case "Monday": haftakuni = 1; break;
                case "Tuesday": haftakuni = 2; break;
                case "Wednesday": haftakuni = 3; break;
                case "Thursday": haftakuni = 4; break;
                case "Friday": haftakuni = 5; break;
                case "Saturday": haftakuni = 6; break;
                case "Sunday": haftakuni = 7; break;

            }

            string kelish = db.IshVaqti.Include(i => i.Doktor).Include(i => i.Kun).Where(i => i.DoktorId == Did && i.KunId == haftakuni).FirstOrDefault().KelishV;
            string ketish = db.IshVaqti.Include(i => i.Doktor).Include(i => i.Kun).Where(i => i.DoktorId == Did && i.KunId == haftakuni).FirstOrDefault().KetishV;
            ViewBag.kel = kelish;
            ViewBag.ket = ketish;
            int soat = 0; int min = 0;
            int a = Convert.ToInt16(kelish);
            int b = Convert.ToInt16(ketish);
            string summ = "";
            for (int i = a; i < b; i++)
            {
                for (int j = 0; j < 60; j = j + 30)
                {
                    string sss = i.ToString() + "." + j.ToString();
                    int borligi = 0;
                    borligi = db.DoktorVaqti.Where(d => d.Sanasi == vaqt && d.DoktorId == Did && string.Equals(d.vaqti, sss)).Count();
                    if (borligi == 0)
                    {
                        summ += i.ToString() + "." + j.ToString() + " ";
                    }


                }
            }
            ViewBag.sum = summ;
            ViewBag.kuni = haftakuni;
            string summ1 = "";

            return View();
        }
        public ActionResult VaqtSaqlash(int? Did,int?Id, DateTime? sana, string vaqti)
        {
            DoktorVaqti dvv = db.DoktorVaqti.Find(Id);
            dvv.holati = 2;
            db.SaveChanges();
            ViewBag.vaqt = vaqti;
            DoktorVaqti dv = new DoktorVaqti();
            dv.DoktorId = Did;
            var idd = db.DoktorVaqti.Where(i=>i.Id==Id).FirstOrDefault().BemorId;

            dv.BemorId = db.Bemor.Where(b => b.Id == idd).FirstOrDefault().Id;
            dv.Sanasi = sana;
            dv.vaqti = vaqti;
            dv.holati = 1;
            db.DoktorVaqti.Add(dv);
            db.SaveChanges();
            return View();
        }
        public ActionResult Pfdga(int?Id)
        {
            //var bem = db.Bemor.Where(d1 => d1.Id == Bid).FirstOrDefault();
            //Korik k3 = db.Korik.Find(id);
            //Doktor d = new Doktor();
            //d = db.Doktor.Where(f => f.Id == k3.DoktorId).FirstOrDefault();
            Bemor bem = new Bemor();
            bem = db.Bemor.Where(f => f.Id == Id).FirstOrDefault();

            var korik1 = db.Korik.Include(k1 => k1.Bemor).Include(k1 => k1.Doktor).Where(b => b.BemorId == Id).ToList();
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            Chunk chunk = new Chunk("ANKETA", FontFactory.GetFont("Arial", 30, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(new Paragraph(chunk));
            foreach( var k in korik1)
            {
                chunk = new Chunk("Doktor:" +k.Doktor.Familya + " " + k.Doktor.Ism + " " + k.Doktor.Sharif, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Bemor:" + k.Bemor.Familya + " " + k.Bemor.Ism + " " + k.Bemor.Sharif, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Tashxis:" + k.Tashxis, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Shikoyat:" + k.Shikoyat, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Kasallik rivojlanishi:" + k.KasRivoj, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Labaratoriya natijasi:" + k.LabNatija, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Tishlash:" + k.Tishlash, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Og'iz bo'shligi, milk, tanglay shilliq pardasining holati:" + k.OgizBosh, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                chunk = new Chunk("Davolanish narxi:" + k.Summa + " so'm", FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
               
                chunk = new Chunk("Davolangan kunlari:", FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                var dkun = db.DoktorVaqti.Where(dd => dd.BemorId == k.Bemor.Id && dd.holati == 2 &&dd.DoktorId == k.DoktorId).ToList();
                foreach (var dok in dkun)
                {
                    chunk = new Chunk("      " + dok.Sanasi.Value.Day.ToString() + "-" + dok.Sanasi.Value.Month.ToString() + "-" + dok.Sanasi.Value.Year.ToString() + "  " + dok.vaqti, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                    pdfDoc.Add(new Paragraph(chunk));
                }
                chunk = new Chunk("Davolanish tugagan sana:", FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                pdfDoc.Add(new Paragraph(chunk));
                int bor = db.DoktorVaqti.Where(dd => dd.BemorId == k.Bemor.Id && dd.holati == 3 && dd.DoktorId == k.DoktorId).Count();
                if (bor > 0)
                {
                    var endd = db.DoktorVaqti.Where(dd => dd.BemorId == k.Bemor.Id && dd.holati == 2).FirstOrDefault();
                    chunk = new Chunk("      " + endd.Sanasi.Value.Day.ToString() + "-" + endd.Sanasi.Value.Month.ToString() + "-" + endd.Sanasi.Value.Year.ToString() + "  " + endd.vaqti, FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                    pdfDoc.Add(new Paragraph(chunk));
                }
                else
                {
                    chunk = new Chunk("    -   ", FontFactory.GetFont("Arial", 15, Font.BOLD, BaseColor.BLACK));
                    pdfDoc.Add(new Paragraph(chunk));

                }

            }
            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+"Anketa"+bem.Familya+DateTime.Now.Minute.ToString()+".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
            return View();

           
        }
        public ActionResult Register(int? Id)
        {
            ViewBag.Id = Id;
            ViewBag.habar = "";
            return View();
        }
        [HttpPost]
        public ActionResult Register(int? Id, string Login, string Parol)
        {

            Userlar user = null;
            ViewBag.Id = Id;
            user = db.Userlar.Where(u => u.Logini == Login).FirstOrDefault();
            if (user != null)
            {
                ViewBag.habar = "Bunday loginli doktor mavjud";
                return View();
            }
            else
            {
                return RedirectToAction("Saqlash", "Admin", new { Login = Login, Parol = Parol, Id = Id });
            }

        }
        public ActionResult Saqlash(string Login, string Parol, int? Id)
        {
            // ViewBag.Sto = db.Stomatologiya.ToList();
            ViewBag.Login = Login;
            ViewBag.Parol = Parol;
            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public ActionResult Saqlash(string Login, string Parol, string Fam, string Ism, string Shar, int? StoId, string Manzil, string PasS, string PasN, string tel, string Ktb, string eskiK, DateTime tugyil, HttpPostedFileBase Imagefile)
        {
            Userlar user = new Userlar();
            user.Logini = Login;
            user.Paroli = Parol;
            // user.XaridorId = db.Xaridor.Where(x => x.Id == idsi).FirstOrDefault().Id;
            user.RollarId = 1;
            db.Userlar.Add(user);
            db.SaveChanges();
            int Idd = db.Userlar.Where(u => u.Logini == Login).FirstOrDefault().Id;
            Doktor doktor = new Doktor();
            doktor.Familya = Fam;
            doktor.Ism = Ism;
            doktor.Sharif = Shar;
            doktor.TugVaqti = tugyil;
            doktor.PasSeria = PasS;
            doktor.PasNomer = PasN;
            doktor.KimTomBer = Ktb;
            doktor.YashManzil = Manzil;
            doktor.TelNomer = tel;
            //doktor.EskiKasallari = eskiK;
            doktor.StomatologiyaId = StoId;
            doktor.UserlarId = Idd;

            if (Imagefile != null)
            {
                string path = Server.MapPath("~/Rasmlar/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = Path.GetFileName(Imagefile.FileName);
                Imagefile.SaveAs(path + Path.GetFileName(Imagefile.FileName));
                doktor.Rasm = filename;
            }
            db.Doktor.Add(doktor);
            db.SaveChanges();
            // var Id = db.doktor.Where(b => b.UserlarId == Idd).FirstOrDefault().Id;
            return RedirectToAction("Index", "Admin", new { Id = StoId });
        }
       public ActionResult Sto()
        {
            Sahifa s = new Sahifa();
            s.Stomatologiya = db.Stomatologiya.ToList();
            return View(s);
        }
        public JsonResult getFan(int id)
        {
            return Json(db.Doktor.Where(y => y.StomatologiyaId == id));
        }
	}
}