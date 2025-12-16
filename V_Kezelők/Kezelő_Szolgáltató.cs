using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Szolgáltató
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
        readonly string jelszó = "Mocó";
        readonly string táblanév="TakarításSzolgáltató";

        public List<Adat_Szolgáltató> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            Adat_Szolgáltató Adat;
            List<Adat_Szolgáltató> Adatok = new List<Adat_Szolgáltató>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {

                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_Szolgáltató(
                                           rekord["SzerződésSzám"].ToStrTrim (),
                                           rekord["IratEleje"].ToStrTrim(),
                                           rekord["IratVége"].ToStrTrim(),
                                           rekord["Aláíró"].ToStrTrim(),
                                           rekord["CégNévAlá"].ToStrTrim(),
                                           rekord["CégCím"].ToStrTrim(),
                                           rekord["CégAdó"].ToStrTrim(),
                                           rekord["CégHosszúNév"].ToStrTrim(),
                                           rekord["Cégjegyzékszám"].ToStrTrim(),
                                           rekord["CsoportAzonosító"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}
