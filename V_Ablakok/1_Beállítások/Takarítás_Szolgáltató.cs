using InputForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Properties;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos
{
    public class Takarítás_Szolgáltató

    {
        InputForm form;
        readonly Kezelő_Szolgáltató KézSzolg = new Kezelő_Szolgáltató();

        public void SzolgálatatóMódosítás() 
        {
            List<Adat_Szolgáltató> AdatokSzolgáltató = KézSzolg.Lista_Adatok();
            Adat_Szolgáltató TakarítóCég = AdatokSzolgáltató.Where (a=>a.ID ==1).FirstOrDefault ();
            Form Ablak = new Form();

            form = new InputForm(Ablak);
            form.Add("SzerződésSzám", (new InputTextbox("Szerződés száma:", TakarítóCég.SzerződésSzám, 50)).AddRule(null))
                .Add("IratEleje", (new InputTextbox("IratEleje", TakarítóCég.IratEleje, 500)).AddRule(null))
                .Add("IratVége", (new InputTextbox("IratVége", TakarítóCég.IratVége, 500)).AddRule(null))
                .Add("Aláíró", (new InputTextbox("Aláíró", TakarítóCég.Aláíró, 50)).AddRule(null))
                .Add("CégNévAlá", (new InputTextbox("CégNévAlá", TakarítóCég.CégNévAlá, 50)).AddRule(null))
                .Add("CégCím", (new InputTextbox("CégCím", TakarítóCég.CégCím, 50)).AddRule(null))
                .Add("CégAdó", (new InputTextbox("CégAdó", TakarítóCég.CégAdó, 50)).AddRule(null))
                .Add("CégHosszúNév", (new InputTextbox("CégHosszúNév", TakarítóCég.CégHosszúNév, 50)).AddRule(null))
                .Add("Cégjegyzékszám", (new InputTextbox("Cégjegyzékszám", TakarítóCég.Cégjegyzékszám, 50)).AddRule(null))
                .Add("CsoportAzonosító", (new InputTextbox("CsoportAzonosító", TakarítóCég.CsoportAzonosító, 50)).AddRule(null))

                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Rögzítés")
                .OnSubmit(() =>
                {
                    string SzerződésSzám = form["SzerződésSzám"];
                    string IratEleje = form["IratEleje"];
                    string IratVége = form["IratVége"];
                    string Aláíró = form["Aláíró"];
                    string CégNévAlá = form["CégNévAlá"];
                    string CégCím = form["CégCím"];
                    string CégAdó = form["CégAdó"];
                    string CégHosszúNév = form["CégHosszúNév"];
                    string Cégjegyzékszám = form["Cégjegyzékszám"];
                    string CsoportAzonosító = form["CsoportAzonosító"];
                    Adat_Szolgáltató ADAT = new Adat_Szolgáltató
                   (    
                        1,
                        SzerződésSzám,
                        IratEleje,
                        IratVége,
                        Aláíró,
                        CégNévAlá,
                        CégCím,
                        CégAdó,
                        CégHosszúNév,
                        Cégjegyzékszám,
                        CsoportAzonosító
                    );
                    KézSzolg.Módosítás(ADAT);
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

            //Ablak beállítások
            Ablak.Width = form.Width + 40;
            Ablak.Height = form.Height + 60;
            Ablak.Text = "Takarítási szerződés Szolgáltató adatok módosítása";
            Ablak.Icon = Properties.Resources.ProgramIkon;
            Ablak.StartPosition = FormStartPosition.CenterScreen;
            Ablak.FormBorderStyle = FormBorderStyle.FixedDialog;
            Ablak.MaximizeBox = false;
            Ablak.Show();
        }
    }
}
