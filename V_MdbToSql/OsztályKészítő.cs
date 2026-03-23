using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Villamos
{
    public class OsztályKészítő
    {
        public string Osztály { get; set; }
        public string Fájlnév { get; set; }

        public OsztályKészítő(string fájlnév, string osztály)
        {
            Osztály = osztály;
            Fájlnév = fájlnév;
        }

        public void OsztályKészítés()
        {
            Lista_Adatok();
            Rögzítés();
            Módosítás();
            Táblalétrehozás();


        }

        private void Módosítás()
        {
            // Típus lekérése név alapján
            Type tipus = Type.GetType($"Villamos.Adatszerkezet.{Osztály}");

            // Innen már ugyanúgy megy, mint eddig
            List<string> propertyk = tipus.GetProperties().Select(p => p.Name).ToList();

            string szöveg = $"\n\n   public void Módosítás({Osztály} Adat)\n";
            szöveg += "   {\r\n       try\r\n       {\r\n";
            szöveg += "           FájlBeállítás(Telephely, Év);\n";
            szöveg += "           string szöveg = $\"UPDATE {táblanév} SET \";\n";
            for (int i = 0; i < propertyk.Count; i++)
            {
                szöveg += $"           szöveg += $@\"{propertyk[i]}=@{propertyk[i]}, \";\n";
            }
            szöveg += "\n           szöveg += $@\"WHERE id=@Id;\";\n\n";
            szöveg += "           SqliteCommand cmd = new SqliteCommand(szöveg);\n\n";
            for (int i = 0; i < propertyk.Count; i++)
            {
                szöveg += $"           cmd.Parameters.AddWithValue(\"@{propertyk[i]}\", Adat.{propertyk[i]});\n";
            }
            szöveg += "\n           MyA.SqLite_Módosítás(hely, jelszó, cmd);";
            szöveg += "\n       }\r\n       catch (HibásBevittAdat ex)\r\n       {\r\n           MessageBox.Show(ex.Message, \"Információ\", MessageBoxButtons.OK, MessageBoxIcon.Information);\r\n       }\r\n       catch (Exception ex)\r\n       {\r\n           HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);\r\n           MessageBox.Show(ex.Message + \"\\n\\n a hiba naplózásra került.\", \"A program hibára futott\", MessageBoxButtons.OK, MessageBoxIcon.Error);\r\n       }\r\n   }";
            szöveg += "\r\n\r\n\r\n\r\n\r\n";
            File.AppendAllText(Fájlnév, szöveg);
        }

        private void Rögzítés()
        {
            // Típus lekérése név alapján
            Type tipus = Type.GetType($"Villamos.Adatszerkezet.{Osztály}");

            // Innen már ugyanúgy megy, mint eddig
            List<string> propertyk = tipus.GetProperties().Select(p => p.Name).ToList();

            string szöveg = $"\n\n   public void Rögzítés(string Telephely, int Év, {Osztály} Adat)\n";
            szöveg += "   {\r\n       try\r\n       {\r\n";
            szöveg += "           FájlBeállítás(Telephely, Év);\n";
            szöveg += $"           string szöveg = $\"INSERT INTO {{táblanév}} (";

            for (int i = 0; i < propertyk.Count; i++)
            {
                if (i != 0) szöveg += ", ";
                szöveg += $"{propertyk[i]}";
            }
            szöveg += ") VALUES \";\n";
            szöveg += "           szöveg += $@\"(";
            for (int i = 0; i < propertyk.Count; i++)
            {
                if (i != 0) szöveg += ", ";
                szöveg += $"@{propertyk[i]}";
            }
            szöveg += ")\"; \n\n\n";
            szöveg += "           SqliteCommand cmd = new SqliteCommand(szöveg);\n";

            for (int i = 0; i < propertyk.Count; i++)
            {
                szöveg += $"           cmd.Parameters.AddWithValue(\"@{propertyk[i]}\", Adat.{propertyk[i]});\n";
            }

            szöveg += "\n           MyA.SqLite_Módosítás(hely, jelszó, cmd);";
            szöveg += "\n       }\r\n       catch (HibásBevittAdat ex)\r\n       {\r\n           MessageBox.Show(ex.Message, \"Információ\", MessageBoxButtons.OK, MessageBoxIcon.Information);\r\n       }\r\n       catch (Exception ex)\r\n       {\r\n           HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);\r\n           MessageBox.Show(ex.Message + \"\\n\\n a hiba naplózásra került.\", \"A program hibára futott\", MessageBoxButtons.OK, MessageBoxIcon.Error);\r\n       }\r\n   }";
            szöveg += "\r\n\r\n\r\n\r\n\r\n";
            File.AppendAllText(Fájlnév, szöveg);
        }

        private void Lista_Adatok()
        {         // Típus lekérése név alapján
            Type tipus = Type.GetType($"Villamos.Adatszerkezet.{Osztály}");

            // Innen már ugyanúgy megy, mint eddig
            List<string> propertyk = tipus.GetProperties().Select(p => p.Name).ToList();

            string szöveg = $"        public List<{Osztály}> Lista_Adatok()\r\n";
            szöveg += "        {\n";
            szöveg += $"            List<{Osztály}> Adatok = new List<{Osztály}>();\n";
            szöveg += "            try\r\n            {\n";
            szöveg += $"                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new {Osztály}(";
            szöveg += "\r\n\r\n\r\n\r\n\r\n";
            szöveg += "                          ));";
            szöveg += "            }\r\n            catch (HibásBevittAdat ex)\r\n            {\r\n                MessageBox.Show(ex.Message, \"Információ\", MessageBoxButtons.OK, MessageBoxIcon.Information);\r\n            }\r\n            catch (Exception ex)\r\n            {\r\n                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);\r\n                MessageBox.Show(ex.Message + \"\\n\\n a hiba naplózásra került.\", \"A program hibára futott\", MessageBoxButtons.OK, MessageBoxIcon.Error);\r\n            }\r\n            return Adatok;\r\n        }";
            szöveg += "\r\n\r\n\r\n\r\n\r\n";
            File.AppendAllText(Fájlnév, szöveg);
        }

        private void Táblalétrehozás()
        {

            string szöveg = "        public void Tábla_Létrehozás()\n";
            szöveg += "        {\r\n            try\r\n            {\n";
            szöveg += "                string szöveg = $@\"CREATE TABLE {táblanév} (";
            szöveg += "";
            szöveg += "";
            szöveg += "";
            szöveg += "";
            szöveg += "                                );\";\n";
            szöveg += "                MyA.SqLite_TáblaLétrehozás(hely.KönyvSzerk(), jelszó, szöveg);\n";
            szöveg += "            }\r\n            catch (HibásBevittAdat ex)\r\n            {\r\n                MessageBox.Show(ex.Message, \"Információ\", MessageBoxButtons.OK, MessageBoxIcon.Information);\r\n            }\r\n            catch (Exception ex)\r\n            {\r\n                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);\r\n                MessageBox.Show(ex.Message + \"\\n\\n a hiba naplózásra került.\", \"A program hibára futott\", MessageBoxButtons.OK, MessageBoxIcon.Error);\r\n            }\r\n        }";
            szöveg += "\r\n\r\n\r\n\r\n\r\n";
            File.AppendAllText(Fájlnév, szöveg);
        }
    }
}