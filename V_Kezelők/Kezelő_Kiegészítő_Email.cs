using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace Villamos.V_Kezelők
{
    public class Kezelő_Kiegészítő_Email
    {
        readonly string jelszó = "Mocó";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
        
        // Statikusan tárolom, hogy csak egyszer kelljen betölteni a címeket.
        public static string ÖsszesEmailCím { get; private set; } = string.Empty;

        public string Email_Cimek(string tábla= "hibanaplo_email")
        {
            // Ha már betöltésre került legalább egyszer, nem olvassa be újra.
            if (!string.IsNullOrEmpty(ÖsszesEmailCím))
                return ÖsszesEmailCím;

            List<string> adatok = new List<string>();
            string kapcsolatSzöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó}";
            string lekérdezés = $"SELECT cim FROM {tábla}";

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
    }
}
