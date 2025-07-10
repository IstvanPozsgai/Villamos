using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Eszterga_Segéd : Form
    {
        public DateTime DátumésIdő { get; private set; }
        public int Mód { get; private set; }
        long betűSzín = 0;
        long háttérSzín = 12632256;
        public event Event_Kidobó Változás;
        Szín_kódolás Szín;
        DateTime Hételső;

        int Év = 1900;
        string telephely = "";
        string Választott = "";

        readonly Kezelő_Kerék_Eszterga_Naptár KézNaptár = new Kezelő_Kerék_Eszterga_Naptár();
        readonly Kezelő_Kerék_Eszterga_Tevékenység KézTevékenység = new Kezelő_Kerék_Eszterga_Tevékenység();
        readonly Kezelő_Kerék_Eszterga_Tengely kézTengely = new Kezelő_Kerék_Eszterga_Tengely();
        readonly Kezelő_Kerék_Eszterga_Igény KézIgény = new Kezelő_Kerék_Eszterga_Igény();

        List<Adat_Kerék_Eszterga_Naptár> Naptár_Adatok;
        List<Adat_Kerék_Eszterga_Naptár> Naptár_Adatok_ideig;
        List<Adat_Kerék_Eszterga_Igény> AdatokIgény = new List<Adat_Kerék_Eszterga_Igény>();

        int elem;
        int elemszám;

        public Ablak_Eszterga_Segéd(DateTime dátumésidő, int mód)
        {
            InitializeComponent();
            DátumésIdő = dátumésidő;
            Mód = mód;
            Start();
        }

        private void Start()
        {
            Tevékenység_feltöltés();
            Marad.Checked = false;
            Hételső = MyF.Hét_elsőnapja(DátumésIdő);
            switch (Mód)
            {
                case 0:
                    this.Text = "Rögzítés/ Törlés";
                    break;
                case 1:
                    this.Text = "Beszúrás csúsztatással";
                    break;
                case 2:
                    this.Text = "Törlés csúsztatással";
                    break;
                case 3:
                    this.Text = "Munkaközi Szünet";
                    break;
            }

            IgénylistaFeltötlés();
            Jogosultságkiosztás();
            Text_Dátum.Text = DátumésIdő.ToString("yyyy.MM.dd");
            Text_Idő.Text = DátumésIdő.ToString("HH:mm");
            Kiírás();
            Igény_Típus_Feltöltés();
            Tábla_Író();
            Automata();
        }


        private void Ablak_Eszterga_Segéd_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }

        private void Ablak_Eszterga_Segéd_Load(object sender, EventArgs e)
        {
        }

        private void Automata()
        {
            if (Mód == 3) Munkaközi_Szünet();

        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Terv_Rögzít.Enabled = false;
            Töröl.Enabled = false;

            if (Program.PostásTelephely.Trim() == "Baross")
            {
                Terv_Rögzít.Visible = true;
                Töröl.Visible = true;
            }
            else
            {
                Terv_Rögzít.Visible = true;
                Töröl.Visible = true;
            }

            melyikelem = 165;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Terv_Rögzít.Enabled = true;
                Töröl.Enabled = true;
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

        private void Tevékenység_feltöltés()
        {
            try
            {
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = KézTevékenység.Lista_Adatok();
                Tevékenység.Items.Clear();
                foreach (Adat_Kerék_Eszterga_Tevékenység Elem in Adatok)
                    Tevékenység.Items.Add(Elem.Tevékenység);
                Tevékenység.Refresh();
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

        private void Terv_Rögzít_Click(object sender, EventArgs e)
        {
            switch (Mód)
            {
                case 0:
                    //Rögzítés
                    Rögzít();
                    break;

                case 1:
                    //Beszúrás
                    Beszúrásos_Csúsztatás();
                    break;

                case 3:
                    //Munkaközi_Szünet
                    Munkaközi_Szünet();
                    break;
            }
        }

        private void Rögzít()
        {
            try
            {
                if (Tevékenység_Vál.Text.Trim() == "") throw new HibásBevittAdat("Tevékenység mezőt ki kell tölteni.");
                if (Norma_Idő.Text.Trim() == "" || !int.TryParse(Norma_Idő.Text, out int NormaIdő)) throw new HibásBevittAdat("Az időszükséglet mező nem lehet üres és pozítív egész számnak kell lennie.");
                if (NormaIdő < 1) throw new HibásBevittAdat("Az időszükséglet mezőnek pozítív egész számnak kell lennie.");

                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(Hételső.Year);
                Adatok = (from a in Adatok
                          where a.Idő >= DátumésIdő
                          orderby a.Idő
                          select a).ToList();
                Holtart.Be();

                Adatok_Rögzítése(NormaIdő);
                Igény_Módosítás();

                Változás?.Invoke();
                this.Close();
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Igény_Módosítás()
        {
            try
            {
                AdatokIgény = KézIgény.Lista_Adatok(Év);
                Adat_Kerék_Eszterga_Igény Elem = (from a in AdatokIgény
                                                  where a.Telephely == telephely.Trim()
                                                  && a.Pályaszám == Választott.Trim()
                                                  && a.Státus <= 2
                                                  select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Kerék_Eszterga_Igény ADAT = new Adat_Kerék_Eszterga_Igény(
                          Választott.Trim(),
                          DátumésIdő,
                          2, // Státusz módosítva 2-re
                          telephely.Trim());
                    KézIgény.Módosítás(Év, ADAT);
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

        private void Igény_Törlés()
        {
            try
            {
                string[] elem = Tevékenység_Vál.Text.Trim().Split('=');
                if (elem.Length < 2) return;

                AdatokIgény = KézIgény.Lista_Adatok(Év);
                Adat_Kerék_Eszterga_Igény Elem = (from a in AdatokIgény
                                                  where a.Telephely == elem[1].Trim()
                                                  && a.Pályaszám == elem[0].Trim()
                                                  && a.Státus == 2
                                                  select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Kerék_Eszterga_Igény ADAT = new Adat_Kerék_Eszterga_Igény(
                             elem[0].Trim(),
                             new DateTime(1900, 1, 1),
                             0, // Státusz módosítva 0-re
                             elem[1].Trim());
                    KézIgény.Módosítás(Elem.Rögzítés_dátum.Year, ADAT, true);
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

        private void Beszúrásos_Csúsztatás()
        {
            try
            {
                if (Megjegyzés.Text.Trim() == "" || Megjegyzés.Text.Trim() == "_")
                    throw new HibásBevittAdat("Megjegyzés mezőt ki kell tölteni.");
                if (Megjegyzés.Text.Trim().Length < 7)
                    throw new HibásBevittAdat("Megjegyzés mezőnek legalább 7 db kell tartalmaznia.");
                if (Tevékenység_Vál.Text.Trim() == "")
                    throw new HibásBevittAdat("Tevékenység mezőt ki kell tölteni.");
                if (Norma_Idő.Text.Trim() == "" || !int.TryParse(Norma_Idő.Text, out int NormaIdő))
                    throw new HibásBevittAdat("Az időszükséglet mező nem lehet üres és pozítív egész számnak kell lennie.");
                if (NormaIdő < 1)
                    throw new HibásBevittAdat("Az időszükséglet mezőnek pozítív egész számnak kell lennie.");

                Holtart.Be();


                Alapra_állítás();
                //beszúrandó adatok
                Adatok_Rögzítése(NormaIdő);

                Adatok_VisszaÍrása(0);
                Igény_Módosítás();

                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Változás?.Invoke();
                this.Close();
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

        private void Alapra_állítás()
        {
            try
            {
                Holtart.Be();
                Naptár_Adatok = KézNaptár.Lista_Adatok(DátumésIdő.Year);
                Naptár_Adatok = (from a in Naptár_Adatok
                                 where a.Idő >= DátumésIdő
                                 && a.Pályaszám != "_"
                                 orderby a.Idő
                                 select a).ToList();
                //Ami nem mozog csak az marad benne a többit visszaállítjuk alapra
                Naptár_Adatok[0].Marad = false;
                List<DateTime> Idők = new List<DateTime>();
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Naptár_Adatok)
                {
                    if (!rekord.Marad) Idők.Add(rekord.Idő);
                    Holtart.Lép();
                }
                KézNaptár.Módosítás_Idő(DátumésIdő.Year, Idők);
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

        private void Adatok_Rögzítése(int NormaIdő)
        {
            try
            {
                Holtart.Be();
                Naptár_Adatok_ideig = KézNaptár.Lista_Adatok(DátumésIdő.Year);
                Naptár_Adatok_ideig = (from a in Naptár_Adatok_ideig
                                       where a.Idő >= DátumésIdő
                                       && a.Munkaidő == true
                                       orderby a.Idő
                                       select a).ToList();

                int Futóidő = 0;
                //Amit be akarunk szúrni azt beszúrjuk
                List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Naptár_Adatok_ideig)
                {
                    if (!rekord.Foglalt && rekord.Munkaidő)
                    {
                        Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(
                                        rekord.Idő,
                                        true,
                                        Tevékenység_Vál.Text.Trim(),
                                        MyF.Szöveg_Tisztítás(Megjegyzés.Text, 0, -1, true),
                                        betűSzín,
                                        háttérSzín,
                                        Marad.Checked);
                        AdatokGy.Add(ADAT);

                        Státus_állítás(0, 2);
                        DátumésIdő = rekord.Idő;
                        Futóidő += 30;
                    }
                    // emeljük az időt addig amíg ...

                    if (Futóidő >= NormaIdő) break;
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézNaptár.Módosítás(DátumésIdő.Year, AdatokGy);
                if (Futóidő <= NormaIdő) Maradék_beírása_Rögzítés(Futóidő, NormaIdő);
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

        private void Maradék_beírása_Rögzítés(int Futóidő, int NormaIdő)
        {
            try
            {
                Holtart.Be();
                Futóidő -= 30;
                //   Ha még van rögzített, de már nincs munkaidővel lefedett akkor azt is rögzítjük
                List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                while (Futóidő < NormaIdő)
                {
                    Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(
                            DátumésIdő,
                            true,
                            Tevékenység_Vál.Text.Trim(),
                            MyF.Szöveg_Tisztítás(Megjegyzés.Text, 0, -1, true),
                            betűSzín,
                            háttérSzín,
                            Marad.Checked);

                    Státus_állítás(0, 2);
                    AdatokGy.Add(ADAT);

                    DátumésIdő = DátumésIdő.AddMinutes(30);
                    Futóidő += 30;

                    Holtart.Lép();
                }
                KézNaptár.Módosítás(DátumésIdő.Year, AdatokGy);
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

        private void Maradék_beírása()
        {
            try
            {
                Holtart.Be();
                //   Ha még van rögzített, de már nincs munkaidővel lefedett akkor azt is rögzítjük
                elem--;
                List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                while (elem <= elemszám)
                {
                    //ha helyben maradó volt akkor tovább lapozzuk
                    while (Naptár_Adatok[elem].Marad)
                        elem++;
                    //A maradó tételek rögzítve vannak
                    if (!Naptár_Adatok[elem].Marad)
                    {
                        //elmentjük az első elemet
                        Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(
                            DátumésIdő,
                            Naptár_Adatok[elem].Foglalt,
                            Naptár_Adatok[elem].Pályaszám.Trim(),
                            Naptár_Adatok[elem].Megjegyzés.Trim(),
                            Naptár_Adatok[elem].BetűSzín,
                            Naptár_Adatok[elem].HáttérSzín,
                            Naptár_Adatok[elem].Marad);
                        AdatokGy.Add(ADAT);

                        elem++;
                        DátumésIdő = DátumésIdő.AddMinutes(30);
                    }
                    Holtart.Lép();
                }
                KézNaptár.Módosítás(DátumésIdő.Year, AdatokGy);
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

        private void Adatok_VisszaÍrása(int kimarad)
        {
            try
            {
                Holtart.Be();
                //Az eredeti adatokat a folytatólagosan tesszük a javított lista alapján
                Naptár_Adatok_ideig = KézNaptár.Lista_Adatok(DátumésIdő.Year);
                Naptár_Adatok_ideig = (from a in Naptár_Adatok_ideig
                                       where a.Idő >= DátumésIdő
                                       && a.Munkaidő
                                       orderby a.Idő
                                       select a).ToList();
                elemszám = Naptár_Adatok.Count - 1;
                elem = kimarad;
                List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Naptár_Adatok_ideig)
                {
                    if (!rekord.Foglalt && rekord.Munkaidő)
                    {
                        //ha helyben maradó volt akkor tovább lapozzuk
                        bool kilép = false;

                        while (Naptár_Adatok[elem].Marad)
                        {
                            elem++;
                            if (elem > elemszám)
                            {
                                kilép = true;
                                break;

                            }
                        }
                        if (kilép) break;
                        //A maradó tételek rögzítve vannak
                        if (!Naptár_Adatok[elem].Marad)
                        {
                            //elmentjük az első elemet
                            Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(
                                  rekord.Idő,
                                  Naptár_Adatok[elem].Foglalt,
                                  Naptár_Adatok[elem].Pályaszám.Trim(),
                                  Naptár_Adatok[elem].Megjegyzés.Trim(),
                                  Naptár_Adatok[elem].BetűSzín,
                                  Naptár_Adatok[elem].HáttérSzín,
                                  Naptár_Adatok[elem].Marad);
                            AdatokGy.Add(ADAT);
                            elem++;
                            DátumésIdő = rekord.Idő;
                        }
                        if (elem > elemszám) break;
                    }
                    Holtart.Lép();
                }
                KézNaptár.Módosítás(DátumésIdő.Year, AdatokGy);
                if (elem <= elemszám) Maradék_beírása();
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

        private void Törléses_Csúsztatás()
        {
            try
            {
                if (Norma_Idő.Text.Trim() == "" || !int.TryParse(Norma_Idő.Text, out int NormaIdő)) throw new HibásBevittAdat("Az időszükséglet mező nem lehet üres és pozítív egész számnak kell lennie.");
                if (NormaIdő < 1) throw new HibásBevittAdat("Az időszükséglet mezőnek pozítív egész számnak kell lennie.");

                //Ami nem mozog csak az marad benne a többit visszaállítjuk alapra
                Alapra_állítás();

                //Ha a kiválasztott elem helyben maradós, akkor visszaállítjuk
                Naptár_Adatok[0].Marad = false;
                int i = 0;
                int ideigint = 0;
                while (ideigint < NormaIdő)
                {
                    i++;
                    ideigint += 30;
                }


                Holtart.Be();

                //Az eredeti adatokat a folytatólagosan tesszük a javított lista alapján
                Adatok_VisszaÍrása(i);

                MessageBox.Show("Az adatok törlésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Változás?.Invoke();
                this.Close();
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

        private void Munkaközi_Szünet()
        {
            try
            {
                Tevékenység_Vál.Text = "Munkaközi szünet";
                Tevékenységválasztás();
                if (Norma_Idő.Text.Trim() == "" || !int.TryParse(Norma_Idő.Text, out int NormaIdő))
                    throw new HibásBevittAdat("Az időszükséglet mező nem lehet üres és pozítív egész számnak kell lennie.");
                if (NormaIdő < 1)
                    throw new HibásBevittAdat("Az időszükséglet mezőnek pozítív egész számnak kell lennie.");

                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(DátumésIdő.Year);
                Adatok = (from a in Adatok
                          where a.Idő >= DátumésIdő
                          && a.Idő <= DátumésIdő.AddDays(3)
                          orderby a.Idő
                          select a).ToList();

                int Futóidő = 0;

                List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Adatok)
                {
                    if (!rekord.Foglalt && rekord.Munkaidő)
                    {
                        Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(
                                 DátumésIdő,
                                 true,
                                 Tevékenység_Vál.Text.Trim(),
                                 Megjegyzés.Text.Trim(),
                                 betűSzín,
                                 háttérSzín,
                                 Marad.Checked);
                        AdatokGy.Add(ADAT);

                        Státus_állítás(0, 2);

                        Futóidő += 30;
                    }
                    // emeljük az időt addig amíg ...
                    DátumésIdő = DátumésIdő.AddMinutes(30);
                    if (Futóidő >= NormaIdő) break;
                }
                KézNaptár.Módosítás(DátumésIdő.Year, AdatokGy);
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Változás?.Invoke();
                this.Close();
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

        private void Státus_állítás(int Státus_Volt, int Státus_Lesz)
        {
            try
            {
                string[] darabol = Tevékenység_Vál.Text.Split('=');
                KézIgény.Módosítás_Státus(DátumésIdő.Year, darabol[0].Trim(), Státus_Volt, Státus_Lesz);
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

        private void Tevékenység_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tevékenység_Vál.Text = Tevékenység.Text.Trim();
            if (Tevékenység.Text.Trim() == "Esztergálás I" || Tevékenység.Text.Trim() == "Esztergálás II")
            {
                Tevékenység_Vál.Enabled = false;
                Norma_Idő.Enabled = false;
            }
            else
            {
                Tevékenység_Vál.Enabled = true;
                Norma_Idő.Enabled = true;
            }

            Tevékenységválasztás();
        }

        private void Tevékenységválasztás()
        {
            try
            {
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = KézTevékenység.Lista_Adatok();
                Adat_Kerék_Eszterga_Tevékenység Adat = (from a in Adatok
                                                        where a.Tevékenység == Tevékenység_Vál.Text.Trim()
                                                        select a).FirstOrDefault();
                if (Adat != null)
                {
                    betűSzín = Adat.Betűszín;
                    háttérSzín = Adat.Háttérszín;
                    Norma_Idő.Text = Adat.Munkaidő.ToString();
                    Szín = MyColor.Szín_váltó(Adat.Háttérszín);
                    Marad.Checked = Adat.Marad;
                    this.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
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

        private void Töröl_Click(object sender, EventArgs e)
        {
            Igény_Törlés();
            if (Mód == 2)
                Törléses_Csúsztatás();
            else
                Töröl_esemény();
        }

        private void Töröl_esemény()
        {
            try
            {
                DateTime Hételső = MyF.Hét_elsőnapja(DátumésIdő);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(DátumésIdő);
                Naptár_Adatok?.Clear();

                Naptár_Adatok = KézNaptár.Lista_Adatok(DátumésIdő.Year);
                Adat_Kerék_Eszterga_Naptár Elem;

                if (!Egy_adat.Checked)
                {
                    Elem = (from a in Naptár_Adatok
                            where a.Idő >= DátumésIdő
                            && a.Pályaszám == Tevékenység_Vál.Text.Trim()
                            select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        KézNaptár.Módosítás_Státus(DátumésIdő.Year, Hételső, Tevékenység_Vál.Text.Trim(), false);
                        Státus_állítás(2, 0);

                        MessageBox.Show("Az adatok törlésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Változás?.Invoke();
                        this.Close();
                    }
                }
                else
                {
                    Elem = (from a in Naptár_Adatok
                            where a.Idő == DátumésIdő
                            && a.Pályaszám == Tevékenység_Vál.Text.Trim()
                            select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        KézNaptár.Módosítás_Státus(DátumésIdő.Year, Hételső, Tevékenység_Vál.Text.Trim(), true);

                        //Csak akkor törölje a státust ha már nincs máshol a héten
                        Elem = (from a in Naptár_Adatok
                                where a.Idő >= Hételső
                                && a.Idő <= Hétutolsó
                                && a.Pályaszám == Tevékenység_Vál.Text.Trim()
                                select a).FirstOrDefault();

                        if (Elem == null) Státus_állítás(2, 0);

                        MessageBox.Show("Az adat törlésre került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Változás?.Invoke();
                        this.Close();
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

        private void Kiírás()
        {
            try
            {
                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(DátumésIdő.Year);
                Adat_Kerék_Eszterga_Naptár Adat = (from a in Adatok
                                                   where a.Idő >= DátumésIdő
                                                   orderby a.Idő
                                                   select a).FirstOrDefault();

                if (Adat != null)
                {
                    if (Adat.Foglalt)
                    {
                        Tevékenység_Vál.Text = Adat.Pályaszám.Trim();
                        Megjegyzés.Text = Adat.Megjegyzés.Trim();
                        switch (Mód)
                        {
                            case 1:        // Ha beszúrunk egy tevékenységet hátrébb tesszük, akkor engedünk rögzíteni
                                Terv_Rögzít.Visible = true;
                                Töröl.Visible = false;
                                Egy_adat.Visible = false;
                                break;
                            case 2:
                                Terv_Rögzít.Visible = false;
                                Töröl.Visible = true;
                                Egy_adat.Visible = true;
                                break;
                            default:
                                Terv_Rögzít.Visible = false;
                                Töröl.Visible = true;
                                Egy_adat.Visible = true;
                                break;
                        }
                        Marad.Checked = Adat.Marad;
                    }
                    else
                    {
                        Tevékenység_Vál.Text = Adat.Pályaszám.Trim();
                        Megjegyzés.Text = Adat.Megjegyzés.Trim();
                        if (Mód == 2)
                        {
                            Terv_Rögzít.Visible = false;
                            Töröl.Visible = true;
                            Egy_adat.Visible = true;
                        }
                        else
                        {
                            Terv_Rögzít.Visible = true;
                            Töröl.Visible = false;
                            Egy_adat.Visible = false;
                        }
                        Marad.Checked = Adat.Marad;
                    }
                }
                else
                {
                    Terv_Rögzít.Visible = true;
                    Töröl.Visible = false;
                    Egy_adat.Visible = false;
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

        private void Tábla_Író()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 10;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Prioritás";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Igénylés ideje";
                Tábla.Columns[1].Width = 180;
                Tábla.Columns[2].HeaderText = "Pályaszám";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Telephely";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Típus";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Státus";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Megjegyzés";
                Tábla.Columns[6].Width = 200;
                Tábla.Columns[7].HeaderText = "Tengely";
                Tábla.Columns[7].Width = 80;
                Tábla.Columns[8].HeaderText = "Norma";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Év";
                Tábla.Columns[9].Width = 80;

                List<Adat_Kerék_Eszterga_Tengely> AdatokT = kézTengely.Lista_Adatok().OrderBy(a => a.Típus).ToList();

                IgénylistaFeltötlés();
                List<Adat_Kerék_Eszterga_Igény> Adatok;
                if (Igény_Típus.Text.Trim() != "")
                    Adatok = (from a in AdatokIgény
                              where a.Státus == 0 && a.Típus == Igény_Típus.Text.Trim()
                              orderby a.Prioritás descending, a.Rögzítés_dátum
                              select a).ToList();
                else
                    Adatok = (from a in AdatokIgény
                              where a.Státus == 0
                              orderby a.Prioritás descending, a.Rögzítés_dátum
                              select a).ToList();

                foreach (Adat_Kerék_Eszterga_Igény rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Prioritás;
                    Tábla.Rows[i].Cells[1].Value = rekord.Rögzítés_dátum;
                    Tábla.Rows[i].Cells[2].Value = rekord.Pályaszám;
                    Tábla.Rows[i].Cells[3].Value = rekord.Telephely;
                    Tábla.Rows[i].Cells[4].Value = rekord.Típus;
                    switch (rekord.Státus)
                    {
                        case 0:
                            Tábla.Rows[i].Cells[5].Value = "Igény";
                            break;
                        case 2:
                            Tábla.Rows[i].Cells[5].Value = "Ütemezett";
                            break;
                        case 7:
                            Tábla.Rows[i].Cells[5].Value = "Elkészült";
                            break;
                        case 9:
                            Tábla.Rows[i].Cells[5].Value = "Törölt";
                            break;
                    }
                    Tábla.Rows[i].Cells[6].Value = rekord.Megjegyzés;
                    Tábla.Rows[i].Cells[7].Value = rekord.Tengelyszám;
                    Tábla.Rows[i].Cells[8].Value = rekord.Norma;
                    Tábla.Rows[i].Cells[9].Value = rekord.Rögzítés_dátum.Year;
                }
                Tábla.Refresh();
                Tábla.Visible = true;
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Tevékenység_Vál.Text = Tábla.Rows[e.RowIndex].Cells[2].Value.ToString() + " = " + Tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
                Év = int.Parse(Tábla.Rows[e.RowIndex].Cells[9].Value.ToString());
                telephely = Tábla.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
                Választott = Tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                int norma = int.Parse(Tábla.Rows[e.RowIndex].Cells[8].Value.ToString());
                Norma_Idő.Text = norma.ToString();
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

        private void Igény_Típus_Feltöltés()
        {
            try
            {
                Igény_Típus.Items.Clear();
                List<Adat_Kerék_Eszterga_Igény> Adatok = KézIgény.Lista_Adatok(DateTime.Today.Year).ToList();
                List<Adat_Kerék_Eszterga_Igény> Ideig = KézIgény.Lista_Adatok(DateTime.Today.Year - 1).ToList();
                Adatok.AddRange(Ideig);
                List<string> AdatokIgény = (from a in Adatok
                                            where a.Státus < 8
                                            orderby a.Típus
                                            select a.Típus).Distinct().ToList();

                foreach (string rekord in AdatokIgény)
                {
                    if (!Igény_Típus.Items.Contains(rekord.Trim()))
                        Igény_Típus.Items.Add(rekord);
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

        private void Igény_Típus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tábla_Író();
        }


        #region Listák
        private void IgénylistaFeltötlés()
        {
            try
            {
                AdatokIgény.Clear();
                AdatokIgény = KézIgény.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Eszterga_Igény> Ideig = KézIgény.Lista_Adatok(DateTime.Today.Year - 1);
                AdatokIgény.AddRange(Ideig);
                AdatokIgény = (from a in AdatokIgény
                               orderby a.Prioritás descending, a.Rögzítés_dátum
                               select a).ToList();
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
