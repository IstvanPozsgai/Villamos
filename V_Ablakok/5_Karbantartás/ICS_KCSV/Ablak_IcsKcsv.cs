﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok.ICS_KCSV;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_IcsKcsv
    {
        string _hely, _jelszó, _szöveg, _fájlexc;
        int utolsósor;
        readonly string helyICS = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
        long HavikmICS = 0;
        int Hónapok = 0;

        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű2 KézJármű2 = new Kezelő_Jármű2();
        readonly Kezelő_Jármű2ICS KézJármű2ICS = new Kezelő_Jármű2ICS();
        readonly Kezelő_T5C5_Kmadatok KézICSKmadatok = new Kezelő_T5C5_Kmadatok("ICS");
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
        readonly Kezelő_Főkönyv_Zser_Km KézKorr = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();

        List<Adat_T5C5_Kmadatok> AdatokICSKmadatok = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Jármű_2> AdatokJármű2 = new List<Adat_Jármű_2>();
        List<Adat_Jármű_2ICS> AdatokJármű2ICS = new List<Adat_Jármű_2ICS>();
        List<Adat_Kerék_Mérés> AdatokMérés = new List<Adat_Kerék_Mérés>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Vezénylés> AdatokVezénylés = new List<Adat_Vezénylés>();
        List<Adat_Főkönyv_Zser_Km> AdatokZserKm = new List<Adat_Főkönyv_Zser_Km>();

        public List<Adat_Jármű> AdatokFőJármű = new List<Adat_Jármű>();
        public Ablak_IcsKcsv()
        {
            InitializeComponent();
        }


        private void IcsKcsv_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();
                Pályaszám_feltöltés();
                CiklusListaFeltöltés();

                // létrehozzuk a  könyvtárat
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Kmfutástábla(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\" + DateTime.Today.Year;
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely += @"\telepikerék.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Méréstáblakerék(hely);

                Fülek.SelectedIndex = 0;
                Fülekkitöltése();

                Jogosultságkiosztás();

                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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


        #region Alap

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                E_rögzít.Enabled = false;

                Utolsó_V_rögzítés.Enabled = false;
                Töröl.Enabled = false;
                SAP_adatok.Enabled = false;


                Btn_Vezénylésbeírás.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Töröl.Visible = true;
                }
                else
                {
                    Töröl.Visible = false;
                }

                melyikelem = 113;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    E_rögzít.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Utolsó_V_rögzítés.Enabled = true;
                    Töröl.Enabled = true;
                    SAP_adatok.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Btn_Vezénylésbeírás.Enabled = true;
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


        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Tulajdonság_ICS.html";
            MyE.Megnyitás(hely);
        }


        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
            Pályaszám_feltöltés();
        }


        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();
                if (!Program.Postás_Vezér && Cmbtelephely.Text.Trim() == "") return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg;

                if (Program.Postás_Vezér)
                {
                    szöveg = "Select * FROM Állománytábla WHERE  törölt=0 AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY azonosító";
                }
                else
                {
                    szöveg = $"Select * FROM Állománytábla WHERE Üzem='{Cmbtelephely.Text.Trim()}' AND ";
                    szöveg += " törölt=0 AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY azonosító";
                }

                // feltöltjük az összes pályaszámot a Comboba

                Pályaszám.BeginUpdate();
                Pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                Pályaszám.EndUpdate();
                Pályaszám.Refresh();
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


        private void Pályaszámkereső_Click(object sender, EventArgs e)
        {
            Frissít();
        }


        private void Frissít()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "")
                    return;

                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            Kiirjaalapadatokat();
                            Hétnapjai_feltöltése();
                            break;
                        }
                    case 1:
                        {
                            break;
                        }

                    case 3:
                        {
                            Kiüríti_lapfül();
                            Kiirjaatörténelmet();
                            break;
                        }
                    case 4:
                        {
                            Kiüríti_lapfül();
                            Kiirjaatörténelmet();
                            break;
                        }
                    case 5:
                        {
                            Ütemezettkocsik();
                            break;
                        }
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


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            // alapadatok
                            // ürítjük a mezőket
                            Típus_text.Text = "";
                            Státus_text.Text = "";
                            Miótaáll_text.Text = "";
                            Takarítás_text.Text = "";
                            Főmérnökség_text.Text = "";
                            Járműtípus_text.Text = "";
                            Combo_E2.Text = "";
                            Combo_E3.Text = "";

                            Kiirjaalapadatokat();
                            Hétnapjai_feltöltése();
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            CiklusrendCombo_feltöltés();
                            Vizsgsorszámcombofeltölés();
                            Üzemek_listázása();
                            break;
                        }
                    case 3:
                        {
                            Kiirjaatörténelmet();
                            break;
                        }
                    case 4:
                        {

                            Pszlista();
                            Telephelylista();
                            break;
                        }
                    case 5:
                        {
                            Ütemezettkocsik();
                            break;
                        }
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


        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frissít();
            Kiüríti_lapfül();
        }

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Fülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }


        #endregion


        #region alapadatok lapfül

        private void Kiirjaalapadatokat()
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Pályaszám.Text.Trim() == "") return;
                JárműListaFeltöltés();
                Jármű2ListaFeltöltés();
                Jármű2ICSListaFeltöltés();

                // ürítjük a mezőket
                Típus_text.Text = "";
                Státus_text.Text = "";
                Miótaáll_text.Text = "";

                Takarítás_text.Text = "";
                Főmérnökség_text.Text = "";
                Járműtípus_text.Text = "";

                Combo_E2.Text = "";
                Combo_E3.Text = "";

                Adat_Jármű rekord = (from a in AdatokJármű
                                     where a.Azonosító == Pályaszám.Text.Trim()
                                     select a).FirstOrDefault();


                if (rekord != null)
                {
                    Típus_text.Text = rekord.Típus.Trim();
                    Járműtípus_text.Text = rekord.Valóstípus2.Trim();
                    Főmérnökség_text.Text = rekord.Valóstípus.Trim();
                    switch (rekord.Státus)
                    {
                        case 0:
                            {
                                Státus_text.Text = "Nincs hibája";
                                break;
                            }
                        case 1:
                            {
                                Státus_text.Text = "Szabad";
                                break;
                            }
                        case 2:
                            {
                                Státus_text.Text = "Beállóba kért";
                                break;
                            }
                        case 3:
                            {
                                Státus_text.Text = "Beállóba adott";
                                break;
                            }
                        case 4:
                            {
                                Státus_text.Text = "Benn maradó";
                                break;
                            }
                    }
                    if (rekord.Miótaáll == null || rekord.Miótaáll == new DateTime(1900, 1, 1))
                        Miótaáll_text.Text = "";
                    else
                        Miótaáll_text.Text = rekord.Miótaáll.ToString("yyyy.MM.dd");
                }

                Adat_Jármű_2 Elem2 = (from a in AdatokJármű2
                                      where a.Azonosító == Pályaszám.Text.Trim()
                                      select a).FirstOrDefault();
                if (Elem2 != null) Takarítás_text.Text = Elem2.Takarítás.ToStrTrim();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos2ICS.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.VillamostáblaICS(hely);

                Adat_Jármű_2ICS Elem2ICS = (from a in AdatokJármű2ICS
                                            where a.Azonosító == Pályaszám.Text.Trim()
                                            select a).FirstOrDefault();

                if (Elem2ICS != null)
                {
                    int E2_sorszám = Elem2ICS.E2;
                    if (E2_sorszám == 0)
                        Combo_E2.Text = "";
                    else
                        Combo_E2.Text = Combo_E2.Items[E2_sorszám - 1].ToString();


                    int E3_sorszám = Elem2ICS.E3;
                    if (E3_sorszám == 0)
                        Combo_E3.Text = "";
                    else
                        Combo_E3.Text = Combo_E3.Items[E3_sorszám - 1].ToString();
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


        private void Hétnapjai_feltöltése()
        {
            Combo_E2.Items.Clear();
            Combo_E2.Items.Add("Hétfő");
            Combo_E2.Items.Add("Kedd");
            Combo_E2.Items.Add("Szerda");
            Combo_E2.Items.Add("Csütörtök");
            Combo_E2.Items.Add("Péntek");
            Combo_E2.Items.Add("Szombat");
            Combo_E2.Items.Add("Vasárnap");

            Combo_E3.Items.Clear();
            Combo_E3.Items.Add("Hétfő");
            Combo_E3.Items.Add("Kedd");
            Combo_E3.Items.Add("Szerda");
            Combo_E3.Items.Add("Csütörtök");
            Combo_E3.Items.Add("Péntek");
            Combo_E3.Items.Add("Szombat");
            Combo_E3.Items.Add("Vasárnap");
        }


        private void E_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") return;

                // leellenőrizzük, hogy létezik-e a kocsi
                JárműListaFeltöltés();
                Adat_Jármű ElemJármű = (from a in AdatokJármű
                                        where a.Azonosító == Pályaszám.Text.Trim() &&
                                        a.Törölt == false
                                        select a).FirstOrDefault();
                string hely = $@"{Application.StartupPath}+\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos.mdb";
                string jelszó = "pozsgaii";


                if (ElemJármű == null)
                {
                    if (MessageBox.Show("Nincs ilyen jármű a telephelyen! Mégis rögzítjük?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        return;
                }

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos2ICS.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.VillamostáblaICS(hely);

                int e2 = 0;
                int e3 = 0;
                if (Combo_E3.Text.Trim() != "")
                {

                    for (int i = 0; i < 7; i++)
                    {
                        if (Combo_E3.Items[i].ToStrTrim() == Combo_E3.Text.Trim())
                        {
                            e3 = i + 1;
                            break;
                        }
                    }
                }
                if (Combo_E2.Text.Trim() != "")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (Combo_E2.Items[i].ToStrTrim() == Combo_E2.Text.Trim())
                        {
                            e2 = i + 1;
                            break;
                        }
                    }
                }
                string szöveg = $"SELECT * FROM Állománytábla";
                Kezelő_Jármű2 KézVizsgálat = new Kezelő_Jármű2();
                List<Adat_Jármű_2> AdatokVizsgálat = KézVizsgálat.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Jármű_2 ElemVizsgálat = (from a in AdatokVizsgálat
                                              where a.Azonosító == Pályaszám.Text.Trim()
                                              select a).FirstOrDefault();
                if (ElemVizsgálat != null)
                {
                    // módosítás
                    szöveg = $"UPDATE Állománytábla  SET E2='{e2}', E3='{e3}'";
                    szöveg += $" WHERE azonosító='{Pályaszám.Text.Trim()}'";
                }
                else
                {
                    // új adat
                    szöveg = "INSERT INTO Állománytábla  (azonosító, takarítás, E2, E3 ) VALUES (";
                    szöveg += $"'{Pályaszám.Text.Trim()}', '1900.01.01',{e2}, {e3}";
                    szöveg += e2.ToString() + ", ";
                    szöveg += e3.ToString() + ")";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Pályaszám_ellenőrzés();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Pályaszám_ellenőrzés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos.mdb";
                string jelszó = "pozsgaii";

                string hely2 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos2ICS.mdb";

                string szöveg = "SELECT * FROM állománytábla  ORDER BY Azonosító";

                Kezelő_Ics Kéz = new Kezelő_Ics();
                List<Adat_ICS> ICS = Kéz.Lista_Adatok(hely2, jelszó, szöveg);

                Kezelő_Jármű KézJÁRMŰ = new Kezelő_Jármű();
                List<Adat_Jármű> Jármű = KézJÁRMŰ.Lista_Adatok(hely, jelszó, szöveg);


                foreach (Adat_ICS rekord in ICS)
                {
                    List<Adat_Jármű> Szűrt = Jármű.Where(e => e.Azonosító.Trim() == rekord.Azonosító.Trim()).ToList();
                    if (Szűrt.Count < 1)
                    {
                        szöveg = $"DELETE FROM állománytábla WHERE azonosító='{rekord.Azonosító.Trim()}'";
                        MyA.ABtörlés(hely2, jelszó, szöveg);
                    }
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


        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "E2-E3 napok listájának készítése",
                    FileName = "E2-E3_tábla_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos2ICS.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "Select * FROM állománytábla";

                Kezelő_Ics Kéz = new Kezelő_Ics();
                List<Adat_ICS> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int sor;
                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();
                string munkalap = "7 napos";
                MyE.Munkalap_átnevezés("Munka1", munkalap);
                MyE.Új_munkalap("3 napos");
                MyE.Munkalap_aktív(munkalap);


                for (int i = 1; i <= 2; i++)
                {
                    if (i == 1)
                        munkalap = "7 napos";
                    else
                        munkalap = "3 napos";

                    MyE.Munkalap_aktív(munkalap);
                    MyE.Munkalap_betű("Calibri", 20);

                    MyE.Oszlopszélesség(munkalap, "a:g", 18);
                    MyE.Sormagasság("1:11", 40);
                    MyE.Rácsoz("a1:g11");
                    MyE.Vastagkeret("a1:g1");
                    MyE.Kiir("Hétfő", "a1");
                    MyE.Kiir("Kedd", "b1");
                    MyE.Kiir("Szerda", "c1");
                    MyE.Kiir("Csütörtök", "d1");
                    MyE.Kiir("Péntek", "e1");
                    MyE.Kiir("Szombat", "f1");
                    MyE.Kiir("Vasárnap", "g1");
                    // kiírjuk a kocsikat
                    for (int j = 1; j <= 7; j++)
                    {
                        sor = 1;
                        List<Adat_ICS> Szűrt;
                        if (i == 1)
                            Szűrt = Adatok.Where(a => a.E2 == j).ToList();
                        else
                            Szűrt = Adatok.Where(a => a.E3 == j).ToList();

                        foreach (Adat_ICS rekord in Szűrt)
                        {
                            sor += 1;
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(j) + sor.ToString());
                        }

                    }
                }

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
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
        #endregion


        #region Lekérdezések
        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 32;
                // fejléc elkészítése

                Tábla_lekérdezés.Columns[0].HeaderText = "Psz";
                Tábla_lekérdezés.Columns[0].Width = 60;
                Tábla_lekérdezés.Columns[1].HeaderText = "Vizsg. foka";
                Tábla_lekérdezés.Columns[1].Width = 80;
                Tábla_lekérdezés.Columns[2].HeaderText = "Vizsg. Ssz.";
                Tábla_lekérdezés.Columns[2].Width = 60;
                Tábla_lekérdezés.Columns[3].HeaderText = "Vizsg. Kezdete";
                Tábla_lekérdezés.Columns[3].Width = 110;
                Tábla_lekérdezés.Columns[4].HeaderText = "Vizsg. Vége";
                Tábla_lekérdezés.Columns[4].Width = 110;
                Tábla_lekérdezés.Columns[5].HeaderText = "Vizsg KM állás";
                Tábla_lekérdezés.Columns[5].Width = 80;
                Tábla_lekérdezés.Columns[6].HeaderText = "Frissítés Dátum";
                Tábla_lekérdezés.Columns[6].Width = 110;
                Tábla_lekérdezés.Columns[7].HeaderText = "KM J-óta";
                Tábla_lekérdezés.Columns[7].Width = 80;
                Tábla_lekérdezés.Columns[8].HeaderText = "V után futott";
                Tábla_lekérdezés.Columns[8].Width = 80;
                Tábla_lekérdezés.Columns[9].HeaderText = "Havi km";
                Tábla_lekérdezés.Columns[9].Width = 80;
                Tábla_lekérdezés.Columns[10].HeaderText = "Felújítás szám";
                Tábla_lekérdezés.Columns[10].Width = 80;
                Tábla_lekérdezés.Columns[11].HeaderText = "Felújítás Dátum";
                Tábla_lekérdezés.Columns[11].Width = 110;
                Tábla_lekérdezés.Columns[12].HeaderText = "Ciklusrend típus";
                Tábla_lekérdezés.Columns[12].Width = 80;
                Tábla_lekérdezés.Columns[13].HeaderText = "Üzembehelyezés km";
                Tábla_lekérdezés.Columns[13].Width = 80;
                Tábla_lekérdezés.Columns[14].HeaderText = "Telephely";
                Tábla_lekérdezés.Columns[14].Width = 80;
                Tábla_lekérdezés.Columns[15].HeaderText = "Típus";
                Tábla_lekérdezés.Columns[15].Width = 80;
                Tábla_lekérdezés.Columns[16].HeaderText = "Kerék-K1";
                Tábla_lekérdezés.Columns[16].Width = 80;
                Tábla_lekérdezés.Columns[17].HeaderText = "Kerék-K2";
                Tábla_lekérdezés.Columns[17].Width = 80;
                Tábla_lekérdezés.Columns[18].HeaderText = "Kerék-K3";
                Tábla_lekérdezés.Columns[18].Width = 80;
                Tábla_lekérdezés.Columns[19].HeaderText = "Kerék-K4";
                Tábla_lekérdezés.Columns[19].Width = 80;
                Tábla_lekérdezés.Columns[20].HeaderText = "Kerék-K5";
                Tábla_lekérdezés.Columns[20].Width = 80;
                Tábla_lekérdezés.Columns[21].HeaderText = "Kerék-K6";
                Tábla_lekérdezés.Columns[21].Width = 80;
                Tábla_lekérdezés.Columns[22].HeaderText = "Kerék-K7";
                Tábla_lekérdezés.Columns[22].Width = 80;
                Tábla_lekérdezés.Columns[23].HeaderText = "Kerék-K8";
                Tábla_lekérdezés.Columns[23].Width = 80;
                Tábla_lekérdezés.Columns[24].HeaderText = "Kerék min";
                Tábla_lekérdezés.Columns[24].Width = 80;
                Tábla_lekérdezés.Columns[25].HeaderText = "Ssz.";
                Tábla_lekérdezés.Columns[25].Width = 80;
                Tábla_lekérdezés.Columns[26].HeaderText = "Végezte";
                Tábla_lekérdezés.Columns[26].Width = 120;
                Tábla_lekérdezés.Columns[27].HeaderText = "Következő V";
                Tábla_lekérdezés.Columns[27].Width = 120;
                Tábla_lekérdezés.Columns[28].HeaderText = "Következő V Ssz.";
                Tábla_lekérdezés.Columns[28].Width = 120;
                Tábla_lekérdezés.Columns[29].HeaderText = "Következő V2-V3";
                Tábla_lekérdezés.Columns[29].Width = 120;
                Tábla_lekérdezés.Columns[30].HeaderText = "Következő V2-V3 Ssz.";
                Tábla_lekérdezés.Columns[30].Width = 120;
                Tábla_lekérdezés.Columns[31].HeaderText = "Utolsó V2-V3 számláló";
                Tábla_lekérdezés.Columns[31].Width = 120;

                // feltöltjük a pályaszámokat
                Tábla_lekérdezés.RowCount = Pályaszám.Items.Count;

                for (int i = 0; i < Tábla_lekérdezés.RowCount; i++)
                    Tábla_lekérdezés.Rows[i].Cells[0].Value = Pályaszám.Items[i].ToString();

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";


                Holtart.Be();

                for (int i = 0; i < Tábla_lekérdezés.RowCount; i++)
                {
                    Holtart.Lép();
                    string szöveg = $"SELECT * FROM KMtábla where [azonosító]='{Tábla_lekérdezés.Rows[i].Cells[0].Value.ToStrTrim()}' ORDER BY vizsgdátumk desc";


                    Adat_T5C5_Kmadatok rekord = KézICSKmadatok.Egy_Adat(hely, jelszó, szöveg);

                    if (rekord != null)
                    {
                        // ki olvassuk az elsőt majd kilépünk
                        Tábla_lekérdezés.Rows[i].Cells[1].Value = rekord.Vizsgfok.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[2].Value = rekord.Vizsgsorszám;
                        Tábla_lekérdezés.Rows[i].Cells[3].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[4].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[5].Value = rekord.Vizsgkm;
                        Tábla_lekérdezés.Rows[i].Cells[6].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[7].Value = rekord.KMUkm;
                        // ha J akkor nem kell különbséget képezni
                        if (rekord.Vizsgsorszám == 0)
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = rekord.KMUkm;
                        else
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = (rekord.KMUkm - rekord.Vizsgkm);

                        Tábla_lekérdezés.Rows[i].Cells[9].Value = rekord.Havikm;
                        Tábla_lekérdezés.Rows[i].Cells[10].Value = rekord.Jjavszám;
                        Tábla_lekérdezés.Rows[i].Cells[11].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[12].Value = rekord.Ciklusrend.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[13].Value = rekord.Teljeskm;

                        Tábla_lekérdezés.Rows[i].Cells[25].Value = rekord.ID;
                        if (rekord.V2végezte.Trim() != "_")
                            Tábla_lekérdezés.Rows[i].Cells[26].Value = rekord.V2végezte.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[27].Value = rekord.KövV.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[28].Value = rekord.KövV_sorszám;
                        Tábla_lekérdezés.Rows[i].Cells[29].Value = rekord.KövV2.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[30].Value = rekord.KövV2_sorszám;
                        Tábla_lekérdezés.Rows[i].Cells[31].Value = rekord.V2V3Számláló;
                    }
                }
                Típus_telephely_kiírás();
                Kerék_kiírás();
                Tábla_lekérdezés.Visible = true;
                Holtart.Ki();
                Tábla_lekérdezés.Refresh();

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


        private void Típus_telephely_kiírás()
        {
            try
            {
                if (Tábla_lekérdezés.Rows.Count < 1)
                    return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";

                // sorbarendezzük a táblát pályaszám szerint

                Tábla_lekérdezés.Sort(Tábla_lekérdezés.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                Tábla_lekérdezés.Visible = false;

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i = 0;
                int hiba = 0;

                foreach (Adat_Jármű rekord in Adatok)
                {

                    if (String.Compare(Tábla_lekérdezés.Rows[i].Cells[0].Value.ToStrTrim(), rekord.Azonosító.Trim()) <= 0)
                    {
                        // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                        while (String.Compare(Tábla_lekérdezés.Rows[i].Cells[0].Value.ToStrTrim(), rekord.Azonosító.Trim()) < 0)
                        {
                            i += 1;
                            if (i == Tábla_lekérdezés.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }

                        if (hiba == 1)
                            break;
                        while (String.Compare(Tábla_lekérdezés.Rows[i].Cells[0].Value.ToStrTrim(), rekord.Azonosító.Trim()) <= 0)
                        {
                            if (Tábla_lekérdezés.Rows[i].Cells[0].Value.ToStrTrim() == rekord.Azonosító.Trim())
                            {
                                // ha egyforma akkor kiírjuk
                                Tábla_lekérdezés.Rows[i].Cells[14].Value = rekord.Üzem.Trim();
                                Tábla_lekérdezés.Rows[i].Cells[15].Value = rekord.Típus.Trim();
                            }
                            i += 1;
                            if (i == Tábla_lekérdezés.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }
                        if (hiba == 1)
                            break;
                    }
                    Holtart.Lép();
                }
                Tábla_lekérdezés.Refresh();
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


        private void Kerék_kiírás()
        {
            try
            {
                if (Tábla_lekérdezés.Rows.Count < 1) return;
                KerékadatokListaFeltöltés();
                if (AdatokMérés == null || AdatokMérés.Count < 1) return;

                Tábla_lekérdezés.Visible = false;

                for (int i = 0; i < Tábla_lekérdezés.RowCount; i++)
                {
                    Holtart.Lép();

                    int kerékminimum = 1000;
                    for (int j = 0; j <= 7; j++)
                    {
                        string[] darabol = Tábla_lekérdezés.Columns[j + 16].HeaderText.Split('-');
                        Adat_Kerék_Mérés Elem = (from a in AdatokMérés
                                                 where a.Azonosító == Tábla_lekérdezés.Rows[i].Cells[0].Value.ToStrTrim() &&
                                                 a.Pozíció == darabol[1].Trim()
                                                 orderby a.Mikor descending
                                                 select a).FirstOrDefault();
                        if (Elem != null)
                        {
                            Tábla_lekérdezés.Rows[i].Cells[16 + j].Value = Elem.Méret;
                            if (kerékminimum > Elem.Méret) kerékminimum = Elem.Méret;
                        }
                    }

                    if (kerékminimum != 1000) Tábla_lekérdezés.Rows[i].Cells[24].Value = kerékminimum;
                }
                Holtart.Ki();
                Tábla_lekérdezés.Refresh();
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


        private void Excellekérdezés_Click(object sender, EventArgs e)
        {
            if (Tábla_lekérdezés.Rows.Count <= 0)
                return;
            string fájlexc;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",

                Title = "Listázott tartalom mentése Excel fájlba",
                FileName = "ICS_KCSV_futásadatok_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;

            fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
            MyE.EXCELtábla(fájlexc, Tábla_lekérdezés, false);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyE.Megnyitás(fájlexc + ".xlsx");
        }


        private void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                // kimeneti fájl helye és neve
                InitialDirectory = "MyDocuments",

                Title = "Adatbázis mentése Excel fájlba",
                FileName = "ICS_KCSV_adatbázis_mentés_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                _fájlexc = SaveFileDialog1.FileName;
            else
                return;

            _hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
            _jelszó = "pocsaierzsi";
            _szöveg = "SELECT * FROM kmtábla ORDER BY azonosító,vizsgdátumk";
            Holtart.Be();
            timer1.Enabled = true;
            SZál_ABadatbázis(() =>
            { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("Az Excel tábla elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(_fájlexc);
            });
        }


        private void SZál_ABadatbázis(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót
                MyE.EXCELtábla(_hely, _jelszó, _szöveg, _fájlexc);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Telephelyi_lekérdezés();
        }


        private void Telephelyi_lekérdezés()
        {
            try
            {
                // kilistázzuk a adatbázis adatait
                string honnan = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                if (!Exists(honnan)) return;
                string szöveg;
                if (Cmbtelephely.Text.Trim() == "Főmérnökség")
                    szöveg = "Select * FROM Állománytábla WHERE  törölt=0 AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY azonosító";
                else
                {
                    szöveg = $"Select * FROM Állománytábla WHERE Üzem='{Cmbtelephely.Text.Trim()}' AND ";
                    szöveg += " törölt=0 AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY azonosító";
                }
                string jelszóhonnan = "pozsgaii";

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ICSKCSV\villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";

                Holtart.Be();

                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 14;
                // fejléc elkészítése
                Tábla_lekérdezés.Columns[0].HeaderText = "Psz";
                Tábla_lekérdezés.Columns[0].Width = 100;
                Tábla_lekérdezés.Columns[1].HeaderText = "KM J-óta";
                Tábla_lekérdezés.Columns[1].Width = 100;
                Tábla_lekérdezés.Columns[2].HeaderText = "Frissítés Dátum";
                Tábla_lekérdezés.Columns[2].Width = 120;
                Tábla_lekérdezés.Columns[3].HeaderText = "Vizsg. Dátum";
                Tábla_lekérdezés.Columns[3].Width = 120;
                Tábla_lekérdezés.Columns[4].HeaderText = "Vizsg KM állás";
                Tábla_lekérdezés.Columns[4].Width = 100;
                Tábla_lekérdezés.Columns[5].HeaderText = "Vizsg. foka";
                Tábla_lekérdezés.Columns[5].Width = 100;
                Tábla_lekérdezés.Columns[6].HeaderText = "Vizsg. Ssz";
                Tábla_lekérdezés.Columns[6].Width = 100;
                Tábla_lekérdezés.Columns[7].HeaderText = "Utolsó V2 km";
                Tábla_lekérdezés.Columns[7].Width = 100;
                Tábla_lekérdezés.Columns[8].HeaderText = "Utolsó V3 km";
                Tábla_lekérdezés.Columns[8].Width = 100;
                Tábla_lekérdezés.Columns[9].HeaderText = "V óta futott km";
                Tábla_lekérdezés.Columns[9].Width = 100;
                Tábla_lekérdezés.Columns[10].HeaderText = "V2 óta futott km";
                Tábla_lekérdezés.Columns[10].Width = 100;
                Tábla_lekérdezés.Columns[11].HeaderText = "V3 óta futott km";
                Tábla_lekérdezés.Columns[11].Width = 100;
                Tábla_lekérdezés.Columns[12].HeaderText = "Ciklusrend";
                Tábla_lekérdezés.Columns[12].Width = 100;
                Tábla_lekérdezés.Columns[13].HeaderText = "Követk. vizsg.";
                Tábla_lekérdezés.Columns[13].Width = 100;

                Kezelő_Jármű KézJ = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJ = KézJ.Lista_Adatok(honnan, jelszóhonnan, szöveg);

                szöveg = "SELECT * FROM KMtábla ";
                List<Adat_T5C5_Kmadatok> AdatokICS = KézICSKmadatok.Lista_Adat(hely, jelszó, szöveg);

                int i;
                foreach (Adat_Jármű rekord in AdatokJ)
                {
                    Tábla_lekérdezés.RowCount++;
                    i = Tábla_lekérdezés.RowCount - 1;
                    Tábla_lekérdezés.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    szöveg = $"SELECT * FROM KMtábla where [azonosító]='{rekord.Azonosító.Trim()}' AND törölt=false order by vizsgdátumk desc";

                    Adat_T5C5_Kmadatok rekordICS = (from a in AdatokICS
                                                    where a.Azonosító == rekord.Azonosító &&
                                                    a.Törölt == false
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();
                    if (rekordICS != null)
                    {
                        Tábla_lekérdezés.Rows[i].Cells[1].Value = rekordICS.KMUkm;
                        Tábla_lekérdezés.Rows[i].Cells[2].Value = rekordICS.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[3].Value = rekordICS.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[4].Value = rekordICS.Vizsgkm;
                        Tábla_lekérdezés.Rows[i].Cells[5].Value = rekordICS.Vizsgfok.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[6].Value = rekordICS.Vizsgsorszám;
                        if (rekordICS.Vizsgsorszám == 0)
                        {
                            // ha J akkor nem kell különbséget képezni
                            Tábla_lekérdezés.Rows[i].Cells[9].Value = rekordICS.KMUkm;
                        }
                        else
                        {
                            Tábla_lekérdezés.Rows[i].Cells[9].Value = (rekordICS.KMUkm - rekordICS.Vizsgkm);
                        }
                        Tábla_lekérdezés.Rows[i].Cells[12].Value = rekordICS.Ciklusrend.Trim();

                        // utolsó V2 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV2 = (from a in AdatokICS
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V2")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV2 != null)
                        {
                            Tábla_lekérdezés.Rows[i].Cells[7].Value = rekordICSV2.Vizsgkm;
                            Tábla_lekérdezés.Rows[i].Cells[10].Value = rekordICS.KMUkm - rekordICSV2.Vizsgkm;
                        }
                        // utolsó V3 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV3 = (from a in AdatokICS
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V3")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV3 != null)
                        {
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = rekordICSV3.Vizsgkm;
                            Tábla_lekérdezés.Rows[i].Cells[11].Value = rekordICS.KMUkm - rekordICSV3.Vizsgkm;
                        }

                        Adat_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                  where a.Típus == rekordICS.Ciklusrend.Trim() &&
                                                  a.Sorszám == rekordICS.Vizsgsorszám + 1
                                                  select a).FirstOrDefault();

                        if (ElemCiklus != null) Tábla_lekérdezés.Rows[i].Cells[13].Value = ElemCiklus.Vizsgálatfok;
                    }
                    Holtart.Lép();
                }
                Tábla_lekérdezés.Refresh();


                // ciklusrend kiírás

                for (i = 0; i < Tábla_lekérdezés.Rows.Count; i++)
                {

                }
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = true;

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
        #endregion


        #region Utolsó vizsgálati adatok lapfül

        private void Új_adat_Click(object sender, EventArgs e)
        {
            try
            {
                // melyik az utolsó elem kiírjuk a táblázatot
                KorrekcióListaFeltöltés();

                int i = Kiirjaatörténelmet();
                int KöVsorszám = int.Parse(Tábla1.Rows[i].Cells[3].Value.ToString()) + 1;
                string Köv_V_név = MyF.Szöveg_Tisztítás(Tábla1.Rows[i].Cells[2].Value.ToString(), 0, 2);
                double kmu_km = double.Parse(Tábla1.Rows[i].Cells[8].Value.ToString())
                              + KM_korrekció(DateTime.Parse(Tábla1.Rows[i].Cells[7].Value.ToString()));
                double V2számláló = double.Parse(Tábla1.Rows[i].Cells[20].Value.ToString());
                string ciklusrend = Tábla1.Rows[i].Cells[13].Value.ToString();
                // beolvassuk a soron következő elemet
                Kiüríti_lapfül();

                // a ciklusrendet kiválasztjuk
                CiklusrendCombo.Text = ciklusrend;
                Vizsgsorszámcombofeltölés();

                VizsgKm.Text = kmu_km.ToString();
                KMUkm.Text = kmu_km.ToString();

                if (Köv_V_név.Trim() == "V2")
                    KövV2_számláló.Text = kmu_km.ToString();
                else
                    KövV2_számláló.Text = V2számláló.ToString();


                Vizsgsorszám.Text = KöVsorszám.ToString();

                Sorszám_válastás(KöVsorszám);
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

        int KM_korrekció(DateTime Dátum)
        {
            int válasz = 0;
            if (AdatokZserKm != null && AdatokZserKm.Count > 0)
            {
                List<Adat_Főkönyv_Zser_Km> AdatokPSZKm = (from a in AdatokZserKm
                                                          where a.Azonosító == Pályaszám.Text.Trim() &&
                                                          a.Dátum > Dátum
                                                          select a).ToList();
                if (AdatokPSZKm != null && AdatokPSZKm.Count > 0) válasz = AdatokPSZKm.Sum(a => a.Napikm);
            }
            return válasz;
        }

        private void Kiüríti_lapfül()
        {
            Sorszám.Text = "";

            Vizsgsorszám.Text = 0.ToString();
            Vizsgfok.Text = "";
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = 0.ToString();
            Üzemek.Text = "";

            KMUkm.Text = 0.ToString();

            KMUdátum.Value = DateTime.Today;

            HaviKm.Text = 0.ToString();
            KMUdátum.Value = DateTime.Today;

            KövV.Text = "";
            KövV_Sorszám.Text = "";
            KövV1km.Text = 0.ToString();
            KövV2.Text = "";
            KövV2_Sorszám.Text = "";
            KövV2_számláló.Text = 0.ToString();
            KövV2km.Text = 0.ToString();
        }


        private void Vizsgsorszámcombofeltölés()
        {
            try
            {
                Vizsgsorszám.Items.Clear();

                if (CiklusrendCombo.Text.Trim() == "")
                    return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";

                string szöveg = $"SELECT * FROM ciklusrendtábla where [típus]='{CiklusrendCombo.Text.Trim()}' AND [törölt]=false ORDER BY sorszám";

                Vizsgsorszám.BeginUpdate();
                Vizsgsorszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "sorszám"));
                Vizsgsorszám.EndUpdate();
                Vizsgsorszám.Refresh();


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


        private void CiklusrendCombo_feltöltés()
        {
            try
            {
                CiklusrendCombo.Items.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";

                string szöveg = "SELECT DISTINCT típus FROM ciklusrendtábla WHERE  [törölt]='0' ORDER BY típus";

                CiklusrendCombo.BeginUpdate();
                CiklusrendCombo.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                CiklusrendCombo.EndUpdate();
                CiklusrendCombo.Refresh();
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


        private void CiklusrendCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgsorszámcombofeltölés();
        }


        private void Üzemek_listázása()
        {
            try
            {

                Üzemek.Items.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM telephelytábla order by sorszám";

                Üzemek.BeginUpdate();
                Üzemek.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
                Üzemek.EndUpdate();
                Üzemek.Refresh();
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


        private void Vizsgsorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = Vizsgsorszám.SelectedIndex;
            Sorszám_válastás(i);
        }


        private void Sorszám_válastás(int sorszám)
        {
            try
            {
                int i = sorszám;
                if (CiklusrendCombo.Text.Trim() == "")
                    return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = $"SELECT * FROM ciklusrendtábla where [típus]='{CiklusrendCombo.Text.Trim()}' AND [törölt]=false ORDER BY sorszám";

                Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
                List<Adat_Ciklus> CiklusAdat = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);

                string Vizsgálatfok = (from a in CiklusAdat
                                       where a.Sorszám == i
                                       select a.Vizsgálatfok).FirstOrDefault();

                if (Vizsgálatfok != null)
                    Vizsgfok.Text = Vizsgálatfok;

                // következő vizsgálat sorszáma
                Vizsgálatfok = (from a in CiklusAdat
                                where a.Sorszám == i + 1
                                select a.Vizsgálatfok).FirstOrDefault();
                if (Vizsgálatfok != null)
                    KövV.Text = Vizsgálatfok;

                KövV_Sorszám.Text = (i + 1).ToString();
                // követekező V2-V3
                KövV2.Text = "J";
                KövV2_Sorszám.Text = "0";
                for (int j = i + 1; j < CiklusAdat.Count; j++)
                {
                    if (CiklusAdat[j].Vizsgálatfok.Contains("V2"))
                    {
                        KövV2.Text = CiklusAdat[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
                    }
                    if (CiklusAdat[j].Vizsgálatfok.Contains("V3"))
                    {
                        KövV2.Text = CiklusAdat[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
                    }
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


        private void Utolsó_V_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (VizsgKm.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat km számláló állása mező nem lehet üres.");
                if (!long.TryParse(VizsgKm.Text, out long Vizsg_Km)) throw new HibásBevittAdat("Vizsgálat km számláló állása mezőnek számnak kell lennie.");
                if (Vizsgfok.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat foka beviteli mező nem lehet üres.");
                if (Vizsgsorszám.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat sorszáma mező nem lehet üres.");
                if (!long.TryParse(Vizsgsorszám.Text, out long Vizsg_sorszám)) throw new HibásBevittAdat("Vizsgálat sorszáma mezőnek számnak kell lennie.");
                if (KMUkm.Text.Trim() == "") throw new HibásBevittAdat("Kmu km mező nem lehet üres.");
                if (!long.TryParse(KMUkm.Text, out long Kmu_km)) throw new HibásBevittAdat("Kmu km mezőnek számnak kell lennie.");
                if (HaviKm.Text.Trim() == "") throw new HibásBevittAdat("Havi km mező nem lehet üres.");
                if (!long.TryParse(HaviKm.Text, out long Havi_km)) throw new HibásBevittAdat("Havi km mezőnek számnak kell lennie.");
                if (Jjavszám.Text.Trim() == "") throw new HibásBevittAdat("Felújítás sorszáma mező nem lehet üres.");
                if (!long.TryParse(Jjavszám.Text, out long Jjav_szám)) throw new HibásBevittAdat("Felújítás sorszáma mezőnek számnak kell lennie.");
                if (TEljesKmText.Text.Trim() == "") throw new HibásBevittAdat("Üzembehelyezés óta futott km mező nem lehet üres.");
                if (!long.TryParse(TEljesKmText.Text, out long Teljes_kmText)) throw new HibásBevittAdat("Üzembehelyezés óta futott km mezőnek számnak kell lennie.");
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Ütemezés típusa nem lehet üres.");
                if (KövV2_Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Következő V2-V3 sorszám mező nem lehet üres.");
                if (!long.TryParse(KövV2_Sorszám.Text, out long Kövv2_sorszám)) throw new HibásBevittAdat("Következő V2-V3 sorszám mezőnek számnak kell lennie.");
                if (KövV_Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Következő V sorszám mező nem lehet üres.");
                if (!long.TryParse(KövV_Sorszám.Text, out long kövv_sorszám)) throw new HibásBevittAdat("Következő V sorszám mezőnek számnak kell lennie.");
                if (KövV2km.Text.Trim() == "") throw new HibásBevittAdat("Következő V2-V2 km mező nem lehet üres.");
                if (!long.TryParse(KövV2km.Text, out long kövv2km)) throw new HibásBevittAdat("Következő V2-V2 km mezőnek számnak kell lennie.");

                // megnézzük az adatbázist, ha nincs ilyen kocsi ICS benne akkor rögzít máskülönben az adatokat módosítja
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                JárműFőListaFeltöltés();
                Adat_Jármű ElemJármű = (from a in AdatokFőJármű
                                        where a.Törölt == false &&
                                        a.Azonosító == Pályaszám.Text.Trim() &&
                                        (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                                        select a).FirstOrDefault();

                long i = 0;
                string szöveg;
                if (ElemJármű != null)
                {
                    KarbListaFeltöltés();
                    hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                    jelszó = "pocsaierzsi";
                    if (Sorszám.Text.Trim() == "")
                    {
                        Adat_T5C5_Kmadatok ElemKarb = (from a in AdatokICSKmadatok
                                                       orderby a.ID descending
                                                       select a).FirstOrDefault();
                        if (ElemKarb != null) i = ElemKarb.ID + 1;

                        // Új adat
                        szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                        szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                        szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                        szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                        szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
                        szöveg += i.ToString() + ", '" + Pályaszám.Text.Trim() + "', " + Jjav_szám + ", " + Kmu_km + ", '" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += "'" + Vizsgfok.Text.Trim() + "', '" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', '" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += Vizsg_Km + ", " + Havi_km + ", " + Vizsg_sorszám + ", '" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += Teljes_kmText + ", '" + CiklusrendCombo.Text.Trim() + "', '" + Üzemek.Text.Trim() + "', " + Kövv2_sorszám + ", '" + KövV2.Text.Trim() + "', ";
                        szöveg += kövv_sorszám + ", '" + KövV.Text.Trim() + "', " + KövV2_számláló.Text.Trim() + ", false)";
                    }
                    else
                    {
                        // módosítjuk az adatokat
                        szöveg = " UPDATE kmtábla SET ";
                        szöveg += " Jjavszám=" + Jjav_szám + ", ";
                        szöveg += " KMUkm=" + Kmu_km + ", ";
                        szöveg += " KMUdátum='" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += " Vizsgfok='" + Vizsgfok.Text.Trim() + "', ";
                        szöveg += " Vizsgdátumv='" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += " Vizsgdátumk='" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += " VizsgKm=" + VizsgKm.Text.Trim() + ", ";
                        szöveg += " HaviKm=" + HaviKm.Text.Trim() + ", ";
                        szöveg += " VizsgSorszám=" + Vizsgsorszám.Text.Trim() + ", ";
                        szöveg += " fudátum='" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += " Teljeskm=" + TEljesKmText.Text.Trim() + ", ";
                        szöveg += " Ciklusrend='" + CiklusrendCombo.Text.Trim() + "', ";
                        szöveg += " V2végezte='" + Üzemek.Text.Trim() + "', ";
                        szöveg += " KövV2_Sorszám=" + KövV2_Sorszám.Text.Trim() + ",  ";
                        szöveg += " KövV2='" + KövV2.Text.Trim() + "', ";
                        szöveg += " KövV_Sorszám=" + KövV_Sorszám.Text.Trim() + ", ";
                        szöveg += " KövV='" + KövV.Text.Trim() + "', ";
                        szöveg += " törölt=false, ";
                        szöveg += " V2V3Számláló=" + KövV2_számláló.Text.Trim();
                        szöveg += " WHERE id=" + Sorszám.Text.Trim();
                    }

                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    // naplózás
                    hely = $@"{Application.StartupPath}\Főmérnökség\Napló\" + "2021Kmnapló" + DateTime.Today.ToString("yyyy") + ".mdb";
                    if (!Exists(hely)) Adatbázis_Létrehozás.KmfutástáblaNapló(hely);

                    szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                    szöveg += i.ToString() + ", '" + Pályaszám.Text.Trim() + "', " + Jjavszám.Text.Trim() + ", " + KMUkm.Text.Trim() + ", '" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + Vizsgfok.Text.Trim() + "', '" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', '" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += VizsgKm.Text.Trim() + ", " + HaviKm.Text.Trim() + ", " + Vizsgsorszám.Text.Trim() + ", '" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += TEljesKmText.Text.Trim() + ", '" + CiklusrendCombo.Text.Trim() + "', '" + Üzemek.Text.Trim() + "', " + KövV2_Sorszám.Text.Trim() + ", '" + KövV2.Text.Trim() + "', ";
                    szöveg += KövV_Sorszám.Text.Trim() + ", '" + KövV.Text.Trim() + "', " + KövV2km.Text.Trim() + ", false, '" + Program.PostásNév.Trim() + "', '" + DateTime.Now.ToString() + "')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("A pályaszám nem ICS-KCSV! ", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Kiirjaatörténelmet();
                Fülek.SelectedIndex = 3;
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

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg;
                if (Sorszám.Text.Trim() != "")
                {
                    if (MessageBox.Show("Valóban töröljük az adatsort?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        szöveg = " UPDATE kmtábla SET ";
                        szöveg += " törölt=true  ";
                        szöveg += " WHERE id=" + Sorszám.Text.Trim();

                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        // naplózás
                        hely = $@"{Application.StartupPath}\Főmérnökség\Napló\" + "2021Kmnapló" + DateTime.Today.Year + ".mdb";
                        if (!Exists(hely))
                            Adatbázis_Létrehozás.KmfutástáblaNapló(hely);

                        int i = int.Parse(Sorszám.Text.Trim());
                        szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                        szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                        szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                        szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                        szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                        szöveg += i.ToString() + ", '" + Pályaszám.Text.Trim() + "', " + Jjavszám.Text.Trim() + ", " + KMUkm.Text.Trim() + ", '" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += "'" + Vizsgfok.Text.Trim() + "', '" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', '" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += VizsgKm.Text.Trim() + ", " + HaviKm.Text.Trim() + ", " + Vizsgsorszám.Text.Trim() + ", '" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += TEljesKmText.Text.Trim() + ", '" + CiklusrendCombo.Text.Trim() + "', '" + Üzemek.Text.Trim() + "', " + KövV2_Sorszám.Text.Trim() + ", '" + KövV2.Text.Trim() + "', ";
                        szöveg += KövV_Sorszám.Text.Trim() + ", '" + KövV.Text.Trim() + "', " + KövV2km.Text.Trim() + ", false, '" + Program.PostásNév.Trim() + "', '" + DateTime.Now.ToString() + "')";
                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        Kiirjaatörténelmet();
                        Fülek.SelectedIndex = 3;
                    }
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
        #endregion


        #region Vizsgálati adatok lapfül

        int Kiirjaatörténelmet()
        {

            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
            string jelszó = "pocsaierzsi";
            string szöveg = $"Select * FROM KMtábla where törölt=false AND [azonosító]='{Pályaszám.Text.Trim()}' order by vizsgdátumk ";

            Tábla1.Rows.Clear();
            Tábla1.Columns.Clear();
            Tábla1.Refresh();
            Tábla1.Visible = false;
            Tábla1.ColumnCount = 21;

            // fejléc elkészítése
            Tábla1.Columns[0].HeaderText = "Ssz.";
            Tábla1.Columns[0].Width = 80;
            Tábla1.Columns[1].HeaderText = "Psz";
            Tábla1.Columns[1].Width = 80;
            Tábla1.Columns[2].HeaderText = "Vizsg. foka";
            Tábla1.Columns[2].Width = 80;
            Tábla1.Columns[3].HeaderText = "Vizsg. Ssz.";
            Tábla1.Columns[3].Width = 80;
            Tábla1.Columns[4].HeaderText = "Vizsg. Kezdete";
            Tábla1.Columns[4].Width = 110;
            Tábla1.Columns[5].HeaderText = "Vizsg. Vége";
            Tábla1.Columns[5].Width = 110;
            Tábla1.Columns[6].HeaderText = "Vizsg KM állás";
            Tábla1.Columns[6].Width = 80;
            Tábla1.Columns[7].HeaderText = "Frissítés Dátum";
            Tábla1.Columns[7].Width = 110;
            Tábla1.Columns[8].HeaderText = "KM J-óta";
            Tábla1.Columns[8].Width = 80;
            Tábla1.Columns[9].HeaderText = "V után futott";
            Tábla1.Columns[9].Width = 80;
            Tábla1.Columns[10].HeaderText = "Havi km";
            Tábla1.Columns[10].Width = 80;
            Tábla1.Columns[11].HeaderText = "Felújítás szám";
            Tábla1.Columns[11].Width = 80;
            Tábla1.Columns[12].HeaderText = "Felújítás Dátum";
            Tábla1.Columns[12].Width = 110;
            Tábla1.Columns[13].HeaderText = "Ciklusrend típus";
            Tábla1.Columns[13].Width = 80;
            Tábla1.Columns[14].HeaderText = "Üzembehelyezés km";
            Tábla1.Columns[14].Width = 80;
            Tábla1.Columns[15].HeaderText = "Végezte";
            Tábla1.Columns[15].Width = 120;
            Tábla1.Columns[16].HeaderText = "Következő V";
            Tábla1.Columns[16].Width = 120;
            Tábla1.Columns[17].HeaderText = "Következő V Ssz.";
            Tábla1.Columns[17].Width = 120;
            Tábla1.Columns[18].HeaderText = "Következő V2-V3";
            Tábla1.Columns[18].Width = 120;
            Tábla1.Columns[19].HeaderText = "Következő V2-V3 Ssz.";
            Tábla1.Columns[19].Width = 120;
            Tábla1.Columns[20].HeaderText = "Utolsó V2-V3 számláló";
            Tábla1.Columns[20].Width = 120;

            List<Adat_T5C5_Kmadatok> Adatok = KézICSKmadatok.Lista_Adat(hely, jelszó, szöveg);
            int i;
            foreach (Adat_T5C5_Kmadatok rekord in Adatok)
            {
                Tábla1.RowCount++;
                i = Tábla1.RowCount - 1;
                Tábla1.Rows[i].Cells[0].Value = rekord.ID;
                Tábla1.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                Tábla1.Rows[i].Cells[2].Value = rekord.Vizsgfok.Trim();
                Tábla1.Rows[i].Cells[3].Value = rekord.Vizsgsorszám;
                Tábla1.Rows[i].Cells[4].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                Tábla1.Rows[i].Cells[5].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                Tábla1.Rows[i].Cells[6].Value = rekord.Vizsgkm;
                Tábla1.Rows[i].Cells[7].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                Tábla1.Rows[i].Cells[8].Value = rekord.KMUkm;

                if (rekord.Vizsgsorszám == 0)
                {
                    // ha J akkor nem kell különbséget képezni
                    Tábla1.Rows[i].Cells[9].Value = rekord.KMUkm;
                }
                else
                {
                    Tábla1.Rows[i].Cells[9].Value = (rekord.KMUkm - rekord.Vizsgkm);
                }
                Tábla1.Rows[i].Cells[10].Value = rekord.Havikm;
                Tábla1.Rows[i].Cells[11].Value = rekord.Jjavszám;
                Tábla1.Rows[i].Cells[12].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                Tábla1.Rows[i].Cells[13].Value = rekord.Ciklusrend.Trim();
                Tábla1.Rows[i].Cells[14].Value = rekord.Teljeskm;
                if (rekord.V2végezte.Trim() != "_")
                    Tábla1.Rows[i].Cells[15].Value = rekord.V2végezte.Trim();
                Tábla1.Rows[i].Cells[16].Value = rekord.KövV.Trim();
                Tábla1.Rows[i].Cells[17].Value = rekord.KövV_sorszám;
                Tábla1.Rows[i].Cells[18].Value = rekord.KövV2.Trim();
                Tábla1.Rows[i].Cells[19].Value = rekord.KövV2_sorszám;
                Tábla1.Rows[i].Cells[20].Value = rekord.V2V3Számláló;
            }

            Tábla1.Visible = true;
            Tábla1.Refresh();

            return Tábla1.RowCount - 1;
        }


        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Kiüríti_lapfül();
            if (e.RowIndex < 0)
                return;

            Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString();

            Vizsgsorszám.Text = Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString();
            Vizsgfok.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
            Vizsgdátumk.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString());
            Vizsgdátumv.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[5].Value.ToString());
            VizsgKm.Text = Tábla1.Rows[e.RowIndex].Cells[6].Value.ToString();
            Üzemek.Text = Tábla1.Rows[e.RowIndex].Cells[15].Value.ToString();

            KMUkm.Text = Tábla1.Rows[e.RowIndex].Cells[8].Value.ToString();
            Jjavszám.Text = Tábla1.Rows[e.RowIndex].Cells[11].Value.ToString();
            Utolsófelújításdátuma.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[12].Value.ToString());

            TEljesKmText.Text = Tábla1.Rows[e.RowIndex].Cells[14].Value.ToString();
            CiklusrendCombo.Text = Tábla1.Rows[e.RowIndex].Cells[13].Value.ToString();

            HaviKm.Text = Tábla1.Rows[e.RowIndex].Cells[10].Value.ToString();
            KMUdátum.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[7].Value.ToString());

            KövV.Text = Tábla1.Rows[e.RowIndex].Cells[16].Value.ToString();
            KövV_Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[17].Value.ToString();
            KövV2.Text = Tábla1.Rows[e.RowIndex].Cells[18].Value.ToString();
            KövV2_Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[19].Value.ToString();
            KövV2_számláló.Text = Tábla1.Rows[e.RowIndex].Cells[20].Value.ToString();

            KövV1km.Text = (int.Parse(KMUkm.Text) - int.Parse(VizsgKm.Text)).ToString();
            KövV2km.Text = (int.Parse(KMUkm.Text) - int.Parse(KövV2_számláló.Text)).ToString();


            Fülek.SelectedIndex = 2;
        }
        #endregion


        #region Állomány tábla
        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                Táblázatlistázás();
                if (Tábla_lekérdezés.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Állománytábla_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_lekérdezés, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc + ".xlsx");
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


        private void Táblázatlistázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (!Exists(hely)) return;
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla  order by típus, azonosító";


                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 2;

                // fejléc elkészítése 
                Tábla_lekérdezés.Columns[0].HeaderText = "Pályaszám";
                Tábla_lekérdezés.Columns[0].Width = 120;
                Tábla_lekérdezés.Columns[1].HeaderText = "Típus";
                Tábla_lekérdezés.Columns[1].Width = 150;

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;
                foreach (Adat_Jármű rekord in Adatok)
                {
                    Tábla_lekérdezés.RowCount++;
                    i = Tábla_lekérdezés.RowCount - 1;

                    Tábla_lekérdezés.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla_lekérdezés.Rows[i].Cells[1].Value = rekord.Típus.Trim();
                }

                Tábla_lekérdezés.Visible = true;
                Tábla_lekérdezés.Refresh();

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


        #endregion


        #region előtervező
        private void Pszlista()
        {
            try
            {
                PszJelölő.Items.Clear();
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";

                string szöveg = "Select * FROM Állománytábla WHERE  törölt=0 AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY azonosító";

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Jármű rekord in Adatok)
                    PszJelölő.Items.Add(rekord.Azonosító.Trim());
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


        private void Telephelylista()
        {
            try
            {
                Telephely.Items.Clear();

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM Állománytábla where [törölt]= false AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY üzem ";

                string szöveg0 = "";

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Jármű rekord in Adatok)
                {
                    if (szöveg0.Trim() != rekord.Üzem.Trim())
                    {
                        Telephely.Items.Add(rekord.Üzem.Trim());
                        szöveg0 = rekord.Üzem.Trim();
                    }
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


        private void Option5_Click(object sender, EventArgs e)
        {
            // Kocsi havi km
            HavikmICS = 0;
        }


        private void Option6_Click(object sender, EventArgs e)
        {
            try
            {
                // telephely átlag
                if (Telephely.Text.Trim() == "")
                {
                    Option8.Checked = true;
                    Text1.Text = "5000";
                    return;
                }

                for (int i = 0; i < PszJelölő.Items.Count; i++)
                    PszJelölő.SetItemChecked(i, false);

                Frissíti_a_pályaszámokat();
                KarbListaFeltöltés();
                // kilistázzuk a adatbázis adatait

                double típusátlag = 0;
                int ii = 0;
                Holtart.Be();


                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    Holtart.Lép();
                    if (PszJelölő.GetItemChecked(j))
                    {
                        Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                                   where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                   orderby a.Vizsgdátumk descending
                                                   select a).FirstOrDefault();
                        if (Elem != null)
                        {
                            típusátlag += Elem.Havikm;
                            ii++;
                        }
                    }
                }
                Holtart.Ki();
                if (ii != 0) típusátlag /= ii;

                HavikmICS = (long)Math.Round(típusátlag);
                Text1.Text = HavikmICS.ToString();
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


        private void Option7_Click(object sender, EventArgs e)
        {
            try
            {
                // típusátlag
                // kilistázzuk a adatbázis adatait
                KarbListaFeltöltés();
                double típusátlag = 0d;
                int ii = 0;
                Holtart.Be();

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    Holtart.Lép();
                    Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                               where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        típusátlag += Elem.Havikm;
                        ii++;
                    }

                }
                Holtart.Ki();
                if (ii != 0) típusátlag /= ii;
                HavikmICS = (long)Math.Round(típusátlag);
                Text1.Text = HavikmICS.ToString();
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


        private void Option9_Click(object sender, EventArgs e)
        {
            try
            {
                // 'kijelöltek átlaga
                double típusátlag = 0d;
                int ii = 0;
                Holtart.Be();

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    Holtart.Lép();
                    if (PszJelölő.GetItemChecked(j))
                    {
                        Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                                   where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                   orderby a.Vizsgdátumk descending
                                                   select a).FirstOrDefault();
                        if (Elem != null)
                        {
                            típusátlag += Elem.Havikm;
                            ii++;
                        }
                    }
                }
                Holtart.Ki();
                if (ii != 0) típusátlag /= ii;
                HavikmICS = (long)Math.Round(típusátlag);
                Text1.Text = HavikmICS.ToString();
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


        private void Frissíti_a_pályaszámokat()
        {
            try
            {
                if (Telephely.Text.Trim() == "")
                    return;

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM Állománytábla where [törölt]= false AND (valóstípus='ICS' OR valóstípus='KCSV-7') AND üzem='{Telephely.Text.Trim()}'  order by azonosító ";

                int i = 0;
                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Jármű rekord in Adatok)
                {
                    while (PszJelölő.Items[i].ToStrTrim() != rekord.Azonosító.Trim())
                    {
                        i += 1;
                        if (PszJelölő.Items.Count - 1 <= i)
                            break;
                    }
                    if (PszJelölő.Items[i].ToStrTrim() == rekord.Azonosító.Trim())
                    {
                        PszJelölő.SetItemChecked(i, true);
                    }
                    i += 1;
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


        private void Command2_Click(object sender, EventArgs e)
        {
            Frissíti_a_pályaszámokat();
        }


        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }


        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }


        private void Text2_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(Text2.Text, out int n)) Hónapok = 24;

            Hónapok = n;
        }


        private void Text1_Leave(object sender, EventArgs e)
        {
            if (Text1.Text.Trim() == "") return;
            if (!int.TryParse(Text1.Text, out int n))
            {
                Text1.Text = "";
                return;
            }
            HaviKm.Text = n.ToString();
            Option8.Checked = true;
        }


        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hónapok == 0) return;
                int volt = 0;

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    if (PszJelölő.GetItemChecked(j) == true)
                    {
                        volt = 1;
                        break;
                    }
                }
                if (volt == 0)
                {
                    return;
                }

                AlHoltart.Be();
                FőHoltart.Be(10);
                Alaptábla();
                FőHoltart.Lép();
                Egyhónaprögzítése();
                Excel_előtervező();
                AlHoltart.Ki();
                FőHoltart.Ki();

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


        private void Alaptábla()
        {
            try
            {
                if (Check1.Checked) return;
                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                string jelszó = "pocsaierzsi";

                if (Exists(hova) && !Check1.Checked) Delete(hova);
                if (!Exists(hova)) Adatbázis_Létrehozás.ElőtervkmfutástáblaICS(hova);

                KerékadatokListaFeltöltés();
                JárműFőListaFeltöltés();
                KarbListaFeltöltés();


                // kilistázzuk a adatbázis adatait


                AlHoltart.Be(PszJelölő.Items.Count + 1);
                int i = 1;

                List<string> SzövegGy = new List<string>();
                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    if (PszJelölő.GetItemChecked(j))
                    {
                        Adat_T5C5_Kmadatok rekord = (from a in AdatokICSKmadatok
                                                     where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                     orderby a.Vizsgdátumk descending
                                                     select a).FirstOrDefault();

                        if (rekord != null)
                        {
                            // Új adat
                            string szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                            szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                            szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                            szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                            szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Honostelephely, tervsorszám, Kerék_K1, Kerék_K2, Kerék_K3, Kerék_K4,Kerék_K5, Kerék_K6, Kerék_K7, Kerék_K8, Kerék_min)";
                            szöveg += " VALUES (";
                            szöveg += i.ToString() + ", ";                                               // id
                            szöveg += "'" + rekord.Azonosító.Trim() + "', ";                            // azonosító
                            szöveg += rekord.Jjavszám + ", ";                                   // jjavszám
                            szöveg += rekord.KMUkm + ", ";                                     // KMUkm
                            szöveg += "'" + rekord.KMUdátum.ToString("yyyy.MM.dd") + "', ";                 // KMUdátum
                            szöveg += "'" + rekord.Vizsgfok.Trim() + "', ";                            // vizsgfok
                            szöveg += "'" + rekord.Vizsgdátumk.ToString("yyyy.MM.dd") + "', ";             // vizsgdátumk
                            szöveg += "'" + rekord.Vizsgdátumv.ToString("yyyy.MM.dd") + "', ";              // vizsgdátumv
                            szöveg += rekord.Vizsgkm + ", ";                                     // vizsgkm
                            szöveg += rekord.Havikm + ", ";                                     // havikm
                            szöveg += rekord.Vizsgsorszám + ", ";                              // vizsgsorszám
                            szöveg += "'" + rekord.Fudátum.ToString("yyyy.MM.dd") + "', ";    // fudátum
                            szöveg += rekord.Teljeskm + ", ";                               // Teljeskm
                            szöveg += "'" + rekord.Ciklusrend.Trim() + "', ";                          // Ciklusrend
                            szöveg += "'" + rekord.V2végezte.Trim() + "', ";                                    // V2végezte
                            szöveg += rekord.KövV2_sorszám + ", ";                             // KövV2_Sorszám
                            szöveg += "'" + rekord.KövV2.Trim() + "', ";                                     // KövV2
                            szöveg += rekord.KövV_sorszám + ", ";                               // KövV_Sorszám
                            szöveg += "'" + rekord.KövV.Trim() + "', ";                                      // KövV
                            szöveg += rekord.V2V3Számláló + ", ";                                // V2V3Számláló
                            szöveg += " false, ";                                                   // törölt

                            Adat_Jármű ElemJármű = (from a in AdatokFőJármű
                                                    where a.Azonosító == PszJelölő.Items[j].ToStrTrim() &&
                                                    a.Törölt == false
                                                    select a).FirstOrDefault();
                            if (ElemJármű != null)                // Honostelephely
                                szöveg += $"'{ElemJármű.Üzem}',";
                            else
                                szöveg += $"'',";

                            szöveg += "0, ";    // tervsorszám

                            double kerékminimum;
                            double Kerék_K1 = 0;
                            double Kerék_K2 = 0;
                            double Kerék_K3 = 0;
                            double Kerék_K4 = 0;
                            double Kerék_K5 = 0;
                            double Kerék_K6 = 0;
                            double Kerék_K7 = 0;
                            double Kerék_K8 = 0;

                            kerékminimum = 1000;


                            // kerék méretek
                            if (AdatokMérés != null)
                            {
                                Kerék_K1 = Kerékméret(rekord.Azonosító.Trim(), "K1");
                                Kerék_K2 = Kerékméret(rekord.Azonosító.Trim(), "K2");
                                Kerék_K3 = Kerékméret(rekord.Azonosító.Trim(), "K3");
                                Kerék_K4 = Kerékméret(rekord.Azonosító.Trim(), "K4");
                                Kerék_K5 = Kerékméret(rekord.Azonosító.Trim(), "K5");
                                Kerék_K6 = Kerékméret(rekord.Azonosító.Trim(), "K6");
                                Kerék_K7 = Kerékméret(rekord.Azonosító.Trim(), "K7");
                                Kerék_K8 = Kerékméret(rekord.Azonosító.Trim(), "K8");
                            }

                            if (kerékminimum > Kerék_K1) kerékminimum = Kerék_K1;
                            if (kerékminimum > Kerék_K2) kerékminimum = Kerék_K2;
                            if (kerékminimum > Kerék_K3) kerékminimum = Kerék_K3;
                            if (kerékminimum > Kerék_K4) kerékminimum = Kerék_K4;
                            if (kerékminimum > Kerék_K5) kerékminimum = Kerék_K5;
                            if (kerékminimum > Kerék_K6) kerékminimum = Kerék_K6;
                            if (kerékminimum > Kerék_K7) kerékminimum = Kerék_K7;
                            if (kerékminimum > Kerék_K8) kerékminimum = Kerék_K8;

                            szöveg += $"{Kerék_K1}, "; // Kerék_K1
                            szöveg += $"{Kerék_K2}, "; // Kerék_K2
                            szöveg += $"{Kerék_K3}, "; // Kerék_K3
                            szöveg += $"{Kerék_K4}, "; // Kerék_K4
                            szöveg += $"{Kerék_K5}, "; // Kerék_K5
                            szöveg += $"{Kerék_K6}, "; // Kerék_K6
                            szöveg += $"{Kerék_K7}, "; // Kerék_K7
                            szöveg += $"{Kerék_K8}, "; // Kerék_K8

                            szöveg += $"{kerékminimum} )";  // Kerék_min
                            SzövegGy.Add(szöveg);
                            i += 1;
                        }
                        AlHoltart.Lép();
                    }
                }
                MyA.ABMódosítás(hova, jelszó, SzövegGy);
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


        private int Kerékméret(string kazonosító, string kpozíció)
        {
            int méret = 0;
            Adat_Kerék_Mérés Elem = (from a in AdatokMérés
                                     where a.Pozíció == kpozíció.Trim() &&
                                     a.Azonosító == kazonosító.Trim()
                                     select a).FirstOrDefault();
            if (Elem != null) méret = Elem.Méret;
            return méret;
        }





        private void Egyhónaprögzítése()
        {
            try
            {
                if (Hónapok == 0) return;
                if (HavikmICS == 0) return;


                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                if (!Exists(hova)) return;

                CiklusListaFeltöltés();


                double Alsó = 0, Felső = 0, Névleges = 0;
                double Havifutás, Mennyi = 0, sorszám = 0, különbözet;
                string Szöveg1;
                string következőv;
                string ideigazonosító;
                double ideigjjavszám;
                double ideigKMUkm;
                DateTime ideigKMUdátum;
                string ideigvizsgfok;
                DateTime ideigvizsgdátumk;
                DateTime ideigvizsgdátumv;
                double ideigvizsgkm;
                double ideighavikm;
                double ideigvizsgsorszám;
                DateTime ideigfudátum;
                double ideigTeljeskm;
                string ideigCiklusrend;
                string ideigV2végezte;
                string ideigHonostelephely;
                double ideigtervsorszám;
                double ideigkövV2_sorszám;
                string ideigkövV2;
                double ideigkövV_sorszám;
                string ideigKövV;
                bool ideigtörölt;
                double ideigkerék_1;
                double ideigkerék_2;
                double ideigkerék_3;
                double ideigkerék_4;
                double ideigkerék_5;
                double ideigkerék_6;
                double ideigkerék_7;
                double ideigkerék_8;
                double ideigkerék_min;

                double ideigV2V3számláló;
                long id_sorszám = 0;
                DateTime elődátum;
                double figyelő;
                string szöveg;

                FőHoltart.Be(PszJelölő.Items.Count + 3);
                AlHoltart.Be(Hónapok + 3);
                // beolvassuk a ID sorszámot, majd növeljük minden rögzítésnél
                string jelszó = "pocsaierzsi";
                KarbListaFeltöltés();
                Adat_T5C5_Kmadatok ElemKarb = (from a in AdatokICSKmadatok
                                               orderby a.ID descending
                                               select a).FirstOrDefault();
                if (ElemKarb != null) id_sorszám = ElemKarb.ID;


                Kezelő_ICS_Előterv Kéz = new Kezelő_ICS_Előterv();
                Adat_ICS_Előterv rekordhova;

                List<string> SzövegGy = new List<string>();

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    if (PszJelölő.GetItemChecked(j))
                    {
                        szöveg = $"SELECT * FROM KMtábla where [azonosító]='{PszJelölő.Items[j].ToStrTrim()}' order by vizsgdátumv desc";
                        rekordhova = Kéz.Egy_Adat(hova, jelszó, szöveg);

                        if (rekordhova != null)
                        {

                            // beolvassuk a kocsi alapadatait, hogy tudjuk növelni.
                            ideigazonosító = rekordhova.Azonosító.Trim();
                            ideigjjavszám = rekordhova.Jjavszám;
                            ideigKMUkm = rekordhova.KMUkm;
                            ideigKMUdátum = rekordhova.KMUdátum;
                            ideigvizsgfok = rekordhova.Vizsgfok;
                            ideigvizsgdátumk = rekordhova.Vizsgdátumk;
                            ideigvizsgdátumv = rekordhova.Vizsgdátumv;
                            ideigvizsgkm = rekordhova.Vizsgkm;
                            ideighavikm = rekordhova.Havikm;
                            ideigvizsgsorszám = rekordhova.Vizsgsorszám;
                            ideigfudátum = rekordhova.Fudátum;
                            ideigTeljeskm = rekordhova.Teljeskm;
                            ideigCiklusrend = rekordhova.Ciklusrend;
                            ideigV2végezte = "Előterv";
                            ideigkövV2_sorszám = rekordhova.KövV2_sorszám;
                            ideigkövV2 = rekordhova.KövV2;
                            ideigkövV_sorszám = rekordhova.KövV_sorszám;
                            ideigKövV = rekordhova.KövV;
                            ideigtörölt = rekordhova.Törölt;
                            ideigHonostelephely = rekordhova.Honostelephely;
                            ideigtervsorszám = rekordhova.Tervsorszám;
                            ideigkerék_1 = rekordhova.Kerék_K1;
                            ideigkerék_2 = rekordhova.Kerék_K2;
                            ideigkerék_3 = rekordhova.Kerék_K3;
                            ideigkerék_4 = rekordhova.Kerék_K4;
                            ideigkerék_5 = rekordhova.Kerék_K5;
                            ideigkerék_6 = rekordhova.Kerék_K6;
                            ideigkerék_7 = rekordhova.Kerék_K7;
                            ideigkerék_8 = rekordhova.Kerék_K8;

                            ideigkerék_min = rekordhova.Kerék_min;
                            ideigV2V3számláló = rekordhova.V2V3Számláló;


                            for (int i = 1; i < Hónapok; i++)
                            {
                                elődátum = DateTime.Now.AddMonths(i);

                                // megnézzük, hogy mi a ciklus határa
                                Adat_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                          where a.Típus == ideigCiklusrend.Trim() &&
                                                          a.Sorszám == ideigvizsgsorszám
                                                          select a).FirstOrDefault();
                                if (ElemCiklus != null)
                                {
                                    Alsó = ElemCiklus.Alsóérték;
                                    Felső = ElemCiklus.Felsőérték;
                                    Névleges = ElemCiklus.Névleges;
                                    sorszám = ElemCiklus.Sorszám;
                                }
                                if (Option10.Checked) Mennyi = Alsó;
                                if (Option11.Checked) Mennyi = Névleges;
                                if (Option12.Checked) Mennyi = Felső;

                                // megnézzük a következő V-t
                                Szöveg1 = $"select * from ciklusrendtábla where típus='{ideigCiklusrend.Trim()}' and sorszám=" + (sorszám + 1).ToString();
                                ElemCiklus = (from a in AdatokCiklus
                                              where a.Típus == ideigCiklusrend.Trim() &&
                                              a.Sorszám == (sorszám + 1)
                                              select a).FirstOrDefault();
                                if (ElemCiklus != null)
                                {
                                    // ha talált akkor
                                    következőv = ElemCiklus.Vizsgálatfok;
                                }
                                else
                                {
                                    // ha nem talált
                                    következőv = "J";
                                }


                                // az utolsó rögzített adatot megvizsgáljuk, hogy a havi km-et át lépjük -e fokozatot
                                if (HavikmICS == 0)
                                    Havifutás = ideighavikm;
                                else
                                    Havifutás = HavikmICS;
                                figyelő = ideigKMUkm - ideigvizsgkm + Havifutás;

                                if (Mennyi <= figyelő)
                                {

                                    különbözet = ideigKMUkm - ideigvizsgkm + Havifutás - Mennyi;
                                    // módosítjuk a határig tartó adatokat
                                    ideigKMUkm = ideigKMUkm + Havifutás - különbözet;
                                    ideigTeljeskm = ideigTeljeskm + Havifutás - különbözet;
                                    id_sorszám += 1;
                                    // ideigvizsgkm = ideigKMUkm + Havifutás - különbözet
                                    ideigvizsgkm += Mennyi;
                                    ideigTeljeskm += Havifutás;
                                    ideigKMUdátum = elődátum;
                                    ideigvizsgfok = következőv;
                                    ideigvizsgdátumk = elődátum;
                                    ideigvizsgdátumv = elődátum;
                                    ideigtervsorszám += 1d;
                                    ideigkerék_1 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_2 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_3 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_4 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_5 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_6 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_7 -= double.Parse(Kerékcsökkenés.Text);
                                    ideigkerék_8 -= double.Parse(Kerékcsökkenés.Text);

                                    ideigkerék_min -= double.Parse(Kerékcsökkenés.Text);
                                    // rögzítjük és egy ciklussal feljebb emeljük
                                    if (következőv == "J")
                                    {
                                        ideigvizsgsorszám = 0d;
                                        ideigKMUkm = 0d;
                                        ideigfudátum = elődátum;
                                        ideigjjavszám += 1d;
                                        ideigvizsgkm = 0d;
                                    }
                                    else
                                    {
                                        ideigvizsgsorszám += 1d;
                                    }
                                    szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Honostelephely, tervsorszám, Kerék_K1,Kerék_K2, Kerék_K3,Kerék_K4,Kerék_K5,Kerék_K6,Kerék_K7,Kerék_K8,Kerék_min)";
                                    szöveg += " VALUES (";
                                    szöveg += id_sorszám.ToString() + ", ";                                               // id
                                    szöveg += "'" + ideigazonosító.Trim() + "', ";                            // azonosító
                                    szöveg += ideigjjavszám.ToString() + ", ";                                   // jjavszám
                                    szöveg += ideigKMUkm.ToString() + ", ";                                     // KMUkm
                                    szöveg += "'" + ideigKMUdátum.ToString("yyyy.MM.dd") + "', ";                 // KMUdátum
                                    szöveg += "'" + ideigvizsgfok.Trim() + "', ";                            // vizsgfok
                                    szöveg += "'" + ideigvizsgdátumk.ToString("yyyy.MM.dd") + "', ";             // vizsgdátumk
                                    szöveg += "'" + ideigvizsgdátumv.ToString("yyyy.MM.dd") + "', ";              // vizsgdátumv
                                    szöveg += ideigvizsgkm.ToString() + ", ";                                     // vizsgkm
                                    szöveg += ideighavikm.ToString() + ", ";                                     // havikm
                                    szöveg += ideigvizsgsorszám.ToString() + ", ";                              // vizsgsorszám
                                    szöveg += "'" + ideigfudátum.ToString("yyyy.MM.dd") + "', ";    // fudátum
                                    szöveg += ideigTeljeskm.ToString() + ", ";                               // Teljeskm
                                    szöveg += "'" + ideigCiklusrend.Trim() + "', ";                          // Ciklusrend
                                    szöveg += "'" + ideigV2végezte.Trim() + "', ";                                    // V2végezte
                                    szöveg += ideigkövV2_sorszám + ", ";                             // KövV2_Sorszám
                                    szöveg += "'" + ideigkövV2.Trim() + "', ";                                     // KövV2
                                    szöveg += ideigkövV_sorszám + ", ";                               // KövV_Sorszám
                                    szöveg += "'" + ideigKövV.Trim() + "', ";                                      // KövV
                                    szöveg += ideigV2V3számláló + ", ";                                // V2V3Számláló
                                    szöveg += " false, ";                                                   // törölt
                                    szöveg += "'" + ideigHonostelephely.Trim() + "', "; // Honostelephely
                                    szöveg += ideigtervsorszám.ToString() + ", ";    // tervsorszám
                                    szöveg += ideigkerék_1.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_2.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_3.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_4.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_5.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_6.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_7.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_8.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_min.ToString().Replace(",", ".") + ") ";
                                    SzövegGy.Add(szöveg);

                                }
                                else
                                {
                                    // módosítjuk az utolsó adatsort
                                    if (ideigKMUkm == 0d) // ha felújítva volt és nem lett lenullázva
                                    {
                                        ideigvizsgkm = 0d;
                                    }
                                    ideigKMUkm += Havifutás;
                                    ideigTeljeskm += Havifutás;
                                }
                                AlHoltart.Lép();
                            }
                        }

                    }

                    FőHoltart.Lép();
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hova, jelszó, SzövegGy);
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


        private void Excel_előtervező()
        {
            try
            {
                string[] cím = new string[5];
                string[] Leírás = new string[5];

                // paraméter tábla feltöltése
                cím[0] = "Munkalapfül";
                Leírás[0] = "Leírás";
                cím[1] = "Adatok";
                Leírás[1] = "Előtervezett adatok";
                cím[2] = "Vizsgálatok";
                Leírás[2] = "Vizsgálati adatok havonta";
                cím[3] = "Éves_terv";
                Leírás[3] = "Vizsgálati adatok éves";
                cím[4] = "Éves_havi_terv";
                Leírás[4] = "Vizsgálati adatok éves/havi";

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Vizsgálat előtervező",
                    FileName = "V_javítások_előtervezése_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.ExcelLétrehozás();
                string munkalap = "Munka1";
                MyE.Munkalap_átnevezés(munkalap, "Tartalom");
                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************

                for (int i = 1; i < 5; i++)
                    MyE.Új_munkalap(cím[i]);

                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                munkalap = "Tartalom";
                MyE.Munkalap_aktív(munkalap);
                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");
                for (int i = 1; i < 5; i++)
                {
                    MyE.Link_beillesztés(munkalap, "A" + (i + 1).ToString(), cím[i].Trim());
                    MyE.Kiir(Leírás[i].Trim(), "b" + (i + 1).ToString());
                }
                MyE.Oszlopszélesség(munkalap, "A:B");

                // ****************************************************
                // Elkészítjük a munkalapokat
                // ****************************************************
                FőHoltart.Be(4);
                Adatoklistázása();
                FőHoltart.Lép();
                Kimutatás();
                FőHoltart.Lép();
                Kimutatás1();
                FőHoltart.Lép();
                Kimutatás2();

                MyE.Munkalap_aktív(munkalap);
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
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


        private void Adatoklistázása()
        {
            try
            {
                string munkalap = "Adatok";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                // megnyitjuk az adatbázist
                string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = "SELECT * FROM KMtábla  order by azonosító,vizsgdátumv ";

                utolsósor = MyE.Tábla_Író(hely, jelszó, szöveg, 3, munkalap);

                // fejlécet kiírjuk
                MyE.Kiir("ID", "a3");
                MyE.Kiir("Pályaszám", "b3");
                MyE.Kiir("Jjavszám", "c3");
                MyE.Kiir("KMUkm", "d3");
                MyE.Kiir("KMUdátum", "e3");
                MyE.Kiir("vizsgfok", "f3");
                MyE.Kiir("vizsgdátumkezdő", "g3");
                MyE.Kiir("vizsgdátumvég", "h3");
                MyE.Kiir("vizsgkmszámláló", "i3");
                MyE.Kiir("havikm", "j3");
                MyE.Kiir("vizsgsorszám", "k3");
                MyE.Kiir("Jdátum", "l3");
                MyE.Kiir("Teljeskm", "m3");
                MyE.Kiir("Ciklusrend", "n3");
                MyE.Kiir("V2végezte", "o3");
                MyE.Kiir("Köv V2 sorszám", "p3");
                MyE.Kiir("Köv V2", "q3");
                MyE.Kiir("Köv V sorszám", "r3");
                MyE.Kiir("köv V", "s3");
                MyE.Kiir("Törölt", "t3");
                MyE.Kiir("Módosító", "u3");
                MyE.Kiir("Módosítás dátuma", "v3");
                MyE.Kiir("Honostelephely", "w3");
                MyE.Kiir("tervsorszám", "x3");
                MyE.Kiir("Kerék_1", "y3");
                MyE.Kiir("Kerék_2", "z3");
                MyE.Kiir("Kerék_3", "aa3");
                MyE.Kiir("Kerék_4", "ab3");
                MyE.Kiir("Kerék_5", "ac3");
                MyE.Kiir("Kerék_6", "ad3");
                MyE.Kiir("Kerék_7", "ae3");
                MyE.Kiir("Kerék_8", "af3");
                MyE.Kiir("Kerék_min", "ag3");
                MyE.Kiir("V2V3 számláló", "ah3");
                MyE.Kiir("Év", "ai3");
                MyE.Kiir("fokozat", "aj3");
                MyE.Kiir("Hónap", "ak3");

                MyE.Kiir("=YEAR(RC[-27])", "Ai4");
                MyE.Kiir("=LEFT(RC[-30],2)", "Aj4");
                MyE.Kiir("=MONTH(RC[-29])", "Ak4");

                MyE.Képlet_másol(munkalap, "AI4:AK4", "AI5:AK" + (utolsósor + 3));

                // megformázzuk
                MyE.Oszlopszélesség(munkalap, "A:Ak");
                MyE.Vastagkeret("a3:Ak3");
                MyE.Rácsoz("a3:AK" + (utolsósor + 3).ToString());
                MyE.Vastagkeret("a3:Ak" + (utolsósor + 3).ToString());
                MyE.Vastagkeret("a3:Ak3");
                // szűrő
                MyE.Szűrés(munkalap, "A3:AK" + (utolsósor + 3), 3);

                // ablaktábla rögzítése

                MyE.Tábla_Rögzítés("3:3", 3);

                // kiírjuk a tábla méretét
                MyE.Munkalap_aktív("Vizsgálatok");
                MyE.Kiir((utolsósor + 2).ToString(), "aa1");

                MyE.Munkalap_aktív("Éves_terv");
                MyE.Kiir((utolsósor + 2).ToString(), "aa1");

                MyE.Munkalap_aktív("Éves_havi_terv");
                MyE.Kiir((utolsósor + 2).ToString(), "aa1");
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


        private void Kimutatás()
        {
            try
            {
                string munkalap = "Vizsgálatok";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AK" + (utolsósor + 3);
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("vizsgdátumkezdő");


                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("tervsorszám");

                oszlopNév.Add("vizsgfok");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
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


        private void Kimutatás1()
        {
            try
            {
                string munkalap = "Éves_terv";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AK" + (utolsósor + 3);
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Év");


                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("tervsorszám");

                oszlopNév.Add("Fokozat");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
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


        private void Kimutatás2()
        {
            try
            {

                string munkalap = "Éves_havi_terv";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AK" + (utolsósor + 3);
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás2";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("ID");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Pályaszám");

                oszlopNév.Add("Hónap");

                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("Év");
                SzűrőNév.Add("Fokozat");



                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
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


        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Vizsgálatok tény adatai",
                    FileName = "T5C5_adatbázis_mentés_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = "SELECT * FROM KMtábla order by azonosító";

                Holtart.Be();

                List<Adat_T5C5_Kmadatok> Adatok = KézICSKmadatok.Lista_Adat(hely, jelszó, szöveg);


                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 12);

                utolsósor = MyE.EXCELtábla(hely, jelszó, szöveg) + 1;
                string munkalap = "Adatok";
                MyE.Munkalap_átnevezés("Munka1", munkalap);
                MyE.Új_munkalap("Kimutatás");
                Holtart.Lép();
                MyE.Munkalap_aktív(munkalap);
                MyE.Kiir("=YEAR(RC[-15])", "v2");
                MyE.Kiir("=MONTH(RC[-16])", "w2");
                MyE.Kiir("=LEFT(RC[-18],2)", "x2");
                MyE.Képlet_másol(munkalap, "V2:X2", "V3:X" + utolsósor);
                MyE.Kiir("Év", "v1");
                MyE.Kiir("hó", "w1");
                MyE.Kiir("Vizsgálat rövid", "x1");
                MyE.Oszlopszélesség(munkalap, "A:X");
                Holtart.Lép();
                MyE.Betű("D:D", "", "M/d/yyyy");
                MyE.Betű("F:F", "", "M/d/yyyy");
                MyE.Betű("G:G", "", "M/d/yyyy");
                MyE.Betű("K:K", "", "M/d/yyyy");

                // rácsozás
                MyE.Rácsoz("A1:X" + utolsósor);
                Holtart.Lép();
                //szűrést felteszük
                MyE.Szűrés("Adatok", "A", "X", 1);

                //Nyomtatási terület kijelülése
                MyE.NyomtatásiTerület_részletes("Adatok", "A1:X" + utolsósor, "$1:$1", "", true);
                Holtart.Lép();
                munkalap = "Kimutatás";
                MyE.Munkalap_aktív(munkalap);

                string munkalap_adat = "Adatok";
                string balfelső = "A1";
                string jobbalsó = "X" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("azonosító");
                Összesít_módja.Add("xlCount");

                sorNév.Add("Vizsgálat rövid");

                oszlopNév.Add("V2végezte");

                SzűrőNév.Add("Év");
                SzűrőNév.Add("hó");
                Holtart.Lép();
                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);

                munkalap = "Adatok";
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
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
        #endregion


        private void SAP_adatok_Click(object sender, EventArgs e)
        {
            string fájlexc = "";
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                MyE.ExcelMegnyitás(fájlexc);

                // ***********************************
                // ***** Ellenőrzés eleje ************
                // ***********************************
                string fejlécell = "";
                for (int ii = 0; ii < 7; ii++)
                    fejlécell += MyE.Beolvas(MyE.Oszlopnév(ii + 1) + "1").Trim();

                if (!MyF.Betöltéshelyes("KM adatok", fejlécell))
                {
                    MessageBox.Show("Nem megfelelő a betölteni kívánt adatok formátuma ! ", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // az excel tábla bezárása
                    MyE.ExcelBezárás();
                    return;
                }
                // ***********************************
                // ***** Ellenőrzés vége  ************
                // ***********************************
                // megnézzük, hogy hány sorból áll a tábla

                Holtart.Be();
                KarbListaFeltöltés();
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";
                string beopályaszám = "";
                // Első adattól végig pörgetjüka beolvasást
                int i = 2;

                List<string> SzövegGy = new List<string>();
                while (MyE.Beolvas($"a{i}") != "_")
                {
                    beopályaszám = MyF.Szöveg_Tisztítás(MyE.Beolvas("a" + i.ToString()), 1, 4);

                    if (beopályaszám.Trim() == "_") return;

                    Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                               where a.Azonosító == beopályaszám.Trim() &&
                                               a.Törölt == false
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        long utolsórögzítés = Elem.ID;

                        string szöveg = "UPDATE kmtábla SET ";
                        if (!DateTime.TryParse(MyE.Beolvas($"C{i}"), out DateTime KMUdátum)) KMUdátum = new DateTime(1900, 1, 1);

                        szöveg += $" KMUdátum='{KMUdátum:yyyy.MM.dd}', ";
                        szöveg += $" KMUkm={MyE.Beolvas($"d{i}")}, ";
                        if (MyE.Beolvas($"b{i}").Trim() == "_")
                            szöveg += " havikm=0, ";
                        else
                            szöveg += $" havikm={MyE.Beolvas($"b{i}")}, ";

                        szöveg += $" Jjavszám={MyE.Beolvas($"f{i}")}, ";

                        if (!DateTime.TryParse(MyE.Beolvas($"G{i}"), out DateTime fudátum)) fudátum = new DateTime(1900, 1, 1);

                        szöveg += $" fudátum='{fudátum:yyyy.MM.dd}', ";
                        szöveg += $" teljeskm={MyE.Beolvas($"e{i}")} ";
                        szöveg += $"WHERE [id]={utolsórögzítés}";
                        SzövegGy.Add(szöveg);
                    }
                    Holtart.Lép();
                    i += 1;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                // az excel tábla bezárása
                MyE.ExcelBezárás();
                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);

                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("System.IO.File.InternalDelete"))
                    MessageBox.Show($"A programnak a beolvasott adatokat tartalmazó fájlt nem sikerült törölni.\n Valószínüleg a {fájlexc} nyitva van.\n\nAz adat konvertálás befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        #region Vizsgálat_ütemező

        private void Ütem_frissít_Click(object sender, EventArgs e)
        {
            Ütemező_lekérdezés();
            Ütemezettkocsik();
        }


        private void Ütemező_lekérdezés()
        {
            try
            {
                // kilistázzuk a adatbázis adatait
                string honnan = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string szöveg = "Select * FROM Állománytábla WHERE  törölt=0 AND (valóstípus='ICS' OR valóstípus='KCSV-7') ORDER BY azonosító";

                if (!Exists(honnan)) return;
                string jelszóhonnan = "pozsgaii";
                Kezelő_Jármű KézJ = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJ = KézJ.Lista_Adatok(honnan, jelszóhonnan, szöveg);

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ICSKCSV\villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";
                szöveg = "SELECT * FROM KMtábla ";

                List<Adat_T5C5_Kmadatok> AdatokICS = KézICSKmadatok.Lista_Adat(hely, jelszó, szöveg);

                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                HibaListaFeltöltés();
                VezénylésListaFeltöltés();
                KorrekcióListaFeltöltés();

                Holtart.Be();

                Tábla_ütemező.Rows.Clear();
                Tábla_ütemező.Columns.Clear();
                Tábla_ütemező.Refresh();
                Tábla_ütemező.Visible = false;
                Tábla_ütemező.ColumnCount = 24;
                // fejléc elkészítése
                Tábla_ütemező.Columns[0].HeaderText = "Psz";
                Tábla_ütemező.Columns[0].Width = 80;
                Tábla_ütemező.Columns[0].Frozen = true;
                Tábla_ütemező.Columns[1].HeaderText = "KM J-óta";
                Tábla_ütemező.Columns[1].Width = 80;
                Tábla_ütemező.Columns[1].Visible = false;
                Tábla_ütemező.Columns[2].HeaderText = "Frissítés Dátum";
                Tábla_ütemező.Columns[2].Width = 100;
                Tábla_ütemező.Columns[3].HeaderText = "Vizsg. Dátum";
                Tábla_ütemező.Columns[3].Width = 100;
                Tábla_ütemező.Columns[4].HeaderText = "Vizsg KM állás";
                Tábla_ütemező.Columns[4].Width = 150;
                Tábla_ütemező.Columns[5].HeaderText = "Utolsó Vizsg. foka";
                Tábla_ütemező.Columns[5].Width = 150;
                Tábla_ütemező.Columns[6].HeaderText = "V óta futott km korrigált";
                Tábla_ütemező.Columns[6].Width = 80;
                Tábla_ütemező.Columns[7].HeaderText = "V2 óta futott km korrigált";
                Tábla_ütemező.Columns[7].Width = 80;
                Tábla_ütemező.Columns[8].HeaderText = "V3 óta futott km korrigált";
                Tábla_ütemező.Columns[8].Width = 80;
                Tábla_ütemező.Columns[9].HeaderText = "Ciklusrend";
                Tábla_ütemező.Columns[9].Width = 100;
                Tábla_ütemező.Columns[10].HeaderText = "Követk. vizsg.";
                Tábla_ütemező.Columns[10].Width = 80;
                Tábla_ütemező.Columns[11].HeaderText = "Jármű hibák";
                Tábla_ütemező.Columns[11].Width = 200;

                Tábla_ütemező.Columns[12].Visible = false;
                Tábla_ütemező.Columns[13].Visible = false;
                Tábla_ütemező.Columns[14].HeaderText = "Mit kér";
                Tábla_ütemező.Columns[14].Width = 100;
                Tábla_ütemező.Columns[14].Visible = false;
                Tábla_ütemező.Columns[15].HeaderText = "Rendelés szám";
                Tábla_ütemező.Columns[15].Width = 100;
                Tábla_ütemező.Columns[15].Visible = false;
                Tábla_ütemező.Columns[16].HeaderText = "Vizsgálat";
                Tábla_ütemező.Columns[16].Width = 100;
                Tábla_ütemező.Columns[16].Visible = false;
                Tábla_ütemező.Columns[17].HeaderText = "Takarítás";
                Tábla_ütemező.Columns[17].Width = 100;
                Tábla_ütemező.Columns[17].Visible = false;
                Tábla_ütemező.Columns[18].HeaderText = "Járműstátus";
                Tábla_ütemező.Columns[18].Width = 100;
                Tábla_ütemező.Columns[18].Visible = false;
                Tábla_ütemező.Columns[19].HeaderText = "Sorszám";
                Tábla_ütemező.Columns[19].Width = 80;
                Tábla_ütemező.Columns[19].Visible = false;

                Tábla_ütemező.Columns[20].HeaderText = "V óta futott km ";
                Tábla_ütemező.Columns[20].Width = 80;
                Tábla_ütemező.Columns[21].HeaderText = "V2 óta futott km ";
                Tábla_ütemező.Columns[21].Width = 80;
                Tábla_ütemező.Columns[22].HeaderText = "V3 óta futott km ";
                Tábla_ütemező.Columns[22].Width = 80;
                Tábla_ütemező.Columns[23].HeaderText = "km korr ";
                Tábla_ütemező.Columns[23].Width = 100;

                // kiírjuk a pályaszámokat


                int i;
                foreach (Adat_Jármű rekord in AdatokJ)
                {

                    Tábla_ütemező.RowCount++;
                    i = Tábla_ütemező.RowCount - 1;
                    Tábla_ütemező.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla_ütemező.Rows[i].Cells[18].Value = rekord.Státus;

                    Adat_T5C5_Kmadatok rekordICS = (from a in AdatokICS
                                                    where a.Azonosító == rekord.Azonosító &&
                                                    a.Törölt == false
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();

                    if (rekordICS != null)
                    {
                        Tábla_ütemező.Rows[i].Cells[14].Value = "0";
                        Tábla_ütemező.Rows[i].Cells[15].Value = "0";
                        Tábla_ütemező.Rows[i].Cells[16].Value = "0";
                        Tábla_ütemező.Rows[i].Cells[19].Value = "0";

                        Tábla_ütemező.Rows[i].Cells[1].Value = rekordICS.KMUkm;
                        Tábla_ütemező.Rows[i].Cells[2].Value = rekordICS.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_ütemező.Rows[i].Cells[3].Value = rekordICS.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_ütemező.Rows[i].Cells[4].Value = rekordICS.Vizsgkm;
                        Tábla_ütemező.Rows[i].Cells[5].Value = rekordICS.Vizsgfok;

                        //km korrekció
                        int korrekció = 0;
                        List<Adat_Főkönyv_Zser_Km> AdatokPSZKm = (from a in AdatokZserKm
                                                                  where a.Azonosító == rekord.Azonosító &&
                                                                  a.Dátum > rekordICS.KMUdátum
                                                                  select a).ToList();
                        if (AdatokPSZKm != null && AdatokPSZKm.Count > 0) korrekció = AdatokPSZKm.Sum(a => a.Napikm);
                        Tábla_ütemező.Rows[i].Cells[23].Value = korrekció;

                        // ha J akkor nem kell különbséget képezni
                        if (rekordICS.Vizsgsorszám == 0)
                        {
                            Tábla_ütemező.Rows[i].Cells[20].Value = rekordICS.KMUkm;
                            Tábla_ütemező.Rows[i].Cells[6].Value = rekordICS.KMUkm + korrekció;
                        }
                        else
                        {
                            Tábla_ütemező.Rows[i].Cells[20].Value = (rekordICS.KMUkm - rekordICS.Vizsgkm);
                            Tábla_ütemező.Rows[i].Cells[6].Value = (rekordICS.KMUkm - rekordICS.Vizsgkm) + korrekció;
                        }

                        Tábla_ütemező.Rows[i].Cells[9].Value = rekordICS.Ciklusrend;
                        Tábla_ütemező.Rows[i].Cells[19].Value = rekordICS.Vizsgsorszám;

                        // utolsó V2 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV2 = (from a in AdatokICS
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V2")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV2 != null)
                        {
                            Tábla_ütemező.Rows[i].Cells[21].Value = rekordICS.KMUkm - rekordICSV2.Vizsgkm;
                            Tábla_ütemező.Rows[i].Cells[7].Value = rekordICS.KMUkm - rekordICSV2.Vizsgkm + korrekció;
                        }

                        // utolsó V3 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV3 = (from a in AdatokICS
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V3")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV3 != null)
                        {
                            Tábla_ütemező.Rows[i].Cells[22].Value = rekordICS.KMUkm - rekordICSV3.Vizsgkm;
                            Tábla_ütemező.Rows[i].Cells[8].Value = rekordICS.KMUkm - rekordICSV3.Vizsgkm + korrekció;
                        }


                        Adat_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                  where a.Típus == rekordICS.Ciklusrend.Trim() &&
                                                  a.Sorszám == rekordICS.Vizsgsorszám + 1
                                                  select a).FirstOrDefault();

                        if (ElemCiklus != null)
                            Tábla_ütemező.Rows[i].Cells[10].Value = ElemCiklus.Vizsgálatfok;
                        else
                            Tábla_ütemező.Rows[i].Cells[10].Value = "";
                        Adat_Nap_Hiba ElemHiba = (from a in AdatokHiba
                                                  where a.Azonosító == rekord.Azonosító
                                                  select a).FirstOrDefault();
                        if (ElemHiba != null) Tábla_ütemező.Rows[i].Cells[11].Value = $"{ElemHiba.Üzemképtelen.Trim()}-{ElemHiba.Beálló.Trim()}-{ElemHiba.Üzemképeshiba.Trim()}";
                    }

                    Holtart.Lép();
                }
                Vezénylés_listázása();

                Tábla_ütemező.Refresh();
                Tábla_ütemező.Sort(Tábla_ütemező.Columns[6], System.ComponentModel.ListSortDirection.Descending);
                Tábla_ütemező.Visible = true;
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

        private void Vezénylés_listázása()
        {
            try
            {
                VezénylésListaFeltöltés();

                foreach (DataGridViewRow Sor in Tábla_ütemező.Rows)
                {
                    Adat_Vezénylés ElemVezénylés = (from a in AdatokVezénylés
                                                    where a.Dátum >= Dátum_ütem.Value &&
                                                    a.Törlés == 0 &&
                                                    a.Azonosító == Sor.Cells[0].Value.ToStrTrim()
                                                    select a).FirstOrDefault();
                    if (ElemVezénylés != null)
                    {
                        if (ElemVezénylés.Vizsgálatraütemez == 1)
                            Sor.Cells[16].Value = "1";
                        else
                            Sor.Cells[16].Value = "0";
                        Sor.Cells[15].Value = ElemVezénylés.Rendelésiszám.Trim();
                        Sor.Cells[14].Value = ElemVezénylés.Státus;
                    }
                    Holtart.Lép();
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

        void Táblázatba_kattint(int sor)
        {
            try
            {
                if (Tábla_ütemező.Rows[sor].Cells[0].Value == null) throw new HibásBevittAdat("Nincs kijelölve érvényes sor.");
                if (!int.TryParse(Tábla_ütemező.Rows[sor].Cells[14].Value.ToString(), out int állapot)) állapot = 0;
                bool ütemez = true;
                if (Tábla_ütemező.Rows[sor].Cells[16].Value.ToStrTrim() == "0") ütemez = false;
                if (!int.TryParse(Tábla_ütemező.Rows[sor].Cells[19].Value.ToString(), out int v_sorszám)) v_sorszám = 0;
                if (!int.TryParse(Tábla_ütemező.Rows[sor].Cells[6].Value.ToString(), out int v_km)) v_km = 0;

                Adat_ICS_Ütem Küld = new Adat_ICS_Ütem(
                    Tábla_ütemező.Rows[sor].Cells[0].Value.ToStrTrim(),
                    állapot,
                    ütemez,
                    Tábla_ütemező.Rows[sor].Cells[15].Value.ToStrTrim(),
                    v_sorszám,
                    Tábla_ütemező.Rows[sor].Cells[5].Value.ToStrTrim(),
                    v_km,
                    Tábla_ütemező.Rows[sor].Cells[10].Value.ToStrTrim(),
                    v_sorszám + 1
                    );

                if (Új_Ablak_ICS_KCSV_segéd != null) Új_Ablak_ICS_KCSV_segéd = null;

                Új_Ablak_ICS_KCSV_segéd = new Ablak_ICS_KCSV_segéd(Dátum_ütem.Value, Cmbtelephely.Text.Trim(), Küld);
                Új_Ablak_ICS_KCSV_segéd.FormClosed += Új_Ablak_ICS_KCSV_segéd_FormClosed;
                Új_Ablak_ICS_KCSV_segéd.Változás += Vezénylés_listázása;
                Új_Ablak_ICS_KCSV_segéd.Változás += Ütemezettkocsik;
                Új_Ablak_ICS_KCSV_segéd.Show();
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

        Ablak_ICS_KCSV_segéd Új_Ablak_ICS_KCSV_segéd;

        private void Tábla_ütemező_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) throw new HibásBevittAdat("Nincs kijelölve érvényes sor.");
                Táblázatba_kattint(e.RowIndex);
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

        private void Új_Ablak_ICS_KCSV_segéd_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_ICS_KCSV_segéd = null;
        }



        private void Ütemezettkocsik()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\";
                if (!Exists(hely))
                    System.IO.Directory.CreateDirectory(hely);
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\" + Dátum_ütem.Value.Year;
                if (!Exists(hely))
                    System.IO.Directory.CreateDirectory(hely);
                hely += @"\vezénylés" + Dátum_ütem.Value.Year + ".mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Vezényléstábla(hely);
                string jelszó = "tápijános";
                DateTime kezdet = Dátum_ütem.Value.AddDays(-5);
                DateTime vége = Dátum_ütem.Value.AddDays(5);

                string szöveg = "SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]>=#" + kezdet.ToString("M-d-yy") + "# AND [dátum]<=#" + vége.ToString("M-d-yy") + "#";
                szöveg += " ORDER BY dátum, vizsgálat, azonosító";

                Tábla_vezénylés.Rows.Clear();
                Tábla_vezénylés.Columns.Clear();
                Tábla_vezénylés.Refresh();
                Tábla_vezénylés.Visible = false;
                Tábla_vezénylés.ColumnCount = 4;

                // fejléc elkészítése
                Tábla_vezénylés.Columns[0].HeaderText = "Dátum";
                Tábla_vezénylés.Columns[0].Width = 100;
                Tábla_vezénylés.Columns[1].HeaderText = "Psz.";
                Tábla_vezénylés.Columns[1].Width = 60;
                Tábla_vezénylés.Columns[2].HeaderText = "Vizsgálat";
                Tábla_vezénylés.Columns[2].Width = 80;
                Tábla_vezénylés.Columns[3].HeaderText = "";
                Tábla_vezénylés.Columns[3].Width = 80;

                Kezelő_Vezénylés Kéz = new Kezelő_Vezénylés();
                List<Adat_Vezénylés> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;
                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    if (rekord.Vizsgálatraütemez == 1)
                    {
                        Tábla_vezénylés.RowCount++;
                        i = Tábla_vezénylés.RowCount - 1;
                        Tábla_vezénylés.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Tábla_vezénylés.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                        Tábla_vezénylés.Rows[i].Cells[2].Value = rekord.Vizsgálat.Trim();
                        if (rekord.Státus == 3)
                            Tábla_vezénylés.Rows[i].Cells[3].Value = "Beálló";
                        else
                            Tábla_vezénylés.Rows[i].Cells[3].Value = "Benn marad";

                    }
                }

                Tábla_vezénylés.Refresh();
                Tábla_vezénylés.Visible = true;

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


        private void Tábla_vezénylés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Tábla_vezénylés.Rows.Count < 1)
                    return;

                Dátum_ütem.Value = DateTime.Parse(Tábla_vezénylés.Rows[e.RowIndex].Cells[0].Value.ToString());
                Ütemező_lekérdezés();
                if (Tábla_ütemező.Rows.Count < 1)
                    return;
                // megkeressük a nagytáblába, majd kiíratjuk

                for (int i = 0; i < Tábla_ütemező.Rows.Count; i++)
                {
                    if (Tábla_vezénylés.Rows[e.RowIndex].Cells[1].Value.ToStrTrim() == Tábla_ütemező.Rows[i].Cells[0].Value.ToStrTrim())
                    {
                        Táblázatba_kattint(i);
                        break;
                    }
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


        private void Tábla_ütemező_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                if (Tábla_ütemező.RowCount < 1)
                    return;
                foreach (DataGridViewRow row in Tábla_ütemező.Rows)
                {
                    if (row.Cells[18].Value.ToString() == "4")
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
                    }
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


        private void Btn_Vezénylésbeírás_Click(object sender, EventArgs e)
        {
            try
            {

                string helyütemez = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum_ütem.Value.Year}\vezénylés{Dátum_ütem.Value.Year}.mdb";
                string jelszóütemez = "tápijános";
                string szöveg = $"SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]=#{Dátum_ütem.Value:M-d-yy}# order by  azonosító";
                Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
                List<Adat_Vezénylés> AdatokVezénylés = KézVezénylés.Lista_Adatok(helyütemez, jelszóütemez, szöveg);

                // Módosítjuk a jármű státuszát
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                JárműListaFeltöltés();

                // megnyitjuk a hibákat
                List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

                // naplózás
                string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Now:yyyyMM}hibanapló.mdb";
                if (!Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);

                Holtart.Be();
                int talált, szín, újstátus = 0;
                DateTime mikor;
                int i = 1;
                // ha van ütemezett kocsi
                foreach (Adat_Vezénylés rekordütemez in AdatokVezénylés)
                {
                    újstátus = 0;
                    Holtart.Lép();
                    if (rekordütemez.Takarításraütemez == 1 | rekordütemez.Vizsgálatraütemez == 1)
                    {
                        // hiba leírása
                        string szöveg1 = rekordütemez.Vizsgálat.Trim() + "-" + rekordütemez.Vizsgálatszám;
                        string szöveg3 = szöveg1;

                        if (rekordütemez.Státus == 4)
                        {
                            szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd") + " Maradjon benn ";
                        }
                        else
                        {
                            szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd") + " Beálló ";
                        }
                        if (rekordütemez.Takarításraütemez == 1)
                        {
                            szöveg1 += "+Mosó ";
                        }
                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                        talált = 0;

                        Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                    where a.Azonosító == rekordütemez.Azonosító &&
                                                    a.Hibaleírása.Contains(szöveg3)
                                                    select a).FirstOrDefault();
                        if (ElemHiba != null) talált = 1;

                        ElemHiba = (from a in AdatokHiba
                                    where a.Azonosító == rekordütemez.Azonosító &&
                                    a.Hibaleírása.Contains(szöveg1)
                                    select a).FirstOrDefault();
                        if (ElemHiba != null) talált = 1;


                        szín = 0;
                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                        if (talált == 0)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell
                            Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                    where a.Azonosító == rekordütemez.Azonosító
                                                    select a).FirstOrDefault();
                            long hibáksorszáma = 0;
                            string típusa = "";
                            long státus = 0;
                            if (ElemJármű != null)
                            {
                                hibáksorszáma = ElemJármű.Hibák;
                                típusa = ElemJármű.Típus;
                                státus = ElemJármű.Státus;
                            }


                            szín = 1;
                            long hiba = hibáksorszáma + 1;

                            if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                            {
                                // ha a következő napra ütemez
                                if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum_ütem.Value.ToString("yyyy.MM.dd"))
                                {
                                    if (rekordütemez.Státus == 4)
                                    {
                                        státus = 4;
                                        mikor = DateTime.Now;
                                    }
                                    else
                                    {
                                        státus = 3;
                                    }
                                }
                                else if (státus < 4)
                                    státus = 3;
                                // ha ma van  
                                if (DateTime.Today.ToString("yyyy.MM.dd") == Dátum_ütem.Value.ToString("yyyy.MM.dd"))
                                {
                                    státus = 4;
                                    mikor = DateTime.Now;
                                }
                            }
                            else
                            {
                                újstátus = 1;
                            }

                            // rögzítjük a villamos.mdb-be
                            szöveg = "UPDATE állománytábla SET ";
                            szöveg += " hibák=" + hiba.ToString() + ", ";
                            // csak akkor módosítkjuk a dátumot, ha nem áll
                            if (státus == 4 & újstátus == 0)
                                szöveg += " miótaáll='" + DateTime.Now.ToString() + "', ";
                            szöveg += " státus=" + státus.ToString();
                            szöveg += " WHERE  [azonosító]='" + rekordütemez.Azonosító.Trim() + "'";
                            MyA.ABMódosítás(hely, jelszó, szöveg);


                            // beírjuk a hibákat

                            if (szín == 1)
                            {
                                szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                                szöveg += "'" + Program.PostásNév.Trim() + "', ";
                                // ha a következő napra ütemez
                                if (DateTime.Today.AddDays(1) == Dátum_ütem.Value)
                                {
                                    if (rekordütemez.Státus == 4)
                                        szöveg += " 4, ";
                                    else
                                        szöveg += " 3, ";
                                }

                                else
                                    szöveg += " 3, ";

                                szöveg += "'" + szöveg1.Trim() + "', ";
                                szöveg += "'" + DateTime.Now.ToString() + "', false, ";
                                szöveg += "'" + típusa.Trim() + "', ";
                                szöveg += "'" + rekordütemez.Azonosító.Trim() + "', " + hibáksorszáma.ToString() + ")";
                                string helyhiba = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\hiba.mdb";
                                MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                                // naplózzuk a hibákat
                                MyA.ABMódosítás(helynapló, jelszó, szöveg);
                            }
                        }

                    }
                    i += 1;
                    if (i == 21)
                        i = 1;
                }


                Holtart.Ki();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_ütemező.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "ICS_KCSV_ütemzés_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_ütemező, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc + ".xlsx");
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


        private void Dátum_ütem_ValueChanged(object sender, EventArgs e)
        {
            Ütemezettkocsik();
        }

        private void VizsAdat_Frissít_Click(object sender, EventArgs e)
        {
            Kiirjaatörténelmet();
        }

        private void VizsAdat_Excel_Click(object sender, EventArgs e)
        {

            try
            {
                if (Tábla1.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"{Pályaszám.Text.Trim()}_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla1, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc + ".xlsx");
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
        #endregion


        #region Listák

        private void CiklusListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = $"select * from ciklusrendtábla";

                AdatokCiklus.Clear();
                AdatokCiklus = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);
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

        private void JárműListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokJármű.Clear();
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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

        private void JárműFőListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokFőJármű.Clear();
                AdatokFőJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KarbListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = "SELECT * FROM kmtábla order by id desc ";
                AdatokICSKmadatok.Clear();
                AdatokICSKmadatok = KézICSKmadatok.Lista_Adat(hely, jelszó, szöveg);
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

        private void KerékadatokListaFeltöltés()
        {
            try
            {
                AdatokMérés.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.AddYears(-1).Year}\telepikerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "SELECT * FROM keréktábla ORDER BY kerékberendezés asc, mikor desc";

                AdatokMérés = KézMérés.Lista_Adatok(hely, jelszó, szöveg);
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\telepikerék.mdb";

                List<Adat_Kerék_Mérés> AdatokMérés1 = KézMérés.Lista_Adatok(hely, jelszó, szöveg);
                AdatokMérés.AddRange(AdatokMérés1);
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

        private void Jármű2ListaFeltöltés()
        {
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\adatok\villamos\villamos2.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM állománytábla";
                AdatokJármű2.Clear();
                AdatokJármű2 = KézJármű2.Lista_Adatok(hely, jelszó, szöveg);

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

        private void Jármű2ICSListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Villamos\Villamos2ICS.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM állománytábla";
                AdatokJármű2ICS.Clear();
                AdatokJármű2ICS = KézJármű2ICS.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Tábla_ütemező_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HibaListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\adatok\villamos\Új_napihiba.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM hiba  ORDER BY azonosító";
                AdatokHiba.Clear();
                AdatokHiba = KézHiba.Lista_adatok(hely, jelszó, szöveg);
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

        private void VezénylésListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum_ütem.Value.Year}\vezénylés{Dátum_ütem.Value.Year}.mdb";
                string jelszó = "tápijános";
                string szöveg = "SELECT * FROM vezényléstábla";
                AdatokVezénylés.Clear();
                AdatokVezénylés = KézVezénylés.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KorrekcióListaFeltöltés()
        {
            try
            {
                AdatokZserKm.Clear();
                List<Adat_Főkönyv_Zser_Km> Előző = KézKorr.Lista_adatok(DateTime.Today.Year);
                AdatokZserKm.AddRange(Előző);

                Előző = KézKorr.Lista_adatok(DateTime.Today.Year - 1);
                AdatokZserKm.AddRange(Előző);
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
        #endregion
    }
}