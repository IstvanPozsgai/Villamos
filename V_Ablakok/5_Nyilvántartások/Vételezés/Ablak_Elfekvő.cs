using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Elfekvő : Form
    {
        readonly Kezelő_Elfekvő KézElfekvő = new Kezelő_Elfekvő();

        public Ablak_Elfekvő()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        private async void Btn_AdatFeldolgozás_Click(object sender, EventArgs e)
        {
            string fájl_MB52 = FájlMegnyitás("Válaszd ki az AKTUÁLIS RAKTÁRKÉSZLET (MB52) Excel fájlt");
            if (string.IsNullOrEmpty(fájl_MB52)) return;

            string fájl_MB51 = FájlMegnyitás("Válaszd ki az ANYAGMOZGÁSOK (MB51) Excel fájlt");
            if (string.IsNullOrEmpty(fájl_MB51)) return;

            Holtart.Be();
            timer1.Enabled = true;

            try
            {
                await Task.Run(() => FeldolgozÉsMent(fájl_MB52, fájl_MB51));

        

                TáblaÍró();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba rögzítésre került a naplóban.", "Hiba történt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                timer1.Enabled = false;
                Holtart.Ki();
            }
        }

        private string FájlMegnyitás(string ablakCím)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "MyDocuments";
                ofd.Title = ablakCím;
                ofd.Filter = "Excel fájlok |*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileName;
            }
            return string.Empty;
        }

        private void FeldolgozÉsMent(string fájl_MB52, string fájl_MB51)
        {

            try
            {



                // MB51 MOZGÁSOK BEOLVASÁSA ÉS ELLENŐRZÉSE
                DataTable tablaMB51 = Függvénygyűjtemény.Excel_Tábla_Beolvas(fájl_MB51);

                // Ellenőrzés a projekt standardja szerint
                if (!Függvénygyűjtemény.Betöltéshelyes("ElfekMB51", tablaMB51))
                    throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt MB51 (Anyagmozgások) adatok formátuma!");

                // Beolvasni kívánt oszlopok lekérdezése
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                // Oszlopnevek beállítása (MB51)
                string oszlopCikkszam51 = (from a in oszlopnév where a.Csoport == "ElfekMB51" && a.Státusz == false && a.Változónév == "Cikkszám" select a.Fejléc).FirstOrDefault();
                string oszlopSarzs51 = (from a in oszlopnév where a.Csoport == "ElfekMB51" && a.Státusz == false && a.Változónév == "Sarzs" select a.Fejléc).FirstOrDefault();
                string oszlopRaktar51 = (from a in oszlopnév where a.Csoport == "ElfekMB51" && a.Státusz == false && a.Változónév == "Raktárhely" select a.Fejléc).FirstOrDefault();
                string oszlopDatum51 = (from a in oszlopnév where a.Csoport == "ElfekMB51" && a.Státusz == false && a.Változónév == "Dátum" select a.Fejléc).FirstOrDefault();

                if (oszlopCikkszam51 == null || oszlopSarzs51 == null || oszlopRaktar51 == null || oszlopDatum51 == null)
                    throw new HibásBevittAdat("Nincs helyesen beállítva a beolvasótábla az MB51 fájlhoz!");

                Dictionary<string, DateTime> mozgásokKereső = new Dictionary<string, DateTime>();

                // Adatok kiolvasása és tisztítása
                foreach (DataRow Sor in tablaMB51.Rows)
                {
                    string cikkszám = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopCikkszam51].ToStrTrim(), 0, 50);
                    string sarzs = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopSarzs51].ToStrTrim(), 0, 50);
                    string raktár = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopRaktar51].ToStrTrim(), 0, 50);

                    // SAP dátumok esetleges felesleges szóközének eltávolítása, majd típuskonverzió
                    string datumSzoveg = Sor[oszlopDatum51].ToStrTrim().Replace(" ", "");
                    DateTime mozgásDátum = datumSzoveg.ToÉrt_DaTeTime();

                    if (string.IsNullOrWhiteSpace(cikkszám) || string.IsNullOrWhiteSpace(sarzs)) continue;

                    string kulcsPontos = $"{cikkszám}|{sarzs}|{raktár}";
                    string kulcsTartalek = $"{cikkszám}|{sarzs}";

                    if (mozgásDátum > new DateTime(1900, 1, 1))
                    {
                        if (!string.IsNullOrWhiteSpace(raktár))
                        {
                            if (mozgásokKereső.TryGetValue(kulcsPontos, out DateTime eddigi))
                            {
                                if (mozgásDátum > eddigi) mozgásokKereső[kulcsPontos] = mozgásDátum;
                            }
                            else mozgásokKereső.Add(kulcsPontos, mozgásDátum);
                        }

                        if (mozgásokKereső.TryGetValue(kulcsTartalek, out DateTime eddigiTartalek))
                        {
                            if (mozgásDátum > eddigiTartalek) mozgásokKereső[kulcsTartalek] = mozgásDátum;
                        }
                        else mozgásokKereső.Add(kulcsTartalek, mozgásDátum);
                    }
                }

                // MB52 KÉSZLET BEOLVASÁSA ÉS ELLENŐRZÉSE
                DataTable tablaMB52 = Függvénygyűjtemény.Excel_Tábla_Beolvas(fájl_MB52);

                if (!Függvénygyűjtemény.Betöltéshelyes("ElfekMB52", tablaMB52))
                    throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt MB52 (Raktárkészlet) adatok formátuma!");

                // Oszlopnevek beállítása (MB52)
                string oszlopCikkszam52 = (from a in oszlopnév where a.Csoport == "ElfekMB52" && a.Státusz == false && a.Változónév == "Cikkszám" select a.Fejléc).FirstOrDefault();
                string oszlopSarzs52 = (from a in oszlopnév where a.Csoport == "ElfekMB52" && a.Státusz == false && a.Változónév == "Sarzs" select a.Fejléc).FirstOrDefault();
                string oszlopRaktar52 = (from a in oszlopnév where a.Csoport == "ElfekMB52" && a.Státusz == false && a.Változónév == "Raktárhely" select a.Fejléc).FirstOrDefault();
                string oszlopMegnevezes52 = (from a in oszlopnév where a.Csoport == "ElfekMB52" && a.Státusz == false && a.Változónév == "Megnevezés" select a.Fejléc).FirstOrDefault();
                string oszlopMennyiseg52 = (from a in oszlopnév where a.Csoport == "ElfekMB52" && a.Státusz == false && a.Változónév == "Mennyiség" select a.Fejléc).FirstOrDefault();
                string oszlopErtek52 = (from a in oszlopnév where a.Csoport == "ElfekMB52" && a.Státusz == false && a.Változónév == "Érték" select a.Fejléc).FirstOrDefault();

                if (oszlopCikkszam52 == null || oszlopSarzs52 == null || oszlopRaktar52 == null || oszlopMegnevezes52 == null || oszlopMennyiseg52 == null || oszlopErtek52 == null)
                    throw new HibásBevittAdat("Nincs helyesen beállítva a beolvasótábla az MB52 fájlhoz!");

                List<Adat_Elfekvő> elfekvőLista = new List<Adat_Elfekvő>();

                // Adatok kiolvasása és tisztítása
                foreach (DataRow Sor in tablaMB52.Rows)
                {
                    string cikkszám = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopCikkszam52].ToStrTrim(), 0, 50);
                    string sarzs = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopSarzs52].ToStrTrim(), 0, 50);
                    string raktár = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopRaktar52].ToStrTrim(), 0, 50);
                    string megnevezes = Függvénygyűjtemény.Szöveg_Tisztítás(Sor[oszlopMegnevezes52].ToStrTrim(), 0, 255);

                    // Mennyiség és érték konvertálása a projekt saját kiterjesztésével
                    double mennyiseg = Sor[oszlopMennyiseg52].ToStrTrim().Replace(",", ".").ToÉrt_Double();
                    double ertek = Sor[oszlopErtek52].ToStrTrim().Replace(",", ".").ToÉrt_Double();

                    if (string.IsNullOrWhiteSpace(cikkszám) || string.IsNullOrWhiteSpace(sarzs)) continue;

                    string kulcsPontos = $"{cikkszám}|{sarzs}|{raktár}";
                    string kulcsTartalek = $"{cikkszám}|{sarzs}";

                    // Dátum párosítása: először pontos (Raktárhely), ha nincs, akkor tartalék
                    DateTime utolsoMozgas = new DateTime(1900, 1, 1);
                    if (mozgásokKereső.TryGetValue(kulcsPontos, out DateTime mDatum))
                        utolsoMozgas = mDatum;
                    else if (mozgásokKereső.TryGetValue(kulcsTartalek, out DateTime tDatum))
                        utolsoMozgas = tDatum;

                    Adat_Elfekvő ADAT = new Adat_Elfekvő(
                        0,
                        cikkszám,
                        megnevezes,
                        raktár,
                        mennyiseg,
                        ertek,
                        sarzs,
                        utolsoMozgas);

                    elfekvőLista.Add(ADAT);
                }

                // RÖGZÍTÉS AZ ADATBÁZISBA
                if (elfekvőLista.Count > 0)
                {
                    KézElfekvő.Tábla_Kiürítés();
                    KézElfekvő.Tömeges_Rögzítés(elfekvőLista);
                }
                MessageBox.Show("Az adatok feldolgozása és az adatbázisba történő rögzítése sikeresen befejeződött!",
                        "Sikeres művelet", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Ablak_Elfekvő_Load(object sender, EventArgs e)
        {
            TáblaÍró();
        }

        private void TáblaÍró()
        {
            try
            {
                List<Adat_Elfekvő> adatok = KézElfekvő.Lista_Adatok();

                Tábla.DataSource = null;
                Tábla.DataSource = adatok;

                OszlopokFormázása();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        private void OszlopokFormázása()
        {
            if (Tábla.Columns["Id"] != null) Tábla.Columns["Id"].Visible = false;

            if (Tábla.Columns["Anyag"] != null) { Tábla.Columns["Anyag"].HeaderText = "Anyag"; Tábla.Columns["Anyag"].Width = 130; }
            if (Tábla.Columns["Anyag_rövid_szövege"] != null) { Tábla.Columns["Anyag_rövid_szövege"].HeaderText = "Anyag rövid szövege"; Tábla.Columns["Anyag_rövid_szövege"].Width = 350; }
            if (Tábla.Columns["Raktárhely"] != null) { Tábla.Columns["Raktárhely"].HeaderText = "Raktárhely"; Tábla.Columns["Raktárhely"].Width = 100; }
            if (Tábla.Columns["Szabadon_használható"] != null) { Tábla.Columns["Szabadon_használható"].HeaderText = "Szabadon használható"; Tábla.Columns["Szabadon_használható"].Width = 150; }
            if (Tábla.Columns["Szab_felh_érték"] != null) { Tábla.Columns["Szab_felh_érték"].HeaderText = "Szab.felh. érték"; Tábla.Columns["Szab_felh_érték"].Width = 150; }
            if (Tábla.Columns["Sarzs"] != null) { Tábla.Columns["Sarzs"].HeaderText = "Sarzs"; Tábla.Columns["Sarzs"].Width = 80; }
            if (Tábla.Columns["Utolsó_mozgás"] != null) { Tábla.Columns["Utolsó_mozgás"].HeaderText = "Utolsó mozgás"; Tábla.Columns["Utolsó_mozgás"].Width = 130; }
        }

        private void Btn_Frissit_Click(object sender, EventArgs e)
        {
            TáblaÍró();
        }

        private void Btn_ExcelExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Elfekvő készlet mentése Excel fájlba";
                sfd.Filter = "Excel fájlok |*.xlsx";
                sfd.FileName = $"Elfekvő_Készlet_{DateTime.Now:yyyyMMdd_HHmm}";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Excel_Elfekvő_Export exporter = new Excel_Elfekvő_Export();

                    exporter.Export(sfd.FileName);

                    MessageBox.Show("Az Excel fájl sikeresen legenerálásra került!", "Sikeres export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Függvénygyűjtemény.Megnyitás(sfd.FileName);
                }
            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            // JAVÍTANDÓ:A súgó gomb nincs bekötve
        }
    }
}