using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;  
using MyF = Függvénygyűjtemény;

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

            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\hibanapló\hiba{DateTime.Today:yyyyMMdd}.log";
            File.AppendAllText(hely, szöveg);

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


            NotifyIcon BuborékAblak = new NotifyIcon
            {
                Icon = SystemIcons.Error,
                BalloonTipTitle = "Programhiba",
                BalloonTipText = "Kérlek jelezd a bekövetkezés körülményeit a pozsgaii@bkv.hu címre küldött leírással, képekkel.\n Köszönettel: Pozsgai István",
                BalloonTipIcon = ToolTipIcon.Info,
                Visible = true
            };
            BuborékAblak.ShowBalloonTip(30000);
        }
    }
}
