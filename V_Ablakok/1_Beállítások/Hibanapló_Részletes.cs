using InputForms;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Kezelők;

namespace Villamos
{
    public class Hibanapló_Részletes
    {

        InputForm form;
        readonly Kezelő_Szolgáltató KézSzolg = new Kezelő_Szolgáltató();

        public void RészletesAdatok(List<string> AdatSor)
        {
            // Dátum;Idő;Telephely;Felhasználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; TeljesIdő
            Form Ablak = new Form();

            form = new InputForm(Ablak);
            form.Add("Dátum", (new InputTextbox("Dátum:", AdatSor[0]).SetWidth(100).SetHeight(26)))
                .Add("Idő", (new InputTextbox("Idő: ", AdatSor[1]).SetWidth(100).SetHeight(26)))
                .Add("Telephely", (new InputTextbox("Telephely: ", AdatSor[2]).SetWidth(600).SetHeight(26)))
                .Add("Felhasználó", (new InputTextbox("Felhasználó: ", AdatSor[3]).SetWidth(600).SetHeight(26)))
                .Add("HibaÜzenet", (new InputTextbox("Hiba üzenet:", AdatSor[4]).SetWidth(600).SetHeight(26)))
                .Add("HibaOsztály", (new InputTextbox("Hiba Osztály: ", AdatSor[5])).SetHeight(150).SetWidth(600))
                .Add("HibaMetódus", (new InputTextbox("Hiba Metódus: ", AdatSor[6])).SetHeight(150).SetWidth(600))
                .Add("Névtér", (new InputTextbox("Névtér :", AdatSor[7]).SetWidth(600).SetHeight(26)))
                .Add("Egyéb", (new InputTextbox("Egyéb: ", AdatSor[8]).SetWidth(600).SetHeight(26)))
                .Add("TeljesIdő", (new InputTextbox("TeljesIdő: ", AdatSor[9]).SetWidth(600).SetHeight(26)))

                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Bezár")
                .OnSubmit(() => { Ablak.Close(); });

            //Ablak beállítások
            Ablak.Width = form.Width + 40;
            Ablak.Height = form.Height + 60;
            Ablak.Text = "Hiba részletes adatai";
            Ablak.Icon = Properties.Resources.ProgramIkon;
            Ablak.StartPosition = FormStartPosition.CenterScreen;
            Ablak.FormBorderStyle = FormBorderStyle.FixedDialog;
            Ablak.MaximizeBox = false;
            Ablak.Show();
        }
    }
}
