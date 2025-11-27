using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
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

            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Cella_Betű(mit: {mit}, aláhúzott: {aláhúzott}, dőlt: {dőlt}, vastag: {vastag}, kezdet: {kezdet}, hossz: {hossz}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
