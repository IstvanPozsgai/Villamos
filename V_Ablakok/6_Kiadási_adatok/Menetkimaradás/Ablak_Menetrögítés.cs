
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos
{
    public partial class Ablak_Menetrögítés
    {
        public int Sorszám { get; set; }
        public string Hely { get; private set; }
        public string Jelszó { get; private set; }
        public string Szöveg { get; private set; }

        public Ablak_Menetrögítés(string hely, string jelszó, string szöveg)
        {
            Hely = hely;
            Jelszó = jelszó;
            Szöveg = szöveg;
            InitializeComponent();
        }

        private void Ablak_Menetrögítés_Load(object sender, System.EventArgs e)

        {

            Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();
            Adat_Menetkimaradás Adat = kéz.Egy_Adat(Hely, Jelszó, Szöveg);
            if (Adat != null)
            {
                txtsorszám.Text = Adat.Id.ToString();
                txteseményjele.Text = Adat.Eseményjele;
                txtviszonylat.Text = Adat.Viszonylat;
                txttípus.Text = Adat.Típus;
                txtpályaszám.Text = Adat.Azonosító;
                txtjvbeírás.Text = Adat.Jvbeírás;
                txthibajavítás.Text = Adat.Javítás;
                Dátum.Value = Adat.Bekövetkezés;
                idő.Value = Adat.Bekövetkezés;
                txtmenet.Text = Adat.Kimaradtmenet.ToString();
                if (Adat.Törölt)
                    chktörlés.Checked = true;
                else
                    chktörlés.Checked = false;
                txtjelentés.Text = Adat.Jelentés;
                txttétel.Text = Adat.Tétel.ToString();
            }
        }
    }
}