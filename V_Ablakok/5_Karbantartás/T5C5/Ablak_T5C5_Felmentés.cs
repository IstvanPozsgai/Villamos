using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyO = Microsoft.Office.Interop.Outlook;

namespace Villamos.V_Ablakok._5_Karbantartás.T5C5
{
    public partial class Ablak_T5C5_Felmentés : Form
    {
        readonly Kezelő_Kiegészítő_Felmentés KézFelmentés = new Kezelő_Kiegészítő_Felmentés();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés Mérés_kéz = new Kezelő_Kerék_Mérés();
        readonly Kezelő_T5C5_Kmadatok KézVkm = new Kezelő_T5C5_Kmadatok("T5C5");

        List<Adat_T5C5_Kmadatok> AdatokVkm = new List<Adat_T5C5_Kmadatok>();
        public string Telephely { get; private set; }

        public Ablak_T5C5_Felmentés(string telephely)
        {
            InitializeComponent();
            Telephely = telephely;
        }


        private void Ablak_T5C5_Felmentés_Load(object sender, EventArgs e)
        {

        }

        private void Email_Click(object sender, EventArgs e)
        {
            try
            {
                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
                string Tábla_html;

                // címzett
                mail.To = Címzett.Text.Trim();

                mail.CC = Másolat.Text.Trim(); // másolatot kap

                string szöveg = Tárgy.Text.Trim();
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.Subject = szöveg.Trim(); // üzenet tárgya

                // üzent szövege
                mail.HTMLBody = "<html><body> <p> ";
                // üzent szövege
                szöveg = Bevezetés.Text.Trim() + "<br>";
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.HTMLBody += szöveg + "</p>";

                // Table start.
                // Adding fejléc.
                Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
                foreach (DataGridViewColumn column in Vizs_tábla.Columns)
                    Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
                Tábla_html += "</tr>";
                // Adding adatsorok.
                foreach (DataGridViewRow row in Vizs_tábla.Rows)
                {
                    Tábla_html += "<tr>";

                    foreach (DataGridViewCell cell in row.Cells)
                        Tábla_html += "<td style='width:120px;border: 1px solid #ccc'>" + cell.Value.ToString() + "</td>";

                    Tábla_html += "</tr>";
                }
                Tábla_html += "</table>";
                // Table end.
                mail.HTMLBody += Tábla_html;

                szöveg = "<p>" + Tárgyalás.Text.Trim() + "<br>";
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.HTMLBody += szöveg + "</p>";


                // Table start.
                // Adding fejléc.
                Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
                foreach (DataGridViewColumn column in Keréktábla.Columns)
                    Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
                Tábla_html += "</tr>";
                // Adding adatsorok.
                foreach (DataGridViewRow row in Keréktábla.Rows)
                {
                    Tábla_html += "<tr>";

                    foreach (DataGridViewCell cell in row.Cells)
                        Tábla_html += "<td style='width:120px;border: 1px solid #ccc'>" + cell.Value.ToString() + "</td>";

                    Tábla_html += "</tr>";
                }
                Tábla_html += "</table>";
                // Table end.
                mail.HTMLBody += Tábla_html;
                szöveg = "<p>" + Befejezés.Text.Trim() + "<br>";
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.HTMLBody += szöveg;

                mail.HTMLBody += "</p></body></html>  ";

                // outlook
                mail.Importance = MyO.OlImportance.olImportanceNormal;
                ((MyO._MailItem)mail).Send();

                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Berendezés_adatok()
        {
            try
            {
                if (Ciklus_Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A Pályaszám beviteli mező nem lehet üres");

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Azonosító == Ciklus_Pályaszám.Text.Trim()
                          && a.Objektumfajta == "V.KERÉKPÁR"
                          orderby a.Pozíció
                          select a).ToList();

                List<Adat_Kerék_Mérés> AdatokMérés = Mérés_kéz.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Mérés> Ideig = Mérés_kéz.Lista_Adatok(DateTime.Today.Year - 1);
                AdatokMérés.AddRange(Ideig);
                AdatokMérés = (from a in AdatokMérés
                               orderby a.Kerékberendezés ascending, a.Mikor descending
                               select a).ToList();

                Keréktábla.Rows.Clear();
                Keréktábla.Columns.Clear();
                Keréktábla.Refresh();
                Keréktábla.Visible = false;
                Keréktábla.ColumnCount = 8;

                // fejléc elkészítése
                Keréktábla.Columns[0].HeaderText = "Psz";
                Keréktábla.Columns[0].Width = 50;
                Keréktábla.Columns[1].HeaderText = "Berendezésszám";
                Keréktábla.Columns[1].Width = 150;
                Keréktábla.Columns[2].HeaderText = "Gyári szám";
                Keréktábla.Columns[2].Width = 100;
                Keréktábla.Columns[3].HeaderText = "Pozíció";
                Keréktábla.Columns[3].Width = 100;
                Keréktábla.Columns[4].HeaderText = "Mérés Dátuma";
                Keréktábla.Columns[4].Width = 170;
                Keréktábla.Columns[5].HeaderText = "Állapot";
                Keréktábla.Columns[5].Width = 100;
                Keréktábla.Columns[6].HeaderText = "Méret";
                Keréktábla.Columns[6].Width = 100;
                Keréktábla.Columns[7].HeaderText = "Megnevezés";
                Keréktábla.Columns[7].Width = 300;

                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {
                    Keréktábla.RowCount++;
                    int i = Keréktábla.RowCount - 1;
                    Keréktábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Keréktábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés;
                    Keréktábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám;
                    Keréktábla.Rows[i].Cells[3].Value = rekord.Pozíció;
                    Keréktábla.Rows[i].Cells[7].Value = rekord.Kerékmegnevezés;
                    Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                              where a.Kerékberendezés == rekord.Kerékberendezés
                                              select a).FirstOrDefault();
                    if (Mérés != null)
                    {
                        Keréktábla.Rows[i].Cells[4].Value = Mérés.Mikor;
                        Keréktábla.Rows[i].Cells[5].Value = Mérés.Állapot.Trim();
                        Keréktábla.Rows[i].Cells[6].Value = Mérés.Méret;
                    }
                }

                Keréktábla.Visible = true;
                Keréktábla.Refresh();
                Keréktábla.Sort(Keréktábla.Columns[3], System.ComponentModel.ListSortDirection.Ascending);
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

        private void CiklusFrissít_Click(object sender, EventArgs e)
        {
            KMU_kiírása();
            Berendezés_adatok();
            Kiirjaatörténelmet();
        }

        private void KMU_kiírása()
        {
            try
            {
                if (Ciklus_Pályaszám.Text.Trim() == "") return;
                V_km_adatok_lista();
                Adat_T5C5_Kmadatok ElemKm = (from a in AdatokVkm
                                             where a.Azonosító == Ciklus_Pályaszám.Text.Trim()
                                             && a.Törölt == false
                                             orderby a.Vizsgdátumk descending
                                             select a).FirstOrDefault();
                J_tőlFutott.Text = ElemKm.KMUkm.ToString();
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

        private void Kiirjaatörténelmet()
        {
            try
            {
                V_km_adatok_lista();
                List<Adat_T5C5_Kmadatok> Adatok = (from a in AdatokVkm
                                                   where a.Azonosító == Ciklus_Pályaszám.Text.Trim()
                                                   && a.Törölt == false
                                                   orderby a.Vizsgdátumv descending
                                                   select a).ToList();
                Vizs_tábla.Rows.Clear();
                Vizs_tábla.Columns.Clear();
                Vizs_tábla.Refresh();
                Vizs_tábla.Visible = false;
                Vizs_tábla.ColumnCount = 5;

                // fejléc elkészítése
                Vizs_tábla.Columns[0].HeaderText = "Ssz.";
                Vizs_tábla.Columns[0].Width = 80;
                Vizs_tábla.Columns[1].HeaderText = "Psz";
                Vizs_tábla.Columns[1].Width = 80;
                Vizs_tábla.Columns[2].HeaderText = "Vizsg. foka";
                Vizs_tábla.Columns[2].Width = 80;
                Vizs_tábla.Columns[3].HeaderText = "Vizsg. Ssz.";
                Vizs_tábla.Columns[3].Width = 80;
                Vizs_tábla.Columns[4].HeaderText = "Vizsg. Vége";
                Vizs_tábla.Columns[4].Width = 110;

                int i;

                foreach (Adat_T5C5_Kmadatok rekord in Adatok)
                {
                    if (rekord.Vizsgfok.Contains("V2") || rekord.Vizsgfok.Contains("V3") || rekord.Vizsgfok.Contains("J"))
                    {
                        Vizs_tábla.RowCount++;
                        i = Vizs_tábla.RowCount - 1;
                        Vizs_tábla.Rows[i].Cells[0].Value = rekord.ID;
                        Vizs_tábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                        Vizs_tábla.Rows[i].Cells[2].Value = rekord.Vizsgfok;
                        Vizs_tábla.Rows[i].Cells[3].Value = rekord.Vizsgsorszám;
                        Vizs_tábla.Rows[i].Cells[4].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                    }
                    if (rekord.Vizsgsorszám == 0)
                        break;
                }
                Vizs_tábla.Visible = true;
                Vizs_tábla.Sort(Vizs_tábla.Columns[4], System.ComponentModel.ListSortDirection.Ascending);
                Vizs_tábla.Refresh();
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

        private void Ciklus_Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Címzett.Text.Trim() == "") throw new HibásBevittAdat("A Címzett mező nem lehet üres.");
                if (CiklusTípus.Text.Trim() == "") throw new HibásBevittAdat("A Ciklus típus mező nem lehet üres.");
                if (Másolat.Text.Trim() == "") Másolat.Text = "_";
                if (Tárgy.Text.Trim() == "") throw new HibásBevittAdat("A Tárgy mező nem lehet üres.");
                if (Kért_vizsgálat.Text.Trim() == "") throw new HibásBevittAdat("A Kért vizsgálat mező nem lehet üres.");
                if (Bevezetés.Text.Trim() == "") throw new HibásBevittAdat("A Bevezetés mező nem lehet üres.");
                if (Tárgyalás.Text.Trim() == "") throw new HibásBevittAdat("A Tárgyalás mező nem lehet üres.");
                if (Befejezés.Text.Trim() == "") throw new HibásBevittAdat("A Befejezés mező nem lehet üres.");
                if (!int.TryParse(Felmentés_Id.Text, out int Id)) Id = 0;

                List<Adat_Kiegészítő_Felmentés> Adatok = KézFelmentés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Kiegészítő_Felmentés Elem = (from a in Adatok
                                                  where a.CiklusTípus == CiklusTípus.Text.Trim()
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Felmentés ADAT = new Adat_Kiegészítő_Felmentés(
                                          Id,
                                          Címzett.Text.Trim(),
                                          Másolat.Text.Trim(),
                                          Tárgy.Text.Trim(),
                                          Kért_vizsgálat.Text.Trim(),
                                          Bevezetés.Text.Trim(),
                                          Tárgyalás.Text.Trim(),
                                          Befejezés.Text.Trim(),
                                          CiklusTípus.Text.Trim());
                if (Elem != null)
                    KézFelmentés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézFelmentés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                MessageBox.Show("Az adatok Mentése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Felmentés_kiírás()
        {
            List<Adat_Kiegészítő_Felmentés> Adatok = KézFelmentés.Lista_Adatok(Cmbtelephely.Text.Trim());

            Adat_Kiegészítő_Felmentés rekord = (from a in Adatok
                                                where a.CiklusTípus == CiklusTípus.Text.Trim()
                                                select a).FirstOrDefault();

            if (rekord != null)
            {
                Címzett.Text = rekord.Címzett;
                Másolat.Text = rekord.Másolat;
                Tárgy.Text = rekord.Tárgy;
                Kért_vizsgálat.Text = rekord.Kértvizsgálat;
                Bevezetés.Text = rekord.Bevezetés;
                Tárgyalás.Text = rekord.Tárgyalás;
                Befejezés.Text = rekord.Befejezés;
                CiklusTípus.Text = rekord.CiklusTípus;
                Felmentés_Id.Text = rekord.Id.ToString();
            }
            else
            {
                MessageBox.Show("Ehhez a Ciklus típushoz nincsenek még beállítva adatok!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void V_km_adatok_lista()
        {
            try
            {
                AdatokVkm.Clear();
                AdatokVkm = KézVkm.Lista_Adatok();
                AdatokVkm = (from a in AdatokVkm
                             where a.Törölt == false
                             orderby a.Azonosító ascending, a.Vizsgdátumk descending
                             select a).ToList();
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
