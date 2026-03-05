using InputForms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos.V_Ablakok._6_Kiadási_adatok
{
    public class Vendég_Járművek_Karbantartása
    {
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

            List<string> Típusok = new List<string>();

            Típusok = (from a in Adatok
                       where a.Törölt == false
                       orderby a.Valóstípus
                       select a.Valóstípus).Distinct().ToList();
            Típusok.Insert(0, "");

            Ablak = new Form();

            // Változók a metóduson belül
            var tipusMezo = new InputSelect("Járműtípus : ", Típusok).SetWidth(200).SetHeight(26).SetMaxDropDownItems();
            var azonositoMezo = new InputSelect("Pályaszám: ", Azonosítók).SetWidth(200).SetHeight(26).SetMaxDropDownItems();

            // Eseménykezelés: Ha változik a típus, szűrjük az azonosítókat
            tipusMezo.SelectedIndexChanged += (s, e) => {
                string kivalasztottTipus = tipusMezo.Value.ToString();

                List<string> szurtAzonositok;
                if (string.IsNullOrEmpty(kivalasztottTipus))
                {
                    szurtAzonositok = Adatok.Where(a => !a.Törölt).Select(a => a.Azonosító).OrderBy(x => x).ToList();
                }
                else
                {
                    szurtAzonositok = Adatok.Where(a => !a.Törölt && a.Valóstípus == kivalasztottTipus)
                                            .Select(a => a.Azonosító)
                                            .OrderBy(x => x).ToList();
                }
                szurtAzonositok.Insert(0, "");
                azonositoMezo.UpdateOptions(szurtAzonositok);
            };

            form = new InputForm(Ablak);
            form.Add("Típus",  tipusMezo)
                .Add("Azonosító", azonositoMezo)
                .Add("KiadóTelephely", (new InputSelect("Kiadó telephely: ", Telephelyek).SetWidth(200).SetHeight(26)))
                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Rögzítés")
                .OnSubmit(() => { Rögzítés(); });

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

        public void Rögzítés()
        {
            Ablak.Close();
        }


    }
}
