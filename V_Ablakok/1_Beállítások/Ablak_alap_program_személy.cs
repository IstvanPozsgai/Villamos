using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_alap_program_személy
    {
        private static string Pdfhely = "";
        #region Kezelők
        readonly Kezelő_OktatásTábla KézOkt = new Kezelő_OktatásTábla();
        readonly Kezelő_Kiegészítő_Feorszámok KézFeorszám = new Kezelő_Kiegészítő_Feorszámok();
        readonly Kezelő_Kiegészítő_Jogtípus KézJogTípus = new Kezelő_Kiegészítő_Jogtípus();
        readonly Kezelő_Kiegészítő_JogVonal KézJogVonal = new Kezelő_Kiegészítő_JogVonal();
        readonly Kezelő_Kiegészítő_Könyvtár kézKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Kiegészítő_főkönyvtábla KézAláíró = new Kezelő_Kiegészítő_főkönyvtábla();
        readonly Kezelő_Kiegészítő_Beosztáskódok kézBeoKód = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Kiegészítő_Védelem KézKiegVéd = new Kezelő_Kiegészítő_Védelem();
        readonly Kezelő_Kiegészítő_Munkakör KézMunkakör = new Kezelő_Kiegészítő_Munkakör();
        readonly Kezelő_Behajtás_Engedélyezés KézBehEng = new Kezelő_Behajtás_Engedélyezés();
        #endregion

        #region Listák
        List<Adat_Behajtás_Engedélyezés> AdatokBehEng = new List<Adat_Behajtás_Engedélyezés>();
        List<Adat_Kiegészítő_Védelem> AdatokKiegVéd = new List<Adat_Kiegészítő_Védelem>();
        List<Adat_Kiegészítő_Beosztáskódok> AdatokBeoKód = new List<Adat_Kiegészítő_Beosztáskódok>();
        List<Adat_Kiegészítő_főkönyvtábla> AdatokAlárás = new List<Adat_Kiegészítő_főkönyvtábla>();
        List<Adat_Kiegészítő_Jelenlétiív> AdatokJelenléti = new List<Adat_Kiegészítő_Jelenlétiív>();
        List<Adat_Kiegészítő_Könyvtár> AdatokKönyvtár = new List<Adat_Kiegészítő_Könyvtár>();
        List<Adat_Kiegészítő_Jogvonal> AdatokJogVonal = new List<Adat_Kiegészítő_Jogvonal>();
        List<Adat_Kiegészítő_Jogtípus> AdatokJog = new List<Adat_Kiegészítő_Jogtípus>();
        List<Adat_OktatásTábla> AdatokOktatás = new List<Adat_OktatásTábla>();
        List<Adat_Kiegészítő_Feorszámok> AdatokFeorSzám = new List<Adat_Kiegészítő_Feorszámok>();
        List<Adat_Kiegészítő_Munkakör> AdatokMunkakör = new List<Adat_Kiegészítő_Munkakör>();
        #endregion


        public Ablak_alap_program_személy()
        {
            InitializeComponent();
        }


        private void AblakProgramadatokszemély_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();
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

        private void Ablak_alap_program_személy_Shown(object sender, EventArgs e)
        {
            try
            {
                Jogosultságkiosztás();
                Fülek.SelectedIndex = 0;
                Fülekkitöltése();
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
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Személy(false));
                if (Program.PostásTelephely == "Főmérnökség")
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString(); }
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
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Feljebb.Visible = true;
                    FrissítMunkakör.Visible = true;
                    Command1.Visible = true;
                    Command4.Visible = true;
                    Feortörlés.Visible = true;


                    Command5.Visible = true;

                    Command3.Visible = true;

                    Command9.Visible = true;
                    Töröl.Visible = true;

                    Munka_Rögzít.Visible = true;

                    Védő_rögzít.Visible = true;

                    Gond_rögzít.Visible = true;
                    Gond_töröl.Visible = true;
                }
                else
                {
                    Feljebb.Visible = false;
                    FrissítMunkakör.Visible = false;
                    Command1.Visible = false;
                    Command4.Visible = false;
                    Feortörlés.Visible = false;


                    Command5.Visible = false;

                    Command3.Visible = false;

                    Command9.Visible = false;
                    Töröl.Visible = false;

                    Munka_Rögzít.Visible = false;

                    Védő_rögzít.Visible = false;

                    Gond_rögzít.Visible = false;
                    Gond_töröl.Visible = false;
                }
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                BtnOktatásFel.Enabled = false;
                BtnOktatásOK.Enabled = false;
                BtnOktatásÚj.Enabled = false;

                Feljebb.Enabled = false;
                FrissítMunkakör.Enabled = false;
                Command1.Enabled = false;
                Command4.Enabled = false;
                Feortörlés.Enabled = false;

                Command5.Enabled = false;

                Command3.Enabled = false;

                Command9.Enabled = false;
                Töröl.Enabled = false;

                Munka_Rögzít.Enabled = false;

                Védő_rögzít.Enabled = false;

                Gond_rögzít.Enabled = false;
                Gond_töröl.Enabled = false;

                melyikelem = 5;
                // módosítás 1 Oktatás lapfül
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    BtnOktatásFel.Enabled = true;
                    BtnOktatásOK.Enabled = true;
                    BtnOktatásÚj.Enabled = true;
                }
                // módosítás 2 Munkakör lapfül
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    FrissítMunkakör.Enabled = true;
                    Command1.Enabled = true;
                    Command4.Enabled = true;
                    Feortörlés.Enabled = true;
                    Feljebb.Enabled = true;
                }
                // módosítás 3 jogosítvány és típus
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Command5.Enabled = true;
                }

                melyikelem = 6;
                // módosítás 1 jogosítvány és vonal
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Command3.Enabled = true;
                }
                // módosítás 2 Szervezeti
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Command9.Enabled = true;
                    Töröl.Enabled = true;
                }
                // módosítás 3 Dokumentumok
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Munka_Rögzít.Enabled = true;
                }

                melyikelem = 8;
                // módosítás 1 Védő
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Védő_rögzít.Enabled = true;
                }
                // módosítás 2 Gondnok
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Gond_rögzít.Enabled = true;
                    Gond_töröl.Enabled = true;
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {

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
            try
            {
                Fülekkitöltése();
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

        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            // csoport
                            CsoportSorszámEll();
                            Csoportlista_listázás();
                            break;
                        }
                    case 1:
                        {
                            // jelenléti ív aláírások
                            Jelenléti_kitöltés();
                            Jelenlét_aláírók();
                            break;
                        }

                    case 2:
                        {
                            // Beosztáskódok
                            BeosztásTáblaíró();
                            break;
                        }
                    case 3:
                        {
                            // oktatás fül
                            TáblaOktatáslistázás();
                            Oktatásistátusok();
                            OktDátum.Value = DateTime.Now;
                            break;
                        }
                    case 4: //PDF
                        {
                            PDF_Betöltése();
                            break;
                        }
                    case 5:
                        {
                            // Munkakör Feor
                            TáblakiírásFeor();
                            break;
                        }
                    case 6:
                        {
                            // jogosítvány és típus
                            Tábla2kiirás();
                            break;
                        }
                    case 7:
                        {
                            // jogosítvány és vonal
                            Tábla1kiirás();
                            break;
                        }
                    case 8:
                        {
                            // Szervezeti könyvtár
                            Tábla3kiirás();
                            break;
                        }
                    case 9:
                        {
                            // Dokumentumok
                            Munka_Tábla_kiirás();
                            Munka_Kategória_Feltöltés();
                            break;
                        }
                    case 10:
                        {
                            // Védő
                            Védő_tábla_kiir();
                            AcceptButton = Védő_rögzít;
                            break;
                        }
                    case 11:
                        {
                            // gondnok
                            Gondnok_tábla_listázás();
                            Gond_ürít();
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

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
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

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Alapszemély.html";
                MyE.Megnyitás(hely);
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


        #region Csoport
        private void CsoportTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                CsoportTábla.Rows[e.RowIndex].Selected = true;
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

        private void CsoportOK_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg;
                CsoportNév.Text = MyF.Szöveg_Tisztítás(CsoportNév.Text);

                if (CsoportNév.Text.Trim() == "") throw new HibásBevittAdat("A Csoport név mező nem lehet üres!");
                if (CsoportTípus.Text.Trim() == "")
                    CsoportTípus.Text = "*";

                List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = CsoportLista();

                Adat_Kiegészítő_Csoportbeosztás Elem = (from a in AdatokCsop
                                                        where a.Csoportbeosztás == CsoportNév.Text.Trim()
                                                        select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = " UPDATE csoportbeosztás SET ";
                    szöveg += $" típus='{CsoportTípus.Text.Trim()}'";
                    szöveg += $" WHERE csoportbeosztás='{CsoportNév.Text.Trim()}'";
                }
                else
                {
                    double utolsó = 1;
                    if (AdatokCsop.Count > 0) utolsó = AdatokCsop.Max(a => a.Sorszám) + 1;
                    // Ha nem talált akkor rögzít
                    szöveg = $"INSERT INTO csoportbeosztás (sorszám, csoportbeosztás, típus) VALUES ({utolsó}, '{CsoportNév.Text.Trim()}', '{CsoportTípus.Text.Trim()}' )";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Csoportlista_listázás();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CsoportTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (CsoportTábla.SelectedRows.Count == 0) return;
                if (!long.TryParse(CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index].Cells[0].Value.ToStrTrim(), out long Sorszám)) throw new HibásBevittAdat("Érvénytelen sorszám");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";

                // megkeressük azt amit törölni kell
                List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = CsoportLista();
                Adat_Kiegészítő_Csoportbeosztás Elem = (from a in AdatokCsop
                                                        where a.Sorszám == Sorszám
                                                        select a).FirstOrDefault();
                if (Elem != null)
                {
                    // Ha  talált akkor töröl
                    string szöveg = $" DELETE FROM csoportbeosztás WHERE sorszám={Sorszám}";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
                Csoportlista_listázás();
                CsoportSorszámEll();
                Csoportlista_listázás();
                MessageBox.Show("Az adat törlése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CsoportFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CsoportTábla.SelectedRows.Count == 0) return;
                if (CsoportTábla.SelectedRows[0].Index == 0) throw new HibásBevittAdat("Az elsőt nem lehet feljebb vinni.");

                string szöveg;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";


                // a kiválasztott sor elé mentjük
                szöveg = " UPDATE csoportbeosztás SET ";
                szöveg += $" sorszám={CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index - 1].Cells[0].Value.ToÉrt_Int()}";
                szöveg += $" WHERE csoportbeosztás='{CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index].Cells[1].Value.ToStrTrim()}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                // az előzőt hátrébb rakjuk
                szöveg = " UPDATE csoportbeosztás SET ";
                szöveg += $" sorszám={CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index].Cells[0].Value.ToÉrt_Int()}";
                szöveg += $" WHERE csoportbeosztás='{CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index - 1].Cells[1].Value.ToStrTrim()}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Csoportlista_listázás();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Csoportlista_listázás()
        {
            try
            {
                List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = CsoportLista();

                CsoportTábla.Rows.Clear();
                CsoportTábla.Columns.Clear();
                CsoportTábla.Refresh();
                CsoportTábla.Visible = false;
                CsoportTábla.ColumnCount = 3;
                // fejléc elkészítése
                CsoportTábla.Columns[0].HeaderText = "Sorszám";
                CsoportTábla.Columns[0].Width = 140;
                CsoportTábla.Columns[1].HeaderText = "Csoport név";
                CsoportTábla.Columns[1].Width = 400;
                CsoportTábla.Columns[2].HeaderText = "Csoport típus";
                CsoportTábla.Columns[2].Width = 100;
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in AdatokCsop)
                {
                    CsoportTábla.RowCount++;
                    int i = CsoportTábla.RowCount - 1;
                    CsoportTábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    CsoportTábla.Rows[i].Cells[1].Value = rekord.Csoportbeosztás;
                    CsoportTábla.Rows[i].Cells[2].Value = rekord.Típus;
                }
                CsoportTábla.Visible = true;
                CsoportTábla.Refresh();
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

        private void CsoportTábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (CsoportTábla.SelectedRows.Count != 0)
                {
                    CsoportNév.Text = CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index].Cells[1].Value.ToStrTrim();
                    CsoportTípus.Text = CsoportTábla.Rows[CsoportTábla.SelectedRows[0].Index].Cells[2].Value.ToStrTrim();
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

        private List<Adat_Kiegészítő_Csoportbeosztás> CsoportLista()
        {
            List<Adat_Kiegészítő_Csoportbeosztás> Válasz = null;
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM csoportbeosztás ORDER BY sorszám";
                Kezelő_Kiegészítő_Csoportbeosztás KézCsop = new Kezelő_Kiegészítő_Csoportbeosztás();
                Válasz = KézCsop.Lista_Adatok(hely, jelszó, szöveg);
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
            return Válasz;
        }

        private void CsoportSorszámEll()
        {
            List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = CsoportLista();
            int i = 1;
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
            string jelszó = "Mocó";
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Kiegészítő_Csoportbeosztás rekord in AdatokCsop)
            {
                long ideig = rekord.Sorszám - 1;
                if (i != ideig)
                {   //Ha a sorszám nem a következő akkor módosítjuk

                    string szöveg = "UPDATE csoportbeosztás  SET ";
                    szöveg += $"sorszám={i + 1}";
                    szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}' AND  Típus='{rekord.Típus}'";
                    SzövegGy.Add(szöveg);
                }
                i++;
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }
        #endregion


        #region Oktatás
        private void OktatásListaFeltöltés()
        {
            try
            {
                AdatokOktatás.Clear();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                string szöveg = $"SELECT * FROM Oktatástábla WHERE telephely='{Cmbtelephely.Text}' ORDER BY listázásisorrend";
                AdatokOktatás = KézOkt.Lista_Adatok(hely, jelszó, szöveg);
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

        private void TáblaOktatáslistázás()
        {
            try
            {
                OktatásListaFeltöltés();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sor- szám");
                AdatTábla.Columns.Add("Oktatás témája");
                AdatTábla.Columns.Add("Kategória");
                AdatTábla.Columns.Add("Gyakoriság");
                AdatTábla.Columns.Add("Gyakoriság hónap");
                AdatTábla.Columns.Add("Státus");
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Telephely");
                AdatTábla.Columns.Add("Listázási sorrend");
                AdatTábla.Columns.Add("PDF fájl neve");

                AdatTábla.Clear();
                foreach (Adat_OktatásTábla rekord in AdatokOktatás)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sor- szám"] = rekord.IDoktatás;
                    Soradat["Oktatás témája"] = rekord.Téma;
                    Soradat["Kategória"] = rekord.Kategória;
                    Soradat["Gyakoriság"] = rekord.Gyakoriság;
                    Soradat["Gyakoriság hónap"] = rekord.Ismétlődés;
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Listázási sorrend"] = rekord.Listázásisorrend;
                    Soradat["PDF fájl neve"] = rekord.PDFfájl;

                    AdatTábla.Rows.Add(Soradat);
                }
                TáblaOktatás.DataSource = AdatTábla;

                TáblaOktatás.Columns["Sor- szám"].Width = 50;
                TáblaOktatás.Columns["Oktatás témája"].Width = 520;
                TáblaOktatás.Columns["Kategória"].Width = 120;
                TáblaOktatás.Columns["Gyakoriság"].Width = 110;
                TáblaOktatás.Columns["Gyakoriság hónap"].Width = 100;
                TáblaOktatás.Columns["Státus"].Width = 100;
                TáblaOktatás.Columns["Dátum"].Width = 110;
                TáblaOktatás.Columns["Telephely"].Width = 120;
                TáblaOktatás.Columns["Listázási sorrend"].Width = 70;
                TáblaOktatás.Columns["PDF fájl neve"].Width = 200;

                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = true;
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

        private void TáblaOktatás_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (TáblaOktatás.RowCount == 0)
                    return;
                {
                    if (e.RowIndex >= 0)
                    {
                        IDoktatás.Text = TáblaOktatás.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                        Téma.Text = TáblaOktatás.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                        CmbKategória.Text = TáblaOktatás.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                        CmbGyakoriság.Text = TáblaOktatás.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
                        Ismétlődés.Text = TáblaOktatás.Rows[e.RowIndex].Cells[4].Value.ToStrTrim();
                        CMBStátus.Text = TáblaOktatás.Rows[e.RowIndex].Cells[5].Value.ToStrTrim();
                        OktDátum.Value = TáblaOktatás.Rows[e.RowIndex].Cells[6].Value.ToÉrt_DaTeTime();
                        TxtSorrend.Text = TáblaOktatás.Rows[e.RowIndex].Cells[8].Value.ToStrTrim();
                        TxtPDFfájl.Text = TáblaOktatás.Rows[e.RowIndex].Cells[9].Value.ToStrTrim();
                        if (TxtPDFfájl.Text.Trim() == "" || TxtPDFfájl.Text.Trim() == "_")
                        {
                            Pdfhely = "";
                        }
                        else
                        {
                            Pdfhely = Application.StartupPath + @"\Főmérnökség\Kezelési\" + Cmbtelephely.Text.Trim() + @"\" + TxtPDFfájl.Text.Trim();
                        }
                        TxtOktatásRow.Text = e.RowIndex.ToString();
                        if (e.RowIndex > 0)
                        {
                            IDoktatáselőző.Text = TáblaOktatás.Rows[e.RowIndex - 1].Cells[0].Value.ToStrTrim();
                            TxtOktatássorszám.Text = TáblaOktatás.Rows[e.RowIndex - 1].Cells[8].Value.ToStrTrim();
                        }
                        else
                        {
                            TxtOktatássorszám.Text = 0.ToString();
                            IDoktatáselőző.Text = 0.ToString();
                        }
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

        private void Oktatásistátusok()
        {
            try
            {
                // töröljük az előzményeket
                CMBStátus.Items.Clear();
                CmbKategória.Items.Clear();
                CmbGyakoriság.Items.Clear();
                // feltöltjök amit kell
                // kategória
                OktatásListaFeltöltés();

                List<Adat_OktatásTábla> Adatok = (from a in AdatokOktatás
                                                  orderby a.Kategória
                                                  where a.Státus == "Érvényes"
                                                  select a).ToList();

                List<string> Kategória = Adatok.Select(a => a.Kategória).Distinct().ToList();
                foreach (string rekord in Kategória)
                    CmbKategória.Items.Add(rekord);


                // gyakoriság
                Adatok = (from a in AdatokOktatás
                          orderby a.Gyakoriság
                          where a.Státus == "Érvényes"
                          select a).ToList();
                List<string> Gyakoriság = Adatok.Select(a => a.Gyakoriság).Distinct().ToList();
                foreach (string rekord in Gyakoriság)
                    CmbGyakoriság.Items.Add(rekord);

                // státusok 
                CMBStátus.Items.Add("Érvényes");
                CMBStátus.Items.Add("Törölt");
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

        private void BtnOktatásÚj_Click(object sender, EventArgs e)
        {
            try
            {
                Oktatásürítés();
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

        private void BtnOktatásOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Téma.Text.Trim() == "") throw new HibásBevittAdat("A Oktatás Téma mező nem lehet üres!");
                if (CmbKategória.Text.Trim() == "") throw new HibásBevittAdat("A Kategória mező nem lehet üres!");
                if (CmbGyakoriság.Text.Trim() == "") throw new HibásBevittAdat("A Gyakorság mező nem lehet üres!");
                if (CMBStátus.Text.Trim() == "") throw new HibásBevittAdat("A Státus mező nem lehet üres!");
                if (Ismétlődés.Text.Trim() == "") throw new HibásBevittAdat("A Gyakoriság hónap mező nem lehet üres!");
                if (!int.TryParse(Ismétlődés.Text, out int result)) throw new HibásBevittAdat("A Gyakoriság hónap mező nem lehet szöveg!");

                if (TxtPDFfájl.Text.Trim() == "") TxtPDFfájl.Text = "_";

                OktatásListaFeltöltés();

                string fájlnév, hely, jelszó, szöveg;
                //Megtisztítjuk a szöveget
                Téma.Text = MyF.Szöveg_Tisztítás(Téma.Text);
                CmbKategória.Text = MyF.Szöveg_Tisztítás(CmbKategória.Text);

                hely = Application.StartupPath + @"\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                jelszó = "pázmányt";
                if (IDoktatás.Text.Trim() == "")
                {
                    // új adat
                    // Az új ID

                    long i = 1;
                    if (AdatokOktatás.Count > 0) i = AdatokOktatás.Max(a => a.IDoktatás) + 1;
                    fájlnév = $"{i}_{Cmbtelephely.Text.Trim()}.pdf";

                    // az új listázásisorrend
                    List<Adat_OktatásTábla> Adatok = (from a in AdatokOktatás
                                                      where a.Telephely == Cmbtelephely.Text.Trim()
                                                      orderby a.Listázásisorrend descending
                                                      select a).ToList();
                    long j = 1;
                    if (Adatok.Count > 0) j = Adatok.Max(a => a.Listázásisorrend) + 1;

                    // új adat
                    szöveg = "INSERT INTO Oktatástábla ( IDoktatás, Téma, Kategória, gyakoriság, ismétlődés, státus, dátum, telephely,listázásisorrend, pdffájl )";
                    szöveg += $" VALUES ( {i}, ";
                    szöveg += $"'{Téma.Text.Trim()}', ";
                    szöveg += $"'{CmbKategória.Text.Trim()}', ";
                    szöveg += $"'{CmbGyakoriság.Text.Trim()}', ";
                    szöveg += $"{Ismétlődés.Text.Trim()}, ";
                    szöveg += $"'{CMBStátus.Text.Trim()}', ";
                    szöveg += $"'{OktDátum.Value:yyyy.MM.dd}', ";
                    szöveg += $"'{Cmbtelephely.Text.Trim()}', ";
                    szöveg += $"{j}, ";
                    if (TxtPDFfájl.Text == "_")
                        szöveg += $"'{TxtPDFfájl.Text.Trim()}' )";
                    else
                        szöveg += "'{fájlnév.Trim()} ' )";
                }
                else
                {
                    // meglévő módosítása

                    fájlnév = $"{IDoktatás.Text.Trim()}_{Cmbtelephely.Text.Trim()}.pdf";
                    szöveg = "UPDATE Oktatástábla SET ";
                    szöveg += $" téma='{Téma.Text.Trim()}', ";
                    szöveg += $" kategória='{CmbKategória.Text.Trim()}', ";
                    szöveg += $" gyakoriság='{CmbGyakoriság.Text.Trim()}', ";
                    szöveg += $" ismétlődés={Ismétlődés.Text.Trim()}, ";
                    szöveg += $" státus='{CMBStátus.Text.Trim()}', ";
                    szöveg += $" dátum='{OktDátum.Value:yyyy.MM.dd}', ";
                    szöveg += $" telephely='{Cmbtelephely.Text.Trim()}', ";
                    if ((TxtPDFfájl.Text == "_") | (TxtPDFfájl.Text.Trim() == fájlnév))
                        szöveg += $" pdffájl='{TxtPDFfájl.Text.Trim()}' ";
                    else
                        szöveg += $" pdffájl='{fájlnév.Trim()}' ";

                    szöveg += $" WHERE IDoktatás={IDoktatás.Text.Trim()}";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                // PDF fájlfeltöltése
                if ((TxtPDFfájl.Text == "_") || (TxtPDFfájl.Text.Trim() == fájlnév))
                {
                }
                else
                {
                    // ha van fájlnév és nem előre generált akkor feltölti
                    // megnézzük, hogy létezik-e a könyvtár
                    hely = Application.StartupPath + @"\Főmérnökség\Kezelési";
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    if (!Exists(hely))
                        System.IO.Directory.CreateDirectory(hely);
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    hely = Application.StartupPath + @"\Főmérnökség\Kezelési\" + Cmbtelephely.Text;
                    if (!Exists(hely))
                        System.IO.Directory.CreateDirectory(hely);
                    hely += @"\" + fájlnév;
                    if (Exists(hely) == true)
                    {
                        if (MessageBox.Show("Ezen a néven már létezik fájl, felülírjuk?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            Delete(hely);
                        }
                    }
                    // ha nem létezik akkor odamásoljuk
                    Copy(TxtPDFfájlteljes.Text, hely);

                }
                Oktatásürítés();
                TáblaOktatáslistázás();
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Oktatásürítés()
        {
            IDoktatás.Text = "";
            Téma.Text = "";
            CmbKategória.Text = "";
            CmbGyakoriság.Text = "";
            CMBStátus.Text = "Érvényes";
            TxtSorrend.Text = "";
            Ismétlődés.Text = "";
            TxtPDFfájl.Text = "";
            TxtPDFfájlteljes.Text = "";
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Oktatásürítés();
                TáblaOktatáslistázás();
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

        private void BtnOktatásFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtOktatásRow.Text.Trim() == "") return;
                if (TxtOktatásRow.Text.ToÉrt_Double() == 0d) return;
                if (!long.TryParse(IDoktatás.Text, out long OktatásID)) throw new HibásBevittAdat("Az Oktatás sorszáma mezőben nem szám van.");
                if (!long.TryParse(IDoktatáselőző.Text, out long OktatásIDelőző)) throw new HibásBevittAdat("Az Oktatás sorszáma mezőben nem szám van.");

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
                string jelszó = "pázmányt";
                List<string> SzövegGy = new List<string>();

                // előrébb visszük
                string szöveg = $"UPDATE Oktatástábla SET listázásisorrend={TxtOktatássorszám.Text} where idoktatás={OktatásID}";
                SzövegGy.Add(szöveg);

                // hátrább visszük
                szöveg = $"UPDATE Oktatástábla SET listázásisorrend={TxtSorrend.Text} where idoktatás={OktatásIDelőző}";
                SzövegGy.Add(szöveg);

                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                TxtOktatássorszám.Text = 0.ToString();
                IDoktatáselőző.Text = 0.ToString();
                Oktatásürítés();
                TáblaOktatáslistázás();
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

        private void TáblaOktatás_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // egész sor színezése ha törölt
                foreach (DataGridViewRow row in TáblaOktatás.Rows)
                {
                    if (row.Cells[5].Value.ToStrTrim() == "Törölt")
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void Button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaOktatás.Rows.Count <= 0) return;

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Oktatások_listája_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMdd}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                MyE.EXCELtábla(fájlexc, TáblaOktatás, true);
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

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                TxtPDFfájl.Text = "";
                TxtPDFfájlteljes.Text = "";
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf"
                };
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    TxtPDFfájl.Text = OpenFileDialog1.SafeFileName;
                    TxtPDFfájlteljes.Text = OpenFileDialog1.FileName;
                    Fülek.SelectedIndex = 3;
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

        private void TáblaOktatás_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                TáblaOktatás.Rows[e.RowIndex].Selected = true;
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


        #region PDF
        private void PDF_Betöltése()
        {
            try
            {
                if (Pdfhely.Trim() == "") return;
                if (!File.Exists(Pdfhely.Trim())) return;
                string hely = Pdfhely.Trim();

                Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);
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


        #region Feor
        private void FeorListaFeltöltés()
        {
            try
            {
                AdatokFeorSzám.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM feorszámok ORDER BY sorszám";
                AdatokFeorSzám = KézFeorszám.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Command4_Click(object sender, EventArgs e)
        {
            try
            {
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                if (!Exists(hely)) return;

                jelszó = "Mocó";
                FeorFeorszám.Text = MyF.Szöveg_Tisztítás(FeorFeorszám.Text);
                FeorFeormegnevezés.Text = MyF.Szöveg_Tisztítás(FeorFeormegnevezés.Text);
                if (FeorFeorszám.Text.Trim() == "") return;
                if (FeorFeormegnevezés.Text.Trim() == "") return;

                if (Feorsorszám.Text.Trim() != "")
                {
                    // módosítás
                    szöveg = "UPDATE feorszámok  SET";
                    szöveg += " feorszám='" + FeorFeorszám.Text.Trim() + "', ";
                    szöveg += " feormegnevezés='" + FeorFeormegnevezés.Text.Trim() + "' ";
                    szöveg += "WHERE sorszám=" + Feorsorszám.Text.Trim();
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                {
                    // új rögzítése
                    szöveg = "INSERT INTO feorszámok ( Feorszám, feormegnevezés, státus) VALUES";
                    szöveg += "( '" + FeorFeorszám.Text.Trim() + "', '" + FeorFeormegnevezés.Text.Trim() + "', 0)";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

                TáblakiírásFeor();
                Feorsorszám.Text = "";
                FeorFeorszám.Text = "";
                FeorFeormegnevezés.Text = "";
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

        private void Command1_Click(object sender, EventArgs e)
        {
            Feorsorszám.Text = "";
            FeorFeorszám.Text = "";
            FeorFeormegnevezés.Text = "";
        }

        private void Feljebb_Click(object sender, EventArgs e)
        {
            try
            {
                FeorListaFeltöltés();

                if (!long.TryParse(Feorsorszám.Text.Trim(), out long SorszámFeor)) return;
                if (SorszámFeor <= 1) return;


                // kiolvassuk a feljebb rakni kívánt rekordot
                Adat_Kiegészítő_Feorszámok Elem = (from a in AdatokFeorSzám
                                                   where a.Sorszám == SorszámFeor
                                                   select a).FirstOrDefault();
                // kiolvassuk az előző sorszámút
                Adat_Kiegészítő_Feorszámok Előző = (from a in AdatokFeorSzám
                                                    where a.Sorszám == SorszámFeor - 1
                                                    select a).FirstOrDefault();

                if (Elem == null || Előző == null) return;

                FeorMódosítás(Elem, Előző.Sorszám);
                FeorMódosítás(Előző, Elem.Sorszám);

                TáblakiírásFeor();
                Feorsorszám.Text = "";
                FeorFeorszám.Text = "";
                FeorFeormegnevezés.Text = "";
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

        private void FeorMódosítás(Adat_Kiegészítő_Feorszámok Adatok, long id)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "UPDATE feorszámok  SET ";
                szöveg += $"feorszám='{Adatok.Feorszám}', ";
                szöveg += $"feormegnevezés='{Adatok.Feormegnevezés}', ";
                szöveg += $"státus={Adatok.Státus} ";
                szöveg += $" WHERE sorszám={id} ";
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

        private void FeorTábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                {
                    if (FeorTábla.SelectedRows.Count != 0)
                    {
                        Feorsorszám.Text = FeorTábla.Rows[FeorTábla.SelectedRows[0].Index].Cells[0].Value.ToStrTrim();
                        FeorFeorszám.Text = FeorTábla.Rows[FeorTábla.SelectedRows[0].Index].Cells[1].Value.ToStrTrim();
                        FeorFeormegnevezés.Text = FeorTábla.Rows[FeorTábla.SelectedRows[0].Index].Cells[2].Value.ToStrTrim();
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

        private void TáblakiírásFeor()
        {
            try
            {
                FeorListaFeltöltés();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Szám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Státus");

                AdatTábla.Clear();
                foreach (Adat_Kiegészítő_Feorszámok rekord in AdatokFeorSzám)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Szám"] = rekord.Feorszám;
                    Soradat["Megnevezés"] = rekord.Feormegnevezés;
                    Soradat["Státus"] = rekord.Státus == 0 ? "Érvényes" : "Törölt";

                    AdatTábla.Rows.Add(Soradat);

                }
                FeorTábla.DataSource = AdatTábla;

                FeorTábla.Columns["Sorszám"].Width = 80;
                FeorTábla.Columns["Szám"].Width = 100;
                FeorTábla.Columns["Megnevezés"].Width = 400;
                FeorTábla.Columns["Státus"].Width = 400;

                FeorTábla.Visible = true;
                FeorTábla.Refresh();
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

        private void FrissítMunkakör_Click(object sender, EventArgs e)
        {
            try
            {
                TáblakiírásFeor();
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

        private void Feortörlés_Click(object sender, EventArgs e)
        {
            try
            {
                FeorListaFeltöltés();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";

                if (!long.TryParse(Feorsorszám.Text.Trim(), out long SorszámFeor)) return;
                if (FeorFeorszám.Text.Trim() == "") return;
                if (FeorFeormegnevezés.Text.Trim() == "") return;

                Adat_Kiegészítő_Feorszámok Elem = (from a in AdatokFeorSzám
                                                   where a.Sorszám == SorszámFeor
                                                   select a).FirstOrDefault();

                if (Elem != null)
                {
                    // módosítás
                    string szöveg = $"UPDATE feorszámok  SET Státus=1  WHERE sorszám={SorszámFeor}"; ;
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

                TáblakiírásFeor();
                Feorsorszám.Text = "";
                FeorFeorszám.Text = "";
                FeorFeormegnevezés.Text = "";
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

        private void FeorTábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                FeorTábla.Rows[e.RowIndex].Selected = true;
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

        private void FeorTábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // egész sor színezése ha törölt
                foreach (DataGridViewRow row in FeorTábla.Rows)
                {
                    if (row.Cells[3].Value.ToStrTrim() == "Törölt")
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void FeorTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                FeorTábla.Rows[e.RowIndex].Selected = true;
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


        #region Jogosítvány típus
        private void JogTípusListaFeltöltés()
        {
            try
            {
                AdatokJog.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM jogtípus  order by  sorszám";
                AdatokJog = KézJogTípus.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Command5_Click(object sender, EventArgs e)
        {
            try
            {
                Text4.Text = MyF.Szöveg_Tisztítás(Text4.Text);
                if (Text4.Text.Trim() == "") return;


                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg;
                if (Text2.Text.Trim() != "")
                {
                    // módosítás
                    szöveg = "UPDATE jogtípus  SET ";
                    szöveg += " típus='" + Text4.Text.Trim() + "' ";
                    szöveg += "WHERE sorszám=" + Text2.Text.Trim();
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                {
                    // új rögtzítés
                    szöveg = "INSERT INTO jogtípus ( típus ) VALUES ( '" + Text4.Text.Trim() + "' )";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                Tábla2kiirás();
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

        private void Tábla2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tábla2.Rows[e.RowIndex].Selected = true;
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


        private void Command6_Click(object sender, EventArgs e)
        {
            Text2.Text = "";
            Text4.Text = "";
        }

        private void Tábla2kiirás()
        {
            try
            {
                JogTípusListaFeltöltés();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Típus");

                AdatTábla.Clear();
                foreach (Adat_Kiegészítő_Jogtípus rekord in AdatokJog)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Típus"] = rekord.Típus;

                    AdatTábla.Rows.Add(Soradat);
                }
                Tábla2.DataSource = AdatTábla;

                Tábla2.Columns["Sorszám"].Width = 80;
                Tábla2.Columns["Típus"].Width = 800;

                Tábla2.Visible = true;
                Tábla2.Refresh();

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

        private void Tábla2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.SelectedRows.Count != 0)
                {
                    Text2.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[0].Value.ToStrTrim();
                    Text4.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[1].Value.ToStrTrim();
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

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla2kiirás();
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

        private void Tábla2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tábla2.Rows[e.RowIndex].Selected = true;
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


        #region Jogosítvány viszonylat
        private void JogVonalListaFeltöltés()
        {
            try
            {
                AdatokJogVonal.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM jogvonal  order by  sorszám";

                AdatokJogVonal = KézJogVonal.Lista_Adatok(hely, jelszó, szöveg);
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
                Vonalszám.Text = MyF.Szöveg_Tisztítás(Vonalszám.Text);
                Megnevezés.Text = MyF.Szöveg_Tisztítás(Megnevezés.Text);

                if (Vonalszám.Text.Trim() == "") return;
                if (Megnevezés.Text == "") return;

                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                if (!Exists(hely)) return;
                jelszó = "Mocó";

                if (Text1.Text.Trim() != "")
                {
                    // módosítás
                    szöveg = "UPDATE jogvonal  SET ";
                    szöveg += " Szám='" + Vonalszám.Text.Trim() + "', ";
                    szöveg += " megnevezés='" + Megnevezés.Text.Trim() + "' ";
                    szöveg += "WHERE sorszám=" + Text1.Text.Trim();
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                {
                    // új rögtzítés
                    szöveg = "INSERT INTO jogvonal ( Szám, megnevezés  ) VALUES ( '" + Vonalszám.Text.Trim() + "', '" + Megnevezés.Text.Trim() + "' )";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                Tábla1kiirás();
                Text1.Text = "";
                Vonalszám.Text = "";
                Megnevezés.Text = "";
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
            Text1.Text = "";
            Vonalszám.Text = "";
            Megnevezés.Text = "";
        }

        private void Tábla1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.SelectedRows.Count != 0)
                {
                    Text1.Text = Convert.ToString(Tábla1.Rows[Tábla1.SelectedRows[0].Index].Cells[0].Value);
                    Vonalszám.Text = Convert.ToString(Tábla1.Rows[Tábla1.SelectedRows[0].Index].Cells[1].Value);
                    Megnevezés.Text = Convert.ToString(Tábla1.Rows[Tábla1.SelectedRows[0].Index].Cells[2].Value);
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

        private void Tábla1kiirás()
        {
            try
            {
                JogVonalListaFeltöltés();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Szám");
                AdatTábla.Columns.Add("Megnevezés");

                AdatTábla.Clear();
                foreach (Adat_Kiegészítő_Jogvonal rekord in AdatokJogVonal)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Szám"] = rekord.Szám;
                    Soradat["Megnevezés"] = rekord.Megnevezés;

                    AdatTábla.Rows.Add(Soradat);

                }
                Tábla1.DataSource = AdatTábla;

                Tábla1.Columns["Sorszám"].Width = 80;
                Tábla1.Columns["Szám"].Width = 140;
                Tábla1.Columns["Megnevezés"].Width = 800;

                Tábla1.Visible = true;
                Tábla1.Refresh();
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
                Tábla1kiirás();
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

        private void Tábla1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tábla1.Rows[e.RowIndex].Selected = true;
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

        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tábla1.Rows[e.RowIndex].Selected = true;
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


        #region Szervezeti könyvtár
        private void KönyvtárListaFeltöltés()
        {
            try
            {
                AdatokKönyvtár.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM könyvtár  order by  id";

                AdatokKönyvtár = kézKönyvtár.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Command9_Click(object sender, EventArgs e)
        {
            try
            {
                Könyvtár.Text = MyF.Szöveg_Tisztítás(Könyvtár.Text);
                if (Könyvtár.Text.Trim() == "") throw new HibásBevittAdat("A könyvtár mező nem lehet üres.");

                if (Csoport1.Text.Trim() == "" || (!int.TryParse(Csoport1.Text, out int Csoport_1))) throw new HibásBevittAdat("A csoport 1 mező nem lehet üres és számnak kell lennie.");
                if (Csoport2.Text.Trim() == "" || (!int.TryParse(Csoport2.Text, out int Csoport_2))) throw new HibásBevittAdat("A csoport 2 mező nem lehet üres és számnak kell lennie.");
                if (Sorrend1.Text.Trim() == "" || (!int.TryParse(Sorrend1.Text, out int Sorrend_1))) throw new HibásBevittAdat("A Sorrend 1 mező nem lehet üres és számnak kell lennie.");
                if (Sorrend2.Text.Trim() == "" || (!int.TryParse(Sorrend2.Text, out int Sorrend_2))) throw new HibásBevittAdat("A Sorrend 2 mező nem lehet üres és számnak kell lennie.");

                string szöveg;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                if (!Exists(hely)) return;
                string jelszó = "Mocó";

                if (TextBox1.Text == "0" || TextBox1.Text.Trim() == "")
                {
                    // új adat
                    szöveg = "INSERT INTO könyvtár ";
                    szöveg += " ( név, csoport1, csoport2, sorrend1, sorrend2, vezér1, vezér2 ) VALUES ";
                    szöveg += "( '" + Könyvtár.Text.Trim() + "', ";
                    szöveg += Csoport_1 + ", ";
                    szöveg += Csoport_2 + ", ";
                    szöveg += Sorrend_1 + ", ";
                    szöveg += Sorrend_2 + ", ";
                    if (!Vezér1.Checked)
                        szöveg += " False, ";
                    else
                        szöveg += " true, ";
                    if (!Vezér2.Checked)
                        szöveg += " False ";
                    else
                        szöveg += " true ";
                    szöveg += ")";
                }

                else
                {
                    // módosítunk
                    szöveg = "UPDATE könyvtár SET ";
                    szöveg += " név='" + Könyvtár.Text.Trim() + "', ";
                    szöveg += $" csoport1={Csoport_1}, ";
                    szöveg += $" csoport2={Csoport_2}, ";
                    szöveg += $" sorrend1={Sorrend_1}, ";
                    szöveg += $" sorrend2={Sorrend_2}, ";
                    if (!Vezér1.Checked)
                        szöveg += " vezér1=False, ";
                    else
                        szöveg += " vezér1=true, ";
                    if (!Vezér2.Checked)
                        szöveg += " vezér2=False ";
                    else
                        szöveg += " vezér2=true ";
                    szöveg += " WHERE id=" + TextBox1.Text.Trim();

                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Tábla3kiirás();
                Könytár_tisztít();
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
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                if (!Exists(hely))
                    return;
                jelszó = "Mocó";
                if (TextBox1.Text.Trim() == "" || Convert.ToInt32(TextBox1.Text) == 0)
                    return;

                szöveg = "Delete FROM könyvtár where id=" + TextBox1.Text.Trim();
                MyA.ABtörlés(hely, jelszó, szöveg);

                Tábla3kiirás();
                Könytár_tisztít();
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

        private void Command7_Click(object sender, EventArgs e)
        {
            try
            {
                Könytár_tisztít();
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

        private void Könytár_tisztít()
        {
            try
            {
                TextBox1.Text = 0.ToString();
                Csoport1.Text = "0";
                Csoport2.Text = "0";
                Könyvtár.Text = "";
                Vezér1.Checked = false;
                Vezér2.Checked = false;
                Sorrend2.Text = 0.ToString();
                Sorrend1.Text = 0.ToString();
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

        private void Tábla3kiirás()
        {
            try
            {
                KönyvtárListaFeltöltés();

                Tábla3.Rows.Clear();
                Tábla3.Columns.Clear();
                Tábla3.Refresh();
                Tábla3.Visible = false;
                Tábla3.ColumnCount = 8;

                // fejléc elkészítése
                Tábla3.Columns[0].HeaderText = "Id";
                Tábla3.Columns[0].Width = 80;
                Tábla3.Columns[1].HeaderText = "Könyvtár";
                Tábla3.Columns[1].Width = 200;
                Tábla3.Columns[2].HeaderText = "csoport1";
                Tábla3.Columns[2].Width = 120;
                Tábla3.Columns[3].HeaderText = "Vezér1";
                Tábla3.Columns[3].Width = 120;
                Tábla3.Columns[4].HeaderText = "Sorrend1";
                Tábla3.Columns[4].Width = 120;
                Tábla3.Columns[5].HeaderText = "csoport2";
                Tábla3.Columns[5].Width = 120;
                Tábla3.Columns[6].HeaderText = "Vezér2";
                Tábla3.Columns[6].Width = 120;
                Tábla3.Columns[7].HeaderText = "Sorrend2";
                Tábla3.Columns[7].Width = 120;



                int i;
                foreach (Adat_Kiegészítő_Könyvtár rekord in AdatokKönyvtár)
                {

                    Tábla3.RowCount++;
                    i = Tábla3.RowCount - 1;
                    Tábla3.Rows[i].Cells[0].Value = rekord.ID;
                    Tábla3.Rows[i].Cells[1].Value = rekord.Név;
                    Tábla3.Rows[i].Cells[2].Value = rekord.Csoport1;
                    if (rekord.Vezér1)
                        Tábla3.Rows[i].Cells[3].Value = "IGAZ";
                    else
                        Tábla3.Rows[i].Cells[3].Value = "HAMIS";

                    Tábla3.Rows[i].Cells[4].Value = rekord.Sorrend1;
                    Tábla3.Rows[i].Cells[5].Value = rekord.Csoport2;
                    if (rekord.Vezér2)
                        Tábla3.Rows[i].Cells[6].Value = "IGAZ";
                    else
                        Tábla3.Rows[i].Cells[6].Value = "HAMIS";

                    Tábla3.Rows[i].Cells[7].Value = rekord.Sorrend2;
                }

                Tábla3.Visible = true;
                Tábla3.Refresh();

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

        private void Tábla3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Táblázat sorának kijelölése
                if (Tábla3.SelectedRows.Count != 0)
                {
                    TextBox1.Text = Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[0].Value.ToString();
                    Könyvtár.Text = Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[1].Value.ToString();
                    Csoport1.Text = Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[2].Value.ToString();
                    if (Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[3].Value.ToString() == "IGAZ")
                        Vezér1.Checked = true;
                    else
                        Vezér1.Checked = false;
                    Sorrend1.Text = Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[4].Value.ToString();
                    Csoport2.Text = Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[5].Value.ToString();
                    if (Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[6].Value.ToString() == "IGAZ")
                        Vezér2.Checked = true;
                    else
                        Vezér2.Checked = false;
                    Sorrend2.Text = Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[7].Value.ToString();
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

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla3kiirás();
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

        private void Tábla3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tábla3.Rows[e.RowIndex].Selected = true;
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

        private void Tábla3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tábla3.Rows[e.RowIndex].Selected = true;
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


        #region Feltölthető fájlok
        private void MunkaListaFeltöltés()
        {
            try
            {
                AdatokMunkakör.Clear();
                string szöveg = "SELECT * FROM Munkakör  order by  kategória, Megnevezés";
                AdatokMunkakör = KézMunkakör.Lista_Adatok(szöveg);
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

        private void Munka_Kategória_Feltöltés()
        {
            try
            {
                Munka_Kategória.Items.Clear();
                List<string> Adatok = AdatokMunkakör.Select(a => a.Kategória).Distinct().ToList();
                foreach (string Elem in Adatok)
                    Munka_Kategória.Items.Add(Elem);

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

        private void Munka_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Munka_Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("A megnevezés beviteli mező nem lehet üres.");
                Munka_Megnevezés.Text = MyF.Szöveg_Tisztítás(Munka_Megnevezés.Text);
                if (Munka_Kategória.Text.Trim() == "") throw new HibásBevittAdat("A kategória beviteli mező nem lehet üres.");

                if (!long.TryParse(Munka_Id.Text, out long Sorszám)) Sorszám = AdatokMunkakör.Max(a => a.Id) + 1;

                Adat_Kiegészítő_Munkakör ADAT = new Adat_Kiegészítő_Munkakör(Sorszám,
                                                                             Munka_Megnevezés.Text.Trim(),
                                                                             Munka_Kategória.Text.Trim(),
                                                                             Munka_Státus.Checked
                                                                            );
                if (AdatokMunkakör.Any(a => a.Id == Sorszám))
                    KézMunkakör.Módosítás(ADAT);
                else
                    KézMunkakör.Rögzítés(ADAT);

                Munka_Tábla_kiirás();
                Munka_Kategória_Feltöltés();
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

        private void Munka_Új_Click(object sender, EventArgs e)
        {
            Munka_Megnevezés.Text = "";
            Munka_Id.Text = "";
            Munka_Kategória.Text = "";
            Munka_Státus.Checked = false;
        }

        private void Munka_Tábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Munka_Tábla.SelectedRows.Count != 0)
                {
                    if (!long.TryParse(Munka_Tábla.Rows[Munka_Tábla.SelectedRows[0].Index].Cells[0].Value.ToString(), out long sorszám)) return;
                    Adat_Kiegészítő_Munkakör Elem = (from a in AdatokMunkakör
                                                     where a.Id == sorszám
                                                     select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        Munka_Id.Text = Elem.Id.ToString();
                        Munka_Megnevezés.Text = Elem.Megnevezés;
                        Munka_Kategória.Text = Elem.Kategória;
                        Munka_Státus.Checked = Elem.Státus;
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

        private void Munka_Tábla_kiirás()
        {
            try
            {
                MunkaListaFeltöltés();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Kategória");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Státus");

                AdatTábla.Clear();
                foreach (Adat_Kiegészítő_Munkakör rekord in AdatokMunkakör)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Id;
                    Soradat["Kategória"] = rekord.Kategória;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Státus"] = rekord.Státus != true ? "Érvényes" : "Törölt";
                    AdatTábla.Rows.Add(Soradat);

                }
                Munka_Tábla.DataSource = AdatTábla;

                Munka_Tábla.Columns["Sorszám"].Width = 100;
                Munka_Tábla.Columns["Kategória"].Width = 350;
                Munka_Tábla.Columns["Megnevezés"].Width = 350;
                Munka_Tábla.Columns["Státus"].Width = 100;

                Munka_Tábla.Visible = true;
                Munka_Tábla.Refresh();
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

        private void Munka_Frissít_Click(object sender, EventArgs e)
        {
            Munka_Tábla_kiirás();
        }

        private void Munka_Tábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Munka_Tábla.Rows[e.RowIndex].Selected = true;
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

        private void Munka_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Munka_Tábla.Rows[e.RowIndex].Selected = true;
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


        #region Jelenléti ív
        private void JelenlétiListaFeltöltés()
        {
            try
            {
                AdatokJelenléti.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM jelenlétiív ORDER BY id";
                AdatokJelenléti = KézJelenléti.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Jelenléti_kitöltés()
        {
            try
            {
                JelenlétiListaFeltöltés();

                JelenlétiText1.Text = "";
                JelenlétiText2.Text = "";
                JelenlétiText3.Text = "";
                JelenlétiText4.Text = "";
                JelenlétiText5.Text = "";
                string eredmény;
                for (int i = 1; i <= 5; i++)
                {
                    Adat_Kiegészítő_Jelenlétiív Elem = (from a in AdatokJelenléti
                                                        where a.Id == i
                                                        select a).FirstOrDefault();
                    if (Elem != null) eredmény = Elem.Szervezet; else eredmény = "";

                    switch (i)
                    {
                        case 1:
                            {
                                JelenlétiText1.Text = eredmény.Trim();
                                break;
                            }
                        case 2:
                            {
                                JelenlétiText2.Text = eredmény.Trim();
                                break;
                            }
                        case 3:
                            {
                                JelenlétiText3.Text = eredmény.Trim();
                                break;
                            }
                        case 4:
                            {
                                JelenlétiText4.Text = eredmény.Trim();
                                break;
                            }
                        case 5:
                            {
                                JelenlétiText5.Text = eredmény.Trim();
                                break;
                            }
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

        private void JelenlétiSzerv_Click(object sender, EventArgs e)
        {
            try
            {
                if (JelenlétiText1.Text.Trim() == "") throw new HibásBevittAdat("A mező nem lehet üres!");
                string szöveg;
                JelenlétiText1.Text = MyF.Szöveg_Tisztítás(JelenlétiText1.Text);

                JelenlétiListaFeltöltés();
                Adat_Kiegészítő_Jelenlétiív Elem = (from a in AdatokJelenléti
                                                    where a.Id == 1
                                                    select a).FirstOrDefault();

                if (Elem != null)
                    szöveg = $"UPDATE jelenlétiív SET szervezet='{JelenlétiText1.Text.Trim()}' where id=1";
                else
                    szöveg = $"INSERT INTO jelenlétiív (id, szervezet) Values (1,'{JelenlétiText1.Text.Trim()} ')";


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Jelenléti_kitöltés();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void JelenlétiIgaz_Click(object sender, EventArgs e)
        {
            try
            {
                if (JelenlétiText2.Text.Trim() == "") throw new HibásBevittAdat("A mező nem lehet üres!");
                string szöveg;
                JelenlétiText2.Text = MyF.Szöveg_Tisztítás(JelenlétiText2.Text);

                JelenlétiListaFeltöltés();
                Adat_Kiegészítő_Jelenlétiív Elem = (from a in AdatokJelenléti
                                                    where a.Id == 2
                                                    select a).FirstOrDefault();

                if (Elem != null)
                    szöveg = $"UPDATE jelenlétiív SET szervezet='{JelenlétiText2.Text.Trim()}' where id=2";
                else
                    szöveg = $"INSERT INTO jelenlétiív (id, szervezet) Values (2,'{JelenlétiText2.Text.Trim()} ')";


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Jelenléti_kitöltés();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void JelenlétiFőmér_Click(object sender, EventArgs e)
        {
            try
            {
                if (JelenlétiText3.Text.Trim() == "") throw new HibásBevittAdat("A mező nem lehet üres!");
                string szöveg;
                JelenlétiText3.Text = MyF.Szöveg_Tisztítás(JelenlétiText3.Text);

                JelenlétiListaFeltöltés();
                Adat_Kiegészítő_Jelenlétiív Elem = (from a in AdatokJelenléti
                                                    where a.Id == 3
                                                    select a).FirstOrDefault();

                if (Elem != null)
                    szöveg = $"UPDATE jelenlétiív SET szervezet='{JelenlétiText3.Text.Trim()}' where id=3";
                else
                    szöveg = $"INSERT INTO jelenlétiív (id, szervezet) Values (3,'{JelenlétiText3.Text.Trim()} ')";


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Jelenléti_kitöltés();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void JelenlétiÜzem_Click(object sender, EventArgs e)
        {
            try
            {
                if (JelenlétiText4.Text.Trim() == "") throw new HibásBevittAdat("A mező nem lehet üres!");
                string szöveg;
                JelenlétiText4.Text = MyF.Szöveg_Tisztítás(JelenlétiText4.Text);

                JelenlétiListaFeltöltés();
                Adat_Kiegészítő_Jelenlétiív Elem = (from a in AdatokJelenléti
                                                    where a.Id == 4
                                                    select a).FirstOrDefault();

                if (Elem != null)
                    szöveg = $"UPDATE jelenlétiív SET szervezet='{JelenlétiText4.Text.Trim()}' where id=4";
                else
                    szöveg = $"INSERT INTO jelenlétiív (id, szervezet) Values (4,'{JelenlétiText4.Text.Trim()} ')";


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Jelenléti_kitöltés();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Eszközhöz_Click(object sender, EventArgs e)
        {
            try
            {
                if (JelenlétiText5.Text.Trim() == "") throw new HibásBevittAdat("A mező nem lehet üres!");
                string szöveg;
                JelenlétiText5.Text = MyF.Szöveg_Tisztítás(JelenlétiText5.Text);

                JelenlétiListaFeltöltés();
                Adat_Kiegészítő_Jelenlétiív Elem = (from a in AdatokJelenléti
                                                    where a.Id == 5
                                                    select a).FirstOrDefault();

                if (Elem != null)
                    szöveg = $"UPDATE jelenlétiív SET szervezet='{JelenlétiText5.Text.Trim()}' where id=5";
                else
                    szöveg = $"INSERT INTO jelenlétiív (id, szervezet) Values (5,'{JelenlétiText5.Text.Trim()} ')";


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Jelenléti_kitöltés();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void JelenlétiAláíróListaFeltöltés()
        {
            try
            {
                AdatokAlárás.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM Főkönyvtábla ";
                AdatokAlárás = KézAláíró.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Jelenlét_aláírók()
        {
            try
            {
                // főkönyvi aláírások
                JelenlétiAláíróListaFeltöltés();

                Adat_Kiegészítő_főkönyvtábla Elem = (from a in AdatokAlárás
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Elem != null)
                {
                    txtnév2.Text = Elem.Név;
                    txtbeosztás2.Text = Elem.Beosztás;
                }

                Elem = (from a in AdatokAlárás
                        where a.Id == 3
                        select a).FirstOrDefault();

                if (Elem != null)
                {
                    txtnév3.Text = Elem.Név;
                    txtbeosztás3.Text = Elem.Beosztás;
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

        private void Btnfőkönyv_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = $"UPDATE Főkönyvtábla SET név='{txtnév2.Text.Trim()}'";
                szöveg += $", beosztás='{txtbeosztás2.Text.Trim()}'";
                szöveg += " WHERE id=2 ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE Főkönyvtábla SET név='{txtnév3.Text.Trim()}'";
                szöveg += $", beosztás= '{txtbeosztás3.Text.Trim()}'";
                szöveg += $" WHERE id=3 ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Jelenlét_aláírók();

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Beosztáskódok
        private void BEOListaFeltöltés()
        {
            try
            {
                AdatokBeoKód.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM beosztáskódok Order By  sorszám";
                AdatokBeoKód = kézBeoKód.Lista_Adatok(hely, jelszó, szöveg);
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

        private void BeoOk_Click(object sender, EventArgs e)
        {
            try
            {
                // Minden adat kötelező 
                if (BeoSorszám.Text.Trim() == "") throw new HibásBevittAdat("A sorszám mező nem lehet üres!");
                if (!int.TryParse(BeoSorszám.Text, out int SorszámBEO)) throw new HibásBevittAdat("A sorszám mező nem lehet szöveg!");
                if (BeoKód.Text.Trim() == "") throw new HibásBevittAdat("A Beosztáskód mező nem lehet üres!!");
                if (BeoMunkaidő.Text.Trim() == "") throw new HibásBevittAdat("A Munkaidő mező nem lehet üres!!");
                if (!int.TryParse(BeoMunkaidő.Text, out int result1)) throw new HibásBevittAdat("A Munkaidő mező nem lehet szöveg!!");
                if (BEOMunkarend.Text.Trim() == "") throw new HibásBevittAdat("A Munkarend mező nem lehet üres!!");
                if (!int.TryParse(BEOMunkarend.Text, out int result2)) throw new HibásBevittAdat("A Munkarend mező nem lehet szöveg!!");
                if (BEOMagyarázat.Text.Trim() == "") throw new HibásBevittAdat("A Magyarázat mező nem lehet üres!!");

                BEOListaFeltöltés();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";

                Adat_Kiegészítő_Beosztáskódok Elem = (from a in AdatokBeoKód
                                                      where a.Sorszám == SorszámBEO
                                                      select a).FirstOrDefault();
                string szöveg;
                if (Elem != null)
                {
                    // Módosítás     
                    szöveg = "UPDATE beosztáskódok SET ";
                    szöveg += $" beosztáskód='{BeoKód.Text.Trim()}', ";
                    szöveg += $" munkaidőkezdet='{BeoIdőKezdete.Value:HH:mm:ss}', ";
                    szöveg += $" munkaidővége='{BeoIdővége.Value:HH:mm:ss}', ";
                    szöveg += $" munkaidő={BeoMunkaidő.Text.Trim()}, ";
                    szöveg += $" munkarend={BEOMunkarend.Text.Trim()}, ";
                    if (BeoÉjszakás.Checked == true)
                        szöveg += $" éjszakás= 1, ";
                    else
                        szöveg += $" éjszakás=0, ";
                    if (BeoSzámoló.Checked == true)
                        szöveg += $" számoló= 1, ";
                    else
                        szöveg += $" számoló= 0, ";
                    szöveg += $" Magyarázat='{BEOMagyarázat.Text.Trim()}' ";
                    szöveg += $" WHERE  sorszám={BeoSorszám.Text} ";
                }
                else
                {
                    // Új rögzítés
                    szöveg = "INSERT INTO beosztáskódok (sorszám, beosztáskód, munkaidőkezdet, munkaidővége,  munkaidő, munkarend, napszak, éjszakás, számoló, 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23, Magyarázat)";
                    szöveg += " VALUES (";
                    szöveg += $" {BeoSorszám.Text}, '{BeoKód.Text.Trim()}', ";
                    szöveg += $" '{BeoIdőKezdete.Value:HH:mm:ss}', ";
                    szöveg += $" '{BeoIdővége.Value:HH:mm:ss}', ";
                    szöveg += $" {BeoMunkaidő.Text.Trim()}, ";
                    szöveg += $" {BEOMunkarend.Text.Trim()}, '_', ";
                    if (BeoÉjszakás.Checked)
                        szöveg += " 1, ";
                    else
                        szöveg += " 0, ";
                    if (BeoSzámoló.Checked)
                        szöveg += " 1, ";
                    else
                        szöveg += " 0, ";
                    szöveg += " 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0, ";
                    szöveg += $" '{BEOMagyarázat.Text.Trim()}') ";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                BeosztásTáblaíró();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BeoTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (BeoKód.Text.Trim() == "") return;
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\Kiegészítő.mdb";
                if (!Exists(hely)) return;
                jelszó = "Mocó";

                szöveg = $"DELETE FROM beosztáskódok where beosztáskód='{BeoKód.Text.Trim()}'";
                MyA.ABtörlés(hely, jelszó, szöveg);

                BeosztásTáblaíró();
                MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BeoÚj_Click(object sender, EventArgs e)
        {
            BeoSorszám.Text = "";
            BeoKód.Text = "";
            BeoIdőKezdete.Value = new DateTime(1900, 1, 1, 6, 0, 0);
            BeoIdővége.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            BeoMunkaidő.Text = "";
            BEOMunkarend.Text = "";
            BeoÉjszakás.Checked = false;
            BeoSzámoló.Checked = false;
            BEOMagyarázat.Text = "";
        }

        private void BeoFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                BeosztásTáblaíró();
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

        private void BeosztásTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                BeosztásTábla.Rows[e.RowIndex].Selected = true;
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

        private void BeoExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (BeosztásTábla.Rows.Count <= 0) return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Beosztáskódok_listája_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, BeosztásTábla, true);

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

        private void BeosztásTábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                BeosztásTábla.Rows[e.RowIndex].Selected = true;
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

        private void BeosztásTábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (BeosztásTábla.SelectedRows.Count != 0)
                {
                    BeoSorszám.Text = BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[0].Value.ToStrTrim();
                    BeoKód.Text = BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[1].Value.ToStrTrim();
                    BeoMunkaidő.Text = BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[4].Value.ToStrTrim();
                    BEOMunkarend.Text = BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[5].Value.ToStrTrim();
                    if (BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[6].Value.ToStrTrim() == "Igen")
                        BeoÉjszakás.Checked = true;
                    else
                        BeoÉjszakás.Checked = false;
                    BEOMagyarázat.Text = BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[7].Value.ToStrTrim();
                    if (BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[8].Value.ToStrTrim() == "Igen")
                        BeoSzámoló.Checked = true;
                    else
                        BeoSzámoló.Checked = false;

                    DateTime ideigidő = ($"1900.01.01 {BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[2].Value.ToStrTrim()}").ToÉrt_DaTeTime();
                    BeoIdőKezdete.Value = ideigidő;
                    ideigidő = ($"1900.01.01 {BeosztásTábla.Rows[BeosztásTábla.SelectedRows[0].Index].Cells[3].Value.ToStrTrim()}").ToÉrt_DaTeTime();
                    BeoIdővége.Value = ideigidő;
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

        private void BeosztásTáblaíró()
        {
            try
            {
                BEOListaFeltöltés();

                DataTable AdatTábla = new DataTable();
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Beosztáskód");
                AdatTábla.Columns.Add("Munkaidő kezdete");
                AdatTábla.Columns.Add("Munkaidő vége");
                AdatTábla.Columns.Add("Munkaidő");
                AdatTábla.Columns.Add("Munkarend");
                AdatTábla.Columns.Add("Éjszakás");
                AdatTábla.Columns.Add("Magyarázat");
                AdatTábla.Columns.Add("Számoló");

                AdatTábla.Clear();

                foreach (Adat_Kiegészítő_Beosztáskódok rekord in AdatokBeoKód)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Beosztáskód"] = rekord.Beosztáskód;
                    Soradat["Munkaidő kezdete"] = rekord.Munkaidőkezdet.ToString("HH:mm:ss");
                    Soradat["Munkaidő vége"] = rekord.Munkaidővége.ToString("HH:mm:ss");
                    Soradat["Munkaidő"] = rekord.Munkaidő;
                    Soradat["Munkarend"] = rekord.Munkarend;
                    Soradat["Éjszakás"] = rekord.Éjszakás ? "Igen" : "Nem";
                    Soradat["Magyarázat"] = rekord.Magyarázat;
                    Soradat["Számoló"] = rekord.Számoló ? "Igen" : "Nem";


                    AdatTábla.Rows.Add(Soradat);

                }
                BeosztásTábla.DataSource = AdatTábla;

                BeosztásTábla.Columns["Sorszám"].Width = 80;
                BeosztásTábla.Columns["Beosztáskód"].Width = 120;
                BeosztásTábla.Columns["Munkaidő kezdete"].Width = 100;
                BeosztásTábla.Columns["Munkaidő vége"].Width = 100; ;
                BeosztásTábla.Columns["Munkaidő"].Width = 80;
                BeosztásTábla.Columns["Munkarend"].Width = 100;
                BeosztásTábla.Columns["Éjszakás"].Width = 100;
                BeosztásTábla.Columns["Magyarázat"].Width = 270;
                BeosztásTábla.Columns["Számoló"].Width = 80;

                BeosztásTábla.Visible = true;
                BeosztásTábla.Refresh();

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


        #region Védőlap
        private void VédelemListaFeltöltés()
        {
            try
            {
                AdatokKiegVéd.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM védelem  order by  sorszám";
                AdatokKiegVéd = KézKiegVéd.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Védő_tábla_kiir()
        {
            try
            {
                VédelemListaFeltöltés();

                Védő_tábla.Rows.Clear();
                Védő_tábla.Columns.Clear();
                Védő_tábla.Refresh();
                Védő_tábla.Visible = false;
                Védő_tábla.ColumnCount = 2;

                // fejléc elkészítése
                Védő_tábla.Columns[0].HeaderText = "Sorszám";
                Védő_tábla.Columns[0].Width = 80;
                Védő_tábla.Columns[1].HeaderText = "Megnevezés";
                Védő_tábla.Columns[1].Width = 800;

                foreach (Adat_Kiegészítő_Védelem rekord in AdatokKiegVéd)
                {
                    Védő_tábla.RowCount++;
                    int i = Védő_tábla.RowCount - 1;
                    Védő_tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Védő_tábla.Rows[i].Cells[1].Value = rekord.Megnevezés;
                }

                Védő_tábla.Visible = true;
                Védő_tábla.Refresh();

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

        private void Védő_frissít_Click(object sender, EventArgs e)
        {
            try
            {
                Védő_tábla_kiir();
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

        private void Védő_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Védő_id.Text = Védő_tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                Védő_Megnevezés.Text = Védő_tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
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

        private void Védő_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                Védő_Megnevezés.Text = MyF.Szöveg_Tisztítás(Védő_Megnevezés.Text);
                if (!long.TryParse(Védő_id.Text.Trim(), out long Sorszám)) return;
                VédelemListaFeltöltés();

                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
                jelszó = "Mocó";

                Adat_Kiegészítő_Védelem Elem = (from a in AdatokKiegVéd
                                                where a.Sorszám == Sorszám
                                                select a).FirstOrDefault();

                if (Elem != null)
                    szöveg = $"UPDATE védelem  SET megnevezés='{Védő_Megnevezés.Text.Trim()}' WHERE sorszám={Védő_id.Text.Trim()}";     // módosítás
                else
                    szöveg = $"INSERT INTO védelem ( sorszám, megnevezés ) VALUES ({Védő_id.Text.Trim()}, '{Védő_Megnevezés.Text.Trim()}' )";     // új rögtzítés


                MyA.ABMódosítás(hely, jelszó, szöveg);
                Védő_tábla_kiir();
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

        private void Védő_új_Click(object sender, EventArgs e)
        {
            Védő_id.Text = "";
            Védő_Megnevezés.Text = "";
        }
        #endregion


        #region Gondnok
        private void GondnokListaFeltöltés()
        {
            try
            {
                AdatokBehEng.Clear();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
                string jelszó = "egérpad";
                string szöveg = "SELECT * FROM  Engedélyezés Order BY ID ";
                if (!Exists(hely)) return;
                AdatokBehEng = KézBehEng.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Gondnok_frissít_Click(object sender, EventArgs e)
        {
            try
            {
                Gondnok_tábla_listázás();
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

        private void Gondnok_tábla_listázás()
        {
            try
            {
                GondnokListaFeltöltés();

                Gondnok_tábla.Rows.Clear();
                Gondnok_tábla.Columns.Clear();
                Gondnok_tábla.Refresh();
                Gondnok_tábla.Visible = false;
                Gondnok_tábla.ColumnCount = 9;

                // fejléc elkészítése
                Gondnok_tábla.Columns[0].HeaderText = "Sorszám";
                Gondnok_tábla.Columns[0].Width = 80;
                Gondnok_tábla.Columns[1].HeaderText = "Telephely";
                Gondnok_tábla.Columns[1].Width = 200;
                Gondnok_tábla.Columns[2].HeaderText = "E-mail cím";
                Gondnok_tábla.Columns[2].Width = 200;
                Gondnok_tábla.Columns[3].HeaderText = "Gondnok";
                Gondnok_tábla.Columns[3].Width = 100;
                Gondnok_tábla.Columns[4].HeaderText = "Szakszolgálat";
                Gondnok_tábla.Columns[4].Width = 120;
                Gondnok_tábla.Columns[5].HeaderText = "Telefonszám";
                Gondnok_tábla.Columns[5].Width = 200;
                Gondnok_tábla.Columns[6].HeaderText = "Szakszolgálat";
                Gondnok_tábla.Columns[6].Width = 120;
                Gondnok_tábla.Columns[7].HeaderText = "Beosztás";
                Gondnok_tábla.Columns[7].Width = 200;
                Gondnok_tábla.Columns[8].HeaderText = "Név";
                Gondnok_tábla.Columns[8].Width = 200;

                foreach (Adat_Behajtás_Engedélyezés rekord in AdatokBehEng)
                {

                    Gondnok_tábla.RowCount++;
                    int i = Gondnok_tábla.RowCount - 1;
                    Gondnok_tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Gondnok_tábla.Rows[i].Cells[1].Value = rekord.Telephely;
                    Gondnok_tábla.Rows[i].Cells[2].Value = rekord.Emailcím;
                    if (rekord.Gondnok)
                    {
                        Gondnok_tábla.Rows[i].Cells[3].Value = "Igen";
                    }
                    else
                    {
                        Gondnok_tábla.Rows[i].Cells[3].Value = "Nem";
                    }
                    if (rekord.Szakszolgálat)
                    {
                        Gondnok_tábla.Rows[i].Cells[4].Value = "Igen";
                    }
                    else
                    {
                        Gondnok_tábla.Rows[i].Cells[4].Value = "Nem";
                    }
                    Gondnok_tábla.Rows[i].Cells[5].Value = rekord.Telefonszám;
                    Gondnok_tábla.Rows[i].Cells[6].Value = rekord.Szakszolgálatszöveg;
                    Gondnok_tábla.Rows[i].Cells[7].Value = rekord.Beosztás;
                    Gondnok_tábla.Rows[i].Cells[8].Value = rekord.Név;
                }
                Gondnok_tábla.Visible = true;
                Gondnok_tábla.Refresh();

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

        private void Gondnok_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Gondnok_tábla.Rows.Count < 1) return;
                if (e.RowIndex < 0) return;


                Gond_sorszám.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                Gond_telephely.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                Gond_email.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                if (Gondnok_tábla.Rows[e.RowIndex].Cells[3].Value.ToStrTrim() == "Igen")
                    Gond_Gondnok.Checked = true;
                else
                    Gond_Gondnok.Checked = false;

                if (Gondnok_tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim() == "Igen")
                    Gond_Szak.Checked = true;
                else
                    Gond_Szak.Checked = false;

                Gond_telefon.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[5].Value.ToStrTrim();
                Gond_szakszolg_szöv.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[6].Value.ToStrTrim();
                Gond_beosztás.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[7].Value.ToStrTrim();
                Gond_Név.Text = Gondnok_tábla.Rows[e.RowIndex].Cells[8].Value.ToStrTrim();
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

        private void Gond_új_Click(object sender, EventArgs e)
        {
            try
            {
                Gond_ürít();
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

        private void Gond_ürít()
        {
            Gond_sorszám.Text = "";
            Gond_telephely.Text = "";
            Gond_email.Text = "";
            Gond_Gondnok.Checked = false;
            Gond_Szak.Checked = false;
            Gond_telefon.Text = "";
            Gond_szakszolg_szöv.Text = "";
            Gond_beosztás.Text = "";
            Gond_Név.Text = "";
        }

        private void Gond_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                Gond_telephely.Text = MyF.Szöveg_Tisztítás(Gond_telephely.Text);
                Gond_email.Text = MyF.Szöveg_Tisztítás(Gond_email.Text);
                Gond_telefon.Text = MyF.Szöveg_Tisztítás(Gond_telefon.Text);
                Gond_szakszolg_szöv.Text = MyF.Szöveg_Tisztítás(Gond_szakszolg_szöv.Text);
                Gond_beosztás.Text = MyF.Szöveg_Tisztítás(Gond_beosztás.Text);
                Gond_Név.Text = MyF.Szöveg_Tisztítás(Gond_Név.Text);


                if (Gond_telephely.Text.Trim() == "") Gond_telephely.Text = "_";
                if (Gond_email.Text.Trim() == "") Gond_email.Text = "_";
                if (Gond_telefon.Text.Trim() == "") Gond_telefon.Text = "_";
                if (Gond_szakszolg_szöv.Text.Trim() == "") Gond_szakszolg_szöv.Text = "_";
                if (Gond_beosztás.Text.Trim() == "") Gond_beosztás.Text = "_";
                if (Gond_Név.Text.Trim() == "") Gond_Név.Text = "_";

                GondnokListaFeltöltés();

                string szöveg;

                if (Gond_sorszám.Text.Trim() == "")
                {
                    // új 
                    long id = 1;
                    if (AdatokBehEng.Count > 0) id = AdatokBehEng.Max(a => a.Id) + 1;
                    Gond_sorszám.Text = id.ToString();

                    szöveg = "INSERT INTO engedélyezés (id, telephely, emailcím, gondnok, szakszolgálat, telefonszám, szakszolgálatszöveg, beosztás, név) VALUES (";
                    szöveg += $"{Gond_sorszám.Text}, "; // id 
                    szöveg += $"'{Gond_telephely.Text.Trim()}', "; // telephely
                    szöveg += $"'{Gond_email.Text.Trim()}', "; // emailcím
                    if (Gond_Gondnok.Checked) // gondnok
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    if (Gond_Szak.Checked) // szakszolgálat
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    szöveg += $"'{Gond_telefon.Text.Trim()}', "; // telefonszám
                    szöveg += $"'{Gond_szakszolg_szöv.Text.Trim()}', "; // szakszolgálatszöveg
                    szöveg += $"'{Gond_beosztás.Text.Trim()}', "; // beosztás
                    szöveg += $"'{Gond_Név.Text.Trim()}') "; // név
                }
                else
                {
                    // módosítás
                    szöveg = "UPDATE engedélyezés SET ";
                    szöveg += $" telephely='{Gond_telephely.Text.Trim()}', "; // telephely
                    szöveg += $" emailcím='{Gond_email.Text.Trim()}', "; // emailcím
                    if (Gond_Gondnok.Checked) // gondnok
                        szöveg += " gondnok=true, ";
                    else
                        szöveg += " gondnok=false, ";

                    if (Gond_Szak.Checked) // szakszolgálat
                        szöveg += " szakszolgálat=true, ";
                    else
                        szöveg += " szakszolgálat=false, ";

                    szöveg += $" telefonszám='{Gond_telefon.Text.Trim()}', "; // telefonszám
                    szöveg += $" szakszolgálatszöveg='{Gond_szakszolg_szöv.Text.Trim()}', "; // szakszolgálatszöveg
                    szöveg += $" beosztás='{Gond_beosztás.Text.Trim()}', "; // beosztás
                    szöveg += $" név='{Gond_Név.Text.Trim()}'"; // név
                    szöveg += $" WHERE id={Gond_sorszám.Text.Trim()}";
                }
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
                string jelszó = "egérpad";

                MyA.ABMódosítás(hely, jelszó, szöveg);
                MessageBox.Show("Az adat rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Gondnok_tábla_listázás();
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

        private void Gond_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                string hely, jelszó, szöveg;
                if (Gond_sorszám.Text.Trim() == "") return;

                if (MessageBox.Show("Biztos, hogy a kijelölt elemet töröljük?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    // Nemet választottuk
                    return;
                }
                else
                {
                    // igent választottuk
                    hely = Application.StartupPath + @"\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
                    jelszó = "egérpad";

                    szöveg = "DELETE FROM engedélyezés WHERE id=" + Gond_sorszám.Text.Trim();
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    MessageBox.Show("Az adat törlésre került.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Gondnok_tábla_listázás();
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
        private void Gondnok_Fel_Click(object sender, EventArgs e)
        {
            try
            {

                if (!int.TryParse(Gond_sorszám.Text, out int Sorszám)) throw new HibásBevittAdat("Nincs kijelölve sor");
                int Elsősorszám = AdatokBehEng.Min(a => a.Id);

                int Maxsorszám = AdatokBehEng.Max(a => a.Id);
                if (Elsősorszám >= Sorszám) throw new HibásBevittAdat("Az első elemet nem lehet előrébb helyezni.");
                GondnokListaFeltöltés();

                Adat_Behajtás_Engedélyezés Elem = (from a in AdatokBehEng
                                                   where a.Id == Sorszám
                                                   select a).FirstOrDefault();
                Adat_Behajtás_Engedélyezés Előző = (from a in AdatokBehEng
                                                    where a.Id < Sorszám
                                                    orderby a.Id descending
                                                    select a).FirstOrDefault();
                if (Elem == null || Előző == null) throw new HibásBevittAdat("Az első elemet nem lehet előrébb helyezni.");

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
                string jelszó = "egérpad";
                List<string> SzövegGy = new List<string>();
                string szöveg = $"UPDATE engedélyezés SET id=0 WHERE id={Elem.Id}";
                SzövegGy.Add(szöveg);
                szöveg = $"UPDATE engedélyezés SET id={Elem.Id} WHERE id={Előző.Id}";
                SzövegGy.Add(szöveg);
                szöveg = $"UPDATE engedélyezés SET id={Előző.Id} WHERE id=0";
                SzövegGy.Add(szöveg);
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                Gondnok_tábla_listázás();
                Gond_ürít();
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