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
        public string Hova { get; private set; }
        public string Név { get; private set; }
        public int Sorszám { get; private set; }

        public event Event_Kidobó Változás;

        public Ablak_Fénykép_Betöltés(string hova, string név, int sorszám)
        {
            Hova = hova;
            Név = név;
            Sorszám = sorszám;
            InitializeComponent();
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

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek\";
                string hova;
                string honnan;

                //      kijelölt elemeket másolja a kijelölt könyvtárba
                for (int i = 0; i < Fényképek.SelectedItems.Count; i++)
                {

                    hova = hely + Név.Trim() + $"_{Sorszám}.jpg";
                    honnan = Könyvtár + @"\" + Fényképek.SelectedItems[i].ToString().Trim();
                    Copy(honnan, hova);
                    Sorszám += 1;
                }

                Változás?.Invoke();
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
            string hely = $@"{Könyvtár}\{Fényképek.SelectedItems[0].ToStrTrim()}";
            if (!Exists(hely)) return;
            Kezelő_Kép.KépMegnyitás(Képtöltő, hely, toolTip1);
        }
    }
}
