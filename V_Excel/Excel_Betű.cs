using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MyExcel = Microsoft.Office.Interop.Excel;


namespace Villamos
{
    public static partial class Module_Excel
    {
        /// <summary>
        /// Betűméretet lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="méret">egész</param>
        public static void Betű(string mit, int méret)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);
                Táblaterület.Font.Size = méret;
                Táblaterület.Font.Strikethrough = false;
                Táblaterület.Font.Superscript = false;
                Táblaterület.Font.Subscript = false;
                Táblaterület.Font.OutlineFont = false;
                Táblaterület.Font.Shadow = false;
                Táblaterület.Font.Underline = MyExcel.XlUnderlineStyle.xlUnderlineStyleNone;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: \"{mit}\", méret: {méret}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Betű(string mit, Color színe)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);
                Táblaterület.Font.Color = ColorTranslator.ToOle(színe);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit}, szín: {színe.Name}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Betű formátumát lehet állítani
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="aláhúzott"></param>
        /// <param name="dőlt"></param>
        /// <param name="vastag"></param>
        public static void Betű(string mit, bool aláhúzott, bool dőlt, bool vastag)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);
                Táblaterület.Font.Underline = aláhúzott
                    ? MyExcel.XlUnderlineStyle.xlUnderlineStyleSingle
                    : MyExcel.XlUnderlineStyle.xlUnderlineStyleNone;
                Táblaterület.Font.Italic = dőlt;
                Táblaterület.Font.Bold = vastag;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit}, aláhúzott: {aláhúzott}, dőlt: {dőlt}, vastag: {vastag}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// A Betű stílusából egyet lehet kiválasztani, annak a formátum maszkját kell elküldeni.
        /// üres string ha nem akarjuk kihasználni.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="stílus"></param>
        /// <param name="formátum"></param>
        public static void Betű(string mit, string stílus = "", string formátum = "")
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);

                if (!string.IsNullOrWhiteSpace(stílus))
                    Táblaterület.Style = stílus;

                if (!string.IsNullOrWhiteSpace(formátum))
                    Táblaterület.NumberFormat = formátum;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit}, stílus: {stílus}, formátum: {formátum}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Cellán belüli szöveg formázásokat láncba kell megadni, a lánc minden elemére vonatkozóan
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="aláhúzott"></param>
        /// <param name="dőlt"></param>
        /// <param name="vastag"></param>
        /// <param name="kezdet"></param>
        /// <param name="hossz"></param>
        public static void Cella_Betű(string mit, bool aláhúzott, bool dőlt, bool vastag, int kezdet, int hossz)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);

                if (Táblaterület.Value2 is string szöveg && szöveg.Length >= kezdet + hossz - 1)
                {
                    MyExcel.Characters karakterek = Táblaterület.Characters[kezdet, hossz];
                    karakterek.Font.Underline = aláhúzott
                        ? MyExcel.XlUnderlineStyle.xlUnderlineStyleSingle
                        : MyExcel.XlUnderlineStyle.xlUnderlineStyleNone;
                    karakterek.Font.Italic = dőlt;
                    karakterek.Font.Bold = vastag;
                }

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Cella_Betű(mit: {mit}, aláhúzott: {aláhúzott}, dőlt: {dőlt}, vastag: {vastag}, kezdet: {kezdet}, hossz: {hossz}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Betű típusát és méretét állítja be a munkalapon
        /// </summary>
        /// <param name="név"></param>
        /// <param name="méret"></param>
        public static void Munkalap_betű(string név, int méret)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Cells;
                Táblaterület.VerticalAlignment = MyExcel.XlVAlign.xlVAlignCenter;
                Táblaterület.Font.Name = név;
                Táblaterület.Font.Size = méret;
                Táblaterület.Font.Strikethrough = false;
                Táblaterület.Font.Superscript = false;
                Táblaterület.Font.Subscript = false;
                Táblaterület.Font.OutlineFont = false;
                Táblaterület.Font.Shadow = false;
                Táblaterület.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone;
                Táblaterület.Font.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorLight1;
                Táblaterület.Font.TintAndShade = 0;
                Táblaterület.Font.ThemeFont = Microsoft.Office.Interop.Excel.XlThemeFont.xlThemeFontNone;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_betű(név: {név}, méret: {méret}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
