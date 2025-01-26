using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;

namespace Villamos
{
    public partial class AblakJelszóváltoztatás
    {
        readonly Kezelő_Belépés_Bejelentkezés Kéz = new Kezelő_Belépés_Bejelentkezés();
        public AblakJelszóváltoztatás(string Telephely, string Név)
        {
            InitializeComponent();
            TxtUserName.Text = Név;
            TxtTelephely.Text = Telephely;
        }


        private void BtnMégse_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btnok_Click(object sender, EventArgs e)
        {
            try
            {

                // ha nem egyforma a két jelszó akkor kilép
                if (Első.Text.Trim().ToUpper() != Második.Text.Trim().ToUpper())
                {
                    MessageBox.Show("A két jelszó nem egyezik!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Első.Focus();
                    return;
                }
                if (Első.Text.Trim().Length < 3)
                {
                    MessageBox.Show("A jelszónak 3 karakternél hosszabbnak kell lennie !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Első.Focus();
                    return;
                }
                if (Első.Text.Trim().Length > 15)
                {
                    MessageBox.Show("A jelszónak 15 karakternél rövidebbnek kell lennie !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Első.Focus();
                    return;
                }

                List<Adat_Belépés_Bejelentkezés> Adatok = Kéz.Lista_Adatok(TxtTelephely.Text.Trim());

                Adat_Belépés_Bejelentkezés Elem = (from a in Adatok
                                                   where a.Név.ToUpper() == TxtUserName.Text.Trim().ToUpper()
                                                   select a).FirstOrDefault();

                if (Elem != null)
                {
                    if (TxtPassword.Text.Trim().ToUpper() != Elem.Jelszó.ToUpper())
                    {
                        TxtPassword.Focus();
                        throw new HibásBevittAdat("A régi jelszó nem egyezik a tárolt adatokkal !");
                    }
                    Adat_Belépés_Bejelentkezés ADAT = new Adat_Belépés_Bejelentkezés(Elem.Sorszám, Elem.Név.ToUpper(), Első.Text.Trim().ToUpper(), Elem.Jogkör);
                    Kéz.Módosítás(TxtTelephely.Text.Trim(), ADAT);
                    MessageBox.Show("A jelszó módosításra került !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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




        private void TxtPassword_MouseLeave(object sender, EventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = true;
        }

        private void TxtPassword_MouseMove(object sender, MouseEventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = false;
        }

        private void Első_MouseMove(object sender, MouseEventArgs e)
        {
            Első.UseSystemPasswordChar = false;
        }

        private void Első_MouseLeave(object sender, EventArgs e)
        {
            Első.UseSystemPasswordChar = true;
        }
        private void Második_MouseMove(object sender, MouseEventArgs e)
        {
            Második.UseSystemPasswordChar = false;
        }

        private void Második_MouseLeave(object sender, EventArgs e)
        {
            Második.UseSystemPasswordChar = true;
        }

        private void AblakJelszóváltoztatás_Load(object sender, EventArgs e)
        {

        }
    }
}