using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    public partial class Ablak_Eszterga_Adatok_Baross : Form
    {
        readonly Kezelő_Baross_Mérési_Adatok KézMérés = new Kezelő_Baross_Mérési_Adatok();

        List<Adat_Baross_Mérési_Adatok> AdatokMérés = new List<Adat_Baross_Mérési_Adatok>();
        public Ablak_Eszterga_Adatok_Baross()
        {
            InitializeComponent();
        }

        private void Ablak_Eszterga_Adatok_Baross_Load(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Kerék_Baross_Mérési_Adatok(hely);
            Jogosultságkiosztás();
            Státuscombo_Feltöltés();

            Dátumtól.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dátumig.Value = new DateTime(DateTime.Today.Year, 12, 31);

            hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.Year;
            if (Directory.Exists(hely) == false)
                Directory.CreateDirectory(hely);

            hely += @"\telepikerék.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Méréstáblakerék(hely);
        }

        #region Alap
        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\MérésBarossKerék.html";
                Module_Excel.Megnyitás(hely);
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


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk
                Beolvassa.Enabled = false;

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                }
                else
                {
                }

                melyikelem = 168;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    // Beolvasás
                    Beolvassa.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    //

                }
                // módosítás 3

                if (MyF.Vanjoga(melyikelem, 3))
                {
                    //

                }


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


        void Státuscombo_Feltöltés()
        {
            Státuscombo.Items.Clear();
            Státuscombo.Items.Add("");
            foreach (string Elem in Enum.GetNames(typeof(MyEn.Eszterga_Állapot_Státus)))
            {
                Státuscombo.Items.Add(Elem);
            }

        }
        #endregion

        #region Beolvasás


        private void Beolvassa_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Pontosvesszővel tagolt|*.csv",
                    Multiselect = true
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                Holtart.Be();

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM mérés ";
                AdatokMérés = KézMérés.Lista_Adatok(hely, jelszó, szöveg);

                string Választás = "TtsssslssssslissTtdddddddddddddddddddTtdddddddddddddddddddddl";

                List<string> CSVAdatok = new List<string>();
                //A kijelölt fájlok beolvasása
                for (int j = 0; j < OpenFileDialog1.FileNames.Count(); j++)
                {
                    using (StreamReader sr = new StreamReader(OpenFileDialog1.FileNames[j]))
                    {
                        while (!sr.EndOfStream)
                        {
                            string EgySor = sr.ReadLine();
                            CSVAdatok.Add(EgySor);
                            Holtart.Lép();
                        }
                    }
                }

                int hibák = 0;
                List<string> szövegGy = new List<string>();
                foreach (string Elem in CSVAdatok)
                {
                    string[] Darabol = Elem.Split(';');
                    if (Darabol[0].Trim() != "DATE_FILE")
                    {
                        string szövegvált = "";
                        string ideig = "";
                        string Megjegyzés = "Hibák:";
                        long ID = 0;
                        for (int i = 0; i < Darabol.Length; i++)
                        {
                            ideig = (Darabol[i].Trim());
                            string ideig1 = Választás[i].ToString();
                            switch (Választás.Substring(i, 1))
                            {
                                case "i":
                                    ideig = VálaszInt(Darabol[i].Trim());
                                    if (ideig.Trim() == "Hiba")
                                    {
                                        Megjegyzés += $"{i},";
                                        szövegvált += "0, ";
                                    }
                                    else
                                    {
                                        szövegvált += $"{ideig}, ";
                                    }
                                    break;

                                case "T":
                                    ideig = VálaszDátum(Darabol[i].Trim());
                                    if (ideig.Trim() == "Hiba")
                                    {
                                        Megjegyzés += $"{i},";
                                        szövegvált += "'1900.01.01";
                                    }
                                    else
                                    {
                                        szövegvált += $"'{ideig} ";
                                    }
                                    break;

                                case "t":
                                    ideig = VálaszIdő(Darabol[i].Trim());
                                    if (ideig.Trim() == "Hiba")
                                    {
                                        Megjegyzés += $"{i},";
                                        szövegvált += " 00:00:00', ";
                                    }
                                    else
                                    {
                                        szövegvált += $" {ideig}', ";
                                    }
                                    break;

                                case "s":
                                    ideig = VálaszString(Darabol[i].Trim());
                                    if (ideig.Trim() == "Hiba")
                                    {
                                        Megjegyzés += $"{i},";
                                        szövegvált += "'_', ";
                                    }
                                    else
                                    {
                                        szövegvált += $"'{ideig}', ";
                                    }
                                    break;

                                case "l":
                                    ideig = VálaszLong(Darabol[i].Trim());
                                    if (ideig.Trim() == "Hiba")
                                    {
                                        Megjegyzés += $"{i},";
                                        szövegvált += "0, ";
                                    }
                                    else
                                    {
                                        szövegvált += $"{ideig}, ";
                                        if (i == 60)
                                            ID = long.Parse(ideig);
                                    }

                                    break;


                                case "d":
                                    ideig = VálaszDouble(Darabol[i].Trim());
                                    if (ideig.Trim() == "Hiba")
                                    {
                                        Megjegyzés += $"{i},";
                                        szövegvált += "0, ";
                                    }
                                    else
                                    {
                                        szövegvált += $"{Math.Round(double.Parse(ideig), 4).ToString().Replace(',', '.')}, ";
                                    }
                                    break;
                            }
                        }
                        if (szövegvált != "Hibák:") hibák++;
                        if (ID != 0)
                        {
                            Adat_Baross_Mérési_Adatok ElemMérés = (from a in AdatokMérés
                                                                   where a.Eszterga_Id == ID
                                                                   select a).FirstOrDefault();

                            if (ElemMérés != null)
                            {
                                //Ha van kitöröljük
                                szöveg = $"DELETE FROM mérés WHERE Eszterga_Id={ID}";
                                MyA.ABtörlés(hely, jelszó, szöveg);
                            }
                            szöveg = "INSERT INTO mérés (Dátum_1, Azonosító, Tulajdonos, kezelő, Profil, Profil_szám, Kerékpár_szám, Adat_1, Adat_2,";
                            szöveg += " Adat_3, Típus_Eszt, KMU, Pozíció_Eszt, Tengely_Aznosító, Adat_4, Dátum_2, Táv_Belső_Futó_K, Táv_Nyom_K, Delta_K,";
                            szöveg += " B_Átmérő_K, J_Átmérő_K, B_Axiális_K, J_Axiális_K, B_Radiális_K, J_Radiális_K, B_Nyom_Mag_K, J_Nyom_Mag_K, B_Nyom_Vast_K,";
                            szöveg += " J_nyom_Vast_K, B_Nyom_Vast_B_K, J_nyom_Vast_B_K, B_QR_K, J_QR_K, B_Profilhossz_K, J_Profilhossz_K, Dátum_3, Táv_Belső_Futó_Ú,";
                            szöveg += " Táv_Nyom_Ú, Delta_Ú, B_Átmérő_Ú, J_Átmérő_Ú, B_Axiális_Ú, J_Axiális_Ú, B_Radiális_Ú, J_Radiális_Ú, B_Nyom_Mag_Ú, J_Nyom_Mag_Ú,";
                            szöveg += " B_Nyom_Vast_Ú, J_nyom_Vast_Ú, B_Nyom_Vast_B_Ú, J_nyom_Vast_B_Ú, B_QR_Ú, J_QR_Ú, B_Szög_Ú, J_Szög_Ú, B_Profilhossz_Ú,";
                            szöveg += $" J_Profilhossz_Ú, Eszterga_Id, Megjegyzés, Státus) Values ({szövegvált} '{Megjegyzés}', 1)";

                            szövegGy.Add(szöveg);
                        }
                        Holtart.Lép();
                    }
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);

                Holtart.Ki();
                MessageBox.Show("Az adatok konvertálása megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Töröljük a fájlt
                for (int j = 0; j < OpenFileDialog1.FileNames.Count(); j++)
                {
                    Delete(OpenFileDialog1.FileNames[j]);
                }

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


        string VálaszInt(string Részelem)
        {
            string válasz;
            if (int.TryParse(Részelem, out int Érték))
                válasz = Érték.ToString();
            else
                válasz = "Hiba";

            return válasz;
        }

        string VálaszLong(string Részelem)
        {
            string válasz;
            if (long.TryParse(Részelem, out long Érték))
                válasz = Érték.ToString();
            else
                válasz = "Hiba";

            return válasz;
        }

        string VálaszDátum(string Részelem)
        {
            string válasz;
            string[] darabol = Részelem.Split('/');
            if (darabol.Length < 3)
            {
                válasz = "Hiba";
            }
            else
            {
                válasz = $"{darabol[2]}.{darabol[1]}.{darabol[0]}";
                if (DateTime.TryParse(válasz, out DateTime Érték))
                    válasz = Érték.ToString("yyyy.MM.dd");
                else
                    válasz = "Hiba";
            }
            return válasz;
        }

        string VálaszIdő(string Részelem)
        {
            string válasz;
            if (DateTime.TryParse(Részelem, out DateTime Érték))
                válasz = Érték.ToString("HH:mm:ss");
            else
                válasz = "Hiba";

            return válasz;
        }

        string VálaszDouble(string Részelem)
        {
            string válasz = Részelem.Replace('.', ',');
            if (double.TryParse(válasz, out double Érték))
                válasz = Érték.ToString();
            else
                válasz = "Hiba";

            return válasz;
        }

        string VálaszString(string Részelem)
        {
            string válasz;
            if (Részelem.Trim() != "")
                válasz = MyF.Szöveg_Tisztítás(Részelem);
            else
                válasz = "_";

            return válasz;
        }


        #endregion

        private void Tábla_Listázás_Click(object sender, EventArgs e)
        {
            Listázás_Tábla();
        }


        private void Listázás_Tábla()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
                string jelszó = "RónaiSándor";
                Holtart.Be();
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 60;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum_1"; Tábla.Columns[0].Width = 180;
                Tábla.Columns[1].HeaderText = "Azonosító"; Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Tulajdonos"; Tábla.Columns[2].Width = 120;
                Tábla.Columns[3].HeaderText = "kezelő"; Tábla.Columns[3].Width = 120;
                Tábla.Columns[4].HeaderText = "Profil"; Tábla.Columns[4].Width = 120;
                Tábla.Columns[5].HeaderText = "Profil_szám"; Tábla.Columns[5].Width = 120;
                Tábla.Columns[6].HeaderText = "Gyári_szám"; Tábla.Columns[6].Width = 120;
                Tábla.Columns[7].HeaderText = "Adat_1"; Tábla.Columns[7].Width = 120;
                Tábla.Columns[8].HeaderText = "Típus"; Tábla.Columns[8].Width = 120;
                Tábla.Columns[9].HeaderText = "Adat_3"; Tábla.Columns[9].Width = 120;

                Tábla.Columns[10].HeaderText = "Típus_Eszt"; Tábla.Columns[10].Width = 120;
                Tábla.Columns[11].HeaderText = "KMU"; Tábla.Columns[11].Width = 120;
                Tábla.Columns[12].HeaderText = "Pozíció_Eszt"; Tábla.Columns[12].Width = 120;
                Tábla.Columns[13].HeaderText = "Tengely_Aznosító"; Tábla.Columns[13].Width = 120;
                Tábla.Columns[14].HeaderText = "Adat_4"; Tábla.Columns[14].Width = 120;
                Tábla.Columns[15].HeaderText = "Dátum_2"; Tábla.Columns[15].Width = 120;
                Tábla.Columns[16].HeaderText = "Táv_Belső_Futó_K"; Tábla.Columns[16].Width = 120;
                Tábla.Columns[17].HeaderText = "Táv_Nyom_K"; Tábla.Columns[17].Width = 120;
                Tábla.Columns[18].HeaderText = "Delta_K"; Tábla.Columns[18].Width = 120;
                Tábla.Columns[19].HeaderText = "B_Átmérő_K"; Tábla.Columns[19].Width = 120;

                Tábla.Columns[20].HeaderText = "J_Átmérő_K"; Tábla.Columns[20].Width = 120;
                Tábla.Columns[21].HeaderText = "B_Axiális_K"; Tábla.Columns[21].Width = 120;
                Tábla.Columns[22].HeaderText = "J_Axiális_K"; Tábla.Columns[22].Width = 120;
                Tábla.Columns[23].HeaderText = "B_Radiális_K"; Tábla.Columns[23].Width = 120;
                Tábla.Columns[24].HeaderText = "J_Radiális_K"; Tábla.Columns[24].Width = 120;
                Tábla.Columns[25].HeaderText = "B_Nyom_Mag_K"; Tábla.Columns[25].Width = 120;
                Tábla.Columns[26].HeaderText = "J_Nyom_Mag_K"; Tábla.Columns[26].Width = 120;
                Tábla.Columns[27].HeaderText = "B_Nyom_Vast_K"; Tábla.Columns[27].Width = 120;
                Tábla.Columns[28].HeaderText = "J_nyom_Vast_K"; Tábla.Columns[28].Width = 120;
                Tábla.Columns[29].HeaderText = "B_Nyom_Vast_B_K"; Tábla.Columns[29].Width = 120;

                Tábla.Columns[30].HeaderText = "J_nyom_Vast_B_K"; Tábla.Columns[30].Width = 120;
                Tábla.Columns[31].HeaderText = "B_QR_K"; Tábla.Columns[31].Width = 120;
                Tábla.Columns[32].HeaderText = "J_QR_K"; Tábla.Columns[32].Width = 120;
                Tábla.Columns[33].HeaderText = "B_Profilhossz_K"; Tábla.Columns[33].Width = 120;
                Tábla.Columns[34].HeaderText = "J_Profilhossz_K "; Tábla.Columns[34].Width = 120;
                Tábla.Columns[35].HeaderText = "Dátum_3"; Tábla.Columns[35].Width = 120;
                Tábla.Columns[36].HeaderText = "Táv_Belső_Futó_Ú"; Tábla.Columns[36].Width = 120;
                Tábla.Columns[37].HeaderText = "Táv_Nyom_Ú"; Tábla.Columns[37].Width = 120;
                Tábla.Columns[38].HeaderText = "Delta_Ú "; Tábla.Columns[38].Width = 120;
                Tábla.Columns[39].HeaderText = "B_Átmérő_Ú"; Tábla.Columns[39].Width = 120;

                Tábla.Columns[40].HeaderText = "J_Átmérő_Ú"; Tábla.Columns[40].Width = 120;
                Tábla.Columns[41].HeaderText = "B_Axiális_Ú"; Tábla.Columns[41].Width = 120;
                Tábla.Columns[42].HeaderText = "J_Axiális_Ú"; Tábla.Columns[42].Width = 120;
                Tábla.Columns[43].HeaderText = "B_Radiális_Ú"; Tábla.Columns[43].Width = 120;
                Tábla.Columns[44].HeaderText = "J_Radiális_Ú"; Tábla.Columns[44].Width = 120;
                Tábla.Columns[45].HeaderText = "B_Nyom_Mag_Ú"; Tábla.Columns[45].Width = 120;
                Tábla.Columns[46].HeaderText = "J_Nyom_Mag_Ú"; Tábla.Columns[46].Width = 120;
                Tábla.Columns[47].HeaderText = "B_Nyom_Vast_Ú"; Tábla.Columns[47].Width = 120;
                Tábla.Columns[48].HeaderText = "J_nyom_Vast_Ú"; Tábla.Columns[48].Width = 120;
                Tábla.Columns[49].HeaderText = "B_Nyom_Vast_B_Ú"; Tábla.Columns[49].Width = 120;

                Tábla.Columns[50].HeaderText = "J_nyom_Vast_B_Ú"; Tábla.Columns[50].Width = 120;
                Tábla.Columns[51].HeaderText = "B_QR_Ú"; Tábla.Columns[51].Width = 120;
                Tábla.Columns[52].HeaderText = "J_QR_Ú"; Tábla.Columns[52].Width = 120;
                Tábla.Columns[53].HeaderText = "B_Szög_Ú"; Tábla.Columns[53].Width = 120;
                Tábla.Columns[54].HeaderText = "J_Szög_Ú"; Tábla.Columns[54].Width = 120;
                Tábla.Columns[55].HeaderText = "B_Profilhossz_Ú"; Tábla.Columns[55].Width = 120;
                Tábla.Columns[56].HeaderText = "J_Profilhossz_Ú"; Tábla.Columns[56].Width = 120;
                Tábla.Columns[57].HeaderText = "Eszterga_Id"; Tábla.Columns[57].Width = 120;
                Tábla.Columns[58].HeaderText = "Megjegyzés"; Tábla.Columns[58].Width = 120;
                Tábla.Columns[59].HeaderText = "Státus"; Tábla.Columns[59].Width = 120;

                string szöveg = $"SELECT * FROM mérés WHERE ";
                szöveg += $" [Dátum_1]>=#{Dátumtól.Value:M-d-yy} 00:00:0#";
                szöveg += $" and [Dátum_1]<#{dátumig.Value:M-d-yy} 23:59:0#";

                if (Státuscombo.Text.Trim() != "")
                {
                    MyEn.Eszterga_Állapot_Státus KiválasztottStátusz = (MyEn.Eszterga_Állapot_Státus)Enum.Parse(typeof(MyEn.Eszterga_Állapot_Státus), Státuscombo.Text.ToString());
                    szöveg += " AND  Státus=" + ((int)KiválasztottStátusz).ToString();
                }

                if (Pályaszám.Text.Trim() != "")
                {
                    szöveg += $" AND  azonosító='{Pályaszám.Text.Trim()}'";
                }
                szöveg += " ORDER BY Eszterga_Id";


                AdatokMérés = KézMérés.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Baross_Mérési_Adatok rekord in AdatokMérés)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[0].Value = rekord.Dátum_1.ToString();
                    Tábla.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Tulajdonos.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Kezelő.Trim();
                    Tábla.Rows[i].Cells[4].Value = rekord.Profil.Trim();
                    Tábla.Rows[i].Cells[5].Value = rekord.Profil_szám.ToString();
                    Tábla.Rows[i].Cells[6].Value = rekord.Kerékpár_szám.Trim();
                    Tábla.Rows[i].Cells[7].Value = rekord.Adat_1.Trim();
                    Tábla.Rows[i].Cells[8].Value = rekord.Adat_2.Trim();
                    Tábla.Rows[i].Cells[9].Value = rekord.Adat_3.Trim();

                    Tábla.Rows[i].Cells[10].Value = rekord.Típus_Eszt.Trim();
                    Tábla.Rows[i].Cells[11].Value = rekord.KMU.ToString();
                    Tábla.Rows[i].Cells[12].Value = rekord.Pozíció_Eszt.ToString();
                    Tábla.Rows[i].Cells[13].Value = rekord.Tengely_Aznosító.Trim();
                    Tábla.Rows[i].Cells[14].Value = rekord.Adat_4.ToString();
                    Tábla.Rows[i].Cells[15].Value = rekord.Dátum_2.ToString();
                    Tábla.Rows[i].Cells[16].Value = rekord.Táv_Belső_Futó_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[17].Value = rekord.Táv_Nyom_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[18].Value = rekord.Delta_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[19].Value = rekord.B_Átmérő_K.ToString().Replace(',', '.');

                    Tábla.Rows[i].Cells[20].Value = rekord.J_Átmérő_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[21].Value = rekord.B_Axiális_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[22].Value = rekord.J_Axiális_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[23].Value = rekord.B_Radiális_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[24].Value = rekord.J_Radiális_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[25].Value = rekord.B_Nyom_Mag_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[26].Value = rekord.J_Nyom_Mag_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[27].Value = rekord.B_Nyom_Vast_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[28].Value = rekord.J_nyom_Vast_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[29].Value = rekord.B_Nyom_Vast_B_K.ToString().Replace(',', '.');

                    Tábla.Rows[i].Cells[30].Value = rekord.J_nyom_Vast_B_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[31].Value = rekord.B_QR_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[32].Value = rekord.J_QR_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[33].Value = rekord.B_Profilhossz_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[34].Value = rekord.J_Profilhossz_K.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[35].Value = rekord.Dátum_3.ToString();
                    Tábla.Rows[i].Cells[36].Value = rekord.Táv_Belső_Futó_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[37].Value = rekord.Táv_Nyom_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[38].Value = rekord.Delta_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[39].Value = rekord.B_Átmérő_Ú.ToString().Replace(',', '.');


                    Tábla.Rows[i].Cells[40].Value = rekord.J_Átmérő_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[41].Value = rekord.B_Axiális_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[42].Value = rekord.J_Axiális_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[43].Value = rekord.B_Radiális_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[44].Value = rekord.J_Radiális_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[45].Value = rekord.B_Nyom_Mag_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[46].Value = rekord.J_Nyom_Mag_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[47].Value = rekord.B_Nyom_Vast_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[48].Value = rekord.J_nyom_Vast_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[49].Value = rekord.B_Nyom_Vast_B_Ú.ToString().Replace(',', '.');

                    Tábla.Rows[i].Cells[50].Value = rekord.J_nyom_Vast_B_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[51].Value = rekord.B_QR_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[52].Value = rekord.J_QR_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[53].Value = rekord.B_Szög_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[54].Value = rekord.J_Szög_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[55].Value = rekord.B_Profilhossz_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[56].Value = rekord.J_Profilhossz_Ú.ToString().Replace(',', '.');
                    Tábla.Rows[i].Cells[57].Value = rekord.Eszterga_Id.ToString();
                    Tábla.Rows[i].Cells[58].Value = rekord.Megjegyzés.Trim();
                    Tábla.Rows[i].Cells[59].Value = Enum.GetName(typeof(MyEn.Eszterga_Állapot_Státus), rekord.Státus);
                    Holtart.Lép();
                }
                TáblaSzínezés();

                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
                Holtart.Ki();
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

        private void TáblaSzínezés()
        {
            Holtart.Be(Tábla.Rows.Count + 1);
            foreach (DataGridViewRow Elem in Tábla.Rows)
            {
                if (Elem.Cells[59].Value != null)
                {
                    string tartalom = Elem.Cells[59].Value.ToStrTrim();
                    Color Háttér = Color.White;
                    Color Betű = Color.Black;
                    Font Stílus = new Font("Arial Narrow", 12f);
                    bool kell = false;
                    switch (tartalom)
                    {
                        case "Beolvasott":
                            break;
                        case "Hibás":
                            Háttér = Color.Red;
                            Betű = Color.White;
                            Stílus = new Font("Arial Narrow", 12f, FontStyle.Italic);
                            kell = true;
                            break;
                        case "Ellenőrzött":
                            Háttér = Color.Aqua;
                            Betű = Color.Black;
                            Stílus = new Font("Arial Narrow", 12f);
                            kell = true;
                            break;
                        case "Villamos_Áttöltött":
                            Háttér = Color.Green;
                            Betű = Color.White;
                            Stílus = new Font("Arial Narrow", 12f, FontStyle.Bold);
                            kell = true;
                            break;
                        case "Törölt":
                            Betű = Color.White;
                            Háttér = Color.IndianRed;
                            Stílus = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                            kell = true;
                            break;
                    }
                    if (kell)
                    {
                        Elem.DefaultCellStyle.BackColor = Háttér;
                        Elem.DefaultCellStyle.ForeColor = Betű;
                        Elem.DefaultCellStyle.Font = Stílus;
                    }
                    Holtart.Lép();
                }

            }
        }

        private void ExcelKimenet_Click(object sender, EventArgs e)
        {
            try
            {

                if (Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Kerékeszterga_mérési_listája_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, true);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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


        private void Ellenőrzések_Click(object sender, EventArgs e)
        {
            EllenőrzésELj();
            Listázás_Tábla();
        }

        private void EllenőrzésELj()
        {
            try
            {
                if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("Nincs a táblázatban ellenőrzendő elem.");

                Holtart.Be();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = $"SELECT * FROM tábla where objektumfajta='V.KERÉKPÁR' order by pozíció ";

                Kezelő_Kerék_Tábla kéz = new Kezelő_Kerék_Tábla();
                List<Adat_Kerék_Tábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);


                hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";

                Kezelő_Jármű Kéz_Jármű = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = Kéz_Jármű.Lista_Adatok(hely, jelszó, szöveg);


                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
                jelszó = "RónaiSándor";

                List<string> szövegGy = new List<string>();
                foreach (DataGridViewRow Elem in Tábla.Rows)
                {
                    //Csak azokat ellenőrizzük ami még nincs áttöltve, és nincs törölve
                    if (!(Elem.Cells[59].Value.ToStrTrim() == "Villamos_Áttöltött" || Elem.Cells[59].Value.ToStrTrim() == "Törölt"))
                    {
                        string Létezik_Psz = (from a in AdatokJármű
                                              where a.Azonosító.Trim() == Elem.Cells[1].Value.ToStrTrim()
                                              select a.Azonosító).FirstOrDefault();


                        string Létezik_kerék = (from a in Adatok
                                                where a.Kerékgyártásiszám == Elem.Cells[6].Value.ToStrTrim()
                                                select a.Kerékmegnevezés).FirstOrDefault();


                        string Létezik_beépítés = (from a in Adatok
                                                   where (a.Azonosító.Trim() == Elem.Cells[1].Value.ToString().Trim() && a.Kerékgyártásiszám == Elem.Cells[6].Value.ToStrTrim())
                                                   select a.Azonosító).FirstOrDefault();

                        if (Létezik_Psz != null && Létezik_kerék != null && Létezik_beépítés != null)
                        {
                            szöveg = "UPDATE mérés SET Státus=4 WHERE Eszterga_Id=" + Elem.Cells[57].Value.ToString();
                            szövegGy.Add(szöveg);
                        }
                        else
                        {
                            string válasz = " Hiba:";
                            if (Létezik_Psz == null || Létezik_Psz.Trim() == "") válasz += " Pályaszám, ";
                            if (Létezik_kerék == null) válasz += " Kerék, ";
                            if (Létezik_beépítés == null || Létezik_beépítés.Trim() == "") válasz += " Beépítés, ";

                            string előző = Elem.Cells[58].Value.ToStrTrim();
                            //Ha volt már ilyen szöveg akkor nem rögzítjük újra
                            if (!előző.Contains(válasz.Trim()))
                            {
                                szöveg = "UPDATE mérés SET Státus=2, ";
                                szöveg += $" Megjegyzés='{előző + "\n" + válasz.Trim()}'";
                                szöveg += " WHERE Eszterga_Id=" + Elem.Cells[57].Value.ToString();
                                szövegGy.Add(szöveg);
                            }
                        }
                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGy);
                    Holtart.Lép();
                }
                Holtart.Ki();
                Listázás_Tábla();
                MessageBox.Show("Az adatok ellenőrzése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve elem törlésre.");
                Holtart.Be();


                foreach (DataGridViewRow Elem in Tábla.SelectedRows)
                {
                    Státus_Állítás(9, long.Parse(Elem.Cells[57].Value.ToString()));
                    Holtart.Lép();
                }
                Holtart.Ki();
                MessageBox.Show("Az adatok törlése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        void Státus_Állítás(int státus, long EsztergaID)
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
            string jelszó = "RónaiSándor";
            string szöveg = "SELECT * FROM mérés ";
            AdatokMérés = KézMérés.Lista_Adatok(hely, jelszó, szöveg);
            Adat_Baross_Mérési_Adatok ElemMérés = (from a in AdatokMérés
                                                   where a.Eszterga_Id == EsztergaID
                                                   select a).FirstOrDefault();

            if (ElemMérés != null)
            {
                szöveg = $"UPDATE mérés SET Státus={státus} WHERE Eszterga_Id={EsztergaID}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
        }



        private void Villamos_programba_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("Nincs a táblázatban rögzítendő elem.");

                Holtart.Be();

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = $"SELECT * FROM tábla where objektumfajta='V.KERÉKPÁR' order by pozíció ";
                Kezelő_Kerék_Tábla kéz = new Kezelő_Kerék_Tábla();
                List<Adat_Kerék_Tábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{dátumig.Value.Year}\telepikerék.mdb";
                szöveg = $"SELECT * FROM esztergatábla order by mikor desc";
                Kezelő_Kerék_Eszterga KézEszt = new Kezelő_Kerék_Eszterga();
                List<Adat_Kerék_Eszterga> AdatokEszt = KézEszt.Lista_Adatok(hely, jelszó, szöveg);
                if (Dátumtól.Value.Year != dátumig.Value.Year)
                {
                    hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{dátumig.Value.Year - 1}\telepikerék.mdb";
                    List<Adat_Kerék_Eszterga> AdatokEszt1 = KézEszt.Lista_Adatok(hely, jelszó, szöveg);
                    if (AdatokEszt1 != null) AdatokEszt.AddRange(AdatokEszt1);
                }

                List<string> szövegGy = new List<string>();
                foreach (DataGridViewRow Elem in Tábla.Rows)
                {
                    // rögzítjük az adatokat, ha későbbi az esztergálás csak akkor rögzítjük
                    if (!DateTime.TryParse(Elem.Cells[0].Value.ToString(), out DateTime D_Mikor))
                        D_Mikor = new DateTime(1900, 1, 1);
                    if (!long.TryParse(Elem.Cells[11].Value.ToStrTrim(), out long kmu))
                        kmu = 0;
                    string azonosító = Elem.Cells[1].Value.ToStrTrim();

                    DateTime EgyikElem = (from a in AdatokEszt
                                          where a.Azonosító == azonosító
                                          orderby a.Eszterga
                                          select a.Eszterga).LastOrDefault();
                    if (EgyikElem == null)
                        EgyikElem = new DateTime(1900, 1, 1);

                    if (EgyikElem < D_Mikor)
                    {
                        //Esztergálás tényének rögzítése
                        szöveg = "INSERT INTO esztergatábla (eszterga, mikor, módosító, azonosító, kmu)  VALUES (";
                        szöveg += $"'{D_Mikor:yyyy.MM.dd}', ";
                        szöveg += $"'{DateTime.Now}', ";
                        szöveg += $"'{Program.PostásNév.Trim()}', ";
                        szöveg += $"'{azonosító}', ";
                        szöveg += $"{kmu} )";
                        szövegGy.Add(szöveg);

                        //Kerék méret adatok rögzítése
                        Adat_Kerék_Tábla Kerék = (from a in Adatok
                                                  where a.Azonosító.Trim() == azonosító && a.Kerékgyártásiszám == Elem.Cells[6].Value.ToStrTrim()
                                                  select a).FirstOrDefault();

                        if (!double.TryParse(Elem.Cells[39].Value.ToString().Replace('.', ','), out double Átmérő))
                            Átmérő = 0;
                        Átmérő = Math.Floor(Átmérő);
                        if (Kerék != null)
                        {
                            szöveg = "INSERT INTO keréktábla  (Azonosító, pozíció, kerékberendezés, kerékgyártásiszám, állapot, méret, mikor, Módosító, Oka, SAP) VALUES (";
                            szöveg += $"'{azonosító}', ";
                            szöveg += $"'{Kerék.Pozíció}', ";
                            szöveg += $"'{Kerék.Kerékberendezés}', ";
                            szöveg += $"'{Kerék.Kerékgyártásiszám}', ";
                            szöveg += "'1', ";
                            szöveg += Átmérő.ToString() + ", ";
                            szöveg += $"'{D_Mikor}', ";
                            szöveg += $"'{Program.PostásNév.Trim()}', ";
                            szöveg += "'Esztergálás Aut', 0 )";
                            szövegGy.Add(szöveg);
                        }
                    }

                    Státus_állítás_Baross(Elem.Cells[1].Value.ToStrTrim(), 7, DateTime.Now);
                    Státus_Állítás(7, long.Parse(Elem.Cells[57].Value.ToString()));
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);

                Holtart.Ki();
                Listázás_Tábla();
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

        public void Státus_állítás_Baross(string Pályaszám, int Státus_Lesz, DateTime Dátum)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Year}_Igény.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"UPDATE igény SET státus={Státus_Lesz}";
                szöveg += $"   WHERE státus=2 AND pályaszám Like '%{Pályaszám.Trim()}%'";
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

        Ablak_Eszterga_Adatok_Javítás Új_Ablak_Eszterga_Adatok_Javítás;
        private void Adat_Javítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count < 1)
                    throw new HibásBevittAdat("A táblázat nem tartalmaz adatot.");
                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");

                if (!long.TryParse(Tábla.SelectedRows[0].Cells[57].Value.ToString(), out long ID))
                    throw new HibásBevittAdat("A táblázat nem tartalmaz adatot.");

                Új_Ablak_Eszterga_Adatok_Javítás?.Close();


                Új_Ablak_Eszterga_Adatok_Javítás = new Ablak_Eszterga_Adatok_Javítás(ID);
                Új_Ablak_Eszterga_Adatok_Javítás.FormClosed += Ablak_Eszterga_Adatok_Javítás_Closed;
                Új_Ablak_Eszterga_Adatok_Javítás.Változás += Listázás_Tábla;
                Új_Ablak_Eszterga_Adatok_Javítás.Show();

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

        private void Ablak_Eszterga_Adatok_Javítás_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Adatok_Javítás = null;
        }

        private void Ablak_Eszterga_Adatok_Baross_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Adatok_Javítás?.Close();
        }

        private void Státuscombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Státuscombo.Text.Trim() == "Ellenőrzött")
                Villamos_programba.Visible = true;
            else
                Villamos_programba.Visible = false;
            Listázás_Tábla();
        }
    }
}
