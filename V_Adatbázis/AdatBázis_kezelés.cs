using System;
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace Villamos.Adatszerkezet
{
    public class AdatBázis_kezelés
    {
        public void AB_Adat_Tábla_Létrehozás(string hely, string jelszó, string szöveg)
        {
            try
            {
                string kapcsolatiszöveg = "";
                if (hely.Contains(".mdb"))
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

        public void AB_Adat_Bázis_Létrehozás(string hely, string jelszó)
        {
            try
            {
                if (File.Exists(hely)) return;       // Ha van ilyen fájl, akkor nem hozza létre ismételten
                ADOX.Catalog cat = new ADOX.Catalog();
                string kapcsolatiszöveg = "";
                if (hely.Contains(".mdb"))
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

        public void AB_Adat_Tábla_Létrehozás(string hely, string jelszó, string szöveg, string táblanév)
        {
            try
            {
                if (TáblaEllenőrzés(hely, jelszó, táblanév)) return; //ha létezik a tábla akkor nem csinál semmit
                string kapcsolatiszöveg = "";
                if (hely.Contains(".mdb"))
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
            catch (System.Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
        }


        public void AB_Új_Oszlop(string hely, string jelszó, string Tábla, string Oszlop, string Típus)
        {
            try
            {
                string kapcsolatiszöveg = "";
                if (hely.Contains(".mdb"))
                    kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{hely}'; Jet Oledb:Database Password={jelszó};";
                else
                    kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{hely}'; Jet OLEDB:Database Password ={jelszó};";
                using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
                {
                    using (OleDbCommand cmdCreate = new OleDbCommand())
                    {
                        string szöveg = $"ALTER TABLE {Tábla} ADD COLUMN {Oszlop} {Típus} ";
                        cmdCreate.Connection = Kapcsolat;
                        cmdCreate.CommandText = szöveg;
                        Kapcsolat.Open();
                        cmdCreate.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        /// <summary>
        /// Leellenőrzi, hogy létezik-e a tábla az adatbázisban.
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="táblanév"></param>
        /// <returns></returns>
        public static bool TáblaEllenőrzés(string hely, string jelszó, string táblanév)
        {
            bool válasz = false;
            string kapcsolatiszöveg;
            if (hely.Contains(".mdb"))
                kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{hely}'; Jet Oledb:Database Password={jelszó};";
            else
                kapcsolatiszöveg = $"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ='{hely}'; Jet OLEDB:Database Password ={jelszó};";

            using (OleDbConnection connection = new OleDbConnection(kapcsolatiszöveg))
            {
                connection.Open();
                DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow row in schemaTable.Rows)
                {
                    string tábla = row["TABLE_NAME"].ToString();
                    if (row["TABLE_NAME"].ToString() == táblanév)
                    {
                        válasz = true;
                        break;
                    }

                }

            }
            return válasz;
        }


    }
}
