using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.V_Kezelők
{
    public class Kezelő_Kiegészítő_Email
    {
        readonly string jelszó = "Mocó";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
        string táblanév = "hibanaplo_email";
        
        // Statikusan tárolom, hogy csak egyszer kelljen betölteni a címeket.
        public static string ÖsszesEmailCím { get; private set; } = string.Empty;

        public string Email_Cimek()
        {
            // Ha már betöltésre került legalább egyszer, nem olvassa be újra.
            if (!string.IsNullOrEmpty(ÖsszesEmailCím))
                return ÖsszesEmailCím;

            List<string> adatok = new List<string>();
            string kapcsolatSzöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó}";
            string lekérdezés = $"SELECT cim FROM {táblanév}";

            try
            {
                using (OleDbConnection kapcsolat = new OleDbConnection(kapcsolatSzöveg))
                {
                    kapcsolat.Open();
                    using (OleDbCommand parancs = new OleDbCommand(lekérdezés, kapcsolat))
                    using (OleDbDataReader rekord = parancs.ExecuteReader())
                    {
                        while (rekord.Read())
                        {
                            if (rekord["cim"] != DBNull.Value)
                                adatok.Add(rekord["cim"].ToString());
                        }
                    }
                }
                ÖsszesEmailCím = string.Join(";", adatok.Distinct());
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Hiba az e-mail címek beolvasásakor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ÖsszesEmailCím;
        }

        public void Rögzítés(string cim)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (cim) VALUES (";
                szöveg += $"{cim}) ";
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

        public void Törlés(string cim)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE cim='{cim}'";
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

