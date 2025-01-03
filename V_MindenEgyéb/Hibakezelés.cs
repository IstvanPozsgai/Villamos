using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MyF = Függvénygyűjtemény;
using MyO = Microsoft.Office.Interop.Outlook;

namespace Villamos
{
    class HibásBevittAdat : Exception
    {
        //üres konstruktor
        public HibásBevittAdat() { }

        //string paraméter befogadása
        public HibásBevittAdat(string message) : base(message) { }
        //innerExteption
        public HibásBevittAdat(string message, Exception innerException)
        : base(message, innerException) { }
    }



    public static class HibaNapló
    {

        /// <summary>
        /// Hiba 
        /// </summary>
        /// <param name="hibaUzenet">ex.Message</param>
        /// <param name="osztaly">this.ToString()</param>
        /// <param name="metodus">ex.StackTrace</param>
        /// <param name="névtér">ex.Source</param>

        public static void Log(string hibaUzenet, string osztaly, string metodus, string névtér, int HibaKód, string Egyéb = "_")
        {
            string Képernyőfájl = KépernyőKép();

            //Beírjuk a napi fájlba
            string szöveg = "\n=======================================================================\n";
            szöveg += $"{DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss")}\n";
            szöveg += $"{Program.PostásTelephely}\n";
            szöveg += $"{Program.PostásNév}\n";
            szöveg += $"{hibaUzenet}\n\n";
            szöveg += $"{osztaly}\n";
            szöveg += $"{metodus}\n";
            szöveg += $"{névtér}\n";
            szöveg += $"{Egyéb}\n";
            szöveg += $"Hibakód: {HibaKód}\n";
            szöveg += " -----------------------------------------------------------------------\n";

            // E-mail küldés
            Email(Képernyőfájl, szöveg);

            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\hibanapló\hiba{DateTime.Today:yyyyMMdd}.log";
            File.AppendAllText(hely, szöveg);

            // beírjuk a csv fájlba
            hely = $@"{Application.StartupPath}\főmérnökség\adatok\hibanapló\hiba{DateTime.Today:yyyy}.csv";
            if (!File.Exists(hely))
            {
                //fejléc 
                szöveg = "Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum\n";
                File.AppendAllText(hely, szöveg, System.Text.Encoding.GetEncoding("iso-8859-2"));
            }
            szöveg = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ";"
                        + Program.PostásTelephely + ";"
                        + Program.PostásNév + ";"
                        + MyF.Szöveg_Tisztítás(hibaUzenet, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(osztaly, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(metodus, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(névtér, 0, -1, true) + ";"
                        + MyF.Szöveg_Tisztítás(Egyéb, 0, -1, true) + ";"
                        + DateTime.Today.ToString("yyyy.MM.dd") + "\n";
            File.AppendAllText(hely, szöveg, Encoding.GetEncoding("iso-8859-2"));



            //Buborék
            NotifyIcon BuborékAblak = new NotifyIcon
            {
                Icon = SystemIcons.Error,
                BalloonTipTitle = "Programhiba",
                BalloonTipText = "A hiba képernyőképpel el lett küldve a pozsgaii@bkv.hu címre.",
                BalloonTipIcon = ToolTipIcon.Info,
                Visible = true
            };
            BuborékAblak.ShowBalloonTip(30000);
        }

        private static string KépernyőKép()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            string Válasz = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Hiba_{DateTime.Now:yyyyMMddHHmmss}.bmp";
            bitmap.Save(Válasz, ImageFormat.Jpeg);
            return Válasz;
        }

        private static void Email(string hely, string hiba)
        {
            MyO._Application _app = new MyO.Application();
            MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
            // címzett
            mail.To = "pozsgaii@bkv.hu";
            // üzenet tárgya
            mail.Subject = $"Hibanapló {DateTime.Now:yyyyMMddHHmmss}";

            //string Html_szöveg = $"<html><body>";
            //Html_szöveg += $"<a>{hiba}<a>";
            //Html_szöveg += $"<center><IMG SRC={hely}></IMG></center>";
            //Html_szöveg += $"</body></html>";
            // üzent szövege
            //mail.HTMLBody = Html_szöveg;
            mail.Body = hiba;
            mail.Importance = MyO.OlImportance.olImportanceNormal;
            mail.Attachments.Add(hely);
            ((MyO._MailItem)mail).Send();
        }
    }
}
