using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_Szinezés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";
        readonly string táblanév = "szinezés";
        public Kezelő_CAF_Szinezés()
        {
        }

        public List<Adat_CAF_Szinezés> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} order by Telephely";
            List<Adat_CAF_Szinezés> Adatok = new List<Adat_CAF_Szinezés>();
            Adat_CAF_Szinezés Adat;

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
                                Adat = new Adat_CAF_Szinezés(
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["SzínPSZgar"].ToÉrt_Double(),
                                        rekord["SzínPsz"].ToÉrt_Double(),
                                        rekord["SzínIStűrés"].ToÉrt_Double(),
                                        rekord["SzínIS"].ToÉrt_Double(),
                                        rekord["SzínP"].ToÉrt_Double(),
                                        rekord["Színszombat"].ToÉrt_Double(),
                                        rekord["SzínVasárnap"].ToÉrt_Double(),
                                        rekord["Szín_E"].ToÉrt_Double(),
                                        rekord["Szín_dollár"].ToÉrt_Double(),
                                        rekord["Szín_Kukac"].ToÉrt_Double(),
                                        rekord["Szín_Hasteg"].ToÉrt_Double(),
                                        rekord["Szín_jog"].ToÉrt_Double(),
                                        rekord["Szín_nagyobb"].ToÉrt_Double()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_CAF_Szinezés Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (telephely, színpsz, színpszgar, színIstűrés, színIS, színP, színSzombat, színVasárnap, szín_e, szín_dollár, ";
                szöveg += " szín_kukac, szín_hasteg, szín_jog, szín_nagyobb ) VALUES (";
                szöveg += $"'{Adat.Telephely}', "; // telephely
                szöveg += Adat.SzínPsz + ", "; // színpsz
                szöveg += Adat.SzínPSZgar + ", "; // színpszgar
                szöveg += Adat.SzínIStűrés + ", "; // színIstűrés
                szöveg += Adat.SzínIS + ", "; // színIS
                szöveg += Adat.SzínP + ", "; // színP
                szöveg += Adat.Színszombat + ", "; // színSzombat
                szöveg += Adat.SzínVasárnap + ", "; // színVasárnap

                szöveg += Adat.Szín_E + ", ";  // szín_e
                szöveg += Adat.Szín_dollár + ", ";   // szín_dollár,
                szöveg += Adat.Szín_Kukac + ", ";  // szín_kukac
                szöveg += Adat.Szín_Hasteg + ", ";   // szín_hasteg
                szöveg += Adat.Szín_jog + ", ";  // szín_jog
                szöveg += Adat.Szín_nagyobb + ") ";  // szín_nagyobb
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

        public void Módosítás(Adat_CAF_Szinezés Adat)
        {
            try
            {
                string szöveg = $"UPDATE  {táblanév} SET ";
                szöveg += $"színpsz={Adat.SzínPsz}, "; // színpsz
                szöveg += $"színpszgar={Adat.SzínPSZgar}, "; // színpszgar
                szöveg += $"színIstűrés={Adat.SzínIStűrés}, "; // színIstűrés
                szöveg += $"színIS={Adat.SzínIS}, "; // színIS
                szöveg += $"színP={Adat.SzínP}, "; // színP
                szöveg += $"színSzombat={Adat.Színszombat}, "; // színSzombat
                szöveg += $"színVasárnap={Adat.SzínVasárnap}, "; // színVasárnap

                szöveg += $" szín_e={Adat.Szín_E}, ";  // szín_e
                szöveg += $" szín_dollár={Adat.Szín_dollár}, ";   // szín_dollár,
                szöveg += $" szín_kukac={Adat.Szín_Kukac}, ";  // szín_kukac
                szöveg += $" szín_hasteg={Adat.Szín_Hasteg}, ";   // szín_hasteg
                szöveg += $" szín_jog={Adat.Szín_jog}, ";  // szín_jog
                szöveg += $" szín_nagyobb={Adat.Szín_nagyobb}";   // szín_nagyobb
                szöveg += $" WHERE  telephely ='{Adat.Telephely}'";
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

        public void Törlés(string Telephely)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} where telephely ='{Telephely}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
    }
}
