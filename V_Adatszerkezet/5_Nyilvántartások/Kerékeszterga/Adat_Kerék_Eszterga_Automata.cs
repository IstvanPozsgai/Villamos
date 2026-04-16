using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Automata
    {
        public string FelhasználóiNév { get; private set; }
        public DateTime UtolsóÜzenet { get; private set; }

        public Adat_Kerék_Eszterga_Automata(string felhasználóiNév, DateTime utolsóÜzenet)
        {
            FelhasználóiNév = felhasználóiNév;
            UtolsóÜzenet = utolsóÜzenet;
        }
    }
}
