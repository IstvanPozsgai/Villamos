using InputForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos
{
    public partial class Ablak_Hibanaplo : Form
    {
        private DataGridViewHelper<Adat_Hiba> Tábla;
        readonly List<Adat_Hiba> Adatok = new List<Adat_Hiba>();
        Hibanapló_Részletes Ablak;
        Adat_Hiba EgyAdat = new Adat_Hiba();

        public Ablak_Hibanaplo()
        {
            InitializeComponent();
            TáblázatBeállítás();
        }

        private void Ablak_Hibanaplo_Load(object sender, EventArgs e)
        {

        }

        private void TáblázatBeállítás()
        {
            List<Adat_Hiba_Elrendezés> Beállítás = new List<Adat_Hiba_Elrendezés>
            {
                new Adat_Hiba_Elrendezés{ Változó="Dátum", Felirat="Dátum", Szélesség=100},
                new Adat_Hiba_Elrendezés{ Változó="Idő", Felirat="Idő", Szélesség=85},
                new Adat_Hiba_Elrendezés{ Változó="Telephely", Felirat="Telephely", Szélesség=130},
                new Adat_Hiba_Elrendezés{ Változó="Felhasználó", Felirat="Felhasználó", Szélesség=115},
                new Adat_Hiba_Elrendezés{ Változó="HibaÜzenet", Felirat="Hiba üzenet", Szélesség=450},
                new Adat_Hiba_Elrendezés{ Változó="HibaOsztály", Felirat="Hiba osztály", Szélesség=300},
                new Adat_Hiba_Elrendezés{ Változó="HibaMetódus", Felirat="Hiba metódus", Szélesség=300},
                new Adat_Hiba_Elrendezés{ Változó="Névtér", Felirat="Névtér", Szélesség=100},
                new Adat_Hiba_Elrendezés{ Változó="Egyéb", Felirat="Egyéb", Szélesség=130},
                new Adat_Hiba_Elrendezés{ Változó="TeljesIdő", Felirat="TeljesIdő", Szélesség=50,Látható=false  },
            };
            AdatokFeltöltése();

            Tábla = new DataGridViewHelper<Adat_Hiba>(this)
               .SetLocationAndSize(5, 55, 770, 200)
               .SetAnchor(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom)
               .AddItems(Adatok)
               .ConfigureColumns(Beállítás)
               .ShowRowHeaders(false)
               .OnSelectionChanged(p => EgyAdat = p)

                   ;
        }

        private void AdatokFeltöltése()
        {
            try
            {
                Adatok.Clear();
                int ideiEv = DateTime.Now.Year;
                List<string> osszesSor = new List<string>();
                osszesSor.AddRange(ÉvesLogFajltBetolt(ideiEv).Skip(1));
                osszesSor.AddRange(ÉvesLogFajltBetolt(ideiEv - 1).Skip(1));

                foreach (string sor in osszesSor)
                {
                    // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum

                    string[] mezok = sor.Split(';');
                    string[] darabol = mezok[0].Split(' ');
                    string Dátum = darabol[0].ToÉrt_DaTeTime().ToString("yyyy.MM.dd");
                    string Idő = darabol[1].Replace(".", ":").ToÉrt_DaTeTime().ToString("HH:mm:ss");
                    Adat_Hiba Elem = new Adat_Hiba
                    {
                        Dátum = Dátum,
                        Idő = Idő,
                        Telephely = mezok[1],
                        Felhasználó = mezok[2],
                        HibaÜzenet = mezok[3],
                        HibaOsztály = mezok[4],
                        HibaMetódus = mezok[5],
                        Névtér = mezok[6],
                        Egyéb = mezok[7],
                        TeljesIdő = mezok[0]
                    };
                    Adatok.Add(Elem);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Hiányzó fájl", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] ÉvesLogFajltBetolt(int ev)
        {
            string[] Válasz = new string[] { };
            try
            {
                string fajlUtvonal = $@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\{ev}\hiba{ev}.csv";
                if (!FileLetezik(ev)) throw new HibásBevittAdat($"A {ev}. évi hibanapló fájl nem található.\n\n{fajlUtvonal}");
                Válasz = File.ReadAllLines(fajlUtvonal, Encoding.GetEncoding(1250));
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
            return Válasz;
        }

        private bool FileLetezik(int ev)
        {
            string fajlUtvonal = $@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\{ev}\hiba{ev}.csv";

            return File.Exists(fajlUtvonal);
        }

        private void Részletek_Click(object sender, EventArgs e)
        {
            try
            {
                if (Adatok.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva érvényes sor.");
                Ablak?.Close();
                Ablak = new Hibanapló_Részletes();
                Ablak.RészletesAdatok(EgyAdat);
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
