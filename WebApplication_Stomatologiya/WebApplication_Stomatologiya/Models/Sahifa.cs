using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Stomatologiya.Models
{
    public class Sahifa
    {
        public List<Stomatologiya> Stomatologiya { get; set; }
        public List<Doktor> Doktor { get; set; }
        public List<Bemor> Bemor { get; set; }
        public List<Tish> Tish { get; set; }
        public List<DoktorVaqti> DoktorVaqti { get; set; }
        public List<IshVaqti> IshVaqti { get; set; }
        public List<Kun> Kun { get; set; }
        public List<Korik> Korik { get; set; }
        public List<BemorgaXabar> BemorgaXabar { get; set; }
        public List<Tuman> Tuman { get; set; }
        public List<Yangilik> Yangilik { get; set; }




    }
}