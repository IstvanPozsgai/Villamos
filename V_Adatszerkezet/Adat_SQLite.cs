using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.V_Adatszerkezet
{
    public class Adat_SQLite
    {
        public Adat_SQLite(int id, string username, DateTime date, bool trueOrFalse)
        {
            ID = id;
            Username = username;
            Date = date;
            TrueOrFalse = trueOrFalse;
        }

        public int ID { get; set; }

        public string Username { get; set; }

        public DateTime Date { get; set; }

        public bool TrueOrFalse { get; set; }

    }
}
