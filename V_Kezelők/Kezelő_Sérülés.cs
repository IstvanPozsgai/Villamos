using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Sérülés_Anyag
    {
        public List<Adat_Sérülés_Anyag> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Anyag> Adatok = new List<Adat_Sérülés_Anyag>();
            Adat_Sérülés_Anyag Adat;

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
                                Adat = new Adat_Sérülés_Anyag(
                                           rekord["cikkszám"].ToStrTrim(),
                                           rekord["anyagnév"].ToStrTrim(),
                                           rekord["mennyiség"].ToÉrt_Double(),
                                           rekord["me"].ToStrTrim(),
                                           rekord["ár"].ToÉrt_Double(),
                                           rekord["állapot"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Double(),
                                           rekord["mozgásnem"].ToStrTrim()
                                                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Anyag Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Anyag Adat = null;

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
                                Adat = new Adat_Sérülés_Anyag(
                                           rekord["cikkszám"].ToStrTrim(),
                                           rekord["anyagnév"].ToStrTrim(),
                                           rekord["mennyiség"].ToÉrt_Double(),
                                           rekord["me"].ToStrTrim(),
                                           rekord["ár"].ToÉrt_Double(),
                                           rekord["állapot"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Double(),
                                           rekord["mozgásnem"].ToStrTrim()
                                                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }

    }

    public class Kezelő_Sérülés_Jelentés
    {
        public List<Adat_Sérülés_Jelentés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Jelentés> Adatok = new List<Adat_Sérülés_Jelentés>();
            Adat_Sérülés_Jelentés Adat;

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
                                Adat = new Adat_Sérülés_Jelentés(
                                           rekord["Sorszám"].ToÉrt_Int(),
                                           rekord["Telephely"].ToStrTrim(),
                                           rekord["Dátum"].ToÉrt_DaTeTime(),
                                           rekord["Balesethelyszín"].ToStrTrim(),
                                           rekord["Viszonylat"].ToStrTrim(),
                                           rekord["Rendszám"].ToStrTrim(),
                                           rekord["Járművezető"].ToStrTrim(),
                                           rekord["Rendelésszám"].ToÉrt_Int(),
                                           rekord["Kimenetel"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int(),
                                           rekord["Iktatószám"].ToStrTrim(),
                                           rekord["Típus"].ToStrTrim(),
                                           rekord["Szerelvény"].ToStrTrim(),
                                           rekord["Forgalmiakadály"].ToÉrt_Int(),
                                           rekord["Műszaki"].ToÉrt_Bool(),
                                           rekord["Anyagikár"].ToÉrt_Bool(),
                                           rekord["Biztosító"].ToStrTrim(),
                                           rekord["Személyisérülés"].ToÉrt_Bool(),
                                           rekord["Személyisérülés1"].ToÉrt_Bool(),
                                           rekord["Biztosítóidő"].ToÉrt_Int(),
                                           rekord["Mivelütközött"].ToStrTrim(),
                                           rekord["Anyagikárft"].ToÉrt_Int(),
                                           rekord["Leírás"].ToStrTrim(),
                                           rekord["Leírás1"].ToStrTrim(),
                                           rekord["Balesethelyszín1"].ToStrTrim(),
                                           rekord["Esemény"].ToStrTrim(),
                                           rekord["Anyagikárft1"].ToÉrt_Int(),
                                           rekord["Státus1"].ToÉrt_Int(),
                                           rekord["Kmóraállás"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Jelentés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Jelentés Adat = null;

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
                                Adat = new Adat_Sérülés_Jelentés(
                                           rekord["Sorszám"].ToÉrt_Int(),
                                           rekord["Telephely"].ToStrTrim(),
                                           rekord["Dátum"].ToÉrt_DaTeTime(),
                                           rekord["Balesethelyszín"].ToStrTrim(),
                                           rekord["Viszonylat"].ToStrTrim(),
                                           rekord["Rendszám"].ToStrTrim(),
                                           rekord["Járművezető"].ToStrTrim(),
                                           rekord["Rendelésszám"].ToÉrt_Int(),
                                           rekord["Kimenetel"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int(),
                                           rekord["Iktatószám"].ToStrTrim(),
                                           rekord["Típus"].ToStrTrim(),
                                           rekord["Szerelvény"].ToStrTrim(),
                                           rekord["Forgalmiakadály"].ToÉrt_Int(),
                                           rekord["Műszaki"].ToÉrt_Bool(),
                                           rekord["Anyagikár"].ToÉrt_Bool(),
                                           rekord["Biztosító"].ToStrTrim(),
                                           rekord["Személyisérülés"].ToÉrt_Bool(),
                                           rekord["Személyisérülés1"].ToÉrt_Bool(),
                                           rekord["Biztosítóidő"].ToÉrt_Int(),
                                           rekord["Mivelütközött"].ToStrTrim(),
                                           rekord["Anyagikárft"].ToÉrt_Int(),
                                           rekord["Leírás"].ToStrTrim(),
                                           rekord["Leírás1"].ToStrTrim(),
                                           rekord["Balesethelyszín1"].ToStrTrim(),
                                           rekord["Esemény"].ToStrTrim(),
                                           rekord["Anyagikárft1"].ToÉrt_Int(),
                                           rekord["Státus1"].ToÉrt_Int(),
                                           rekord["Kmóraállás"].ToStrTrim());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Sérülés_Költség
    {
        public List<Adat_Sérülés_Költség> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Költség> Adatok = new List<Adat_Sérülés_Költség>();
            Adat_Sérülés_Költség Adat;

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
                                Adat = new Adat_Sérülés_Költség(
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Anyagköltség"].ToÉrt_Int(),
                                           rekord["Munkaköltség"].ToÉrt_Int(),
                                           rekord["Gépköltség"].ToÉrt_Int(),
                                           rekord["Szolgáltatás"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Költség Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Költség Adat = null;

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
                                Adat = new Adat_Sérülés_Költség(
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Anyagköltség"].ToÉrt_Int(),
                                           rekord["Munkaköltség"].ToÉrt_Int(),
                                           rekord["Gépköltség"].ToÉrt_Int(),
                                           rekord["Szolgáltatás"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
    public class Kezelő_Sérülés_Tarifa
    {
        public List<Adat_Sérülés_Tarifa> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Tarifa> Adatok = new List<Adat_Sérülés_Tarifa>();
            Adat_Sérülés_Tarifa Adat;

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
                                Adat = new Adat_Sérülés_Tarifa(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["D60tarifa"].ToÉrt_Int(),
                                           rekord["D03tarifa"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Tarifa Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Tarifa Adat = null;

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
                                Adat = new Adat_Sérülés_Tarifa(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["D60tarifa"].ToÉrt_Int(),
                                           rekord["D03tarifa"].ToÉrt_Int());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
    public class Kezelő_Sérülés_Művelet
    {
        public List<Adat_Sérülés_Művelet> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Művelet> Adatok = new List<Adat_Sérülés_Művelet>();
            Adat_Sérülés_Művelet Adat;

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
                                Adat = new Adat_Sérülés_Művelet(
                                           rekord["Teljesítményfajta"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Műveletszöveg"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Művelet Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Művelet Adat = null;

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
                                Adat = new Adat_Sérülés_Művelet(
                                           rekord["Teljesítményfajta"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Műveletszöveg"].ToStrTrim());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
    public class Kezelő_Sérülés_Visszajelentés
    {
        public List<Adat_Sérülés_Visszajelentés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Visszajelentés> Adatok = new List<Adat_Sérülés_Visszajelentés>();
            Adat_Sérülés_Visszajelentés Adat;

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
                                Adat = new Adat_Sérülés_Visszajelentés(
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Munkaidő"].ToÉrt_Int(),
                                           rekord["Storno"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Teljesítményfajta"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Visszajelentés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Visszajelentés Adat = null;

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
                                Adat = new Adat_Sérülés_Visszajelentés(
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Munkaidő"].ToÉrt_Int(),
                                           rekord["Storno"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Teljesítményfajta"].ToStrTrim());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
    public class Kezelő_Sérülés_Ideig
    {
        public List<Adat_Sérülés_Ideig> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Ideig> Adatok = new List<Adat_Sérülés_Ideig>();
            Adat_Sérülés_Ideig Adat;

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
                                Adat = new Adat_Sérülés_Ideig(
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Anyagköltség"].ToÉrt_Int(),
                                           rekord["Munkaköltség"].ToÉrt_Int(),
                                           rekord["Gépköltség"].ToÉrt_Int(),
                                           rekord["Szolgáltatás"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Sérülés_Ideig Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Ideig Adat = null;

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
                                Adat = new Adat_Sérülés_Ideig(
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Anyagköltség"].ToÉrt_Int(),
                                           rekord["Munkaköltség"].ToÉrt_Int(),
                                           rekord["Gépköltség"].ToÉrt_Int(),
                                           rekord["Szolgáltatás"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
}
