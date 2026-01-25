using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Zuby.ADGV;

namespace InputForms
{
    public class DataGridViewHelper<T> where T : class
    {
        private readonly AdvancedDataGridView GridView_Data;
        private readonly Form Form_parent;
        private List<string> VáltozóNév = new List<string>();
        private List<string> MezőFelirat = new List<string>();
        private List<int> OszlopSzélesség = new List<int>();
        private List<bool> OszlopLáthatóság = new List<bool>();
        private BindingSource Source_binding;
        private BindingList<T> List_binding;

        public List<T> GetData() => new List<T>(List_binding); // ha szükséged van a teljes listára
        public DataGridView GetDataGridView() => GridView_Data;

        public DataGridViewHelper(Form parentForm)
        {
            Form_parent = parentForm ?? throw new ArgumentNullException(nameof(parentForm));
            GridView_Data = new AdvancedDataGridView();
            Form_parent.Controls.Add(GridView_Data);

            List_binding = new BindingList<T>();
            Source_binding = new BindingSource { DataSource = List_binding };
            GridView_Data.DataSource = Source_binding;

            GridView_Data.Font = Form_parent.Font;

            GridView_Data.EnableHeadersVisualStyles = false; // ← fontos! különben a szín nem érvényesül
            GridView_Data.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control; // szürke alap
            GridView_Data.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;
            GridView_Data.ColumnHeadersDefaultCellStyle.Font = new Font(GridView_Data.Font, FontStyle.Bold);

            GridView_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // ← egész sor kijelölése
        }


        /// <summary>
        ///        sorfejléc (row header) láthatóság
        /// </summary>
        /// <param name="show">true alapérték</param>
        /// <returns></returns>
        public DataGridViewHelper<T> ShowRowHeaders(bool show = true)
        {
            GridView_Data.RowHeadersVisible = show;
            return this;
        }

        /// <summary>
        /// Beállítja a pozíciót és méretet.
        /// </summary>
        public DataGridViewHelper<T> SetLocationAndSize(int x, int y, int width, int height)
        {
            GridView_Data.Location = new System.Drawing.Point(x, y);
            GridView_Data.Size = new System.Drawing.Size(width, height);
            return this;
        }

        /// <summary>
        /// Csak a FEJLÉCEKET és SZÉLESSÉGEKET állítja be – az oszlopokat a DataSource hozza létre!
        /// </summary>
        public DataGridViewHelper<T> ConfigureColumns(List<Adat_Hiba_Elrendezés> Beállítás)
        {
            if (Beállítás?.Count < 1) throw new ArgumentException("A lista nem tartalmaz elemet.");

            foreach (Adat_Hiba_Elrendezés Elem in Beállítás)
            {
                VáltozóNév.Add(Elem.Változó);
                MezőFelirat.Add(Elem.Felirat);
                OszlopSzélesség.Add(Elem.Szélesség);
                OszlopLáthatóság.Add(Elem.Látható);
            }


            // MEGJEGYZÉS: Az oszlopokat a DataGridView AUTOMATIKUSAN létrehozza a DataSource alapján.
            // Ezért itt CSAK a fejlécet és szélességet állítjuk be – de CSAK AKKOR, ha a DataGridView már generálta az oszlopokat.
            // Ezért ezt a beállítást a GetDataGridView() vagy egy külön metódusban célszerű meghívni,
            // vagy akkor, amikor biztosan léteznek az oszlopok (pl. DataBindingComplete esemény után).

            // Alternatíva: késleltetett beállítás
            GridView_Data.DataBindingComplete += (s, e) =>
            {
                ApplyColumnSettings();
            };

            return this;
        }

        private void ApplyColumnSettings()
        {
            for (int i = 0; i < VáltozóNév.Count; i++)
            {
                var propName = VáltozóNév[i];
                if (GridView_Data.Columns.Contains(propName))
                {
                    var col = GridView_Data.Columns[propName];
                    col.HeaderText = MezőFelirat[i];
                    col.Width = OszlopSzélesség[i];
                    col.Visible = OszlopLáthatóság[i]; // ← itt állítjuk be a láthatóságot
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Elemek"></param>
        /// <returns></returns>
        public DataGridViewHelper<T> AddItems(List<T> Elemek)
        {
            if (Elemek == null) return this;

            GridView_Data.SuspendLayout();
            try
            {
                foreach (var elem in Elemek)
                {
                    List_binding.Add(elem);
                }
            }
            finally
            {
                GridView_Data.ResumeLayout();
            }

            return this;
        }

        /// <summary>
        /// A kiválasztás változásakor meghívja a megadott callback függvényt a kiválasztott T típusú elemmel.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public DataGridViewHelper<T> OnSelectionChanged(Action<T> callback)
        {
            GridView_Data.SelectionChanged += (sender, e) =>
            {
                if (Source_binding.Current is T item)
                {
                    callback?.Invoke(item);
                }
            };
            return this;
        }

        /// <summary>
        /// Kijelölt elemek listájának lekérése.
        /// </summary>
        /// <returns></returns>
        public List<T> GetSelectedItems()
        {
            var selected = new List<T>();
            foreach (DataGridViewRow row in GridView_Data.SelectedRows)
            {
                if (row.DataBoundItem is T item)
                {
                    selected.Add(item);
                }
            }
            return selected;
        }

        /// <summary>
        /// Több kijelölt elem esetén meghívja a megadott callback függvényt a kiválasztott T típusú elemek listájával.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public DataGridViewHelper<T> OnSelectionChanged(Action<List<T>> callback)
        {
            GridView_Data.SelectionChanged += (sender, e) =>
            {
                var selected = GetSelectedItems();
                callback?.Invoke(selected);
            };
            return this;
        }

        /// <summary>
        /// Beállítja az Anchor tulajdonságot.
        /// </summary>
        public DataGridViewHelper<T> SetAnchor(AnchorStyles anchor)
        {
            GridView_Data.Anchor = anchor;
            return this;
        }

        /// <summary>
        /// Ki-/bekapcsolja a többtöbbszörös kijelölést.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public DataGridViewHelper<T> EnableMultiSelect(bool enable = true)
        {
            GridView_Data.MultiSelect = enable;
            return this;
        }




        //Tesztelni kell !

        // Új metódus: fejléc háttérszínének testreszabása
        public DataGridViewHelper<T> SetHeaderBackColor(Color color)
        {
            GridView_Data.ColumnHeadersDefaultCellStyle.BackColor = color;
            return this;
        }

        //  SetFont: frissítse a fejléc betűtípusát ÉS magasságát
        public DataGridViewHelper<T> SetFont(Font font = null)
        {
            var finalFont = font ?? Form_parent.Font;
            GridView_Data.Font = finalFont;

            // Fejléc betűtípusa
            GridView_Data.ColumnHeadersDefaultCellStyle.Font = new Font(finalFont, FontStyle.Bold);

            // Fejléc magasságának automatikus beállítása a betűméret alapján
            // Tapasztalati képlet: magasság ≈ betűméret * 1.8 + 4
            int headerHeight = Math.Max(20, (int)(finalFont.Height * 1.8) + 4);
            GridView_Data.ColumnHeadersHeight = headerHeight;

            return this;
        }

        public DataGridViewHelper<T> AddItem(T Elem)
        {
            List_binding.Add(Elem);
            return this;
        }

        public DataGridViewHelper<T> ClearData()
        {
            List_binding.Clear();
            return this;
        }

        /// <summary>
        /// Betölti az adatokat a DataGridView-ba.
        /// </summary>
        public DataGridViewHelper<T> LoadData(List<T> data)
        {
            GridView_Data.Rows.Clear();
            if (data == null) return this;

            foreach (var item in data)
            {
                var rowValues = new List<object>();
                foreach (var propName in VáltozóNév)
                {
                    var prop = typeof(T).GetProperty(propName);
                    rowValues.Add(prop?.GetValue(item) ?? string.Empty);
                }
                GridView_Data.Rows.Add(rowValues.ToArray());
            }

            return this;
        }
    }
}