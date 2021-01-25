using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;
using System.IO;
using System.Data;
using System.Globalization;
using System.Threading;
namespace tg_bot
{
    static class DB
    {
        public static cSQL db { get; set; }
    }
    class Program
    {

        //870087205:AAGn0reGr2JffDZ_Ze4XQXefuZnjN4i9lrg   uzmu
        //829316573:AAFqMO3l_GgHjeenALzmDdFZy8jvb5an2uA
        //797548406:AAEii29fFm7w2YbgcSEXUIu5aPJ23X7onb8 nMUN
        //730423484:AAHGrRqB_fot1SvOKDVXuIJOaSVIqhIprbY stomat
        public static readonly TelegramBotClient bot = new TelegramBotClient("730423484:AAHGrRqB_fot1SvOKDVXuIJOaSVIqhIprbY");
        //static 
        static ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup();
        static ReplyKeyboardMarkup markup1 = new ReplyKeyboardMarkup();
        public string xabar="";
       static InlineKeyboardMarkup inline = new InlineKeyboardMarkup();       
        static void Main(string[] args)
       {
           
            try
            {
               // Console.WriteLine("қ");
                string joyi = "";
                StreamReader uqish = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "//Baza.txt");
                while (uqish.Peek() >= 0)
                {
                    joyi += uqish.ReadLine();
                }
                uqish.Close();
                 DB.db = new cSQL();
                 DB.db.cSQL_init(joyi);
                DB.db.Connect();
               }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            bot.StartReceiving();
         bot.OnMessage += bot_x;
          Timer t = new Timer(TimerCallback, null, 0,2000 );
          Console.ReadLine();
            Console.ReadLine();
        }
        private static void TimerCallback(Object o)
        {
            // Display the date/time when this method got called.
            //Console.WriteLine("In TimerCallback: " + DateTime.Now);
            // Force a garbage collection to occur for this demo.
            //GC.Collect();
          //  bot.SendTextMessageAsync("789645375", "sasas", ParseMode.Markdown);

        }
         private static async void bot_x(object sender,Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                var txt = e.Message.Text;
                Console.WriteLine(e.Message.Chat.Id);
                DataTable bt = DB.db.Query("select * from Bot where BotId=" + e.Message.Chat.Id);
                int borligi = bt.Rows.Count;
                Console.WriteLine(bt.Rows.Count);
               // var chatid = e.Message.Chat.Id;
                if(txt!=null)
                {
                    if (txt == "Chiqish")
                    {
                        string habar = "Stomatologiya.uz saytining rasmiy botiga xush kelibsiz qo'llanma uchun 'Yordam' tugmasini bosing ";
                        if (borligi != 0)
                        {
                            string zapros = "";
                            zapros += "delete from Bot where BotId='" + e.Message.Chat.Id.ToString() + "'";
                            if (DB.db.SetCommand(zapros) == 1)
                            {
                                Console.WriteLine("o'chirildi");
                            }
                        }
                        markup.ResizeKeyboard = true;
                        markup.Keyboard = new KeyboardButton[][]
                     {              
                    
                     new KeyboardButton[]
                    {
                       
                         new KeyboardButton("Yordam")

                       
                     }
                };
                        Console.WriteLine(habar);
                        habar = "``` " + habar + " ```";
                        await bot.SendTextMessageAsync(e.Message.Chat.Id, habar, ParseMode.Markdown, false, false, 0, markup);


                    }
                    if (txt == "Yordam")
                    {
                       
                        string Idbot = e.Message.Chat.Id.ToString();
                        Console.WriteLine(" Yordam knopka bosildi  " + Idbot);
                        string habar = "Siz bu botdan foydalanish uchun saytdan foydalanish uchun registratsiyadan o'tgan loginingizni kiritishingiz kerak!!!";
                        habar = "``` " + habar + " ```";
                        await bot.SendTextMessageAsync(e.Message.Chat.Id, habar, ParseMode.Markdown);

                    }                  
                 
                    else if (txt == "/start")
                    {
                        string habar = "Stomatologiya.uz saytining rasmiy botiga xush kelibsiz qo'llanma uchun 'Yordam' tugmasini bosing ";
                        if (borligi != 0)
                        {
                            string zapros = "";
                            zapros += "delete from Bot where BotId='" + e.Message.Chat.Id.ToString()+"'";
                            if (DB.db.SetCommand(zapros) == 1)
                            {
                                Console.WriteLine("o'chirildi");
                            }
                        }
                        markup.ResizeKeyboard = true;
                        markup.Keyboard = new KeyboardButton[][]
                     {              
                    
                     new KeyboardButton[]
                    {
                       
                         new KeyboardButton("Yordam")

                       
                     }
                };
                        Console.WriteLine(habar);
                        habar = "``` " + habar + " ```";
                        await bot.SendTextMessageAsync(e.Message.Chat.Id, habar, ParseMode.Markdown, false, false, 0, markup);


                    }
                    else if(borligi==0)
                    {
                        string zapros = "";
                        zapros += "select * from Userlar where Logini='" + txt.ToString()+"'";
                        DataTable dt = DB.db.Query(zapros);
                        if(dt.Rows.Count>0)
                        {

                            int userid = Convert.ToInt32(dt.Rows[0]["Id"]);
                        int rolid = Convert.ToInt32(dt.Rows[0]["RollarId"]);
                        switch(rolid)
                        {
                            case 1: {
                                DataTable dokid = DB.db.Query("select * from Doktor where UserlarId=" + userid);
                                int dokidd = Convert.ToInt32(dokid.Rows[0]["Id"]);
                                Console.WriteLine("doktrr id si " + dokidd.ToString());
                                string zap = "";
                                zap += "Insert into Bot(BotId,StatusId,UserId) values('"+e.Message.Chat.Id+"',"+1+","+dokidd+")";
                              if(DB.db.SetCommand(zap)==1)
                              {
                                  Console.WriteLine("Doktor qo'shildi");
                              }
                              DataTable yanbem = DB.db.Query("select * from DoktorVaqti where holati=0 and DoktorId=" + dokidd);
                              markup.ResizeKeyboard = true;
                              markup.Keyboard = new KeyboardButton[][]
                                       {              
                    
                                    new KeyboardButton[]
                                       {                      
                                      new KeyboardButton("Bugungi bemorlar")                       
                                           },
                                    new KeyboardButton[]
                                      {                      
                                      new KeyboardButton("Yangi bemorlar")                       
                                            }
                                            ,
                                    new KeyboardButton[]
                                      {                      
                                      new KeyboardButton("Davolanayotgan bemorlar")                       
                                            }
                                             ,
                                    new KeyboardButton[]
                                      {                      
                                      new KeyboardButton("Chiqish")                       
                                            }
                                        };
                              string ism = "";
                              ism += dokid.Rows[0]["Familya"].ToString() + "  " + dokid.Rows[0]["Ism"].ToString() + "  " + dokid.Rows[0]["Sharif"].ToString();
                                    await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` Salom "+ism+" ```", ParseMode.Markdown, false, false, 0, markup);
                      
                            }; break;
                            case 2: { await bot.SendTextMessageAsync(e.Message.Chat.Id, "Salom Admin", ParseMode.Markdown); }; break;                           
                            case 3: {
                                DataTable bemid = DB.db.Query("select * from Bemor where UserlarId=" + userid);
                                int bemidd = Convert.ToInt32(bemid.Rows[0]["Id"]);
                                Console.WriteLine("doktrr id si " + bemidd.ToString());
                                string zap = "";
                                zap += "Insert into Bot(BotId,StatusId,UserId) values('" + e.Message.Chat.Id + "'," + 3 + "," + bemidd + ")";
                                if (DB.db.SetCommand(zap) == 1)
                                {
                                    Console.WriteLine("Doktor qo'shildi");
                                }

                                markup.ResizeKeyboard = true;
                                markup.Keyboard = new KeyboardButton[][]
                                       {              
                    
                                    new KeyboardButton[]
                                       {                      
                                      new KeyboardButton("Stomatologiyalar")                       
                                           },
                                    new KeyboardButton[]
                                      {                      
                                      new KeyboardButton("Doktorlar")                       
                                            }
                                            ,
                                    new KeyboardButton[]
                                      {                      
                                      new KeyboardButton("Xabar") ,
                                      new KeyboardButton("Anketa")                  
                      
                                            }
                                            ,
                                    new KeyboardButton[]
                                      {                      
                                      new KeyboardButton("Chiqish")                
                      
                                            }
                                        };
                                string ism = "";
                                ism += bemid.Rows[0]["Familya"].ToString() + "  " + bemid.Rows[0]["Ism"].ToString() + "  " + bemid.Rows[0]["Sharif"].ToString();
                                await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` Salom " + ism + " ```", ParseMode.Markdown, false, false, 0, markup);
                      
                            }; break;
                        }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, "Loginingiz hato kitirilgan iltimos etiborli bo'ling", ParseMode.Markdown);

                        }
                    }
                    if (txt == "Davolanayotgan bemorlar")
                    {
                        DataTable dokid = DB.db.Query("select * from Bot where StatusId=1 and BotId='" + e.Message.Chat.Id.ToString() + "'");
                        DataTable yanbem = DB.db.Query("select * from DoktorVaqti where holati=1 and DoktorId=" + Convert.ToInt32(dokid.Rows[0]["UserId"]));
                        for (int i = 0; i < yanbem.Rows.Count; i++)
                        {
                            DataTable bemor = DB.db.Query("select * from Bemor where Id=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]));
                            string ism = "";
                            ism += bemor.Rows[0]["Familya"].ToString() + " ";
                            ism += bemor.Rows[0]["Ism"].ToString() + " ";
                            ism += bemor.Rows[0]["Sharif"].ToString() + " ";

                            string vaqtcha = yanbem.Rows[i]["Sanasi"].ToString();
                            int joyi = 0;
                            for (int j = 0; j < vaqtcha.Length; j++)
                            {
                                if (vaqtcha[j] == ' ')
                                { joyi = j; }
                            }
                            vaqtcha = vaqtcha.Substring(0, joyi);
                            ism += vaqtcha + " da soat ";
                            ism += yanbem.Rows[i]["vaqti"].ToString() + " da keladi";

                            await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + ism + " ```", ParseMode.Markdown);

                        }
                        if (yanbem.Rows.Count == 0)
                        {
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` Davolanayotgan bemorlar yo'q ```", ParseMode.Markdown);

                        }

                    }
                     if (txt == "Yangi bemorlar")
                    {
                        DataTable dokid = DB.db.Query("select * from Bot where StatusId=1 and BotId='"+e.Message.Chat.Id.ToString()+"'");
                        DataTable yanbem = DB.db.Query("select * from DoktorVaqti where holati=0 and DoktorId=" + Convert.ToInt32(dokid.Rows[0]["UserId"]));
                        for(int i=0;i<yanbem.Rows.Count;i++)
                        {
                            DataTable bemor = DB.db.Query("select * from Bemor where Id=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]));
                            string ism = "";
                            ism += bemor.Rows[0]["Familya"].ToString() + " ";
                            ism += bemor.Rows[0]["Ism"].ToString() + " ";
                            ism += bemor.Rows[0]["Sharif"].ToString() + " ";
                            
                            string vaqtcha = yanbem.Rows[i]["Sanasi"].ToString();
                            int joyi = 0;
                            for (int j = 0; j < vaqtcha.Length;j++ )
                            {
                                if(vaqtcha[j]==' ')
                                { joyi = j; }
                            }
                            vaqtcha = vaqtcha.Substring(0, joyi);
                            ism += vaqtcha + " da soat ";
                            ism += yanbem.Rows[i]["vaqti"].ToString()+ " da keladi";

                                await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + ism + " ```", ParseMode.Markdown);

                        }
                         if(yanbem.Rows.Count==0)
                         {
                              await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` Yangi bemor yo'q ```", ParseMode.Markdown);

                         }

                    }
                     if (txt == "Bugungi bemorlar")
                     {
                         Console.WriteLine("dsdasdsd");
                         DataTable dokid = DB.db.Query("select * from Bot where StatusId=1 and BotId='" + e.Message.Chat.Id.ToString() + "'");
                         int krish = 0;
                         DataTable yanbem = DB.db.Query("select * from DoktorVaqti where (holati=0 or holati=1) and DoktorId=" + Convert.ToInt32(dokid.Rows[0]["UserId"]));
                         for (int i = 0; i < yanbem.Rows.Count; i++)
                         {
                             DateTime vaqt = Convert.ToDateTime(yanbem.Rows[i]["Sanasi"]);
                             Console.WriteLine(vaqt);
                             if(vaqt.Year-DateTime.Now.Year==0&&vaqt.Month-DateTime.Now.Month==0&&vaqt.Day-DateTime.Now.Day==0)
                             {
                                 krish = 1;
                                 DataTable bemor = DB.db.Query("select * from Bemor where Id=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]));
                                 string ism = "";
                                 ism += bemor.Rows[0]["Familya"].ToString() + " ";
                                 ism += bemor.Rows[0]["Ism"].ToString() + " ";
                                 ism += bemor.Rows[0]["Sharif"].ToString() + " ";

                                 ism += yanbem.Rows[i]["vaqti"].ToString() + " da keladi";

                                 await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + ism + " ```", ParseMode.Markdown);

                             }
                            
                            
                         }
                         if (yanbem.Rows.Count == 0 || krish==0)
                         {
                             await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` Bugun davolanadigan bemor yo'q ```", ParseMode.Markdown);

                         }

                     }
                     if (txt == "Xabar")
                     {
                         
                         DataTable bemid = DB.db.Query("select * from Bot where StatusId=3 and BotId='" + e.Message.Chat.Id.ToString() + "'");
                         int krish = 0;
                         DataTable yanbem = DB.db.Query("select * from DoktorVaqti where holati=1 and BemorId=" + Convert.ToInt32(bemid.Rows[0]["UserId"]));
                         for (int i = 0; i < yanbem.Rows.Count; i++)
                         {
                             DateTime vaqt = Convert.ToDateTime(yanbem.Rows[i]["Sanasi"]);
                             Console.WriteLine(vaqt);
                             if (vaqt.Year - DateTime.Now.Year == 0 && vaqt.Month - DateTime.Now.Month == 0 && vaqt.Day - DateTime.Now.Day == 0)
                             {
                                 DataTable doktor = DB.db.Query("select * from Doktor where Id=" + yanbem.Rows[i]["DoktorId"]);
                                 
                                 DataTable stom = DB.db.Query("select * from Stomatologiya where Id=" + doktor.Rows[0]["StomatologiyaId"]);
                                 Console.WriteLine(stom.Rows[0]["Nomi"]);
                                 krish = 1;
                                 DataTable bemor = DB.db.Query("select * from Bemor where Id=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]));
                                 string ism = "";
                                 ism += bemor.Rows[0]["Familya"].ToString() + " ";
                                 ism += bemor.Rows[0]["Ism"].ToString() + " ";
                                 ism += bemor.Rows[0]["Sharif"].ToString() + " bugun soat ";


                                 ism += yanbem.Rows[i]["vaqti"].ToString() + " da ";
                                 ism += stom.Rows[0]["Nomi"].ToString() + " stomatologiyasiga ko'rikga borishingiz kerak!!! ";


                                 await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + ism + " ```", ParseMode.Markdown);

                             }
                             

                         }
                         int xabson = 0;
                         DataTable bugungiX = DB.db.Query("select * from BemorgaXabar where Holati=0 and BemorId=" + Convert.ToInt32(bemid.Rows[0]["UserId"]));
                         for (int j = 0; j < bugungiX.Rows.Count; j++)
                         {
                             xabson = 1;
                             await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + bugungiX.Rows[j]["Matni"] + " ```", ParseMode.Markdown);

                         }
                         if ((yanbem.Rows.Count == 0 ||krish == 0)&& xabson==0)
                         {
                             await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` Xabar yo'q ```", ParseMode.Markdown);

                         }

                     }
                    if(txt=="Anketa")
                    {
                        DataTable bemid = DB.db.Query("select * from Bot where StatusId=3 and BotId='" + e.Message.Chat.Id.ToString() + "'");
                        int krish = 0;
                        DataTable yanbem = DB.db.Query("select * from Korik where BemorId=" + Convert.ToInt32(bemid.Rows[0]["UserId"]));
                        for(int i=0;i<yanbem.Rows.Count;i++)
                        {
                            DataTable doktor = DB.db.Query("select * from Doktor where Id=" + Convert.ToInt32(yanbem.Rows[i]["DoktorId"]));
                            Console.WriteLine(doktor.Rows[0]["Ism"]);
                            DataTable bemor = DB.db.Query("select * from Bemor where Id=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]));
                               
                            string anketa = "";
                            anketa += "Davolovchi doktor: " + doktor.Rows[0]["Familya"].ToString() + " " + doktor.Rows[0]["Ism"].ToString() + " " + doktor.Rows[0]["Sharif"].ToString() + " \n";
                            anketa += " Bemor: " + bemor.Rows[0]["Familya"].ToString() + " " + bemor.Rows[0]["Ism"].ToString() + " " + bemor.Rows[0]["Sharif"].ToString() + " \n";
                            anketa += " Shikoyat: " + yanbem.Rows[i]["Shikoyat"].ToString() + " \n";
                            anketa += " Tashxis: " + yanbem.Rows[i]["Tashxis"].ToString() + " \n";
                            anketa += " Kasallik rivojlanishi: " + yanbem.Rows[i]["KasRivoj"].ToString() + " \n";
                            anketa += " Labaratoriya natijasi: " + yanbem.Rows[i]["LabNatija"].ToString() + " \n";
                            anketa += " Tishlash: " + yanbem.Rows[i]["Tishlash"].ToString() + " \n";
                            anketa += " Davolanish narxi: " + yanbem.Rows[i]["Summa"].ToString()+" so'm" + " \n";
                            anketa += " Davolangan kunlari:  \n";

                            DataTable davokuni = DB.db.Query("select * from DoktorVaqti where holati=2 and BemorId=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]) + " and DoktorId=" + Convert.ToInt32(yanbem.Rows[i]["DoktorId"]));
                            for (int j = 0; j < davokuni.Rows.Count;j++ )
                            {
                                string vaqtcha = davokuni.Rows[j]["Sanasi"].ToString();
                                int joyi = 0;
                                for (int j1 = 0; j1 < vaqtcha.Length; j1++)
                                {
                                    if (vaqtcha[j1] == ' ')
                                    { joyi = j1; }
                                }
                                vaqtcha = vaqtcha.Substring(0, joyi);
                                anketa += "  " + vaqtcha + " " + davokuni.Rows[j]["vaqti"].ToString() + "\n";
                            }
                            anketa += " Davolanish tugagan sana:  \n";
                             davokuni = DB.db.Query("select * from DoktorVaqti where holati=3 and BemorId=" + Convert.ToInt32(yanbem.Rows[i]["BemorId"]) + " and DoktorId=" + Convert.ToInt32(yanbem.Rows[i]["DoktorId"]));
                             int krishii = 0;
                             for (int j = 0; j < davokuni.Rows.Count; j++)
                             {
                                 krishii = 1;
                                 string vaqtcha = davokuni.Rows[j]["Sanasi"].ToString();
                                 int joyi = 0;
                                 for (int j1 = 0; j1 < vaqtcha.Length; j1++)
                                 {
                                     if (vaqtcha[j1] == ' ')
                                     { joyi = j1; }
                                 }
                                 vaqtcha = vaqtcha.Substring(0, joyi);
                                 anketa += "  " + vaqtcha + " " + davokuni.Rows[j]["vaqti"].ToString() + "\n";
                             }
                            if(krishii==0)
                            {
                                anketa += "  - \n";
                            }

                                await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + anketa + " ```", ParseMode.Markdown);

                        }
                    }
                    if (txt == "Stomatologiyalar")
                    {
                        DataTable stom = DB.db.Query("select * from Stomatologiya");
                        string stomlar = "";
                        for(int i=0;i<stom.Rows.Count;i++)
                        {
                            string ss = "";
                            if(i!=0)
                            {
                                ss = " ";
                            }
                           
                            stomlar += ""+ss + (i+1).ToString() + ". " + stom.Rows[i]["Nomi"].ToString()+"\n";
                            stomlar +="    Manzili: " + stom.Rows[i]["Manzil"].ToString() + "\n";
                            stomlar +="    Telefon raqami: " + stom.Rows[i]["TelNomer"].ToString()+"\n";
                        }
                        await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + stomlar + " ```", ParseMode.Markdown);


                    }
                    if(txt=="Doktorlar")
                    {
                        DataTable doktor = DB.db.Query("select * from Doktor");
                        string stomlar = "";
                        for (int i = 0; i < doktor.Rows.Count; i++)
                        {
                            string ss = "";
                            if (i != 0)
                            {
                                ss = " ";
                            }

                            stomlar += "" + ss + (i + 1).ToString() + ". " +doktor.Rows[i]["Familya"].ToString() + " " + doktor.Rows[i]["Ism"].ToString() + " " + doktor.Rows[i]["Sharif"].ToString() + " \n";
                            DataTable stom = DB.db.Query("select * from Stomatologiya where Id=" + doktor.Rows[i]["StomatologiyaId"]);
                            stomlar += "    Ish joyi: " + stom.Rows[0]["Nomi"].ToString() + "\n";
                            stomlar += "    Telefon raqami: " + doktor.Rows[i]["TelNomer"].ToString() + "\n";
                           DataTable davokuni = DB.db.Query("select * from DoktorVaqti where holati=3 and DoktorId=" + Convert.ToInt32(doktor.Rows[i]["Id"]));
                           stomlar += "    Davolagan bemorlar soni: " + davokuni.Rows.Count + " ta \n";
                            
                        }
                        await bot.SendTextMessageAsync(e.Message.Chat.Id, "``` " + stomlar + " ```", ParseMode.Markdown);

                        
                    }
                }

            }
             catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

       public static string rep(string matn)
        {

            Console.WriteLine("murojat bo'ldi");
            matn = matn.Replace("А", "A");
            matn = matn.Replace("а", "a");
            matn = matn.Replace("Б", "B");
            matn = matn.Replace("б", "b");
            matn = matn.Replace("С", "C");
            matn = matn.Replace("с", "c");
            matn = matn.Replace("Д", "D");
            matn = matn.Replace("д", "d");
            matn = matn.Replace("Е", "E");
            matn = matn.Replace("е", "e");
            matn = matn.Replace("Ф", "F");
            matn = matn.Replace("ф", "f");
            matn = matn.Replace("Г", "G");
            matn = matn.Replace("г", "g");
            matn = matn.Replace("К", "K");
            matn = matn.Replace("к", "k");
            matn = matn.Replace("Л", "L");
            matn = matn.Replace("л", "l");
            matn = matn.Replace("М", "M");
            matn = matn.Replace("м", "m");
            matn = matn.Replace("О", "O");
            matn = matn.Replace("о", "o");
            matn = matn.Replace("Р", "R");
            matn = matn.Replace("р", "r");
            matn = matn.Replace("Т", "T");
            matn = matn.Replace("т", "t");
            matn = matn.Replace("Х", "X");
            matn = matn.Replace("х", "x");
            matn = matn.Replace("У", "Y");
            matn = matn.Replace("у", "y");

            return matn;
        }
       
    }
}
