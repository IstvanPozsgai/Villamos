using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyCaf = Villamos.Villamos_Ablakok.CAF_Ütemezés.CAF_Közös_Eljárások;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Segéd : Form
    {
        public event Event_Kidobó Változás;
        public CAF_Segéd_Adat Adat { get; private set; }
        public DateTime Dátumig { get; private set; }


        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        List<Adat_CAF_Adatok> Adatok = new List<Adat_CAF_Adatok>();

        public Ablak_CAF_Segéd(CAF_Segéd_Adat adat, DateTime dátumig)
        {
            InitializeComponent();
            Adat = adat;
            Dátumig = dátumig;
            Start();
        }

        private void Start()
        {
            Jogosultságkiosztás();

            if (Adat != null)
            {
                Segéd_pályaszám.Text = Adat.Azonosító.Trim();
                Segéd_dátum.Value = Adat.Dátum;

                if (Adat.Sorszám == 0)
                {
                    // ha nincs ütemezett akkor lehet, akarunk oda tenni valamit

                    // Ilyenkor nem lehet átütemezni, törölni, beütemezni
                    Segéd_átütemez.Visible = false;
                    Segéd_ütemez.Visible = false;
                    Segéd_Töröl.Visible = false;

                    if (Program.PostásTelephely.Trim() == "Főmérnökség")
                        Segéd_Pót_Rögzít.Visible = true;
                    else
                        Segéd_Pót_Rögzít.Visible = false;

                }
                else
                {

                    // ha van benne valami
                    if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    {
                        Segéd_átütemez.Visible = true;
                        Segéd_ütemez.Visible = true;
                        Segéd_Töröl.Visible = true;
                        Segéd_Pót_Rögzít.Visible = true;
                    }
                    else
                    {
                        Segéd_átütemez.Visible = false;
                        Segéd_ütemez.Visible = false;
                        Segéd_Töröl.Visible = false;
                        Segéd_Pót_Rögzít.Visible = false;
                    }

                }
            }
        }

        private void Ablak_CAF_Segéd_Load(object sender, EventArgs e)
        {
            try
            {
                if (Adat != null) Kiír();
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

        private void Kiír()
        {
            try
            {
                Segéd_dátum.Text = Adat.Dátum.ToString("yyyy.MM.dd");
                Segéd_pályaszám.Text = Adat.Azonosító.Trim();
                Adatok = KézAdatok.Lista_Adatok();
                Adat_CAF_Adatok rekord;

                if (Adat.Sorszám != 0)
                    rekord = (from a in Adatok
                              where a.Id == Adat.Sorszám
                              select a).FirstOrDefault();
                else
                    rekord = (from a in Adatok
                              where a.Azonosító == Adat.Azonosító
                              && a.Dátum.ToShortDateString() == Adat.Dátum.ToShortDateString()
                              && a.Státus < 9
                              select a).FirstOrDefault();

                if (rekord != null)
                {
                    Segéd_sorszám.Text = rekord.Id.ToString();
                    Segéd_Vizsg.Text = rekord.Vizsgálat.Trim();
                    Segéd_darab.Text = "1";
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false

                Segéd_átütemez.Enabled = false;
                Segéd_ütemez.Enabled = false;
                Segéd_Töröl.Enabled = false;
                Segéd_Pót_Rögzít.Enabled = false;

                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Segéd_átütemez.Visible = true;
                    Segéd_ütemez.Visible = true;
                    Segéd_Töröl.Visible = true;
                    Segéd_Pót_Rögzít.Visible = true;

                }
                else
                {
                    Segéd_átütemez.Visible = false;
                    Segéd_ütemez.Visible = false;
                    Segéd_Töröl.Visible = false;
                    Segéd_Pót_Rögzít.Visible = false;

                }

                melyikelem = 116;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {

                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {

                }
                melyikelem = 117;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {

                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                    Segéd_átütemez.Enabled = true;
                    Segéd_ütemez.Enabled = true;
                    Segéd_Töröl.Enabled = true;
                    Segéd_Pót_Rögzít.Enabled = true;
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

        private void Segéd_Pót_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Segéd_Vizsg.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat neve nem lehet üres.");
                if (Segéd_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");

                if (!int.TryParse(Segéd_darab.Text, out int Darab)) throw new HibásBevittAdat("A darab mező nem lehet üres és pozitív egész számnak kell lennie.");
                if (Darab <= 0) throw new HibásBevittAdat("A darab mező nem lehet nullánál kisebb.");

                for (int i = 0; i < Darab; i++)
                {
                    DateTime újnap = Segéd_dátum.Value.AddDays(i);

                    // következő sorszám
                    double Segéd_Sorszám = KézAdatok.Sorszám();
                    Segéd_sorszám.Text = Segéd_Sorszám.ToString();

                    Adat_CAF_Adatok rekord = new Adat_CAF_Adatok(
                        0,
                        Segéd_pályaszám.Text.Trim(),
                        Segéd_Vizsg.Text.Trim(),
                        újnap,
                        new DateTime(1900, 1, 1), 0, 8, 0, 0, 0,
                        "Ütemezési Segéd");
                    KézAdatok.Döntés(rekord);
                }

                Változás?.Invoke();
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

        private void Segéd_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(Segéd_sorszám.Text.Trim(), out double sorszám)) throw new HibásBevittAdat("Nincs törlendő elem.");
                KézAdatok.Módosítás_Státus(sorszám, 9);
                Változás?.Invoke();
                MessageBox.Show("Az adatok törlés befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Segéd_ütemez_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(Segéd_sorszám.Text.Trim(), out double sorszám)) throw new HibásBevittAdat("Az elemet nem lehet ütemezni.");
                KézAdatok.Módosítás_Státus(sorszám, 2);
                Változás?.Invoke();
                MessageBox.Show("Az kocsi ütemezése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Segéd_átütemez_Click(object sender, EventArgs e)
        {
            try
            {
                if (Segéd_sorszám.Text.Trim() == "") throw new HibásBevittAdat("Az elemet nem lehet ütemezni.");
                if (!double.TryParse(Segéd_sorszám.Text.Trim(), out double Sorszám)) throw new HibásBevittAdat("Az elemet nem lehet ütemezni.");

                Adatok = KézAdatok.Lista_Adatok();

                Adat_CAF_Adatok rekord = (from a in Adatok
                                          where a.Id == Sorszám
                                          select a).FirstOrDefault();

                switch (rekord.IDŐvKM)
                {
                    case 1:
                        Idő_átütemezés(rekord, Segéd_dátum.Value);
                        break;
                    case 2:
                        Km_átütemezés(rekord, Segéd_dátum.Value);
                        break;
                }

                Változás?.Invoke();
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

        private void Idő_átütemezés(Adat_CAF_Adatok Adat, DateTime Dátumra)
        {
            try
            {

                // ha nem raktuk át másik napra akkor kilépünk
                if (Adat.Dátum == Dátumra) throw new HibásBevittAdat("Nem történt meg az átütemezés");

                if (Adat != null)
                {

                    // rögzítjük az új dátumra az adatot
                    // újat hoz létre
                    Adat_CAF_Adatok Új_adat = new Adat_CAF_Adatok(
                        Adat.Id,
                        Adat.Azonosító,
                        Adat.Vizsgálat,
                        Dátumra,
                        Adat.Dátum_program,
                        Adat.Számláló,
                        Adat.Státus,
                        Adat.KM_Sorszám,
                        Adat.IDŐ_Sorszám,
                        Adat.IDŐvKM,
                        "Átütemezés");
                    KézAdatok.Döntés(Új_adat);

                    // töröljük az új dátum utáni tervet
                    List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                    Adat_CAF_Adatok Elem = (from a in Adatok
                                            where a.Azonosító == Adat.Azonosító.Trim()
                                            && a.Dátum > Dátumra
                                            && a.Státus == 0
                                            select a).FirstOrDefault();
                    if (Elem != null) KézAdatok.Törlés(Dátumra.AddDays(1), Adat.Azonosító.Trim());

                    // ütemezzük újra a kocsikat
                    // idő szerit
                    List<Adat_CAF_Adatok> IdőAdatok = MyCaf.IDŐ_EgyKocsi(Adat.Azonosító.Trim(), Dátumig, Dátumra);

                    // km szerint
                    List<Adat_CAF_Adatok> KMAdatok = MyCaf.KM_EgyKocsi(Adat.Azonosító.Trim(), Dátumig);
                    IdőAdatok.AddRange(KMAdatok);
                    KézAdatok.Rögzítés(IdőAdatok);
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

        private void Km_átütemezés(Adat_CAF_Adatok Adat, DateTime Dátumra)
        {
            try
            {
                Adatok = KézAdatok.Lista_Adatok();
                if (Adat.Dátum_program != Dátumra)
                {
                    if (Adat != null)
                    {
                        // töröltre állítjuk az aktuális sorszámot
                        KézAdatok.Módosítás_Státus(Adat.Id, 9);

                        // ezen a napon ha van már idő alapú akkor töröljük
                        Adat_CAF_Adatok rekord = (from a in Adatok
                                                  where a.Dátum >= Dátumra
                                                  && a.Azonosító == Adat.Azonosító.Trim()
                                                  && a.Státus == 0
                                                  select a).FirstOrDefault();
                        if (rekord != null) KézAdatok.Törlés(Dátumra, Adat.Azonosító.Trim());

                        // rögzítjük az új dátumra az adatot
                        Adat_CAF_Adatok Új_adat = new Adat_CAF_Adatok(
                          Adat.Id,
                          Adat.Azonosító,
                          Adat.Vizsgálat,
                          Dátumra,
                          Adat.Dátum_program,
                          Adat.Számláló,
                          0,
                          Adat.KM_Sorszám,
                          0,
                          Adat.IDŐvKM,
                          "Átütemezés");
                        KézAdatok.Döntés(Új_adat);
                    }
                }
                // ütemezzük újra a kocsikat
                // idő szerit
                List<Adat_CAF_Adatok> IdőAdatok = MyCaf.IDŐ_EgyKocsi(Adat.Azonosító.Trim(), Dátumig, new DateTime(1900, 1, 1));

                // km szerint
                List<Adat_CAF_Adatok> KMAdatok = MyCaf.KM_EgyKocsi(Adat.Azonosító.Trim(), Dátumig);
                IdőAdatok.AddRange(KMAdatok);
                KézAdatok.Rögzítés(IdőAdatok);
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
