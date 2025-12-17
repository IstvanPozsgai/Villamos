using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public  class Adat_Szolgáltató
    {
        public int ID { get; set; }
        public string SzerződésSzám    { get; private set; }
        public string IratEleje        { get; private set; }
        public string IratVége         { get; private set; }
        public string Aláíró           { get; private set; }
        public string CégNévAlá        { get; private set; }
        public string CégCím           { get; private set; }
        public string CégAdó           { get; private set; }
        public string CégHosszúNév     { get; private set; }
        public string Cégjegyzékszám   { get; private set; }
        public string CsoportAzonosító { get; private set; }

        public Adat_Szolgáltató(int iD, string szerződésSzám, string iratEleje,
            string iratVége, string aláíró, string cégNévAlá, string cégCím, string cégAdó, string cégHosszúNév, string cégjegyzékszám, string csoportAzonosító)
        {
            ID = iD;
            SzerződésSzám = szerződésSzám;
            IratEleje = iratEleje;
            IratVége = iratVége;
            Aláíró = aláíró;
            CégNévAlá = cégNévAlá;
            CégCím = cégCím;
            CégAdó = cégAdó;
            CégHosszúNév = cégHosszúNév;
            Cégjegyzékszám = cégjegyzékszám;
            CsoportAzonosító = csoportAzonosító;
        }
    }
}
