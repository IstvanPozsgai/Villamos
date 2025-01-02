using System.Collections.Generic;
using System.Data.OleDb;
using MyA = Adatbázis;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Kezelő_T5C5_Havi_Nap
    {
        public List<Adat_T5C5_Havi_Nap> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Havi_Nap> Adatok = new List<Adat_T5C5_Havi_Nap>();
            Adat_T5C5_Havi_Nap Adat;

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
                                Adat = new Adat_T5C5_Havi_Nap(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["N1"].ToStrTrim(),
                                    rekord["N2"].ToStrTrim(),
                                    rekord["N3"].ToStrTrim(),
                                    rekord["N4"].ToStrTrim(),
                                    rekord["N5"].ToStrTrim(),
                                    rekord["N6"].ToStrTrim(),
                                    rekord["N7"].ToStrTrim(),
                                    rekord["N8"].ToStrTrim(),
                                    rekord["N9"].ToStrTrim(),
                                    rekord["N10"].ToStrTrim(),
                                    rekord["N11"].ToStrTrim(),
                                    rekord["N12"].ToStrTrim(),
                                    rekord["N13"].ToStrTrim(),
                                    rekord["N14"].ToStrTrim(),
                                    rekord["N15"].ToStrTrim(),
                                    rekord["N16"].ToStrTrim(),
                                    rekord["N17"].ToStrTrim(),
                                    rekord["N18"].ToStrTrim(),
                                    rekord["N19"].ToStrTrim(),
                                    rekord["N20"].ToStrTrim(),
                                    rekord["N21"].ToStrTrim(),
                                    rekord["N22"].ToStrTrim(),
                                    rekord["N23"].ToStrTrim(),
                                    rekord["N24"].ToStrTrim(),
                                    rekord["N25"].ToStrTrim(),
                                    rekord["N26"].ToStrTrim(),
                                    rekord["N27"].ToStrTrim(),
                                    rekord["N28"].ToStrTrim(),
                                    rekord["N29"].ToStrTrim(),
                                    rekord["N30"].ToStrTrim(),
                                    rekord["N31"].ToStrTrim(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
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

    public class Kezelő_T5C5_Futás
    {
        public List<Adat_T5C5_Futás> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Futás> Adatok = new List<Adat_T5C5_Futás>();
            Adat_T5C5_Futás Adat;

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
                                Adat = new Adat_T5C5_Futás(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Futásstátus"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Long()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
    public class Kezelő_T5C5_Futás1
    {
        public List<Adat_T5C5_Futás1> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Futás1> Adatok = new List<Adat_T5C5_Futás1>();
            Adat_T5C5_Futás1 Adat;

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
                                Adat = new Adat_T5C5_Futás1(
                                    rekord["Státus"].ToÉrt_Long()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Futás1 Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_T5C5_Futás1 Adat = null;
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

                            Adat = new Adat_T5C5_Futás1(
                                rekord["Státus"].ToÉrt_Long()
                                ); ;
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_T5C5_Állomány
    {
        public List<Adat_T5C5_Állomány> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Állomány> Adatok = new List<Adat_T5C5_Állomány>();
            Adat_T5C5_Állomány Adat;

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
                                Adat = new Adat_T5C5_Állomány(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Állomány Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_T5C5_Állomány Adat = null;

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
                                Adat = new Adat_T5C5_Állomány(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_T5C5_Kmadatok
    {
        public List<Adat_T5C5_Kmadatok> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Kmadatok> Adatok = new List<Adat_T5C5_Kmadatok>();
            Adat_T5C5_Kmadatok Adat;

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
                                Adat = new Adat_T5C5_Kmadatok(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["jjavszám"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                    rekord["vizsgfok"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                    rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                    rekord["vizsgkm"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),

                                    rekord["vizsgsorszám"].ToÉrt_Long(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["V2végezte"].ToStrTrim(),

                                    rekord["KövV2_sorszám"].ToÉrt_Long(),
                                    rekord["KövV2"].ToStrTrim(),
                                    rekord["KövV_sorszám"].ToÉrt_Long(),
                                    rekord["KövV"].ToStrTrim(),
                                    rekord["törölt"].ToÉrt_Bool(),

                                    rekord["V2V3Számláló"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_T5C5_Kmadatok> Lista_Szűrt_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Kmadatok> Adatok = new List<Adat_T5C5_Kmadatok>();
            Adat_T5C5_Kmadatok Adat;

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
                                Adat = new Adat_T5C5_Kmadatok(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Kmadatok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_T5C5_Kmadatok Adat = null;

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

                            Adat = new Adat_T5C5_Kmadatok(
                                rekord["ID"].ToÉrt_Long(),
                                rekord["Azonosító"].ToStrTrim(),
                                rekord["jjavszám"].ToÉrt_Long(),
                                rekord["KMUkm"].ToÉrt_Long(),
                                rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                rekord["vizsgfok"].ToStrTrim(),
                                rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                rekord["vizsgkm"].ToÉrt_Long(),
                                rekord["havikm"].ToÉrt_Long(),

                                rekord["vizsgsorszám"].ToÉrt_Long(),
                                rekord["fudátum"].ToÉrt_DaTeTime(),
                                rekord["Teljeskm"].ToÉrt_Long(),
                                rekord["Ciklusrend"].ToStrTrim(),
                                rekord["V2végezte"].ToStrTrim(),

                                rekord["KövV2_sorszám"].ToÉrt_Long(),
                                rekord["KövV2"].ToStrTrim(),
                                rekord["KövV_sorszám"].ToÉrt_Long(),
                                rekord["KövV"].ToStrTrim(),
                                rekord["törölt"].ToÉrt_Bool(),

                                rekord["V2V3Számláló"].ToÉrt_Long()
                                ); ;
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzít(string hely, string jelszó, Adat_T5C5_Kmadatok Rekord)
        {

            string szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
            szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
            szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
            szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
            szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
            szöveg += Rekord.ID + ", '" + Rekord.Azonosító + "', " + Rekord.Jjavszám + ", " + Rekord.KMUkm + ", '" + Rekord.KMUdátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += "'" + Rekord.Vizsgfok.Trim() + "', '" + Rekord.Vizsgdátumk.ToString("yyyy.MM.dd") + "', '" + Rekord.Vizsgdátumv.ToString("yyyy.MM.dd") + "', ";
            szöveg += Rekord.Vizsgkm + ", " + Rekord.Havikm + ", " + Rekord.Vizsgsorszám + ", '" + Rekord.Fudátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += Rekord.Teljeskm + ", '" + Rekord.Ciklusrend.Trim() + "', '" + Rekord.V2végezte.Trim() + "', " + Rekord.KövV2_sorszám + ", '" + Rekord.KövV2.Trim() + "', ";
            szöveg += Rekord.KövV_sorszám + ", '" + Rekord.KövV.Trim() + "', " + Rekord.V2V3Számláló + ", " + Rekord.Törölt + " )";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        public void Módosít(string hely, string jelszó, Adat_T5C5_Kmadatok Rekord)
        {
            string szöveg = " UPDATE kmtábla SET ";
            szöveg += " Jjavszám=" + Rekord.Jjavszám + ", ";
            szöveg += " KMUkm=" + Rekord.KMUkm + ", ";
            szöveg += " KMUdátum='" + Rekord.KMUdátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += " Vizsgfok='" + Rekord.Vizsgfok.Trim() + "', ";
            szöveg += " Vizsgdátumk='" + Rekord.Vizsgdátumk.ToString("yyyy.MM.dd") + "', ";
            szöveg += " Vizsgdátumv='" + Rekord.Vizsgdátumv.ToString("yyyy.MM.dd") + "', ";
            szöveg += " VizsgKm=" + Rekord.Vizsgkm + ", ";
            szöveg += " HaviKm=" + Rekord.Havikm + ", ";
            szöveg += " VizsgSorszám=" + Rekord.Vizsgsorszám + ", ";
            szöveg += " fudátum='" + Rekord.Fudátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += " Teljeskm=" + Rekord.Teljeskm + ", ";
            szöveg += " Ciklusrend='" + Rekord.Ciklusrend.Trim() + "', ";
            szöveg += " V2végezte='" + Rekord.V2végezte.Trim() + "', ";
            szöveg += " KövV2_Sorszám=" + Rekord.KövV2_sorszám + ",  ";
            szöveg += " KövV2='" + Rekord.KövV2.Trim() + "', ";
            szöveg += " KövV_Sorszám=" + Rekord.KövV_sorszám + ", ";
            szöveg += " KövV='" + Rekord.KövV.Trim() + "', ";
            szöveg += " törölt=false, ";
            szöveg += " V2V3Számláló=" + Rekord.V2V3Számláló;
            szöveg += " WHERE id=" + Rekord.ID;
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public List<string> Lista_String(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["Ciklusrend"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_T5C5_Kmadatok_Napló
    {
        public List<Adat_T5C5_Kmadatok_Napló> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Kmadatok_Napló> Adatok = new List<Adat_T5C5_Kmadatok_Napló>();
            Adat_T5C5_Kmadatok_Napló Adat;

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
                                Adat = new Adat_T5C5_Kmadatok_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["jjavszám"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                    rekord["vizsgfok"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                    rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                    rekord["vizsgkm"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),

                                    rekord["vizsgsorszám"].ToÉrt_Long(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["V2végezte"].ToStrTrim(),

                                    rekord["KövV2_sorszám"].ToÉrt_Long(),
                                    rekord["KövV2"].ToStrTrim(),
                                    rekord["KövV_sorszám"].ToÉrt_Long(),
                                    rekord["KövV"].ToStrTrim(),
                                    rekord["törölt"].ToÉrt_Bool(),

                                    rekord["V2V3Számláló"].ToÉrt_Long(),
                                    rekord["Módosító"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime()
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

    public class Kezelő_T5C5_Előterv
    {

        public List<Adat_T5C5_Előterv> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Előterv> Adatok = new List<Adat_T5C5_Előterv>();
            Adat_T5C5_Előterv Adat;

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

                                Adat = new Adat_T5C5_Előterv(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["jjavszám"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                    rekord["vizsgfok"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                    rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                    rekord["vizsgkm"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),

                                    rekord["vizsgsorszám"].ToÉrt_Long(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["V2végezte"].ToStrTrim(),

                                    rekord["KövV2_sorszám"].ToÉrt_Long(),
                                    rekord["KövV2"].ToStrTrim(),
                                    rekord["KövV_sorszám"].ToÉrt_Long(),
                                    rekord["KövV"].ToStrTrim(),
                                    rekord["törölt"].ToÉrt_Bool(),

                                    rekord["Honostelephely"].ToStrTrim(),
                                    rekord["tervsorszám"].ToÉrt_Long(),
                                    rekord["Kerék_K11"].ToÉrt_Double(),
                                    rekord["Kerék_K12"].ToÉrt_Double(),
                                    rekord["Kerék_K21"].ToÉrt_Double(),
                                    rekord["Kerék_K22"].ToÉrt_Double(),
                                    rekord["Kerék_min"].ToÉrt_Double(),

                                    rekord["V2V3Számláló"].ToÉrt_Long()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Előterv Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_T5C5_Előterv Adat = null;

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

                            Adat = new Adat_T5C5_Előterv(
                                rekord["ID"].ToÉrt_Long(),
                                rekord["Azonosító"].ToStrTrim(),
                                rekord["jjavszám"].ToÉrt_Long(),
                                rekord["KMUkm"].ToÉrt_Long(),
                                rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                rekord["vizsgfok"].ToStrTrim(),
                                rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                rekord["vizsgkm"].ToÉrt_Long(),
                                rekord["havikm"].ToÉrt_Long(),

                                rekord["vizsgsorszám"].ToÉrt_Long(),
                                rekord["fudátum"].ToÉrt_DaTeTime(),
                                rekord["Teljeskm"].ToÉrt_Long(),
                                rekord["Ciklusrend"].ToStrTrim(),
                                rekord["V2végezte"].ToStrTrim(),

                                rekord["KövV2_sorszám"].ToÉrt_Long(),
                                rekord["KövV2"].ToStrTrim(),
                                rekord["KövV_sorszám"].ToÉrt_Long(),
                                rekord["KövV"].ToStrTrim(),
                                rekord["törölt"].ToÉrt_Bool(),

                                rekord["Honostelephely"].ToStrTrim(),
                                rekord["tervsorszám"].ToÉrt_Long(),
                                rekord["Kerék_K11"].ToÉrt_Double(),
                                rekord["Kerék_K12"].ToÉrt_Double(),
                                rekord["Kerék_K21"].ToÉrt_Double(),
                                rekord["Kerék_K22"].ToÉrt_Double(),
                                rekord["Kerék_min"].ToÉrt_Double(),

                                rekord["V2V3Számláló"].ToÉrt_Long()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_T5C5_Göngyöl_DátumTábla
    {
        public List<Adat_T5C5_Göngyöl_DátumTábla> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Göngyöl_DátumTábla> Adatok = new List<Adat_T5C5_Göngyöl_DátumTábla>();
            Adat_T5C5_Göngyöl_DátumTábla Adat;

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

                                Adat = new Adat_T5C5_Göngyöl_DátumTábla(
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Zárol"].ToÉrt_Bool()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_T5C5_Göngyöl_DátumTábla Adat)
        {
            string szöveg = $"INSERT INTO Dátumtábla (telephely, utolsórögzítés) ";
            szöveg += $"VALUES ('{Adat.Telephely}',";
            szöveg += $"'{Adat.Utolsórögzítés}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
        /// <summary>
        /// Telephely
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_T5C5_Göngyöl_DátumTábla Adat)
        {
            string szöveg = $"UPDATE Dátumtábla SET ";
            szöveg += $"utolsórögzítés='{Adat.Utolsórögzítés}' ";
            szöveg += $"WHERE telephely='{Adat.Telephely}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

    }
}
