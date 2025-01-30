using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás
{
    public partial class Ablak_Opció : Form
    {
        public DateTime Dátum { get; private set; }
        public bool Irány { get; private set; }
        public string Telephely { get; private set; }

        readonly Kezelő_Takarítás_Opció KézOpció = new Kezelő_Takarítás_Opció();
        readonly Kezelő_Takarítás_Telep_Opció KézTelep = new Kezelő_Takarítás_Telep_Opció();

        List<Adat_Takarítás_Opció> AdatokTakOpció = new List<Adat_Takarítás_Opció>();
        List<Adat_Takarítás_Telep_Opció> AdatokTakTelepOpció = new List<Adat_Takarítás_Telep_Opció>();

        DataTable AdatTábla = new DataTable();

        public Ablak_Opció(DateTime dátum, bool irány, string telephely)
        {
            Dátum = dátum;
            Irány = irány;
            InitializeComponent();
            Telephely = telephely;
        }


        private void Ablak_Opció_Load(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\Opcionális{Dátum.Year}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ÉpülettakarításTelepOpcionálisLétrehozás(hely);

            if (Irány)
            {
                this.Text = "Opciós tételek tény adatainak rögzítése";
                DátumMező.Text = $"{Dátum.Year} év {Dátum:MMMM} havi opciós tételek TÉNY adatainak rögzítése";
            }
            else
            {
                this.Text = "Opciós tételek megrendelésének rögzítése";
                DátumMező.Text = $"{Dátum.Year} év {Dátum:MMMM} havi opciós tételek megrendelésének rögzítése";
            }
        }

        private void Frissítés_Click(object sender, EventArgs e)
        {
            Táblaírás();
        }

        private void Táblaírás()
        {
            OpcióListaFeltöltés();
            OpcióListaTelepFeltöltés();
            OpcióTáblaListázás();
            if (Irány)
                OpcióTényZárolás();
            else
                OpcióMegZárolás();
        }

        private void OpcióMegZárolás()
        {
            Opció_Tábla.Columns["Sorszám"].ReadOnly = true;
            Opció_Tábla.Columns["Megnevezés"].ReadOnly = true;
            Opció_Tábla.Columns["Mennyisége"].ReadOnly = true;
            Opció_Tábla.Columns["Megrendelt"].ReadOnly = false;
            Opció_Tábla.Columns["Teljesített"].ReadOnly = true;
        }

        private void OpcióTényZárolás()
        {
            Opció_Tábla.Columns["Sorszám"].ReadOnly = true;
            Opció_Tábla.Columns["Megnevezés"].ReadOnly = true;
            Opció_Tábla.Columns["Mennyisége"].ReadOnly = true;
            Opció_Tábla.Columns["Megrendelt"].ReadOnly = true;
            Opció_Tábla.Columns["Teljesített"].ReadOnly = false;
        }

        #region Megrendelt -Tény
        private void OpcióTáblaListázás()
        {
            try
            {
                AdatTábla.Clear();
                ABFejlécMeg();
                ABFeltöltéseMeg();
                Opció_Tábla.DataSource = AdatTábla;
                ABOszlopSzélességMeg();
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

        private void ABFejlécMeg()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám", typeof(int));
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Mennyisége");
                AdatTábla.Columns.Add("Megrendelt", typeof(double));
                AdatTábla.Columns.Add("Teljesített", typeof(double));
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

        private void ABFeltöltéseMeg()
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Takarítás_Opció rekord in AdatokTakOpció)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Sorszám"] = rekord.Id;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Mennyisége"] = rekord.Mennyisége;
                    Adat_Takarítás_Telep_Opció Elem = AdatokTakTelepOpció.Where(a => a.Id == rekord.Id && a.Dátum ==new DateTime  (Dátum.Year , Dátum.Month,1)  ).FirstOrDefault();
                    if (Elem == null)
                    {
                        Soradat["Megrendelt"] = 0;
                        Soradat["Teljesített"] = 0;
                    }
                    else
                    {
                        Soradat["Megrendelt"] = Elem.Megrendelt;
                        Soradat["Teljesített"] = Elem.Teljesített;
                    }
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
        }

        private void ABOszlopSzélességMeg()
        {
            Opció_Tábla.Columns["Sorszám"].Width = 100;
            Opció_Tábla.Columns["Megnevezés"].Width = 400;
            Opció_Tábla.Columns["Mennyisége"].Width = 150;
            Opció_Tábla.Columns["Megrendelt"].Width = 150;
            Opció_Tábla.Columns["Teljesített"].Width = 150;
        }
        #endregion

        #region Listák
        private void OpcióListaFeltöltés()
        {
            try
            {
                AdatokTakOpció.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Opcionális.mdb";
                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM TakarításOpcionális ORDER BY ID";
                AdatokTakOpció = KézOpció.Lista_Adatok(hely, jelszó, szöveg);
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

        private void OpcióListaTelepFeltöltés()
        {
            try
            {
                AdatokTakTelepOpció.Clear();
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\Opcionális{Dátum.Year}.mdb";
                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM TakarításOpcTelepAdatok";
                AdatokTakTelepOpció = KézTelep.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_Tábla.Rows.Count <= 0) return;
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\Opcionális{Dátum.Year}.mdb";
                string jelszó = "seprűéslapát";
                List<Adat_Takarítás_Telep_Opció> AdatokMód = new List<Adat_Takarítás_Telep_Opció>();
                List<Adat_Takarítás_Telep_Opció> AdatokRögz = new List<Adat_Takarítás_Telep_Opció>();

                foreach (DataGridViewRow Sor in Opció_Tábla.Rows)
                {
                    Adat_Takarítás_Telep_Opció Elem = new Adat_Takarítás_Telep_Opció(
                        Sor.Cells["Sorszám"].Value.ToÉrt_Int(),
                        new DateTime(Dátum.Year, Dátum.Month, 1),
                        Sor.Cells["Megrendelt"].Value.ToÉrt_Double(),
                        Sor.Cells["Teljesített"].Value.ToÉrt_Double());
                    Adat_Takarítás_Telep_Opció ElemVolt = AdatokTakTelepOpció.Where(a => a.Id == Sor.Cells["Sorszám"].Value .ToÉrt_Int() && a.Dátum ==new DateTime (Dátum.Year,Dátum.Month ,1)).FirstOrDefault();
                    if (ElemVolt != null)
                        AdatokMód.Add(Elem);
                    else
                        AdatokRögz.Add(Elem);
                }
                if (AdatokMód.Count > 0)
                {
                    KézTelep.Módosít(hely, jelszó, AdatokMód);
                    MessageBox.Show("Az Módosítás megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (AdatokRögz.Count > 0)
                {
                    KézTelep.Rögzít(hely, jelszó, AdatokRögz);
                    MessageBox.Show("Az Rögzítés megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Táblaírás();
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
    }
}
