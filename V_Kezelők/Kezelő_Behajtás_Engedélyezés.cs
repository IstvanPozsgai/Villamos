using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Behajtás_Engedélyezés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
        readonly string jelszó = "egérpad";

        public List<Adat_Behajtás_Engedélyezés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Engedélyezés> Adatok = new List<Adat_Behajtás_Engedélyezés>();
            Adat_Behajtás_Engedélyezés Adat;

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
                                Adat = new Adat_Behajtás_Engedélyezés(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Emailcím"].ToStrTrim(),
                                        rekord["Gondnok"].ToÉrt_Bool(),
                                        rekord["Szakszolgálat"].ToÉrt_Bool(),
                                        rekord["Telefonszám"].ToStrTrim(),
                                        rekord["Szakszolgálatszöveg"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
     
        public List<Adat_Behajtás_Engedélyezés> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM engedélyezés ORDER BY id";
            List<Adat_Behajtás_Engedélyezés> Adatok = new List<Adat_Behajtás_Engedélyezés>();
            Adat_Behajtás_Engedélyezés Adat;

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
                                Adat = new Adat_Behajtás_Engedélyezés(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Emailcím"].ToStrTrim(),
                                        rekord["Gondnok"].ToÉrt_Bool(),
                                        rekord["Szakszolgálat"].ToÉrt_Bool(),
                                        rekord["Telefonszám"].ToStrTrim(),
                                        rekord["Szakszolgálatszöveg"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim());
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
