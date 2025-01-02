using System;
using System.Data.OleDb;


namespace Villamos.Adatszerkezet
{
    public class AdatBázis_kezelés
    {
        public void AB_Adat_Tábla_Létrehozás(string hely, string jelszó, string szöveg,bool Bit32 = true)
        {
            try
            {
                string kapcsolatiszöveg = "";
                if (Bit32)
                    kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{hely}'; Jet Oledb:Database Password={jelszó};";
                else
                    kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{hely}'; Jet OLEDB:Database Password ={jelszó};";
                using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
                {
                    using (OleDbCommand cmdCreate = new OleDbCommand())
                    {
                        cmdCreate.Connection = Kapcsolat;
                        cmdCreate.CommandText = szöveg;
                        Kapcsolat.Open();
                        cmdCreate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        public void AB_Adat_Bázis_Létrehozás(string hely, string jelszó, bool Bit32 = true)
        {
            try
            {

                ADOX.Catalog cat = new ADOX.Catalog();
                string kapcsolatiszöveg = "";
                if (Bit32)
                    kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{hely}'; Jet Oledb:Database Password={jelszó};";
                else
                    kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{hely}'; Jet OLEDB:Database Password ={jelszó};";

                cat.Create(kapcsolatiszöveg);

                //Now Close the database
                if (cat.ActiveConnection is ADODB.Connection con)
                    con.Close();

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }
    }
}
