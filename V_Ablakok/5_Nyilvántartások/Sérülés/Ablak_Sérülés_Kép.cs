using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_MindenEgyéb;
using static System.IO.File;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Sérülés
{
    public partial class Ablak_Sérülés_Kép : Form
    {
        public event Event_Kidobó Változás;
        private DateTime Dátum { get; set; }
        private int FenykepekValue { get; set; }
        private int Sorszam { get; set; }
        private string Pályaszám { get; set; }
        private string utvonal;
        private List<string> Képek { get; set; }

        public Ablak_Sérülés_Kép(DateTime dátum, int fenykepekValue, int sorszam, string pályaszám, List<string> képek)
        {
            Dátum = dátum;
            FenykepekValue = fenykepekValue;
            Sorszam = sorszam;
            Pályaszám = pályaszám;
            Képek = képek;
            InitializeComponent();
        }

        public Ablak_Sérülés_Kép()
        {
            InitializeComponent();
        }

        public void Képpekkel()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum.Year}\Képek\";
                ListBox1.Items.Clear();
                FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
                using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                        utvonal = folderBrowserDialog1.SelectedPath;

                        FileInfo[] jpgFiles = di.GetFiles("*.jpg");
                        FileInfo[] jpegFiles = di.GetFiles("*.jpeg");
                        FileInfo[] allImages = jpgFiles.Concat(jpegFiles).ToArray();
                        foreach (FileInfo fi in allImages)
                            ListBox1.Items.Add(fi.Name);
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Képválasztó_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBox1.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva kép!");
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum.Year}\Képek\";
                string hova, honnan;
                int sorszám;

                if (FenykepekValue == 0)
                    sorszám = 0;
                else
                {
                    string szöveg = Képek[Képek.Count - 1].Trim().Substring(0, Képek[Képek.Count - 1].Trim().Length - 4);
                    string[] adat = szöveg.Split('_');
                    sorszám = int.Parse(adat[adat.Length - 1]);
                }

                // kijelölt elemeket másolja a kijelölt könyvtárba
                for (int i = 0; i < ListBox1.SelectedItems.Count; i++)
                {
                    sorszám += 1;
                    hova = $@"{$"{hely}{Dátum.Year}"}_{Sorszam.ToStrTrim()}_{Pályaszám.ToStrTrim()}_{sorszám}.jpg";
                    honnan = $@"{utvonal}\{ListBox1.SelectedItems[i].ToStrTrim()}";
                    Képek.Add($@"{$"{Dátum.Year}"}_{Sorszam.ToStrTrim()}_{Pályaszám.ToStrTrim()}_{sorszám}.jpg");
                    Copy(honnan, hova);
                }
                FenykepekValue += ListBox1.SelectedItems.Count;
                MessageBox.Show("A kép feltöltés sikeres volt.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Változás?.Invoke();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Képnyitó_Click(object sender, EventArgs e)
        {
            Képpekkel();
        }


        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListBox1.SelectedItems.Count == 0) return;
                string hely = $@"{utvonal}\{ListBox1.SelectedItems[0]}";
                if (!Exists(hely)) return;
                Kezelő_Kép.KépMegnyitás(Képtöltő, hely, toolTip1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Ablak_Sérülés_Kép_Load(object sender, EventArgs e)
        {

        }
    }
}
