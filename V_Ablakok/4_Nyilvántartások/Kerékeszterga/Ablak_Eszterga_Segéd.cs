using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
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

        int Év = 1900;
        string telephely = "";
        string Választott = "";

        string hely;
        string jelszó = "RónaiSándor";
        string helyTörzs = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";

        readonly Kezelő_Kerék_Eszterga_Naptár Naptár_Kéz = new Kezelő_Kerék_Eszterga_Naptár();
        List<Adat_Kerék_Eszterga_Naptár> Naptár_Adatok;
        List<Adat_Kerék_Eszterga_Naptár> Naptár_Adatok_ideig;

        readonly Kezelő_Kerék_Eszterga_Igény KézIgény = new Kezelő_Kerék_Eszterga_Igény();
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

        void Start()
        {

            Tevékenység_feltöltés();
            Marad.Checked = false;
            DateTime Hételső = MyF.Hét_elsőnapja(DátumésIdő);
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Hételső.Year}_Esztergálás.mdb";
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
            Jogosultságkiosztás();
            Text_Dátum.Text = DátumésIdő.ToString("yyyy.MM.dd");
            Text_Idő.Text = DátumésIdő.ToString("HH:mm");
            Kiírás();
            Igény_Típus_Feltöltés();
            Tábla_Író();
            Automata();
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


        void Tevékenység_feltöltés()
        {
            string szöveg = "SELECT * FROM Tevékenység ORDER BY id";
            Tevékenység.Items.Clear();
            Tevékenység.BeginUpdate();
            Tevékenység.Items.AddRange(MyF.ComboFeltöltés(helyTörzs, jelszó, szöveg, "Tevékenység"));
            Tevékenység.EndUpdate();
            Tevékenység.Refresh();
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


        void Rögzít()
        {
            try
            {
                if (Tevékenység_Vál.Text.Trim() == "")
                    throw new HibásBevittAdat("Tevékenység mezőt ki kell tölteni.");
                if (Norma_Idő.Text.Trim() == "" || !int.TryParse(Norma_Idő.Text, out int NormaIdő))
                    throw new HibásBevittAdat("Az időszükséglet mező nem lehet üres és pozítív egész számnak kell lennie.");
                if (NormaIdő < 1)
                    throw new HibásBevittAdat("Az időszükséglet mezőnek pozítív egész számnak kell lennie.");

                string szöveg = $"SELECT * FROM naptár WHERE idő>=#{DátumésIdő.ToString("MM-dd-yyyy H:m:s")}# ORDER BY idő ";

                Kezelő_Kerék_Eszterga_Naptár kéz = new Kezelő_Kerék_Eszterga_Naptár();
                List<Adat_Kerék_Eszterga_Naptár> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                Holtart.Be();

                Adatok_Rögzítése(NormaIdő);
                Igény_Módosítás();

                if (Változás != null) Változás();
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
                string helyigény = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Év}_Igény.mdb";
                if (!File.Exists(helyigény)) return;
                string szöveg = $"SELECT * FROM Igény";
                AdatokIgény = KézIgény.Lista_Adatok(helyigény, jelszó, szöveg);
                Adat_Kerék_Eszterga_Igény Elem = (from a in AdatokIgény
                                                  where a.Telephely == telephely.Trim()
                                                  && a.Pályaszám == Választott.Trim()
                                                  && a.Státus <= 2
                                                  select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = $"UPDATE  Igény SET státus=2, ütemezés_dátum='{Text_Dátum.Text}' WHERE státus<=2   AND telephely='{telephely.Trim()}' AND pályaszám='{Választott.Trim()}'";
                    MyA.ABMódosítás(helyigény, jelszó, szöveg);
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

                Adat_Kerék_Eszterga_Igény EgyIgény = (from a in AdatokIgény
                                                      where a.Státus == 2 && a.Telephely == elem[1].Trim() && a.Pályaszám == elem[0].Trim()
                                                      select a).FirstOrDefault();
                if (EgyIgény != null)
                {
                    string helyigény = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{(EgyIgény.Rögzítés_dátum.Year)}_Igény.mdb";
                    string szöveg = $"UPDATE  Igény SET státus=0, ütemezés_dátum='1900.01.01' WHERE státus=2 AND telephely='{elem[1].Trim()}' AND pályaszám='{elem[0].Trim()}'";
                    MyA.ABMódosítás(helyigény, jelszó, szöveg);
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

        void Beszúrásos_Csúsztatás()
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
                if (Változás != null) Változás();
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
            Holtart.Be();
            string szöveg = $@"SELECT * FROM naptár WHERE idő>=#{DátumésIdő.ToString("MM-dd-yyyy H:m:s")}#  AND  pályaszám<>'_'  ORDER BY idő ";
            Naptár_Adatok = Naptár_Kéz.Lista_Adatok(hely, jelszó, szöveg);
            //Ami nem mozog csak az marad benne a többit visszaállítjuk alapra

            Naptár_Adatok[0].Marad = false;


            foreach (Adat_Kerék_Eszterga_Naptár rekord in Naptár_Adatok)
            {
                if (!rekord.Marad)
                {
                    Adat_Kerék_Eszterga_Naptár Ideig_Naptár = new Adat_Kerék_Eszterga_Naptár(rekord.Idő);
                    Naptár_Kéz.Adat_RögzítésIdő(hely, jelszó, Ideig_Naptár);
                }

                Holtart.Lép();
            }
        }


        private void Adatok_Rögzítése(int NormaIdő)
        {
            Holtart.Be();
            string szöveg = $"SELECT * FROM naptár WHERE idő>=#{DátumésIdő.ToString("MM-dd-yyyy H:m:s")}#  AND  Munkaidő=true   ORDER BY idő";
            Naptár_Adatok_ideig = Naptár_Kéz.Lista_Adatok(hely, jelszó, szöveg);

            int Futóidő = 0;
            //Amit be akarunk szúrni azt beszúrjuk
            foreach (Adat_Kerék_Eszterga_Naptár rekord in Naptár_Adatok_ideig)
            {
                if (!rekord.Foglalt && rekord.Munkaidő)
                {
                    Adat_Kerék_Eszterga_Naptár Ideig_Naptár = new Adat_Kerék_Eszterga_Naptár(
                                    rekord.Idő,
                                    rekord.Munkaidő,
                                    rekord.Foglalt,
                                    Tevékenység_Vál.Text.Trim(),
                                    MyF.Szöveg_Tisztítás(Megjegyzés.Text, 0, -1, true),
                                    betűSzín,
                                    háttérSzín,
                                    Marad.Checked);

                    Naptár_Kéz.Adat_Rögzítés(hely, jelszó, Ideig_Naptár);

                    Státus_állítás(0, 2);

                    DátumésIdő = rekord.Idő;
                    Futóidő += 30;
                }
                // emeljük az időt addig amíg ...

                if (Futóidő >= NormaIdő) break;
                Holtart.Lép();
            }
            if (Futóidő <= NormaIdő)
                Maradék_beírása_Rögzítés(Futóidő, NormaIdő);
        }


        private void Maradék_beírása_Rögzítés(int Futóidő, int NormaIdő)
        {
            Holtart.Be();
            Futóidő -= 30;

            //   Ha még van rögzített, de már nincs munkaidővel lefedett akkor azt is rögzítjük

            List<string> SzövegGy = new List<string>();
            while (Futóidő < NormaIdő)
            {
                string szöveg = $"UPDATE naptár SET pályaszám='{Tevékenység_Vál.Text.Trim()}', foglalt=true, Megjegyzés='{Megjegyzés.Text.Trim()}', ";
                szöveg += $" betűszín={betűSzín}, háttérszín={háttérSzín}, marad={Marad.Checked} ";
                szöveg += $"WHERE idő=#{DátumésIdő.ToString("MM-dd-yyyy HH:mm")}#";
                Státus_állítás(0, 2);

                SzövegGy.Add(szöveg);
                DátumésIdő = DátumésIdő.AddMinutes(30);
                Futóidő += 30;

                Holtart.Lép();
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }


        private void Maradék_beírása()
        {
            Holtart.Be();

            //   Ha még van rögzített, de már nincs munkaidővel lefedett akkor azt is rögzítjük
            elem--;
            List<string> SzövegGy = new List<string>();
            while (elem <= elemszám)
            {

                //ha helyben maradó volt akkor tovább lapozzuk
                while (Naptár_Adatok[elem].Marad)
                    elem++;
                //A maradó tételek rögzítve vannak
                if (!Naptár_Adatok[elem].Marad)
                {
                    //elmentjük az első elemet
                    string szöveg = $"UPDATE naptár SET pályaszám='{Naptár_Adatok[elem].Pályaszám.Trim()}', foglalt={Naptár_Adatok[elem].Foglalt}, Megjegyzés='{Naptár_Adatok[elem].Megjegyzés.Trim()}', ";
                    szöveg += $" betűszín={Naptár_Adatok[elem].BetűSzín}, háttérszín={Naptár_Adatok[elem].HáttérSzín}, marad={Naptár_Adatok[elem].Marad} ";
                    szöveg += $"WHERE idő=#{DátumésIdő.ToString("MM-dd-yyyy HH:mm")}#";
                    SzövegGy.Add(szöveg);
                    elem++;
                    DátumésIdő = DátumésIdő.AddMinutes(30);
                }
                Holtart.Lép();
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }


        private void Adatok_VisszaÍrása(int kimarad)
        {
            try
            {
                Holtart.Be();

                //Az eredeti adatokat a folytatólagosan tesszük a javított lista alapján
                string szöveg = $"SELECT * FROM naptár WHERE idő>=#{DátumésIdő.ToString("MM-dd-yyyy H:m:s")}#  AND Munkaidő=true  ORDER BY idő";
                if (Naptár_Adatok_ideig != null) Naptár_Adatok_ideig.Clear();
                Naptár_Adatok_ideig = Naptár_Kéz.Lista_Adatok(hely, jelszó, szöveg);
                elemszám = Naptár_Adatok.Count - 1;
                elem = kimarad;
                List<string> SzövegGy = new List<string>();
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
                            szöveg = $"UPDATE naptár SET pályaszám='{Naptár_Adatok[elem].Pályaszám.Trim()}', foglalt={Naptár_Adatok[elem].Foglalt}, Megjegyzés='{Naptár_Adatok[elem].Megjegyzés.Trim()}', ";
                            szöveg += $" betűszín={Naptár_Adatok[elem].BetűSzín}, háttérszín={Naptár_Adatok[elem].HáttérSzín}, marad={Naptár_Adatok[elem].Marad} ";
                            szöveg += $"WHERE idő=#{rekord.Idő.ToString("MM-dd-yyyy HH:mm")}#";
                            SzövegGy.Add(szöveg);
                            elem++;
                            DátumésIdő = rekord.Idő;
                        }
                        if (elem > elemszám) break;
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
                if (Változás != null) Változás();
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

                string szöveg = $"SELECT * FROM naptár WHERE idő>=#{DátumésIdő.ToString("MM-dd-yyyy H:m:s")}# AND ";
                szöveg += $" idő<=#{DátumésIdő.AddDays(3).ToString("MM-dd-yyyy H:m:s")}# Order BY idő";

                Kezelő_Kerék_Eszterga_Naptár kéz = new Kezelő_Kerék_Eszterga_Naptár();
                List<Adat_Kerék_Eszterga_Naptár> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                int Futóidő = 0;

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Adatok)
                {
                    if (!rekord.Foglalt && rekord.Munkaidő)
                    {
                        szöveg = $"UPDATE naptár SET pályaszám='{Tevékenység_Vál.Text.Trim()}', foglalt=true, Megjegyzés='{Megjegyzés.Text.Trim()}', ";
                        szöveg += $" betűszín={betűSzín}, háttérszín={háttérSzín}, marad={Marad.Checked} ";
                        szöveg += $"WHERE idő=#{DátumésIdő.ToString("MM-dd-yyyy HH:mm")}#";
                        Státus_állítás(0, 2);

                        SzövegGy.Add(szöveg);
                        Futóidő += 30;
                    }
                    // emeljük az időt addig amíg ...
                    DátumésIdő = DátumésIdő.AddMinutes(30);
                    if (Futóidő >= NormaIdő) break;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Változás != null) Változás();
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
                string helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DátumésIdő.Year}_Igény.mdb";

                string[] darabol = Tevékenység_Vál.Text.Split('=');
                string szöveg = $"UPDATE igény SET státus={Státus_Lesz}";
                szöveg += $"   WHERE státus={Státus_Volt} AND pályaszám='{darabol[0].Trim()}'";
                MyA.ABMódosítás(helyi, jelszó, szöveg);
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


        void Tevékenységválasztás()
        {
            try
            {
                string szöveg = $"SELECT * FROM Tevékenység WHERE Tevékenység='{Tevékenység_Vál.Text.Trim()}'";

                Kezelő_Kerék_Eszterga_Tevékenység kéz = new Kezelő_Kerék_Eszterga_Tevékenység();
                Adat_Kerék_Eszterga_Tevékenység Adat = kéz.Egy_Adat(helyTörzs, jelszó, szöveg);

                //      if (Adat.Betűszín != 0)
                betűSzín = Adat.Betűszín;
                //     if (Adat.Háttérszín != 0)
                háttérSzín = Adat.Háttérszín;
                Norma_Idő.Text = Adat.Munkaidő.ToString();

                Szín = MyColor.Szín_váltó(Adat.Háttérszín);
                Marad.Checked = Adat.Marad;
                this.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
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


        void Töröl_esemény()
        {
            try
            {
                DateTime Hételső = MyF.Hét_elsőnapja(DátumésIdő);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(DátumésIdő);
                Naptár_Adatok?.Clear();
                string szöveg = $"SELECT * FROM naptár ";
                Naptár_Adatok = Naptár_Kéz.Lista_Adatok(hely, jelszó, szöveg);
                Adat_Kerék_Eszterga_Naptár Elem;

                if (!Egy_adat.Checked)
                {
                    Elem = (from a in Naptár_Adatok
                            where a.Idő >= DátumésIdő
                            && a.Pályaszám == Tevékenység_Vál.Text.Trim()
                            select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        szöveg = $"UPDATE naptár SET  foglalt=false, Megjegyzés='', betűszín=0, háttérszín=12632256, pályaszám='', marad=false ";
                        szöveg += $" WHERE idő>=#{Hételső:MM-dd-yyyy H:m:s}# AND pályaszám='{Tevékenység_Vál.Text.Trim()}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        Státus_állítás(2, 0);

                        MessageBox.Show("Az adatok törlésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (Változás != null) Változás();
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
                        szöveg = $"UPDATE naptár SET  foglalt=false, Megjegyzés='', betűszín=0, háttérszín=12632256, pályaszám='', marad=false  ";
                        szöveg += $" WHERE idő=#{DátumésIdő:MM-dd-yyyy H:m:s}#  AND pályaszám='{Tevékenység_Vál.Text.Trim()}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        //Csak akkor törölje a státust ha már nincs máshol a héten
                        Elem = (from a in Naptár_Adatok
                                where a.Idő >= Hételső
                                && a.Idő <= Hétutolsó
                                && a.Pályaszám == Tevékenység_Vál.Text.Trim()
                                select a).FirstOrDefault();

                        if (Elem == null) Státus_állítás(2, 0);

                        MessageBox.Show("Az adat törlésre került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (Változás != null) Változás();
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
                string szöveg = $"SELECT * FROM naptár WHERE idő>=#{DátumésIdő.ToString("MM-dd-yyyy HH:m:s")}# ORDER BY idő";

                Kezelő_Kerék_Eszterga_Naptár kéz = new Kezelő_Kerék_Eszterga_Naptár();
                Adat_Kerék_Eszterga_Naptár Adat = kéz.Egy_Adat(hely, jelszó, szöveg);

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

                string szövegT = $"SELECT * FROM tengely ORDER BY  típus";
                string jelszó = "RónaiSándor";

                Kezelő_Kerék_Eszterga_Tengely kézTengely = new Kezelő_Kerék_Eszterga_Tengely();
                List<Adat_Kerék_Eszterga_Tengely> AdatokT = kézTengely.Lista_Adatok(helyTörzs, jelszó, szövegT);

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




        void Igény_Típus_Feltöltés()
        {
            try
            {
                Igény_Típus.Items.Clear();

                for (int ii = -1; ii < 1; ii++)
                {
                    string helyF = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.AddYears(ii).Year}_Igény.mdb";
                    if (File.Exists(helyF))
                    {
                        string jelszó = "RónaiSándor";
                        string szöveg = $"SELECT DISTINCT típus FROM Igény WHERE státus<8  ORDER BY  típus";
                        Kezelő_Általános_String KézIgény = new Kezelő_Általános_String();
                        List<string> AdatokIgény = KézIgény.Lista_Adatok(helyF, jelszó, szöveg, "típus");

                        foreach (string rekord in AdatokIgény)
                        {
                            if (!Igény_Típus.Items.Contains(rekord.Trim()))
                                Igény_Típus.Items.Add(rekord);
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
                for (int ii = -1; ii < 1; ii++)
                {
                    string helyF = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.AddYears(ii).Year}_Igény.mdb";
                    if (File.Exists(helyF))
                    {
                        string szöveg = "SELECT * FROM Igény ORDER BY Prioritás desc, Rögzítés_dátum";
                        List<Adat_Kerék_Eszterga_Igény> AdatokIgényIdeig = KézIgény.Lista_Adatok(helyF, jelszó, szöveg);
                        AdatokIgény.AddRange(AdatokIgényIdeig);
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
    }
}
