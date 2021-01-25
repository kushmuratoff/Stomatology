using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication_Stomatologiya.Models;
namespace WebApplication_Stomatologiya.Models
{
    public class BazaContext:DbContext
    {
        public DbSet<Viloyat> Viloyat { get; set; }
        public DbSet<Tuman> Tuman { get; set; }
        public DbSet<Rollar> Rollar { get; set; }
        public DbSet<Userlar> Userlar { get; set; }
        public DbSet<Stomatologiya> Stomatologiya { get; set; }
        public DbSet<Doktor> Doktor { get; set; }
        public DbSet<Bemor> Bemor { get; set; }
        public DbSet<Korik> Korik { get; set; }
        public DbSet<Xulosa> Xulosa { get; set; }
        public DbSet<Tish> Tish { get; set; }
        public DbSet<Kun> Kun { get; set; }
        public DbSet<IshVaqti> IshVaqti { get; set; }
        public DbSet<DoktorVaqti> DoktorVaqti { get; set; }
        public DbSet<BemorgaXabar> BemorgaXabar { get; set; }
        public DbSet<Yangilik> Yangilik { get; set; }
        public DbSet<Bot> Bot { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
        
    }
    public class BazaInit : CreateDatabaseIfNotExists<BazaContext>
    {
        protected override void Seed(BazaContext context)
        {
            base.Seed(context);
            context.Viloyat.Add(new Viloyat { Nomi = "Samarqand" });
            context.SaveChanges();
        }
    }
}