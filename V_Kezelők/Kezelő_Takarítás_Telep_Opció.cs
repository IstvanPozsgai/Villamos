using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Takarítás_Telep_Opció
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\Opcionális{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ÉpülettakarításTelepOpcionálisLétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Takarítás_Telep_Opció> Lista_Adatok(string Telephely, int Év)
        {
            List<Adat_Takarítás_Telep_Opció> Adatok = new List<Adat_Takarítás_Telep_Opció>();
            Adat_Takarítás_Telep_Opció Adat;
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM TakarításOpcTelepAdatok";

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
                                Adat = new Adat_Takarítás_Telep_Opció(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megrendelt"].ToÉrt_Double(),
                                        rekord["Teljesített"].ToÉrt_Double()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(string Telephely, int Év, Adat_Takarítás_Telep_Opció Adat)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"INSERT INTO TakarításOpcTelepAdatok (Id, Dátum, Megrendelt, Teljesített) VALUES (";
            szöveg += $"{Adat.Id}, '{Adat.Dátum}', {Adat.Megrendelt}, {Adat.Teljesített}))";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        public void Rögzít(string Telephely, int Év, List<Adat_Takarítás_Telep_Opció> Adatok)
        {
            FájlBeállítás(Telephely, Év);
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Takarítás_Telep_Opció Adat in Adatok)
            {
                string szöveg = $"INSERT INTO TakarításOpcTelepAdatok (Id, Dátum, Megrendelt, Teljesített) VALUES (";
                szöveg += $"{Adat.Id}, '{Adat.Dátum.ToShortDateString()}', {Adat.Megrendelt}, {Adat.Teljesített})";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }

        public void Módosít(string Telephely, int Év, Adat_Takarítás_Telep_Opció Adat)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"UPDATE TakarításOpcTelepAdatok  SET ";
            szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
            szöveg += $"Megrendelt={Adat.Megrendelt}, ";
            szöveg += $"Teljesített={Adat.Teljesített} ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(string Telephely, int Év, List<Adat_Takarítás_Telep_Opció> Adatok)
        {
            FájlBeállítás(Telephely, Év);
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Takarítás_Telep_Opció Adat in Adatok)
            {
                string szöveg = $"UPDATE TakarításOpcTelepAdatok  SET ";
                szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
                szöveg += $"Megrendelt={Adat.Megrendelt}, ";
                szöveg += $"Teljesített={Adat.Teljesített} ";
                szöveg += $" WHERE id={Adat.Id}";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }
    }
}
