using InputForms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok._6_Kiadási_adatok
{
    public class Vendég_Járművek_Karbantartása
    {
        readonly Kezelő_Jármű_Vendég Kéz = new Kezelő_Jármű_Vendég();
        InputForm form;
        Form Ablak;
        public void Módosítás(List<Adat_Jármű> Adatok, List<Adat_Jármű_Vendég> AdatokTelep)
        {
            List<string> Azonosítók = new List<string>();
            Azonosítók = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Azonosító
                          select a.Azonosító).ToList();
            Azonosítók.Insert(0, "");
            List<string> Telephelyek = new List<string>();

            Telephelyek = (from a in Adatok
                           where a.Törölt == false
                           orderby a.Üzem
                           select a.Üzem).Distinct().ToList();
            Telephelyek.Insert(0, "");

            List<string> BázisTelephelyek = new List<string>();

            BázisTelephelyek = (from a in Adatok
                           where a.Törölt == false
                           orderby a.Üzem
                           select a.Üzem).Distinct().ToList();
            BázisTelephelyek.Insert(0, "");

            List<string> Típusok = new List<string>();

            Típusok = (from a in Adatok
                       where a.Törölt == false
                       orderby a.Valóstípus
                       select a.Valóstípus).Distinct().ToList();
            Típusok.Insert(0, "");

            Ablak = new Form();

            // Változók a metóduson belül
            InputSelect tipusMezo = new InputSelect("Járműtípus : ", Típusok).SetWidth(200).SetHeight(26).SetMaxDropDownItems();
            InputSelect azonositoMezo = new InputSelect("Pályaszám: ", Azonosítók).SetWidth(200).SetHeight(26).SetMaxDropDownItems();
            InputSelect bazistelephely = new InputSelect("Bázis telephely: ", BázisTelephelyek).SetWidth(200).SetHeight(26).SetMaxDropDownItems();
       

            // Eseménykezelés: Meghívjuk a kiszervezett eljárást
            tipusMezo.SelectedIndexChanged += (s, e) => FrissitAzonositok(tipusMezo, azonositoMezo, Adatok);

            // Amikor kiválasztanak egy pályaszámot, a bázis telephely ugorjon az üzemére
            azonositoMezo.SelectedIndexChanged += (s, e) => FrissitBazisTelephely(azonositoMezo, bazistelephely, Adatok);


            form = new InputForm(Ablak);
            form.Add("Típus",  tipusMezo)
                .Add("Azonosító", azonositoMezo)
                .Add("KiadóTelephely", (new InputSelect("Kiadó telephely: ", Telephelyek).SetWidth(200).SetHeight(26)))
                .Add("BázisTelephely", bazistelephely)
                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Rögzítés")
                .OnSubmit(() => {
                   string Azonosító = form["Azonosító"];
                   string Típus = form["Típus"];
                   string KiadóTelephely = form["KiadóTelephely"];
                   string BázisTelephely = form["BázisTelephely"];


                    Adat_Jármű_Vendég Adat = new Adat_Jármű_Vendég(Azonosító, Típus, BázisTelephely, KiadóTelephely);
                    // töröljük, ha üresre van állítva
                    if ((KiadóTelephely == null || KiadóTelephely.Trim() == "") || (BázisTelephely.Trim() == KiadóTelephely.Trim()))
                        Kéz.Törlés(Adat);
                    else
                        // JAVÍTANDÓ:

                        Kéz.Rögzítés(Adat);
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Ablak.Close();
                });

            //Ablak beállítások
            Ablak.Width = form.Width + 40;
            Ablak.Height = form.Height + 60;
            Ablak.Text = "Kiadó telephely beállítása";
            Ablak.Icon = Properties.Resources.ProgramIkon;
            Ablak.StartPosition = FormStartPosition.CenterScreen;
            Ablak.FormBorderStyle = FormBorderStyle.FixedDialog;
            Ablak.MaximizeBox = false;
            Ablak.Show();
        }

        private void FrissitAzonositok(InputSelect tipusMezo, InputSelect azonositoMezo, List<Adat_Jármű> Adatok)
        {
            string kivalasztottTipus = tipusMezo.Value?.ToString() ?? "";

            List<string> szurtAzonositok;
            if (string.IsNullOrEmpty(kivalasztottTipus))
            {
                szurtAzonositok = Adatok.Where(a => !a.Törölt)
                                        .Select(a => a.Azonosító)
                                        .OrderBy(x => x).ToList();
            }
            else
            {
                szurtAzonositok = Adatok.Where(a => !a.Törölt && a.Valóstípus == kivalasztottTipus)
                                        .Select(a => a.Azonosító)
                                        .OrderBy(x => x).ToList();
            }

            szurtAzonositok.Insert(0, "");
            azonositoMezo.UpdateOptions(szurtAzonositok);
        }

        private void FrissitBazisTelephely(InputSelect azonositoMezo, InputSelect bazisMezo, List<Adat_Jármű> Adatok)
        {
            string kivalasztottAzonosito = azonositoMezo.Value?.ToString();

            if (!string.IsNullOrEmpty(kivalasztottAzonosito))
            {
                // Megkeressük az adott azonosítóhoz tartozó üzemet
                var jarmu = Adatok.FirstOrDefault(a => a.Azonosító == kivalasztottAzonosito);
                if (jarmu != null)
                {
                    // Beállítjuk a választómező értékét az üzemre
                    bazisMezo.Value = jarmu.Üzem;
                }
            }
        }

    }
}
