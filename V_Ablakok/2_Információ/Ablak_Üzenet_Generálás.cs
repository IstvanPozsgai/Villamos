﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok.Közös
{
    public partial class Ablak_Üzenet_Generálás : Form
    {
        public event Event_Kidobó Változás;
        readonly Kezelő_Üzenet KézÜzenet = new Kezelő_Üzenet();
        readonly Kezelő_Kiegészítő_Könyvtár KézKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgTelep = new Kezelő_Kiegészítő_Szolgálattelepei();

        public string Telephely { get; private set; }
        public string Előterv { get; private set; }
        public int Válasz { get; private set; }

        public Ablak_Üzenet_Generálás(string telephely, string előterv, int válasz)
        {
            Telephely = telephely;
            Előterv = előterv;
            Válasz = válasz;
            InitializeComponent();
            Start();
        }

        public Ablak_Üzenet_Generálás()
        {
            InitializeComponent();
        }

        private void Start()
        {
            Txtírásimező.Text = Előterv;
            //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
            //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
            if (Program.PostásJogkör.Any(c => c != '0'))
            {
                GombokLátszanak(false);
                Jogosultságkiosztás();
                Üzemekfeltöltése();
            }
            else
            {
                GombLathatosagKezelo.Beallit(this, Program.Postás_Felhasználó.Szervezet);
                ÜzemekfeltöltéseÚj();
            }

            if (Válasz != 0)
                this.Text = $"{Válasz} számú üzenetre válasz";
            else
                this.Text = "Új üzenet írása";
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                Btnrögzítés.Visible = false;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                int melyikelem = 200;
                // módosítás 1

                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Btnrögzítés.Visible = true;
                }
                melyikelem = 200;
                // módosítás 2 főmérnökségi belépés és mindenhova tud írni
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    //Minden üzemhez tud írni, így nem kell gomb
                    GombokLátszanak(true);

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

        private void Ablak_Utasítás_Generálás_Load(object sender, EventArgs e)
        {
        }

        private void Btnrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txtírásimező.Text.Trim() == "") return;
                // megtisztítjuk a szöveget

                Txtírásimező.Text = Txtírásimező.Text.Replace('"', '°').Replace('\'', '°');

                Adat_Üzenet ADAT = new Adat_Üzenet(
                              0,
                              Txtírásimező.Text.Trim(),
                              Program.PostásNév.Trim(),
                              DateTime.Now,
                              Válasz);
                for (int i = 0; i < Üzemek.CheckedItems.Count; i++)
                    KézÜzenet.Rögzítés(Üzemek.CheckedItems[i].ToString(), DateTime.Today.Year, ADAT);

                MessageBox.Show($"Az üzenet rögzítése megtörtént!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Üzemekfeltöltése()
        {
            try
            {
                Üzemek.Items.Clear();
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKönyvtár.Lista_Adatok().OrderBy(a => a.Név).ToList();
                foreach (Adat_Kiegészítő_Könyvtár adat in Adatok)
                {
                    Üzemek.Items.Add(adat.Név);
                }

                for (int i = 0; i < Üzemek.Items.Count; i++)
                {
                    if (Üzemek.Items[i].ToStrTrim() == Telephely.Trim()) Üzemek.SetItemChecked(i, true);
                }
                Üzemek.Enabled = false;
                Üzemek.Visible = true;

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

        private void ÜzemekfeltöltéseÚj()
        {
            try
            {
                Üzemek.Items.Clear();
                foreach (string adat in GombLathatosagKezelo.Telephelyek(this.Name))
                {
                    Üzemek.Items.Add(adat);
                }

                for (int i = 0; i < Üzemek.Items.Count; i++)
                {
                    if (Üzemek.Items[i].ToStrTrim() == Telephely.Trim()) Üzemek.SetItemChecked(i, true);
                }
                Üzemek.Enabled = Üzemek.Items.Count > 1;

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


        #region Gombok
        private void GombokLátszanak(bool Látszik)
        {
            Üzemek.Enabled = Látszik;
            MindKijelöl.Visible = Látszik;
            MindVissza.Visible = Látszik;
            ISzak.Visible = Látszik;
            IISzak.Visible = Látszik;
            IIISzak.Visible = Látszik;
            Üzemek.Visible = Látszik;
        }

        private void MindKijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Üzemek.Items.Count; i++)
                Üzemek.SetItemChecked(i, true);

        }

        private void MindVissza_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Üzemek.Items.Count; i++)
                Üzemek.SetItemChecked(i, false);
        }

        private void ISzak_Click(object sender, EventArgs e)
        {
            Telepek("I. Vontatási");
        }

        private void IISzak_Click(object sender, EventArgs e)
        {
            Telepek("II. Vontatási");
        }

        private void IIISzak_Click(object sender, EventArgs e)
        {
            Telepek("III. Vontatási");
        }

        private void Telepek(string Szolgálatnév)
        {
            List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgTelep.Lista_Adatok();
            List<Adat_Kiegészítő_Szolgálattelepei> EgySzolg = Adatok.Where(a => a.Szolgálatnév.Trim() == Szolgálatnév).ToList();
            for (int j = 0; j < Üzemek.Items.Count; j++)
                if (EgySzolg.Any(a => a.Telephelynév == Üzemek.Items[j].ToStrTrim())) Üzemek.SetItemChecked(j, true);
        }
        #endregion

    }
}
