using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace Villamos.MindenEgyéb
{
    public static class PDF_Töltés
    {
        /// <summary>
        ///  A kiírandó értéket beírja egy cellába
        /// </summary>
        /// <param name="érték">kiírandó szöveg</param>
        ///  <param name="keret">alapérték igen akkor cellánként keretez</param>
        /// <returns></returns>
        public static PdfPCell Cella(Paragraph érték, bool keret = true)
        {
            PdfPCell válasz = new PdfPCell();
            válasz.AddElement(érték);
            if (keret)
            {
                válasz.Border = iTextSharp.text.Rectangle.BOX;
                válasz.BorderWidth = 0.5f;
                válasz.BorderColor = BaseColor.BLACK;
            }
            else
                válasz.Border = PdfPCell.NO_BORDER;
            return válasz;
        }


        /// <summary>
        /// A kiírandó szöveget kiírja arial 12 betűként
        /// </summary>
        /// <param name="szöveg">Kiírandó szöveg</param>
        /// <param name="betű">
        /// N- normál, vagy default
        /// V- vastag betű
        /// D- dőlt betű   
        /// A- Aláhúzott
        /// </param>
        /// <returns></returns>
        public static Paragraph Kiírás(string szöveg, string betű = "N", Single méret = 12f)
        {
            Paragraph válasz;
            string betűtípus = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont alapFont = BaseFont.CreateFont(betűtípus, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font Betű;
            switch (betű)
            {
                case "VD":
                    Betű = new iTextSharp.text.Font(alapFont, méret, iTextSharp.text.Font.BOLDITALIC, BaseColor.BLACK);
                    break;
                case "D":
                    Betű = new iTextSharp.text.Font(alapFont, méret, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
                    break;
                case "V":
                    Betű = new iTextSharp.text.Font(alapFont, méret, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    break;
                case "N":
                    Betű = new iTextSharp.text.Font(alapFont, méret, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    break;
                default:
                    Betű = new iTextSharp.text.Font(alapFont, méret, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    break;
            }
            válasz = new Paragraph(szöveg, Betű)
            {
                Alignment = Element.ALIGN_CENTER
            };
            return válasz;
        }

    }
}
