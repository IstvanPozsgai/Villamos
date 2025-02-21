using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyLista = Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok.Karbantartási_ListaFeltöltés;

namespace Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok
{
    public partial class Ablak_Karbantartás_Csoport : Form
    {
        public string Cmbtelephely { get; private set; }

        readonly Kezelő_Technológia_Változat KézVáltozat = new Kezelő_Technológia_Változat();

        List<Adat_Technológia_Alap> AdatokTípusT = new List<Adat_Technológia_Alap>();
        List<Adat_technológia_Ciklus> AdatokCiklus = new List<Adat_technológia_Ciklus>();
        List<Adat_Technológia_Új> AdatokTechnológia = new List<Adat_Technológia_Új>();
        List<Adat_Technológia_Változat> AdatokVáltozat = new List<Adat_Technológia_Változat>();
        DataTable AdatTábla = new DataTable();

        public Ablak_Karbantartás_Csoport(string cmbTelephely)
        {
            InitializeComponent();
            Cmbtelephely = cmbTelephely;
        }

        private void Ablak_Karbantartás_Csoport_Load(object sender, EventArgs e)
        {
            Jogosultságkiosztás();
            Csoport_Típus_feltöltés();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Csoport_rögzít.Enabled = false;
            Csoport_Töröl.Enabled = false;

            melyikelem = 170;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Csoport_rögzít.Enabled = true;
                Csoport_Töröl.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void Csoport_Típus_feltöltés()
        {
            try
            {
                Csoport_típus.Items.Clear();
                AdatokTípusT = MyLista.TípustáblaLista();
                foreach (Adat_Technológia_Alap rekord in AdatokTípusT)
                    Csoport_típus.Items.Add(rekord.Típus);
                Csoport_típus.Refresh();
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

        private void Csoport_Ciklus_feltöltés()
        {
            try
            {
                if (Csoport_típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");

                Csoport_Ciklus.Items.Clear();
                AdatokCiklus = MyLista.KarbCiklusLista(Csoport_típus.Text.Trim());
                foreach (Adat_technológia_Ciklus rekord in AdatokCiklus)
                    Csoport_Ciklus.Items.Add(rekord.Fokozat);
                Csoport_Ciklus.Refresh();
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

        private void Csoport_típus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Törli_Csoport_Mezőket();
            Csoport_Ciklus_feltöltés();
        }

        private void Törli_Csoport_Mezőket()
        {
            Csoport_tábla.Rows.Clear();
            Csoport_Ciklus.Items.Clear();
            Csoport_változat.Items.Clear();
            Csoport_Végző.Items.Clear();
            Label111.Text = "Sorszám:";
        }

        private void Csoport_Ciklus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Csoport_Változat_feltöltés();
        }

        private void Csoport_Változat_feltöltés()
        {
            try
            {
                if (Csoport_típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");
                if (Csoport_Ciklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva karbantartási ciklus.");
                List<Adat_Technológia_Változat> Adatok = KézVáltozat.Lista_Adatok(Csoport_típus.Text.Trim(), Cmbtelephely.Trim());
                List<string> Változatok = Adatok.Where(a => a.Karbantartási_fokozat == Csoport_Ciklus.Text.Trim()).Select(a => a.Változatnév).Distinct().ToList();

                Csoport_változat.Text = "";
                Csoport_Végző.Text = "";
                Csoport_Végző.Items.Clear();
                Csoport_változat.Items.Clear();

                foreach (string rekord in Változatok)
                    Csoport_változat.Items.Add(rekord);
                Csoport_változat.Refresh();
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

        private void Csoport_változat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Végzi_feltöltés();
        }

        private void Végzi_feltöltés()
        {
            try
            {
                if (Csoport_típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");
                if (Csoport_Ciklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva karbantartási ciklus.");
                if (Csoport_változat.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva csoport változatnév.");

                List<Adat_Technológia_Változat> Adatok = KézVáltozat.Lista_Adatok(Csoport_típus.Text.Trim(), Cmbtelephely.Trim());
                List<string> Végzők = (from a in Adatok
                                       where a.Változatnév == Csoport_változat.Text.Trim()
                                       orderby a.Végzi
                                       select a.Végzi).Distinct().ToList();
                List<string> Változatok = Adatok.Where(a => a.Változatnév == Csoport_változat.Text.Trim()).Select(a => a.Végzi).Distinct().ToList();

                Csoport_Végző.Text = "";
                Csoport_Végző.Items.Clear();
                foreach (string rekord in Változatok)
                    Csoport_Végző.Items.Add(rekord);

                Csoport_Végző.Refresh();
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

        private void Csoport_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Csoport_tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva a táblázatban egy sor sem.");
                if (Csoport_típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");
                if (Csoport_Ciklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva karbantartási ciklus.");
                if (Csoport_változat.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve csoport változatnév.");
                if (Csoport_Végző.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve munkát végző csoportnév.");
                if (Csoport_Végző.Text.Trim().Length > 50) throw new HibásBevittAdat("Nem lehet 50 karakternél hosszabbb a munkát végző csoportnév.");
                if (Csoport_változat.Text.Trim().Length > 50) throw new HibásBevittAdat("Nem lehet 50 karakternél hosszabbb a munkát végző csoportnév.");


                List<Adat_Technológia_Változat> AdatokMód = new List<Adat_Technológia_Változat>();
                List<Adat_Technológia_Változat> AdatokRögz = new List<Adat_Technológia_Változat>();
                for (int i = 0; i < Csoport_tábla.SelectedRows.Count; i++)
                {
                    if (!long.TryParse(Csoport_tábla.SelectedRows[i].Cells[0].Value.ToString(), out long MelyikSorszám)) MelyikSorszám = 0;
                    Adat_Technológia_Változat Elem = (from a in AdatokVáltozat
                                                      where a.Karbantartási_fokozat == Csoport_Ciklus.Text.Trim()
                                                      && a.Változatnév == Csoport_változat.Text.Trim()
                                                      && a.Technológia_Id == MelyikSorszám
                                                      select a).FirstOrDefault();

                    Adat_Technológia_Változat ADAT = new Adat_Technológia_Változat(
                                                    MelyikSorszám,
                                                    Csoport_változat.Text.Trim(),
                                                    Csoport_Végző.Text.Trim(),
                                                    Csoport_Ciklus.Text.Trim());

                    if (Elem != null)
                        AdatokMód.Add(ADAT);
                    else
                        AdatokRögz.Add(ADAT);
                }
                if (AdatokMód.Count > 0) KézVáltozat.Módosítás(Csoport_típus.Text.Trim(), Cmbtelephely.Trim(), AdatokMód);
                if (AdatokRögz.Count > 0) KézVáltozat.Rögzítés(Csoport_típus.Text.Trim(), Cmbtelephely.Trim(), AdatokRögz);

                MessageBox.Show("Adatok rögzítése megtörtént", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Label111.Text = "Sorszám:";
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

        private void Csoport_frissít_Click(object sender, EventArgs e)
        {
            Csoport_tábla_író();
        }

        private void Csoport_tábla_író()
        {
            try
            {
                if (Csoport_típus.Text.Trim() == "") throw new HibásBevittAdat("Jármű típushoz tartozó címet választani kell.");
                if (Csoport_Ciklus.Text.Trim() == "") throw new HibásBevittAdat("Karbantartási ciklusoz tartozó címet választani kell.");
                Csoport_tábla.Visible = false;
                AdatTábla.Clear();
                ABFejléc();
                ABTartalom();
                Csoport_tábla.DataSource = AdatTábla;
                ABOszlopSzélesség();

                Csoport_tábla.Visible = true;
                Csoport_tábla.Refresh();
                Csoport_tábla.ClearSelection();
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
            finally
            {
                Holtart.Ki();
            }
        }

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("ID");
                AdatTábla.Columns.Add("Rész- egység");
                AdatTábla.Columns.Add("Utasítás szám");
                AdatTábla.Columns.Add("Utasítás cím");
                AdatTábla.Columns.Add("Utasítás leírása");
                AdatTábla.Columns.Add("Változatnév");
                AdatTábla.Columns.Add("Csoportosítási elnevezés");
                AdatTábla.Columns.Add("Dátumtól", typeof(DateTime));
                AdatTábla.Columns.Add("Dátumig", typeof(DateTime));
                AdatTábla.Columns.Add("Karb Fokozat");
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

        private void ABOszlopSzélesség()
        {
            Csoport_tábla.Columns["ID"].Width = 70;
            Csoport_tábla.Columns["Rész- egység"].Width = 70;
            Csoport_tábla.Columns["Utasítás szám"].Width = 70;
            Csoport_tábla.Columns["Utasítás cím"].Width = 250;
            Csoport_tábla.Columns["Utasítás leírása"].Width = 400;
            Csoport_tábla.Columns["Változatnév"].Width = 200;
            Csoport_tábla.Columns["Csoportosítási elnevezés"].Width = 200;
            Csoport_tábla.Columns["Dátumtól"].Width = 100;
            Csoport_tábla.Columns["Dátumig"].Width = 100;
            Csoport_tábla.Columns["Karb Fokozat"].Width = 100;
        }

        private void ABTartalom()
        {
            try
            {
                AdatokVáltozat = MyLista.VáltozatLista(Csoport_típus.Text.Trim(), Cmbtelephely.Trim());
                AdatokCiklus = MyLista.KarbCiklusLista(Csoport_típus.Text.Trim());
                AdatokTechnológia = MyLista.TechnológiaLista(Csoport_típus.Text.Trim());
                if (CHKÉrvényes.Checked)
                    AdatokTechnológia = (from a in AdatokTechnológia
                                         where a.Érv_kezdete <= DateTime.Now && a.Érv_vége >= DateTime.Now
                                         select a).ToList();

                Adat_technológia_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                      where a.Fokozat == Csoport_Ciklus.Text.Trim()
                                                      select a).FirstOrDefault();
                if (ElemCiklus == null) return;

                List<Adat_Technológia_Új> Adatok_Tech = (from a in AdatokTechnológia
                                                         where a.Karb_ciklus_eleje <= ElemCiklus.Sorszám && a.Karb_ciklus_vége >= ElemCiklus.Sorszám
                                                         orderby a.ID, a.Részegység, a.Munka_utasítás_szám
                                                         select a).ToList();


                List<Adat_Technológia_Munkalap> Adatok = MyLista.Adatok_Egyesítése(Adatok_Tech, AdatokVáltozat);
                Holtart.Be(Adatok.Count + 1);


                List<Adat_Technológia_Munkalap> Változat = new List<Adat_Technológia_Munkalap>();

                if (Csoport_változat.Text.Trim() == "")
                {
                    //minden változatot kiír ami már kivan töltve
                    Változat = (from a in Adatok
                                where (a.Karbantartási_fokozat.Trim() == Csoport_Ciklus.Text.Trim() || a.Karbantartási_fokozat.Trim() == "")
                                orderby a.ID
                                select a).ToList();
                }
                else
                {
                    Változat = (from a in Adatok
                                where (a.Karbantartási_fokozat.Trim() == Csoport_Ciklus.Text.Trim() || a.Karbantartási_fokozat.Trim() == "")
                                && a.Változatnév.Trim() == Csoport_változat.Text.Trim()
                                orderby a.ID
                                select a).ToList();
                    if (Csoport_Végző.Text.Trim() != "")
                        Változat = (from a in Adatok
                                    where (a.Karbantartási_fokozat.Trim() == Csoport_Ciklus.Text.Trim() || a.Karbantartási_fokozat.Trim() == "")
                                    && a.Változatnév.Trim() == Csoport_változat.Text.Trim()
                                    && a.Végzi == Csoport_Végző.Text.Trim()
                                    orderby a.ID
                                    select a).ToList();
                }

                AdatTábla.Clear();
                foreach (Adat_Technológia_Munkalap adat in Változat)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["ID"] = adat.ID;
                    Soradat["Rész- egység"] = adat.Részegység.Trim();
                    Soradat["Utasítás szám"] = adat.Munka_utasítás_szám.Trim();
                    Soradat["Utasítás cím"] = adat.Utasítás_Cím.Trim().Replace("\n", " ");
                    Soradat["Utasítás leírása"] = adat.Utasítás_leírás.Trim().Replace("\n", " ");
                    Soradat["Változatnév"] = "";
                    Soradat["Csoportosítási elnevezés"] = "";
                    Soradat["Dátumtól"] = adat.Érv_kezdete;
                    Soradat["Dátumig"] = adat.Érv_vége;
                    Soradat["Karb Fokozat"] = adat.Karbantartási_fokozat.Trim();

                    if (adat.Karbantartási_fokozat.Trim() == Csoport_Ciklus.Text.Trim())
                    {
                        Soradat["Változatnév"] = adat.Változatnév.Trim();
                        Soradat["Csoportosítási elnevezés"] = adat.Végzi.Trim();
                    }
                    AdatTábla.Rows.Add(Soradat);
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
            finally
            {
                Holtart.Ki();
            }
        }

        private void Csoport_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Sorszám.Text = Csoport_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            Csoport_változat.Text = Csoport_tábla.Rows[e.RowIndex].Cells[5].Value.ToStrTrim();
            Csoport_Végző.Text = Csoport_tábla.Rows[e.RowIndex].Cells[6].Value.ToStrTrim();
        }

        private void Csoport_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kivásztva a táblázatban érvényes sor.");

                if (!long.TryParse(Sorszám.Text.Trim(), out long MelyikSorszám)) MelyikSorszám = 0;

                Adat_Technológia_Változat Elem = (from a in AdatokVáltozat
                                                  where a.Karbantartási_fokozat == Csoport_Ciklus.Text.Trim()
                                                  && a.Változatnév == Csoport_változat.Text.Trim()
                                                  && a.Technológia_Id == MelyikSorszám
                                                  && a.Végzi == Csoport_Végző.Text.Trim()
                                                  select a).FirstOrDefault();

                Adat_Technológia_Változat ADAT = new Adat_Technológia_Változat(
                                MelyikSorszám,
                                Csoport_változat.Text.Trim(),
                                Csoport_Végző.Text.Trim(),
                                Csoport_Ciklus.Text.Trim());
                if (Elem != null)
                    KézVáltozat.Törlés(Csoport_típus.Text.Trim(), Cmbtelephely.Trim(), ADAT);
                Csoport_tábla_író();
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

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Csoport_tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Munkalap_csoportosítás_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Csoport_tábla, false);
                MessageBox.Show("Elkészült az Excel tábla: \n" + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void BtnÜres_Click(object sender, EventArgs e)
        {
            Csoport_változat.Text = "";
            Csoport_Végző.Text = "";
        }
    }
}
