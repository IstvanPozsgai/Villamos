using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.V_Adatszerkezet
{
    public class Adat_SQLite
    {
        public Adat_SQLite(string username, DateTime date, int age, bool trueOrFalse)
        {
            Username = username;
            Date = date;
            TrueOrFalse = trueOrFalse;
        }

        public string Username { get; set; }

        public DateTime Date { get; set; }

        public bool TrueOrFalse { get; set; }

    }
}
