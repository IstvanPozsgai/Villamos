using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{

    public class Kezelő_Kerék_Eszterga_Tengely
    {
        readonly string táblanév = "tengely";
        readonly string jelszó = "RónaiSándor";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";

        public Kezelő_Kerék_Eszterga_Tengely()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Törzs(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Eszterga_Tengely> Lista_Adatok()
        {
            List<Adat_Kerék_Eszterga_Tengely> Adatok = new List<Adat_Kerék_Eszterga_Tengely>();
            Adat_Kerék_Eszterga_Tengely Adat;
            string szöveg = $"SELECT * FROM {táblanév}";
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
                                Adat = new Adat_Kerék_Eszterga_Tengely(
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Állapot"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Kerék_Eszterga_Tengely Adat)
        {
            try
            {
                List<Adat_Kerék_Eszterga_Tengely> Adatok = Lista_Adatok();
                Adat_Kerék_Eszterga_Tengely Elem = (from a in Adatok
                                                    where a.Típus == Adat.Típus
                                                    && a.Állapot == Adat.Állapot
                                                    select a).FirstOrDefault();
                if (Elem != null)
                    Módosítás(Adat);
                else
                    Rögzítés(Adat);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(Adat_Kerék_Eszterga_Tengely Adat)
        {
            try
            {
                string szöveg = "UPDATE tengely SET ";
                szöveg += $" munkaidő={Adat.Munkaidő} ";
                szöveg += $" WHERE típus='{Adat.Típus.Trim()}' AND Állapot={Adat.Állapot}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_Kerék_Eszterga_Tengely Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO tengely ( Típus, munkaidő, állapot) VALUES ('{Adat.Típus.Trim()}', {Adat.Munkaidő}, {Adat.Állapot})";
                MyA.ABMódosítás(hely, jelszó, szöveg);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Elkopó
        public List<Adat_Kerék_Eszterga_Tengely> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Tengely> Adatok = new List<Adat_Kerék_Eszterga_Tengely>();
            Adat_Kerék_Eszterga_Tengely Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Tengely(
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Állapot"].ToÉrt_Int()
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
