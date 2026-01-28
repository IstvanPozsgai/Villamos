using InputForms;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos
{
    public class Hibanapló_Részletes
    {
        InputForm form;
        Form Ablak;

        public void RészletesAdatok(Adat_Hiba AdatSor)
        {
            // Dátum;Idő;Telephely;Felhasználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; TeljesIdő
            Ablak = new Form();

            form = new InputForm(Ablak);
            form.Add("Dátum", (new InputDate("Dátum:", AdatSor.Dátum.ToÉrt_DaTeTime()).SetWidth(100).SetHeight(26)))
                .Add("Idő", (new InputTime("Idő: ", AdatSor.Idő.ToÉrt_DaTeTime()).SetWidth(100).SetHeight(26)))
                .Add("Telephely", (new InputTextbox("Telephely: ", AdatSor.Telephely).SetWidth(200).SetHeight(26)))
                .Add("Felhasználó", (new InputTextbox("Felhasználó: ", AdatSor.Felhasználó).SetWidth(200).SetHeight(26)))
                .Add("HibaÜzenet", (new InputTextbox("Hiba üzenet:", AdatSor.HibaÜzenet).SetWidth(600).SetHeight(78)).FüggőlegesGörgetés())
                .Add("HibaOsztály", (new InputTextbox("Hiba Osztály: ", AdatSor.HibaOsztály)).SetHeight(150).SetWidth(600).FüggőlegesGörgetés())
                .Add("HibaMetódus", (new InputTextbox("Hiba Metódus: ", AdatSor.HibaMetódus)).SetHeight(150).SetWidth(600).FüggőlegesGörgetés())
                .Add("Névtér", (new InputTextbox("Névtér :", AdatSor.Névtér).SetWidth(600).SetHeight(26)))
                .Add("Egyéb", (new InputTextbox("Egyéb: ", AdatSor.Egyéb).SetWidth(600).SetHeight(26)))
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

        public void Close()
        {
            Ablak?.Close();
        }
    }
}
