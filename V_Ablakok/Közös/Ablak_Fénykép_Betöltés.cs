using System;
using System.IO;
using System.Windows.Forms;
using Villamos.V_MindenEgyéb;
using static System.IO.File;

namespace Villamos.Villamos_Ablakok.Közös
{
    public partial class Ablak_Fénykép_Betöltés : Form
    {
        string Könyvtár = "";
        public string Hely { get; private set; }
        public string Név { get; private set; }

        int Sorszám = 0;

        public event Event_Kidobó Változás;

        /// <summary>
        /// Fényképek betöltése és új képek feltöltése
        /// </summary>
        /// <param name="hely">Az a hely ahova akarjuk menteni a képeket</param>
        /// <param name="név">Az az azonosító melyhez kötni szeretnénk a képeket</param>

        public Ablak_Fénykép_Betöltés(string hely, string név)
        {
            Hely = hely;
            Név = név;

            InitializeComponent();
            Sorszám = SorszámMax() + 1;
            this.Text = $"A {név} cikkszámú anyag fényképei";
            FényképekFeltöltése();
        }

        private void FényképekFeltöltése()
        {
            try
            {
                //Választott könyvtár beállítása
                Könyvtár = Hely;
                DirectoryInfo dir = new DirectoryInfo(Hely);
                System.IO.FileInfo[] aryFi = dir.GetFiles($"*{Név.Trim()}*.jpg");
                foreach (FileInfo fi in aryFi)
                    Fényképek.Items.Add(fi.Name);
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

        public Ablak_Fénykép_Betöltés()
        {
            InitializeComponent();
        }

        private void Ablak_Fénykép_Betöltés_Load(object sender, EventArgs e)
        {

        }

        private void Képnyitó_Click(object sender, EventArgs e)
        {
            Képpekkel();

        }

        private void Képpekkel()
        {
            try
            {
                Fényképek.Items.Clear();
                FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
                if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {

                    DirectoryInfo di = new DirectoryInfo(FolderBrowserDialog1.SelectedPath);
                    Könyvtár = FolderBrowserDialog1.SelectedPath;
                    System.IO.FileInfo[] aryFi = di.GetFiles("*.jpg");

                    foreach (FileInfo fi in aryFi)
                        Fényképek.Items.Add(fi.Name);
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

        private void Képválasztó_Click(object sender, EventArgs e)
        {
            try
            {
                if (Fényképek.SelectedItems.Count < 1) return;

                string hova;
                string honnan;

                //      kijelölt elemeket másolja a kijelölt könyvtárba
                for (int i = 0; i < Fényképek.SelectedItems.Count; i++)
                {

                    hova = Hely + @"\" + Név.Trim() + $"_{Sorszám}.jpg";
                    honnan = Könyvtár + @"\" + Fényképek.SelectedItems[i].ToStrTrim();
                    Copy(honnan, hova);
                    Sorszám += 1;
                }

                Változás?.Invoke();
                MessageBox.Show("Fényképek feltöltése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void Fényképek_SelectedIndexChanged(object sender, EventArgs e)
        {
            string helyi = $@"{Könyvtár}\{Fényképek.SelectedItems[0].ToStrTrim()}";
            if (!Exists(helyi)) return;
            Kezelő_Kép.KépMegnyitás(Képtöltő, helyi, toolTip1);
        }

        private int SorszámMax()
        {
            int sorszám = 0;
            //Megnézzük, hogy melyik az utolsó fénykép az adott azonosítóból
            DirectoryInfo dir = new DirectoryInfo(Hely);
            foreach (FileInfo Elem in dir.GetFiles($"*{Név.Trim()}*.jpg"))
            {
                string[] darabol = Elem.Name.Split('_');
                string[] ideig = darabol[1].Split('.');
                if (int.TryParse(ideig[0], out int sor))
                    if (sorszám < sor) sorszám = sor;
            }
            return sorszám;
        }
    }
}
