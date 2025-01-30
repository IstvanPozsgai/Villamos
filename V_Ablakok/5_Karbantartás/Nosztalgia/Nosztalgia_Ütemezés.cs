using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public class Nosztalgia_Ütemezés : Ablak_Nosztalgia
    {
        #region Kezelők-Listák
        Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();

        Kezelő_Nosztalgia_Állomány KézÁllomány = new Kezelő_Nosztalgia_Állomány();
        List<Adat_Nosztalgia_Állomány> AdatokÁllomány = new List<Adat_Nosztalgia_Állomány>();

        Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();

        Kezelő_Vezénylés KézVez = new Kezelő_Vezénylés();
        List<Adat_Vezénylés> AdatokVez = new List<Adat_Vezénylés>();

        private void ListaFeltöltés()
        {
            string hely = Application.StartupPath + @"\" + Cmbtelephely.Text + @"\adatok\villamos\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = $"SELECT * FROM állománytábla";
            AdatokJármű?.Clear();
            AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
            jelszó = "kloczkal";
            szöveg = $"SELECT * FROM Állomány";
            AdatokÁllomány?.Clear();
            AdatokÁllomány = KézÁllomány.Lista_Adat(hely, jelszó, szöveg);

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Ciklus.mdb";
            jelszó = "pocsaierzsi";
            szöveg = $"SELECT * FROM Ciklusrendtábla";
            AdatokCiklus?.Clear();
            AdatokCiklus = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\" + Dátum_ütem.Value.ToString("yyyy") + @"\vezénylés" + Dátum_ütem.Value.ToString("yyyy") + ".mdb";
            jelszó = "tápijános";
            szöveg = "SELECT * FROM vezényléstábla";
            AdatokVez = KézVez.Lista_Adatok(hely, jelszó, szöveg);
        }
        #endregion

        public void NosztalgiaÜtemezés()
        {
            try
            {
                ListaFeltöltés();

                Holtart.Visible = true;
                Holtart.Maximum = 100;

                AdatokVez = (from a in AdatokVez
                             where a.Típus.Contains("noszt")
                             select a).ToList();

                foreach (Adat_Vezénylés rekord in AdatokVez)
                {

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
        private void Terv_lezárás_Nosztalgia()
        {
            try
            {

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
