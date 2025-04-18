﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Application = System.Windows.Forms.Application;
using Funkció = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás
{
    public delegate void Event_Kidobó();
    public partial class Ablak_Eszterga_Karbantartás : Form
    {
        #region Osztalyszintu elemek
        readonly DateTime MaiDatum = DateTime.Today;
        DateTime TervDatum;
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        private string AktívTáblaTípus;
        readonly DataTable AdatTábla = new DataTable();
        #endregion

        #region Listák
        private List<Adat_Eszterga_Műveletek> AdatokMűvelet;
        private List<Adat_Eszterga_Üzemóra> AdatokÜzemóra;
        private List<Adat_Eszterga_Műveletek_Napló> AdatokMűveletNapló;
        #endregion

        #region Kezelők
        readonly Kezelő_Eszterga_Műveletek Kéz_Művelet = new Kezelő_Eszterga_Műveletek();
        readonly Kezelő_Eszterga_Műveletek_Napló Kéz_Művelet_Napló = new Kezelő_Eszterga_Műveletek_Napló();
        #endregion

        #region Alap
        public Ablak_Eszterga_Karbantartás()
        {
            InitializeComponent();
        }
        private void Ablak_Eszterga_Karbantartás_Load(object sender, EventArgs e)
        {
            long Üzemóra = 0;
            string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/Kerékeszterga";

            if (Directory.Exists(hely))
                Directory.CreateDirectory(hely);

            //hely += "/Eszterga_Karbantartás.accdb";
            hely += "/Eszterga_Karbantartás.mdb";

            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Eszterga_Karbantartás(hely);

            //string helyNapló = $@"{Application.StartupPath}/Főmérnökség/adatok/Kerékeszterga/Eszterga_Karbantartás_{DateTime.Now.Year}_napló.accdb";
            string helyNapló = $@"{Application.StartupPath}/Főmérnökség/adatok/Kerékeszterga/Eszterga_Karbantartás_{DateTime.Now.Year}_napló.mdb";

            if (!File.Exists(helyNapló))
                Adatbázis_Létrehozás.Eszterga_Karbantartás_Napló(helyNapló);

            AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();

            Adat_Eszterga_Üzemóra rekord = (from a in AdatokÜzemóra
                                            where a.Dátum.Date == MaiDatum && a.Státus != true
                                            select a).FirstOrDefault();

            if (rekord != null)
            {
                MessageBox.Show($"A mai napon már rögzítettek üzemóra adatot.\nAz utolsó rögzített üzemóra: {rekord.Üzemóra}.",
                                "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Üzemóra = rekord.Üzemóra;
            }
            else
            {
                using (Ablak_Eszterga_Karbantartás_Segéd SegedAblak = new Ablak_Eszterga_Karbantartás_Segéd())
                {
                    if (SegedAblak.ShowDialog() == DialogResult.OK)
                        Üzemóra = SegedAblak.Üzemóra;
                    else
                    {
                        this.Close();
                        return;
                    }
                }
            }
            Jogosultságkiosztás();
            TáblaListázás();
            ÁtlagÜzemóraFrissítés(30);
            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            Tábla.ClearSelection();
        }
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                melyikelem = 160;
                Btn_Rögzít.Visible = Baross;
                Btn_Módosítás.Visible = Baross;

                //módosítás 1
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat
                Btn_Módosítás.Enabled = MyF.Vanjoga(melyikelem, 1);

                //módosítás 2
                Btn_Rögzít.Enabled = MyF.Vanjoga(melyikelem, 2);

                //módosítás 3
                //Ablak_Eszterga_Karbantartás_Módosít oldalon is felhasználva.
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

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Módosít Új_ablak_EsztergaMódosít;

        private void Btn_Módosítás_Click(object sender, EventArgs e)
        {
            if (Új_ablak_EsztergaMódosít == null)
            {
                Új_ablak_EsztergaMódosít = new Ablak_Eszterga_Karbantartás_Módosít();
                Új_ablak_EsztergaMódosít.FormClosed += Új_ablak_EsztergaMódosít_Closed;
                Új_ablak_EsztergaMódosít.Show();
                Új_ablak_EsztergaMódosít.Eszterga_Változás += TáblaListázás;
            }
            else
            {
                Új_ablak_EsztergaMódosít.Activate();
                Új_ablak_EsztergaMódosít.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_EsztergaMódosít = null;
        }

        private void Ablak_Eszterga_Karbantartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_EsztergaMódosít?.Close();
        }
        #endregion



        #region Egyseg
        public enum EsztergaEgyseg
        {
            Dátum = 1,
            Üzemóra = 2,
            Bekövetkezés = 3
        }
        #endregion

        #region Tábla Listázások && Naplózás
        private void TáblaListázás()
        {
            AktívTáblaTípus = "Muvelet";
            TáblaÜrítés();
            AdatTábla.Columns.Clear();
            AdatTábla.Rows.Clear();
            AdatTábla.Columns.Add("Sorszám");
            AdatTábla.Columns.Add("Művelet");
            AdatTábla.Columns.Add("Egység");
            AdatTábla.Columns.Add("Nap");
            AdatTábla.Columns.Add("Óra");
            AdatTábla.Columns.Add("Státusz");
            AdatTábla.Columns.Add("Utolsó Dátum");
            AdatTábla.Columns.Add("Utolsó Üzemóra");
            AdatTábla.Columns.Add("Esedékesség Dátuma");
            AdatTábla.Columns.Add("Becsült Üzemóra");
            AdatTábla.Columns.Add("Megjegyzés");

            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();
            TervDatum = DtmPckrElőTerv.Value.Date;

            AdatokMűvelet = AdatokMűvelet.OrderBy(rekord =>
                Kiszínezés(rekord, TervDatum) == Color.IndianRed ? 0 :
                Kiszínezés(rekord, TervDatum) == Color.Yellow ? 1 : 2).ToList();

            foreach (Adat_Eszterga_Műveletek rekord in AdatokMűvelet)
            {
                if (rekord.Státus != true)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();

                    Adat_Eszterga_Üzemóra uzemoraRekord = AdatokÜzemóra
                        .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);

                    Soradat["Utolsó Üzemóra"] = uzemoraRekord != null ? uzemoraRekord.Üzemóra : rekord.Utolsó_Üzemóra_Állás;
                    DateTime EsedekesDatum = DatumEsedekesegSzamitasa(rekord.Utolsó_Dátum, rekord, uzemoraRekord);
                    Soradat["Esedékesség Dátuma"] = EsedekesDatum.ToShortDateString();
                    Soradat["Becsült Üzemóra"] = BecsültÜzemóra(EsedekesDatum);

                    Soradat["Megjegyzés"] = rekord.Megjegyzés;

                    AdatTábla.Rows.Add(Soradat);
                }
            }

            Tábla.DataSource = AdatTábla;
            SorSzinezes();
            OszlopSzélesség();
            for (int i = 0; i < 10; i++)
                Tábla.Columns[i].ReadOnly = true;
            Tábla.Visible = true;
            Tábla.ClearSelection();
        }
        private void EloreTervezesListazasa()
        {
            AktívTáblaTípus = "EloreTervezes";
            TáblaÜrítés();
            DataTable AdatTábla = new DataTable();
            AdatTábla.Columns.Clear();
            AdatTábla.Rows.Clear();
            AdatTábla.Columns.Add("Sorszám");
            AdatTábla.Columns.Add("Művelet");
            AdatTábla.Columns.Add("Egység");
            AdatTábla.Columns.Add("Nap");
            AdatTábla.Columns.Add("Óra");
            AdatTábla.Columns.Add("Státusz");
            AdatTábla.Columns.Add("Utolsó Dátum");
            AdatTábla.Columns.Add("Utolsó Üzemóra");
            AdatTábla.Columns.Add("Esedékesség Dátuma");
            AdatTábla.Columns.Add("Becsült Üzemóra");
            AdatTábla.Columns.Add("Megjegyzés");

            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();
            TervDatum = DtmPckrElőTerv.Value.Date;
            double SzuksegesNapok;

            List<DataRow> RendezettSorok = new List<DataRow>();

            foreach (Adat_Eszterga_Műveletek rekord in AdatokMűvelet)
            {
                if (rekord.Státus == true) continue;

                int ID = rekord.ID;
                DateTime UtolsoDatum = rekord.Utolsó_Dátum;
                long UtolsoUzemora = rekord.Utolsó_Üzemóra_Állás;
                long BecsultUzemora = BecsültÜzemóra(TervDatum);

                while (UtolsoDatum.AddDays(rekord.Mennyi_Dátum) <= TervDatum || (UtolsoUzemora + rekord.Mennyi_Óra) >= BecsultUzemora)
                {
                    bool Esedekes = false;

                    if (rekord.Egység == (int)EsztergaEgyseg.Dátum)
                    {
                        if ((TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum)
                        {
                            Esedekes = true;
                            UtolsoDatum = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                        }
                    }
                    else
                    {
                        double AtlagosNapiUzemNovekedes = AtlagUzemoraNovekedesKiszamitasa(TervDatum);

                        if (rekord.Egység == (int)EsztergaEgyseg.Üzemóra)
                        {
                            if ((BecsultUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra)
                            {
                                Esedekes = true;

                                SzuksegesNapok = Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes);

                                UtolsoDatum = UtolsoDatum.AddDays(SzuksegesNapok);
                                UtolsoUzemora += rekord.Mennyi_Óra;
                            }
                        }
                        else if (rekord.Egység == (int)EsztergaEgyseg.Bekövetkezés)
                        {
                            bool NapEsedekes = (TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum;
                            bool UzemoraEsedekes = (BecsultUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra;

                            if (NapEsedekes && UzemoraEsedekes)
                            {
                                DateTime EsedekesDatumNap = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                                DateTime EsedekesDatumUzemora = UtolsoDatum.AddDays(Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes));

                                if (EsedekesDatumNap <= EsedekesDatumUzemora)
                                {
                                    Esedekes = true;
                                    UtolsoDatum = EsedekesDatumNap;
                                }
                                else
                                {
                                    Esedekes = true;
                                    UtolsoDatum = EsedekesDatumUzemora;
                                    UtolsoUzemora += rekord.Mennyi_Óra;
                                }
                            }
                            else if (NapEsedekes)
                            {
                                Esedekes = true;
                                UtolsoDatum = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                            }
                            else if (UzemoraEsedekes)
                            {
                                Esedekes = true;
                                SzuksegesNapok = Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes);
                                UtolsoUzemora += rekord.Mennyi_Óra;
                                UtolsoDatum = UtolsoDatum.AddDays(SzuksegesNapok);
                            }
                        }
                    }

                    if (Esedekes && UtolsoDatum.Date <= DtmPckrElőTerv.Value.Date)
                    {
                        DataRow Soradat = AdatTábla.NewRow();

                        Soradat["Sorszám"] = rekord.ID;
                        Soradat["Művelet"] = rekord.Művelet;
                        Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                        Soradat["Nap"] = rekord.Mennyi_Dátum;
                        Soradat["Óra"] = rekord.Mennyi_Óra;
                        Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                        Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();

                        Adat_Eszterga_Üzemóra uzemoraRekord = AdatokÜzemóra
                            .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);
                        Soradat["Utolsó Üzemóra"] = uzemoraRekord != null ? uzemoraRekord.Üzemóra : rekord.Utolsó_Üzemóra_Állás;

                        Soradat["Esedékesség Dátuma"] = UtolsoDatum.ToShortDateString();
                        Soradat["Becsült Üzemóra"] = BecsültÜzemóra(UtolsoDatum);
                        Soradat["Megjegyzés"] = rekord.Megjegyzés;

                        RendezettSorok.Add(Soradat);
                    }
                    if (!Esedekes) break;
                }
            }

            IEnumerable<DataRow> RendezettAdatok = RendezettSorok
                .OrderBy(sor => DateTime.Parse(sor["Esedékesség Dátuma"].ToStrTrim()))
                .ThenBy(sor => int.Parse(sor["Sorszám"].ToStrTrim()));

            foreach (DataRow sor in RendezettAdatok)
                AdatTábla.Rows.Add(sor);

            Tábla.DataSource = AdatTábla;
            SorSzinezes();
            OszlopSzélesség();
            for (int i = 0; i < 11; i++)
                Tábla.Columns[i].ReadOnly = true;
            Tábla.Visible = true;
            Tábla.ClearSelection();
        }
        private void TáblaNaplóListázás()
        {
            AktívTáblaTípus = "Napló";
            Tábla.DataSource = null;
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            DataTable AdatTábla = new DataTable();
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Művelet Sorszáma");
            AdatTábla.Columns.Add("Művelet");
            AdatTábla.Columns.Add("Nap");
            AdatTábla.Columns.Add("Óra");
            AdatTábla.Columns.Add("Utolsó Dátum");
            AdatTábla.Columns.Add("Utolsó Üzemóra");
            AdatTábla.Columns.Add("Megjegyzés");
            AdatTábla.Columns.Add("Rögzítő");
            AdatTábla.Columns.Add("Rögzítés Dátuma");

            AdatokMűveletNapló = Funkció.Eszterga_KarbantartasNaplóFeltölt();
            List<DataRow> RendezettSorok = new List<DataRow>();
            foreach (Adat_Eszterga_Műveletek_Napló rekord in AdatokMűveletNapló)
            {
                DataRow Soradat = AdatTábla.NewRow();

                Soradat["Művelet Sorszáma"] = rekord.ID;
                Soradat["Művelet"] = rekord.Művelet;
                Soradat["Nap"] = rekord.Mennyi_Dátum;
                Soradat["Óra"] = rekord.Mennyi_Óra;
                Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;
                Soradat["Megjegyzés"] = rekord.Megjegyzés;
                Soradat["Rögzítő"] = rekord.Rögzítő;
                Soradat["Rögzítés Dátuma"] = rekord.Rögzítés_Dátuma.ToShortDateString();

                RendezettSorok.Add(Soradat);
            }
            IEnumerable<DataRow> RendezettAdatok = RendezettSorok
                .OrderBy(sor => DateTime.Parse(sor["Utolsó Dátum"].ToStrTrim()))
                .ThenBy(sor => int.Parse(sor["Művelet Sorszáma"].ToStrTrim()));

            foreach (DataRow sor in RendezettAdatok)
                AdatTábla.Rows.Add(sor);

            Tábla.DataSource = AdatTábla;

            Tábla.Columns["Művelet Sorszáma"].Width = 110;
            Tábla.Columns["Művelet"].Width = 950;
            Tábla.Columns["Nap"].Width = 60;
            Tábla.Columns["Óra"].Width = 60;
            Tábla.Columns["Utolsó Dátum"].Width = 105;
            Tábla.Columns["Utolsó Üzemóra"].Width = 120;
            Tábla.Columns["Megjegyzés"].Width = 221;
            Tábla.Columns["Rögzítő"].Width = 150;
            Tábla.Columns["Rögzítés Dátuma"].Width = 115;
            for (int i = 0; i < 9; i++)
                Tábla.Columns[i].ReadOnly = true;
            Tábla.Visible = true;
            Tábla.ClearSelection();
        }
        private void OszlopSzélesség()
        {
            Tábla.Columns["Sorszám"].Width = 97;
            Tábla.Columns["Művelet"].Width = 700;
            Tábla.Columns["Egység"].Width = 110;
            Tábla.Columns["Nap"].Width = 60;
            Tábla.Columns["Óra"].Width = 60;
            Tábla.Columns["Státusz"].Width = 90;
            Tábla.Columns["Utolsó Dátum"].Width = 110;
            Tábla.Columns["Utolsó Üzemóra"].Width = 140;
            Tábla.Columns["Esedékesség Dátuma"].Width = 130;
            Tábla.Columns["Becsült Üzemóra"].Width = 140;
            Tábla.Columns["Megjegyzés"].Width = 254;
        }
        private void TáblaÜrítés()
        {
            Tábla.DataSource = null;
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
        }
        private void Naplózás(DataGridViewRow Sor, DateTime UtolsóDátum, long UtolsóÜzemóra)
        {
            try
            {
                int Id = Sor.Cells[0].Value.ToÉrt_Int();
                string Művelet = Sor.Cells[1].Value?.ToStrTrim() ?? "";
                int MennyiNap = Sor.Cells[3].Value.ToÉrt_Int();
                int MennyiÓra = Sor.Cells[2].Value.ToÉrt_Int();
                string Megjegyzes = Sor.Cells[10].Value.ToStrTrim();
                string Rögzítő = Program.PostásNév.ToStrTrim();
                DateTime MaiDátum = DateTime.Today;

                Adat_Eszterga_Műveletek_Napló ADAT = new Adat_Eszterga_Műveletek_Napló(Id,
                                                              Művelet,
                                                              MennyiNap,
                                                              MennyiÓra,
                                                              UtolsóDátum,
                                                              UtolsóÜzemóra,
                                                              Megjegyzes,
                                                              Rögzítő,
                                                              MaiDátum);
                Kéz_Művelet_Napló.EsztergaNaplózás(ADAT);

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

        #region Szinezes
        private void SorSzinezes()
        {
            foreach (DataGridViewRow row in Tábla.Rows)
            {
                if (AktívTáblaTípus == "Napló") return;
                if (row.IsNewRow) continue;
                if (row.Cells["Sorszám"].Value != null && int.TryParse(row.Cells["Sorszám"].Value.ToStrTrim(), out int Sorszam))
                {
                    Adat_Eszterga_Műveletek rekord = AdatokMűvelet.FirstOrDefault(r => r.ID == Sorszam);

                    if (rekord != null)
                    {
                        Color háttérszín = Kiszínezés(rekord, TervDatum);
                        row.DefaultCellStyle.BackColor = háttérszín;
                    }
                }
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private Color Kiszínezés(Adat_Eszterga_Műveletek rekord, DateTime TervDatum)
        {
            int Egyseg = rekord.Egység;
            DateTime UtolsoDatum = rekord.Utolsó_Dátum;
            long UtolsoUzemora = rekord.Utolsó_Üzemóra_Állás;
            long AktualisUzemora = BecsültÜzemóra(TervDatum);
            int ElteltNapok = (int)(TervDatum - UtolsoDatum).TotalDays;
            long ElteltOrak = AktualisUzemora - UtolsoUzemora;
            int ElőrejelezDátum = rekord.Mennyi_Dátum - TxtBxNapi.Text.ToÉrt_Int();
            int ElőrejelezÜzem = rekord.Mennyi_Óra - TxtBxÜzem.Text.ToÉrt_Int();

            if (Egyseg == (int)EsztergaEgyseg.Dátum)
            {
                if (ElteltNapok >= rekord.Mennyi_Dátum)
                    return Color.IndianRed;
                else if (ElteltNapok >= ElőrejelezDátum && rekord.Mennyi_Dátum > 1)
                    return Color.Yellow;
                else
                    return Color.LawnGreen;
            }
            else if (Egyseg == (int)EsztergaEgyseg.Üzemóra)
            {
                if (ElteltOrak >= rekord.Mennyi_Óra)
                    return Color.IndianRed;
                else if (ElteltOrak >= ElőrejelezÜzem)
                    return Color.Yellow;
                else
                    return Color.LawnGreen;
            }
            else if (Egyseg == (int)EsztergaEgyseg.Bekövetkezés)
            {
                bool Datum = (TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum;
                bool Uzemora = (AktualisUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra;

                if (Datum && Uzemora || Datum || Uzemora)
                    return Color.IndianRed;
                else if (ElteltNapok >= ElőrejelezDátum || ElteltOrak >= ElőrejelezÜzem)
                    return Color.Yellow;
                else
                    return Color.LawnGreen;
            }
            return Color.LawnGreen;
        }
        #endregion

        #region Szamolasok
        private double AtlagUzemoraNovekedesKiszamitasa(DateTime tervDatum)
        {
            List<Adat_Eszterga_Üzemóra> rekord = AdatokÜzemóra
                .Where(a => a.Dátum <= tervDatum && !a.Státus)
                .OrderBy(a => a.Dátum)
                .ToList();

            if (rekord.Count < 2)
                throw new Exception("Nincs elegendő adat az üzemóra átlagának számításához.");

            double NapiAtlagaosUzemNovekedes = 0;
            for (int i = 1; i < rekord.Count; i++)
            {
                double napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                if (napok > 0)
                    NapiAtlagaosUzemNovekedes += (rekord[i].Üzemóra - rekord[i - 1].Üzemóra) / napok;
            }
            return NapiAtlagaosUzemNovekedes / (rekord.Count - 1);
        }
        private DateTime DatumEsedekesegSzamitasa(DateTime UtolsoDatum, Adat_Eszterga_Műveletek rekord, Adat_Eszterga_Üzemóra uzemoraRekord)
        {
            DateTime? EsedekesDatumNap = null;
            if (rekord.Mennyi_Dátum > 0)
                EsedekesDatumNap = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);

            DateTime? EsedekesDatumUzemora = null;
            if (rekord.Mennyi_Óra > 0 && uzemoraRekord != null)
            {
                double NapiUzemoraNovekedes = AtlagUzemoraNovekedesKiszamitasa(UtolsoDatum);

                if (NapiUzemoraNovekedes > 0)
                {
                    double NapokEsedekessegig = rekord.Mennyi_Óra / NapiUzemoraNovekedes;
                    EsedekesDatumUzemora = UtolsoDatum.AddDays(Math.Ceiling(NapokEsedekessegig));
                }
            }

            if (EsedekesDatumNap.HasValue && EsedekesDatumUzemora.HasValue)
            {
                return EsedekesDatumNap.Value <= EsedekesDatumUzemora.Value
                    ? EsedekesDatumNap.Value
                    : EsedekesDatumUzemora.Value;
            }

            if (EsedekesDatumNap.HasValue)
                return EsedekesDatumNap.Value;

            if (EsedekesDatumUzemora.HasValue)
                return EsedekesDatumUzemora.Value;

            return UtolsoDatum;
        }
        private long BecsültÜzemóra(DateTime ElőDátum)
        {
            if (AdatokÜzemóra == null || AdatokÜzemóra.Count < 2)
                return 0;

            List<Adat_Eszterga_Üzemóra> rekord = (from a in AdatokÜzemóra
                                                  where !a.Státus
                                                  orderby a.Dátum
                                                  select a).ToList();

            if (rekord.Count < 2)
                return 0;

            double NapiNövekedés = 0;

            for (int i = 1; i < rekord.Count; i++)
            {
                double Napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                if (Napok > 0)
                    NapiNövekedés += (rekord[i].Üzemóra - rekord[i - 1].Üzemóra) / Napok;
            }
            NapiNövekedés /= rekord.Count - 1;

            Adat_Eszterga_Üzemóra UtolsóRekord = rekord
                .Where(a => !a.Státus)
                .LastOrDefault();

            double NapokElőDátumhoz = (ElőDátum - UtolsóRekord.Dátum).TotalDays;

            return UtolsóRekord.Üzemóra + (long)(NapiNövekedés * NapokElőDátumhoz);
        }
        private bool DátumEllenőrzés(DateTime MaiDatum, DateTime TervDatum)
        {
            if (MaiDatum != TervDatum)
            {
                MessageBox.Show("Nem lehet előretervezéssel módosítani a műveletet.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void ÁtlagÜzemóraFrissítés(int Napok = 30)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtBxNapiUzemoraAtlag.Text))
                    TxtBxNapiUzemoraAtlag.Text = "30";

                DateTime KezdetiDatum = MaiDatum.AddDays(-Napok);

                List<Adat_Eszterga_Üzemóra> rekord = (from a in AdatokÜzemóra
                                                      where !a.Státus && a.Dátum >= KezdetiDatum
                                                      orderby a.Dátum
                                                      select a).ToList();

                if (rekord.Count < 2)
                {
                    LblÁtlagÜzemóraSzám.Text = $"Nincs elegendő adat az átlag számításhoz.";
                    return;
                }

                List<double> NovekedesiAranyok = new List<double>();
                for (int i = 1; i < rekord.Count; i++)
                {
                    double Különbség = rekord[i].Üzemóra - rekord[i - 1].Üzemóra;
                    double napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                    if (napok > 0)
                        NovekedesiAranyok.Add(Különbség / napok);
                }

                double Átlag = NovekedesiAranyok.Count > 0 ? NovekedesiAranyok.Average() : 0;

                LblÁtlagÜzemóraSzám.Text = $"Üzemóra növekedése {Napok} napig átlagolva: {Átlag:F2} üzemóra";
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

        #region Gombok && Muveletek
        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\KerékEsztergaKarbantartás.html";
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
        private void Btn_Frissít_Click(object sender, EventArgs e)
        {
            TxtBxNapiUzemoraAtlag.Text = "30";
            TxtBxNapi.Text = "5";
            TxtBxÜzem.Text = "8";
            DtmPckrElőTerv.Value = MaiDatum;
            Btn_Rögzít.Visible = true;
            TáblaListázás();
            ÁtlagÜzemóraFrissítés();
        }
        private void Btn_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow Sor in Tábla.SelectedRows)
                    {
                        Color HatterSzin = Sor.DefaultCellStyle.BackColor;
                        TervDatum = DtmPckrElőTerv.Value.Date;

                        if (!DátumEllenőrzés(MaiDatum, TervDatum))
                            return;
                        if (HatterSzin == Color.LawnGreen)
                        {
                            MessageBox.Show("Ez a sor nem módosítható, mert már a művelet elkészült vagy nem kell még végrehajtani.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        int Id = Sor.Cells[0].Value.ToÉrt_Int();
                        long AktivUzemora = 0;
                        if (AdatokÜzemóra.Count > 0) AktivUzemora = AdatokÜzemóra.Max(a => a.Üzemóra);

                        Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(MaiDatum,
                                                              AktivUzemora,
                                                              Id);

                        Kéz_Művelet.Módosítás(ADAT);
                        Kéz_Művelet.Törlés(ADAT, false);
                        Sor.Cells[4].Value = MaiDatum;
                        Sor.Cells[5].Value = AktivUzemora;

                        Naplózás(Sor, MaiDatum, AktivUzemora);
                    }
                    TáblaListázás();

                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Válasszon ki egy vagy több sort a táblázatból.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Teljes tartalom mentése Excel fájlba",
                    FileName = $"Eszterga_Karbantartás_Műveletek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                MyE.EXCELtábla(fájlexc, Tábla, false, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás($"{fájlexc}.xlsx");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Bttn_Napló_Listáz_Click(object sender, EventArgs e)
        {
            try
            {
                TáblaNaplóListázás();
                Btn_Rögzít.Visible = false;
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
        private void TxtBxNapiUzemoraAtlag_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtBxNapiUzemoraAtlag.Text, out int napok) && napok > 0)
                ÁtlagÜzemóraFrissítés(napok);
        }
        private void TxtBxNapi_TextChanged(object sender, EventArgs e)
        {
            if (MaiDatum == DtmPckrElőTerv.Value)
                TáblaListázás();
            else
                EloreTervezesListazasa();
        }
        private void TxtBxÜzem_TextChanged(object sender, EventArgs e)
        {
            if (MaiDatum == DtmPckrElőTerv.Value)
                TáblaListázás();
            else
                EloreTervezesListazasa();
        }
        private void DtmPckrElőTerv_ValueChanged(object sender, EventArgs e)
        {
            if (DtmPckrElőTerv.Value >= MaiDatum)
            {
                if (DtmPckrElőTerv.Value > MaiDatum)
                    EloreTervezesListazasa();
                else
                    TáblaListázás();
                Btn_Rögzít.Visible = true;
            }
            else
            {
                MessageBox.Show("A dátum nem lehet kisebb, mint a mai dátum.", "Érvénytelen dátum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DtmPckrElőTerv.Value = MaiDatum;
            }
        }
        private void Tábla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow sor = Tábla.Rows[e.RowIndex];

                if (!Baross || !DátumEllenőrzés(MaiDatum, TervDatum))
                {
                    sor.Cells[10].Value = null;
                    Tábla.InvalidateRow(e.RowIndex);
                    return;
                }

                Tábla.EndEdit();

                string Megjegyzes = sor.Cells[10].Value?.ToStrTrim();
                int ID = sor.Cells[0].Value.ToÉrt_Int();

                string ElozoMegjegyzes = (from rekord in Funkció.Eszterga_KarbantartasFeltölt()
                                          where rekord.ID == ID
                                          select rekord.Megjegyzés)?.FirstOrDefault()?.ToStrTrim();

                if (ElozoMegjegyzes == Megjegyzes)
                {
                    MessageBox.Show("Nem történt változás a megjegyzés változtatásakor.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!string.IsNullOrEmpty(Megjegyzes))
                {
                    Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(Megjegyzes, ID);
                    Kéz_Művelet.Megjegyzés_Módosítás(ADAT);
                    MessageBox.Show("A megjegyzés mentésre került.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(ID);
                    Kéz_Művelet.Törlés(ADAT, false);
                    MessageBox.Show("A megjegyzés törlésre került.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Tábla.InvalidateRow(e.RowIndex);
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
        private void Tábla_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            Tábla.DataBindingComplete += (s, ev) => SorSzinezes();
            Tábla.Refresh();
        }
        private void Tábla_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            Tábla.DataBindingComplete += (s, ev) => SorSzinezes();
            Tábla.Refresh();
        }
        #endregion
    }
}