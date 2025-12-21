using static Villamos.V_MindenEgyéb.Enumok;

namespace Villamos.Adatszerkezet
{
    public class Beállítás_Ferde
    {
        /// <summary>
        /// Munkalap neve
        /// </summary>
        public string Munkalap { get; set; } = "";
        /// <summary>
        ///  Nyomtatási terület beállítása pl. $"a1:i{sor}"
        /// </summary>
        public string Terület { get; set; } = "";

        /// <summary>
        /// Ferde vonal iránya
        /// </summary>
        public bool Jobb { get; set; } = true;

        public KeretVastagsag Felső { get; set; } = KeretVastagsag.Nincs;
        public KeretVastagsag Alsó { get; set; } = KeretVastagsag.Nincs;
        public KeretVastagsag BalOldal { get; set; } = KeretVastagsag.Nincs;
        public KeretVastagsag JobbOldal { get; set; } = KeretVastagsag.Nincs;
    }
}
