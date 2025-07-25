﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{

    public partial class Ablak_Kerék_segéd : Form
    {
        public event Event_Kidobó Változás;
        public string CmbTelephely { get; private set; }
        public string Pályaszám { get; private set; }
        public int Tengely_darab { get; private set; }
        public int Proritás_db { get; private set; }
        public string Típus { get; private set; }

        public int Norma { get; private set; }

        readonly Kezelő_Kerék_Eszterga_Igény Kéz = new Kezelő_Kerék_Eszterga_Igény();

        public Ablak_Kerék_segéd(string cmbTelephely, string pályaszám, int tengely_darab, int proritás_db, string típus, int norma)
        {
            Tengely_darab = tengely_darab;
            Proritás_db = proritás_db;
            Pályaszám = pályaszám;
            CmbTelephely = cmbTelephely;
            Típus = típus;
            Norma = norma;
            InitializeComponent();
        }

        private void Ablak_Kerék_segéd_Load(object sender, EventArgs e)
        {
            try
            {

                Pályaszámok.Text = Pályaszám.Trim();
                Telephely.Text = CmbTelephely.Trim();
                string[] darabol = Pályaszám.Split('-');
                Szerelvény_db.Text = darabol.Length.ToString();
                Prioritás.Text = Proritás_db.ToString();
                Tengely_db.Text = Tengely_darab.ToString();
                NormaIdő.Text = Norma.ToString();

                List<Adat_Kerék_Eszterga_Igény> Adatok = Kéz.Lista_Adatok(DateTime.Today.Year);

                string ellenőr = string.Join("-", (from elem in darabol
                                                   where Adatok.Any(a => a.Státus < 7 && a.Pályaszám.Contains(elem))
                                                   select elem).ToList());

                if (ellenőr != "")
                {
                    MessageBox.Show($"Ebben a szerelvényben lévő pályaszámok közül {ellenőr} már van esztergálási igényben.", "Rögzítési feltétel nem megfelelő", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Btnakurögzít_Click(object sender, EventArgs e)
        {
            try
            {
                // ellenőrzés, hogy már szerepel-e valamelyik kocsi az igénylésben
                string[] darabol = Pályaszám.Split('-');

                if (Szerelvény_db.Text.Trim() == "" || !int.TryParse(Szerelvény_db.Text.Trim(), out int szerelvény))
                    throw new HibásBevittAdat("A szerelvényszám mezőnek egész számnak kell lennie");

                if (Tengely_db.Text.Trim() == "" || !int.TryParse(Tengely_db.Text.Trim(), out int tengely))
                    throw new HibásBevittAdat("A tengelyek száma mezőnek egész számnak kell lennie");

                if (Prioritás.Text.Trim() == "" || !int.TryParse(Prioritás.Text.Trim(), out int prioritás))
                    throw new HibásBevittAdat("A prioritás mezőnek egész számnak kell lennie");

                Adat_Kerék_Eszterga_Igény adat = new Adat_Kerék_Eszterga_Igény(
                    Pályaszám,
                    MyF.Szöveg_Tisztítás(Megjegyzés.Text.Trim(), 0, -1, true),
                    DateTime.Now,
                    Program.PostásNév.Trim(),
                    tengely,
                    szerelvény,
                    prioritás,
                    new DateTime(1900, 1, 1),
                    0, // státusz
                    CmbTelephely.Trim(),
                    Típus.Trim(),
                    Norma);
                Kéz.Rögzítés(DateTime.Today.Year, adat);

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
    }
}
