using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputSelect : InputField
    {
        readonly List<string> Options;
        readonly Label label;
        readonly string Tartalom;
        readonly int MaxLength;
        string rule;
        public int Height => input.Height;

        public InputSelect(string LabelSzöveg, List<string> options, int maxLength = 15, Control parent = null) : base(parent)
        {
            label = new Label
            {
                Text = LabelSzöveg,
                Font = new Font("sans-serif", 12f),
                AutoSize = true
            };

            Options = options;
            MaxLength = maxLength;


            ComboBox comboBox = (ComboBox)input;
            comboBox.MaxLength = maxLength;
            comboBox.Width = Szélesség();
            comboBox.Items.Clear();
            foreach (string option in Options)
            {
                comboBox.Items.Add(option);
            }

            if (Options.Count > 0)
                comboBox.SelectedIndex = 0;

            if (parent != null) Add(parent);
        }

        public override InputField Add(Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        protected override Control CreateField()
        {
            ComboBox combobox = new ComboBox()
            {
                Font = new Font("sans-serif", 12f),
                Width = Szélesség(),
                MaxLength = MaxLength
            };

            return combobox;
        }

        public InputSelect MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;
            return this;
        }


        public InputSelect WithValue(object v)
        {
            this.Value = v;
            return this;
        }


        public override object Value
        {
            get => ((ComboBox)input).Text;
            set
            {
                var combo = (ComboBox)input;
                var textValue = value?.ToString() ?? string.Empty;

                // pontos egyezést keresünk
                int idx = combo.FindStringExact(textValue);
                if (idx >= 0)
                {
                    combo.SelectedIndex = idx;
                }
                else
                {
                    // csak akkor írjuk a Text-et, ha nem DropDownList
                    if (combo.DropDownStyle != ComboBoxStyle.DropDownList)
                        combo.Text = textValue;
                    // ha DropDownList és nincs benne, nem állítjuk
                }
            }
        }

        public int Szélesség()
        {
            int válasz = 10;
            using (Font font = new Font  ("Microsoft Sans Serif", 12f))
            {
                string worstCase = new string ('W', MaxLength);
                Size textSize = TextRenderer.MeasureText(
                    worstCase,
                    font,
                    Size.Empty,
                    TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix
                );

                // Margó hozzáadása (a kurzor, belső padding miatt)
                válasz = textSize.Width + 8;
            }
            return válasz;
        }

        public InputSelect AddRule(string rule)
        {
            this.rule = rule;
            return this;
        }

        public InputSelect SetHeight(int magas)
        {
            input.Height = magas;
            return this;
        }

        public InputSelect SetWidth(int széles)
        {
            input.Width = széles;
            return this;
        }
    }
}
