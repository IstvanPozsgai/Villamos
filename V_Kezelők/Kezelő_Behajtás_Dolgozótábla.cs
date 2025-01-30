using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Behajtás_Dolgozótábla
    {
        readonly string jelszó = "egérpad";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
        public List<Adat_Behajtás_Dolgozótábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Dolgozótábla> Adatok = new List<Adat_Behajtás_Dolgozótábla>();
            Adat_Behajtás_Dolgozótábla Adat;

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
                                Adat = new Adat_Behajtás_Dolgozótábla(
                                    rekord["SZTSZ"].ToStrTrim(),
                                    rekord["Családnévutónév"].ToStrTrim(),
                                    rekord["Szervezetiegység"].ToStrTrim(),
                                    rekord["Munkakör"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Behajtás_Dolgozótábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM Dolgozóktábla";
            List<Adat_Behajtás_Dolgozótábla> Adatok = new List<Adat_Behajtás_Dolgozótábla>();
            Adat_Behajtás_Dolgozótábla Adat;

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
                                Adat = new Adat_Behajtás_Dolgozótábla(
                                    rekord["SZTSZ"].ToStrTrim(),
                                    rekord["Családnévutónév"].ToStrTrim(),
                                    rekord["Szervezetiegység"].ToStrTrim(),
                                    rekord["Munkakör"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Dolgozótábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Dolgozótábla Adat = null;

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
                            rekord.Read();
                            Adat = new Adat_Behajtás_Dolgozótábla(
                                        rekord["SZTSZ"].ToStrTrim(),
                                        rekord["Családnévutónév"].ToStrTrim(),
                                        rekord["Szervezetiegység"].ToStrTrim(),
                                        rekord["Munkakör"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int());
                        }
                    }
                }
            }
            return Adat;
        }
    }
}
