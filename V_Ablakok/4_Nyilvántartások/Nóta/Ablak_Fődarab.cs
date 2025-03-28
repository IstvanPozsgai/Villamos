using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using static Villamos.V_MindenEgyéb.Enumok;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    public partial class Ablak_Fődarab : Form
    {
        readonly Kezelő_Nóta KézNóta = new Kezelő_Nóta();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        int id = 0;
        string szűrő = "";
        string sorba = "";

        #region Alap
        public Ablak_Fődarab()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Jogosultságkiosztás();
        }

        private void Ablak_Fődarab_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Fődarab_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Nóta_Részletes?.Close();
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Nóta.html";
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

        private void Jogosultságkiosztás()
        {

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            int melyikelem = 210;

            // módosítás 1 
            BtnSAP.Enabled = MyF.Vanjoga(melyikelem, 1);
            BtnSAP.Visible = MyF.Vanjoga(melyikelem, 1);
        }
        #endregion


        #region Táblázat
        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            TáblázatÍrás();
        }

        private void TáblázatÍrás()
        {
            if (!ChkSzűrés.Checked)
                szűrő = "";
            else
                szűrő = Táblalista.FilterString;

            if (!ChkRendezés.Checked)
                sorba = "";
            else
                sorba = Táblalista.SortString;

            if (!ChkRendezés.Checked && !ChkSzűrés.Checked)
                Táblalista.CleanFilterAndSort();
            else
                Táblalista.LoadFilterAndSort(szűrő, sorba);

            KötésiOsztály.DataSource = AdatTáblaFeltöltés();
            Táblalista.DataSource = KötésiOsztály;
            OszlopSzélesség();

            for (int i = 0; i < Táblalista.Columns.Count; i++)
            {
                Táblalista.SetFilterEnabled(Táblalista.Columns[i], true);
                Táblalista.SetSortEnabled(Táblalista.Columns[i], true);
                Táblalista.SetFilterCustomEnabled(Táblalista.Columns[i], true);

            }

            Táblalista.Refresh();
            Táblalista.Visible = true;
            Táblalista.ClearSelection();

        }

        private void OszlopSzélesség()
        {
            Táblalista.Columns["Id"].Width = 50;
            Táblalista.Columns["Berendezés"].Width = 100;
            Táblalista.Columns["Készlet Sarzs"].Width = 80;
            Táblalista.Columns["Raktár"].Width = 80;
            Táblalista.Columns["Telephely"].Width = 120;
            Táblalista.Columns["Gyártási Szám"].Width = 80;
            Táblalista.Columns["Forgóváz"].Width = 80;
            Táblalista.Columns["Beépíthető"].Width = 100;
            Táblalista.Columns["Műszaki Megjegyzés"].Width = 250;
            Táblalista.Columns["Osztási Megjegyzés"].Width = 250;
            Táblalista.Columns["Dátum"].Width = 120;
            Táblalista.Columns["Státus"].Width = 150;
        }

        private DataTable AdatTáblaFeltöltés()
        {
            DataTable AdatTábla = new DataTable();
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Id", typeof(long));
                AdatTábla.Columns.Add("Berendezés", typeof(string));
                AdatTábla.Columns.Add("Készlet Sarzs", typeof(string));
                AdatTábla.Columns.Add("Raktár", typeof(string));
                AdatTábla.Columns.Add("Telephely", typeof(string));
                AdatTábla.Columns.Add("Forgóváz", typeof(string));
                AdatTábla.Columns.Add("Gyártási Szám", typeof(long));
                AdatTábla.Columns.Add("Átmérő", typeof(int));
                AdatTábla.Columns.Add("Állapot", typeof(string));
                AdatTábla.Columns.Add("Beépíthető", typeof(string));
                AdatTábla.Columns.Add("Műszaki Megjegyzés", typeof(string));
                AdatTábla.Columns.Add("Osztási Megjegyzés", typeof(string));
                AdatTábla.Columns.Add("Dátum", typeof(DateTime));
                AdatTábla.Columns.Add("Státus", typeof(string));

                List<Adat_Nóta> Adatok = KézNóta.Lista_Adat(!Aktív.Checked);
                List<Adat_Kerék_Tábla> AdatokKerék = KézKerék.Lista_Adatok();

                List<Adat_Kerék_Mérés> AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.Year - 1);
                List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(Ideig);
                AdatokMérés = AdatokMérés.OrderBy(a => a.Mikor).ToList();

                foreach (Adat_Nóta rekord in Adatok)
                {
                    Adat_Kerék_Tábla EgyKerék = AdatokKerék.FirstOrDefault(x => x.Kerékberendezés == rekord.Berendezés);
                    string gyáriszám = "";
                    if (EgyKerék != null) gyáriszám = EgyKerék.Kerékgyártásiszám;

                    Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                              where a.Kerékberendezés == rekord.Berendezés
                                              orderby a.Mikor ascending
                                              select a).LastOrDefault();
                    int átmérő = 0;
                    string állapot = "";
                    if (Mérés != null)
                    {
                        átmérő = Mérés.Méret;
                        állapot = $"{Mérés.Állapot}-{Enum.GetName(typeof(Kerék_Állapot), Mérés.Állapot.ToÉrt_Int()).Replace('_', ' ')}";
                    }


                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Id"] = rekord.Id;
                    Soradat["Berendezés"] = rekord.Berendezés;
                    Soradat["Készlet Sarzs"] = rekord.Készlet_Sarzs;
                    Soradat["Raktár"] = rekord.Raktár;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Forgóváz"] = rekord.Forgóváz;
                    Soradat["Gyártási Szám"] = gyáriszám.ToÉrt_Long();
                    Soradat["Beépíthető"] = rekord.Beépíthető ? "Igen" : "Nem";
                    Soradat["Műszaki Megjegyzés"] = rekord.MűszakiM;
                    Soradat["Osztási Megjegyzés"] = rekord.OsztásiM;
                    Soradat["Dátum"] = rekord.Dátum;
                    Soradat["Státus"] = $"{rekord.Státus} - {((Nóta_Státus)rekord.Státus).ToStrTrim().Replace('_', ' ')}";
                    Soradat["Átmérő"] = átmérő.ToÉrt_Int();
                    Soradat["Állapot"] = állapot;

                    AdatTábla.Rows.Add(Soradat);
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
            return AdatTábla;
        }

        private void Táblalista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            id = Táblalista.Rows[e.RowIndex].Cells[0].Value.ToString().ToÉrt_Int();
        }
        #endregion

        #region Módosítás
        Ablak_Nóta_Részletes Új_Ablak_Nóta_Részletes;
        private void Módosítás_Click(object sender, EventArgs e)
        {
            if (id == 0) return;
            Új_Ablak_Nóta_Részletes?.Close();

            Új_Ablak_Nóta_Részletes = new Ablak_Nóta_Részletes(id);
            Új_Ablak_Nóta_Részletes.FormClosed += Ablak_Kerék_Gyűjtő_Closed;
            Új_Ablak_Nóta_Részletes.Változás += TáblázatÍrás;
            Új_Ablak_Nóta_Részletes.Show();
        }

        private void Ablak_Kerék_Gyűjtő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Nóta_Részletes = null;
        }

        #endregion

        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Táblalista.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Fődarab-Nóta-{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Táblalista, true);
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

        private void BtnSAP_Click(object sender, EventArgs e)
        {
            string fájlexc = "";
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DateTime Eleje = DateTime.Now;
                //Adattáblába tesszük
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexc);

                if (!MyF.Betöltéshelyes("Nóta", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                //Készítünk egy listát az adatszerkezetnek megfelelően
                List<Adat_Nóta_SAP> Excel_Listában = Excel_Beolvas(Tábla);

                if (Excel_Listában != null) SAP(Excel_Listában);

                DateTime Vége = DateTime.Now;

                //kitöröljük a betöltött fájlt
                File.Delete(fájlexc);
                TáblázatÍrás();
                MessageBox.Show($"Az adat konvertálás befejeződött!\nidő:{Vége - Eleje}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SAP(List<Adat_Nóta_SAP> Adatok)
        {
            try
            {
                // Új vagy módosuló adatok rögzítése
                List<Adat_Nóta> AdatokNóta = KézNóta.Lista_Adat(true);
                List<Adat_Nóta> AdatokM = new List<Adat_Nóta>();
                List<Adat_Nóta> AdatokR = new List<Adat_Nóta>();
                foreach (Adat_Nóta_SAP rekord in Adatok)
                {
                    //Feltételek ami után rögzítünk vagy sem
                    bool kell = false;
                    if (rekord.Rendszerstátus == "RAKT" && !rekord.Rendezési.ToUpper().Contains("SEL")) kell = true;
                    else if (rekord.Rendszerstátus == "RNDÁ" && !rekord.Rendezési.ToUpper().Contains("SEL")) kell = true;

                    if (kell)
                    {
                        Adat_Nóta adat_Nóta = AdatokNóta.FirstOrDefault(x => x.Berendezés == rekord.Berendezés);
                        Adat_Nóta ADAT = new Adat_Nóta(
                                   adat_Nóta != null ? adat_Nóta.Id : 0,
                                   rekord.Berendezés,
                                   rekord.Készlet_Sarzs,
                                   rekord.Raktár,
                                   adat_Nóta != null ? 0 : 1);
                        if (adat_Nóta != null)
                            AdatokM.Add(ADAT);
                        else
                            AdatokR.Add(ADAT);
                    }
                }
                if (AdatokM != null && AdatokM.Count > 0) KézNóta.Módosítás(AdatokM);
                if (AdatokR != null && AdatokR.Count > 0) KézNóta.Rögzítés(AdatokR);

                //Amik beépítésre kerültek kisorolása
                AdatokNóta = KézNóta.Lista_Adat(true);
                List<long> IDK = new List<long>();
                foreach (Adat_Nóta rekord in AdatokNóta)
                {
                    Adat_Nóta_SAP Elem = Adatok.FirstOrDefault(a => a.Berendezés == rekord.Berendezés);
                    if (Elem == null)
                        IDK.Add(rekord.Id);       //Ha nincs ilyen elem akkor felvesszük a listára
                    else
                    {
                        bool kell = false;
                        //ha időközben selejtezésre került
                        if (Elem.Rendszerstátus == "RAKT" && Elem.Rendezési.ToUpper().Contains("SEL")) kell = true;
                        if (Elem.Rendszerstátus == "RNDÁ" && Elem.Rendezési.ToUpper().Contains("SEL")) kell = true;
                        //ha véletlenül tartalmazza a lista a beépített elemet
                        if (Elem.Rendszerstátus == "EHEQ") kell = true;
                        if (kell)
                        {
                            Adat_Nóta adat_Nóta = AdatokNóta.FirstOrDefault(x => x.Berendezés == rekord.Berendezés);
                            if (adat_Nóta != null) IDK.Add(adat_Nóta.Id);
                        }
                    }
                }
                if (IDK != null && IDK.Count > 0) KézNóta.Módosítás(IDK);



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

        private List<Adat_Nóta_SAP> Excel_Beolvas(DataTable EgyTábla)
        {
            List<Adat_Nóta_SAP> Adatok = new List<Adat_Nóta_SAP>();
            if (EgyTábla != null)
            {
                for (int i = 0; i < EgyTábla.Rows.Count; i++)
                {
                    Adat_Nóta_SAP Adat = new Adat_Nóta_SAP(
                                                EgyTábla.Rows[i]["Berendezés"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Rendszerstátus"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Készletsarzs"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Raktárhely"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Rendezési mező"].ToStrTrim());
                    Adatok.Add(Adat);
                }
            }
            return Adatok;
        }

        #region Összesítés ablak
        Ablak_Nóta_Összesítés Új_Ablak_Nóta_Összesítés;

        private void Ablak_Nóta_Összesítés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Nóta_Összesítés = null;
        }

        private void Összesítés_Click(object sender, EventArgs e)
        {

            Új_Ablak_Nóta_Összesítés?.Close();

            Új_Ablak_Nóta_Összesítés = new Ablak_Nóta_Összesítés();
            Új_Ablak_Nóta_Összesítés.FormClosed += Ablak_Nóta_Összesítés_Closed;
            Új_Ablak_Nóta_Összesítés.Show();
        }
        #endregion


    }
}
