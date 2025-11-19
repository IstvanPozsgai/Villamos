namespace Villamos.Adatszerkezet
{
    /// <summary>
    /// Represents the print settings for a worksheet, including page layout, margins, headers, and footers.
    /// </summary>
    /// <remarks>This class provides properties to configure various aspects of printing, such as the paper size, 
    /// orientation, margins, and repeating rows or columns. It also allows customization of headers and  footers for the
    /// printed output.</remarks>
    ///  <param name="Munkalap"></param>
    public class Beállítás_Nyomtatás
    {
        public string Munkalap { get; set; } = "";
        public string NyomtatásiTerület { get; set; } = "";
        public string IsmétlődőSorok { get; set; } = "";
        public string IsmétlődőOszlopok { get; set; } = "";
        public bool Álló { get; set; } = true;
        public int LapSzéles { get; set; } = 0;// Automatikus érték
        public int LapMagas { get; set; } = 0;// Automatikus érték                     
        public int? BalMargó { get; set; } = 15;
        public int? JobbMargó { get; set; } = 15;
        public int? AlsóMargó { get; set; } = 20;
        public int? FelsőMargó { get; set; } = 20;
        public int? FejlécMéret { get; set; } = 13;
        public int? LáblécMéret { get; set; } = 13;
        public string Papírméret { get; set; } = "A4";
        public string FejlécJobb { get; set; } = "";
        public string FejlécKözép { get; set; } = "";
        public string FejlécBal { get; set; } = "";
        public string LáblécJobb { get; set; } = "";
        public string LáblécKözép { get; set; } = "";
        public string LáblécBal { get; set; } = "";
    }
}
