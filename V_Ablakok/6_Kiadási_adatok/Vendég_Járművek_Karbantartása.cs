using InputForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok._6_Kiadási_adatok
{
    public class Vendég_Járművek_Karbantartása
    {
        public event System.Action AdatokFrissültek;
        readonly Kezelő_Jármű_Vendég Kéz = new Kezelő_Jármű_Vendég();
        InputForm form;
        Form Ablak;
        public void Beviteli_Ablak(List<Adat_Jármű> Adatok, List<Adat_Jármű_Vendég> AdatokTelep)
        {
            List<string> Azonosítók = Pályaszámok(Adatok);
            List<string> Telephelyek = Üzemek(Adatok);
            List<string> Típusok = TípusLista(Adatok);
            string Honos = "";

            Ablak = new Form();

            // Változók a metóduson belül
            InputSelect tipusMezo = new InputSelect("Járműtípus : ", Típusok).SetWidth(200).SetHeight(26).SetMaxDropDownItems();
            InputSelect azonositoMezo = new InputSelect("Pályaszám: ", Azonosítók).SetWidth(200).SetHeight(26).SetMaxDropDownItems();
            InputSelect kiadómező = new InputSelect("Kiadó telephely: ", Telephelyek).SetWidth(200).SetHeight(26).SetMaxDropDownItems().WithValue(Program.PostásTelephely);
            InputTextbox bazistelephely = new InputTextbox("Állományi Telephely: ", Honos).SetWidth(200).SetHeight(26).Enabled(false);

            // Eseménykezelés: Meghívjuk a kiszervezett eljárást
            tipusMezo.SelectedIndexChanged += (s, e) => FrissitAzonositok(tipusMezo, azonositoMezo, Adatok);
            // Amikor kiválasztanak egy pályaszámot, a bázis telephely ugorjon az üzemére
            azonositoMezo.SelectedIndexChanged += (s, e) => FrissitBazisTelephely(azonositoMezo, bazistelephely, kiadómező, Adatok);

            form = new InputForm(Ablak);
            form.Add("Típus", tipusMezo)
                .Add("Azonosító", azonositoMezo)
                .Add("KiadóTelephely", kiadómező)
                .Add("BázisTelephely", bazistelephely)
                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Rögzítés")
                .OnSubmit(() => Adatok_Rögzítése(Adatok, AdatokTelep));

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

        private void Adatok_Rögzítése(List<Adat_Jármű> Adatok, List<Adat_Jármű_Vendég> AdatokTelep)
        {
            try
            {
                string Azonosító = form["Azonosító"];
                Adat_Jármű Elem = Adatok.FirstOrDefault(a => a.Azonosító == Azonosító);

                if (Elem == null) throw new HibásBevittAdat("Nincs ilyen pályaszámú jármű az állományban!");
                string Típus = Elem.Valóstípus;
                string KiadóTelephely = form["KiadóTelephely"];
                string BázisTelephely = Elem.Üzem;
                Adat_Jármű_Vendég Adat = new Adat_Jármű_Vendég(Azonosító, Típus, BázisTelephely, KiadóTelephely);

                Adat_Jármű_Vendég AdatSzűrt = AdatokTelep.FirstOrDefault(a => a.Azonosító == Azonosító);
                if (AdatSzűrt != null) Kéz.Törlés(Adat);  //Ha van előzmény akkor azt töröljük, majd újra létrehozzuk a megadott értékekkel, ha nincs akkor csak létrehozzuk
                if (!((KiadóTelephely == null || KiadóTelephely.Trim() == "") || (BázisTelephely.Trim() == KiadóTelephely.Trim()))) Kéz.Rögzítés(Adat);
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Ablak.Close();
                AdatokFrissültek?.Invoke();
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

        private void FrissitBazisTelephely(InputSelect azonositoMezo, InputTextbox bazisMezo, InputSelect kiadómező, List<Adat_Jármű> Adatok)
        {
            string kivalasztottAzonosito = azonositoMezo.Value?.ToString();

            if (!string.IsNullOrEmpty(kivalasztottAzonosito))
            {
                // Megkeressük az adott azonosítóhoz tartozó üzemet
                Adat_Jármű jarmu = Adatok.FirstOrDefault(a => a.Azonosító == kivalasztottAzonosito);
                if (jarmu != null)
                {
                    // Beállítjuk a választómező értékét az üzemre
                    bazisMezo.Value = jarmu.Üzem;
                    List<string> szűrtÜzemek = new List<string>();
                    if (jarmu.Üzem.Trim() == Program.PostásTelephely)
                    {
                        szűrtÜzemek = Üzemek(Adatok);
                    }
                    else
                    {
                        szűrtÜzemek.Add(Program.PostásTelephely);
                        szűrtÜzemek.Insert(0, "");
                    }
                    kiadómező.UpdateOptions(szűrtÜzemek);
                }
            }
        }

        private List<string> Pályaszámok(List<Adat_Jármű> Adatok)
        {
            List<string> Pályaszámok = (from a in Adatok
                                        where a.Törölt == false
                                        orderby a.Azonosító
                                        select a.Azonosító).ToList();
            Pályaszámok.Insert(0, "");
            return Pályaszámok;

        }

        private List<string> Üzemek(List<Adat_Jármű> Adatok)
        {
            //Ha a honos telephelyen van akkor bárhova mehet, ha nincs akkor csak a postás telephelyre mehet

            List<string> Üzemek = (from a in Adatok
                                   where a.Törölt == false
                                   orderby a.Üzem
                                   select a.Üzem).Distinct().ToList();
            Üzemek.Insert(0, "");
            return Üzemek;
        }

        private List<string> TípusLista(List<Adat_Jármű> Adatok)
        {
            List<string> Típusok = (from a in Adatok
                                    where a.Törölt == false
                                    orderby a.Valóstípus
                                    select a.Valóstípus).Distinct().ToList();
            Típusok.Insert(0, "");
            return Típusok;
        }
    }
}
