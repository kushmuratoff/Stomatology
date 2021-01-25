//brr
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Stomatologiya.Models
{
    public class Viloyat
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public ICollection<Tuman> Tumanlar { get; set; }
    }
    public class Tuman
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public int? ViloyatId { get; set; }
        public Viloyat Viloyat { get; set; }
        public ICollection<Stomatologiya> Stomatologiyalar { get; set; }

    }
    public class Rollar
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public ICollection<Userlar> Userlarlar { get; set; }
    }
    public class Userlar
    {
        public int Id { get; set; }
        public string Logini { get; set; }
        public string Paroli { get; set; }
        public int? RollarId { get; set; }
        public Rollar Rollar { get; set; }
        public ICollection<Stomatologiya> Stomatologiyalar { get; set; }
        public ICollection<Doktor> Doktorlar { get; set; }
        public ICollection<Bemor> Bemorlar { get; set; }
    }
    public class Stomatologiya
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public string Logatip { get; set; }
        public int? TumanId { get; set; }
        public Tuman Tuman { get; set; }
        public string Manzil { get; set; }
        public string TelNomer { get; set; }
        public int? UserlarId { get; set; }
        public Userlar Userlar { get; set; }
        public ICollection<Doktor> Doktorlar { get; set; }
        public ICollection<Yangilik> Yangiliklar { get; set; }
       
    }
    public class Doktor
    {
        public int Id { get; set; }
        public string Familya { get; set; }
        public string Ism { get; set; }
        public string Sharif { get; set; }       
        public DateTime TugVaqti { get; set; }
        public string PasSeria { get; set; }
        public string PasNomer { get; set; }
        public string KimTomBer { get; set; }
        public string Rasm { get; set; }
        public string YashManzil { get; set; }
        public string TelNomer { get; set; }
        public int? UserlarId { get; set; }
        public Userlar Userlar { get; set; }
        public int? StomatologiyaId { get; set; }
        public Stomatologiya Stomatologiya { get; set; }
        public ICollection<Korik> Koriklar { get; set; }
        public ICollection<IshVaqti> IshVaqtilar { get; set; }
        public ICollection<DoktorVaqti> DoktorVaqtilar { get; set; }



    }
    public class Bemor
    {
        public int Id { get; set; }
        public string Familya { get; set; }
        public string Ism { get; set; }
        public string Sharif { get; set; }
        public DateTime TugVaqti { get; set; }
        public string PasSeria { get; set; }
        public string PasNomer { get; set; }
        public string KimTomBer { get; set; }
        public string YashManzil { get; set; }
        public string TelNomer { get; set; }
        public string EskiKasallari { get; set; }
        public int? UserlarId { get; set; }
        public Userlar Userlar { get; set; }
        public ICollection<Korik> Koriklar { get; set; }
        public ICollection<DoktorVaqti> DoktorVaqtilar { get; set; }
        public ICollection<BemorgaXabar> BemorgaXabarlar { get; set; }
    }
    public class Korik
    {
        public int Id { get; set; }
        public int? DoktorId { get; set; }
        public Doktor Doktor { get; set; }
        public int? BemorId { get; set; }
        public Bemor Bemor { get; set; }
        public string Tashxis { get; set; }
        public string Shikoyat { get; set; }
        public string KasRivoj { get; set; }
        public string LabNatija { get; set; }
        public string Tishlash { get; set; }
        public string OgizBosh { get; set; }
        public int Summa { get; set; }
        public int Holat { get; set; }
        public ICollection<Xulosa> Xulosalar { get; set; }
    }
    public class Xulosa
    {
        public int Id { get; set; }
        public int? KorikId { get; set; }
        public Korik Korik { get; set; }
        public int Summa { get; set; }
        public DateTime? Vaqt { get; set; }
    }
    public class Tish
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public string Malumot { get; set; }
    }
    public class Kun
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public ICollection<IshVaqti> IshVaqtilar { get; set; }
    }
    public class IshVaqti
    {
        public int Id { get; set; }
        public int? KunId { get; set; }
        public Kun Kun { get; set; }
        public int? DoktorId { get; set; }
        public Doktor Doktor { get; set; }
        public string KelishV { get; set; }
        public string KetishV { get; set; }
    }
    public class DoktorVaqti
    {
        public int Id { get; set; }
        public int? DoktorId { get; set; }
        public Doktor Doktor { get; set; }
        public int? BemorId { get; set; }
        public Bemor Bemor { get; set; }
        public DateTime? Sanasi { get; set; }
        public string vaqti { get; set; }
        public int holati { get; set; }
    }
    public class BemorgaXabar
    {
        public int Id { get; set; }
        public int? BemorId { get; set; }
        public Bemor Bemor { get; set; }
        public int Holati { get; set; }
        public string Matni { get; set; }
    }
    public class Yangilik
    {
        public int Id { get; set; }
        public int? StomatologiyaId { get; set; }
        public Stomatologiya Stomatologiya { get; set; }
        public string Rasm { get; set; }
        public string Mavzu { get; set; }
        public string Batafsil { get; set; }
        public DateTime? Vaqti { get; set; }
        public int Holati { get; set; }
    }
    public class Bot
    {
        public int Id { get; set; }
        public string BotId { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
    }
}