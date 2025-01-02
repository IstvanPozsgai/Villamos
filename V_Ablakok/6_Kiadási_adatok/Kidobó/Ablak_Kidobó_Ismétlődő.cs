using System; 
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Kidobó_Ismétlődő : Form
    {
        public event Event_Kidobó Ismétlődő_Változás;
        public string Cmbtelephely { get; private set; }
        public string Alsópanel { get; private set; }
        public Adat_Kidobó_Segéd Rekord { get; private set; }
        public DateTime Dátum { get; private set; }

        public Ablak_Kidobó_Ismétlődő(string cmbtelephely, Adat_Kidobó_Segéd rekord, DateTime dátum, string alsópanel)
        {
            Cmbtelephely = cmbtelephely;
            Dátum = dátum;
            Rekord = rekord;
            Alsópanel = alsópanel;

            InitializeComponent();
            Combováltozatfeltölt();
            Adatok_kiírása(rekord);
        }

        Ablak_Kidobó_változat Új_Ablak_Kidobó_változat;


        private void Ablak_Kidobó_Ismétlődő_Load(object sender, EventArgs e)
        {

        }


        void Adatok_kiírása(Adat_Kidobó_Segéd rekord)
        {
            if (rekord != null)
            {
                Frame3KezdésiHely.Text = rekord.Kezdéshely.Trim();
                Frame3VégzésiHely.Text = rekord.Végzéshely.Trim();
                Frame3Megjegyzés.Text = rekord.Megjegyzés.Trim();
                Frame3KezdésiIdő.Value = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, rekord.Kezdés.Hour, rekord.Kezdés.Minute, rekord.Kezdés.Second);
                Frame3VégzésiIdő.Value = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, rekord.Végzés.Hour, rekord.Végzés.Minute, rekord.Végzés.Second);
                Frame3Szolgálatiszám.Text = rekord.Szolgálatiszám.Trim();
                Frame3ForgalmiSzám.Text = rekord.Forgalmiszám.Trim();
                ComboVáltozat.Text = rekord.Változatnév.Trim();
            }
        }

        private void Combováltozatfeltölt()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            string jelszó = "erzsébet";

            string szöveg = "SELECT * FROM Változattábla  order by id";

            ComboVáltozat.Items.Clear();
            ComboVáltozat.Items.Add("");
            ComboVáltozat.BeginUpdate();
            ComboVáltozat.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "Változatnév"));
            ComboVáltozat.EndUpdate();
            ComboVáltozat.Refresh();
            if (Ismétlődő_Változás != null) Ismétlődő_Változás();
        }

        private void Command5_Click(object sender, EventArgs e)
        {
            Frame3KezdésiHely.Text = Alsópanel;
        }

        private void Command4_Click(object sender, EventArgs e)
        {
            Frame3VégzésiHely.Text = Alsópanel;
        }

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboVáltozat.Text.Trim() == "") throw new HibásBevittAdat("A változat nevét meg kell adni.");
                if (Frame3Szolgálatiszám.Text.Trim() == "") throw new HibásBevittAdat("A szolgálati szám nem lehet üres mező.");
                // menti a változat adatait.

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
                string jelszó = "erzsébet";

                // ha nincs olyan akkor rögzít különben módosít

                string szöveg = $"SELECT * FROM Kidobósegédtábla where szolgálatiszám='{Frame3Szolgálatiszám.Text.Trim()}' AND változatnév='{ComboVáltozat.Text.Trim()}'";

                Kezelő_Kidobó_Segéd KézKidobSeg = new Kezelő_Kidobó_Segéd();
                List<Adat_Kidobó_Segéd> AdatokKidobSeg = KézKidobSeg.Lista_Adat(hely, jelszó, szöveg);

                Adat_Kidobó_Segéd AdatKidobSeg = (from a in AdatokKidobSeg
                                                  where a.Szolgálatiszám == Frame3Szolgálatiszám.Text.Trim()
                                                  && a.Változatnév == ComboVáltozat.Text.Trim()
                                                  select a).FirstOrDefault();

                if (AdatKidobSeg != null)
                {
                    // ha már van ilyen akkor módosít
                    szöveg = "UPDATE Kidobósegédtábla  SET ";
                    if (Frame3KezdésiHely.Text.Trim() == "")
                        szöveg += "Kezdéshely='_', ";
                    else
                        szöveg += "Kezdéshely='" + Frame3KezdésiHely.Text.Trim() + "', ";

                    if (Frame3VégzésiHely.Text.Trim() == "")
                        szöveg += "Végzéshely='_', ";
                    else
                        szöveg += "Végzéshely='" + Frame3VégzésiHely.Text.Trim() + "', ";

                    if (Frame3Megjegyzés.Text.Trim() == "")
                        szöveg += "megjegyzés='_', ";
                    else
                        szöveg += "megjegyzés='" + Frame3Megjegyzés.Text.Trim() + "', ";

                    szöveg += " Kezdés='" + Frame3KezdésiIdő.Value.ToString("HH:mm") + "', ";
                    szöveg += " végzés='" + Frame3VégzésiIdő.Value.ToString("HH:mm") + "' ";
                    szöveg += $" WHERE  szolgálatiszám='{Frame3Szolgálatiszám.Text.Trim()}' AND változatnév='{ComboVáltozat.Text.Trim()}'";
                }
                else
                {
                    // ha nincs akkor rögzít
                    szöveg = "INSERT INTO Kidobósegédtábla (változatnév, forgalmiszám, szolgálatiszám, Kezdéshely, Végzéshely, megjegyzés, Kezdés, végzés) VALUES (";
                    szöveg += "'" + ComboVáltozat.Text.Trim() + "', ";
                    szöveg += "'" + Frame3ForgalmiSzám.Text.Trim() + "', ";
                    szöveg += "'" + Frame3Szolgálatiszám.Text.Trim() + "', ";
                    if (Frame3KezdésiHely.Text.Trim() == "")
                        szöveg += "'_', ";
                    else
                        szöveg += $"'{Frame3KezdésiHely.Text.Trim()}', ";

                    if (Frame3VégzésiHely.Text.Trim() == "")
                        szöveg += "'_', ";
                    else
                        szöveg += $"'{Frame3VégzésiHely.Text.Trim()}', ";

                    if (Frame3Megjegyzés.Text.Trim() == "")
                        szöveg += "'_', ";
                    else
                        szöveg += $"'{Frame3Megjegyzés.Text.Trim()}', ";

                    szöveg += "'" + Frame3KezdésiIdő.Value.ToString("HH:mm") + "', ";
                    szöveg += "'" + Frame3VégzésiIdő.Value.ToString("HH:mm") + "')";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                if (Ismétlődő_Változás != null) Ismétlődő_Változás();
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

        public void Változatkarb_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Kidobó_változat == null)
            {
                Új_Ablak_Kidobó_változat = new Ablak_Kidobó_változat(Cmbtelephely.Trim());
                Új_Ablak_Kidobó_változat.FormClosed += Ablak_Kidobó_változat_Closed;
                Új_Ablak_Kidobó_változat.Top = 350;
                Új_Ablak_Kidobó_változat.Left = 500;
                Új_Ablak_Kidobó_változat.Show();
                Új_Ablak_Kidobó_változat.Változat_Változás += Combováltozatfeltölt;
            }
            else
            {
                Új_Ablak_Kidobó_változat.Activate();
            }
        }

        private void Ablak_Kidobó_változat_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_változat = null;
        }

        private void Ablak_Kidobó_Ismétlődő_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Új_Ablak_Kidobó_változat != null)
                Új_Ablak_Kidobó_változat.Close();
        }

        private void Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboVáltozat.Text == "")
                    throw new HibásBevittAdat("A változat neve nem lehet üres!");
                if (Frame3Szolgálatiszám.Text.Trim() == "")
                    throw new HibásBevittAdat("A Szolgálatszám mezőnek értéket kell tartalmaznia.");
                // menti a változat adatait.
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
                string jelszó = "erzsébet";

                // ha nincs olyan akkor rögzít különben módosít
                string szöveg = $"SELECT * FROM Kidobósegédtábla";

                Kezelő_Kidobó_Segéd KézKidobSeg = new Kezelő_Kidobó_Segéd();
                List<Adat_Kidobó_Segéd> AdatokKidobSeg = KézKidobSeg.Lista_Adat(hely, jelszó, szöveg);

                Adat_Kidobó_Segéd AdatKidobSeg = (from a in AdatokKidobSeg
                                                  where a.Szolgálatiszám == Frame3Szolgálatiszám.Text.Trim()
                                                  && a.Változatnév == ComboVáltozat.Text.Trim()
                                                  select a).FirstOrDefault();


                if (AdatKidobSeg != null)
                {

                    szöveg = $"DELETE FROM Kidobósegédtábla WHERE szolgálatiszám='{Frame3Szolgálatiszám.Text.Trim()}' and változatnév='{ComboVáltozat.Text.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
                if (Ismétlődő_Változás != null) Ismétlődő_Változás();

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

        private void Ablak_Kidobó_Ismétlődő_KeyDown(object sender, KeyEventArgs e)
        {
            //Esc
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }


        }
    }
}
