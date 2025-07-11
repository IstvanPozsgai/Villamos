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
        public static PdfPCell Cella(Paragraph érték, bool keret = true, bool alsó = true, bool felső = true, string háttér = "")
        {
            PdfPCell válasz = new PdfPCell();
            válasz.AddElement(érték);

            // Vízszintesen és függőlegesen középre igazítás
            if (keret)
            {
                válasz.Border = iTextSharp.text.Rectangle.BOX;
                válasz.BorderWidth = 0.5f;
                válasz.BorderColor = BaseColor.BLACK;
                if (!alsó) válasz.BorderWidthBottom = 0f;  // Csak az alsó vonalat kikapcsolni
                if (!felső) válasz.BorderWidthTop = 0f;   // Csak a felső vonalat kikapcsolni:
            }
            else
                válasz.Border = PdfPCell.NO_BORDER;

            switch (háttér.Trim())
            {
                case "LIGHT_GRAY":
                    válasz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    break;
            }

            return válasz;
        }

        /// <summary>
        /// A kiírandó szöveget kiírja arial 10 betűként
        /// </summary>
        /// <param name="szöveg">Kiírandó szöveg</param>
        /// <param name="betű">
        /// N- normál, vagy default
        /// V- vastag betű
        /// D- dőlt betű   
        /// A- Aláhúzott
        /// </param>
        /// <returns></returns>
        public static Paragraph Kiírás(string szöveg, string betű = "N", Single méret = 10f, int igazítás = 1, Single sortáv = 10f)
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
            válasz = new Paragraph(szöveg, Betű);
            switch (igazítás)
            {
                case 0:
                    válasz.Alignment = Element.ALIGN_LEFT;
                    break;
                case 1:
                    válasz.Alignment = Element.ALIGN_CENTER;
                    break;
                case 2:
                    válasz.Alignment = Element.ALIGN_RIGHT;
                    break;
                case -1:
                    válasz.Alignment = Element.ALIGN_UNDEFINED;
                    break;
                case 5:
                    válasz.Alignment = Element.ALIGN_MIDDLE;
                    break;
            }
            válasz.Leading = sortáv; //sorok között 
            return válasz;
        }

    }


    public class CustomFooter : PdfPageEventHelper
    {
        private PdfTemplate totalPages;
        private BaseFont baseFont;
        private readonly string leftText;
        private readonly string rightText;


        public CustomFooter(string leftText, string rightText)
        {
            this.leftText = leftText;
            this.rightText = rightText;
        }


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            totalPages = writer.DirectContent.CreateTemplate(50, 50);
            baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            float leftMargin = document.LeftMargin;
            float rightMargin = document.RightMargin;
            float pageWidth = document.PageSize.Width;
            float y = document.BottomMargin / 2;

            // Bal alsó sarok: "hatályos"
            cb.BeginText();
            cb.SetFontAndSize(baseFont, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, leftText, leftMargin, y, 0);
            cb.EndText();

            // Középen: "Oldal X / Y"
            string text = "Oldal " + writer.PageNumber + " / ";
            float textWidth = baseFont.GetWidthPoint(text, 9);
            float centerX = pageWidth / 2;
            cb.BeginText();
            cb.SetFontAndSize(baseFont, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, text, centerX, y, 0);
            cb.EndText();
            cb.AddTemplate(totalPages, centerX + textWidth / 2, y);

            // Jobb alsó sarok: "technológia"
            cb.BeginText();
            cb.SetFontAndSize(baseFont, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, rightText, pageWidth - rightMargin, y, 0);
            cb.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            totalPages.BeginText();
            totalPages.SetFontAndSize(baseFont, 9);
            totalPages.ShowText("" + (writer.PageNumber));
            totalPages.EndText();
        }
    }

}
