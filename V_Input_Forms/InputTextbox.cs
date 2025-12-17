using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace InputForms
{
    class InputTextbox : InputField
    {
        readonly Label label;
        string rule;
        readonly string Tartalom;
        readonly int MaxLength;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelSzöveg">Label felirat</param>
        /// <param name="parent"></param>
        /// <param name="MaxLength"></param>
        public InputTextbox(string LabelSzöveg, string tartalom, int maxLength = 15, Control parent = null) : base(parent)
        {
            MaxLength = maxLength;
            Tartalom = tartalom;
            label = new Label
            {
                Text = LabelSzöveg,
                Font = new Font("sans-serif", 12f),
                AutoSize = true
            };

            input = CreateField();

            if (parent != null) Add(parent);
        }



        public override InputField Add(Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        public InputTextbox MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;
            return this;
        }

        public InputTextbox AddRule(string rule)
        {
            this.rule = rule;
            return this;
        }

        public bool IsValid()
        {
            string magyar = @"[aábcdeéfghiíjklmnoóöőpqrstuúüűvwxyzAÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ ]";
            if (rule == null) rule = magyar;
            return Regex.IsMatch((string)Value, "^" + rule + "+$");
        }

        protected override Control CreateField()
        {
            TextBox textBox = new TextBox
            {
                Font = new Font("sans-serif", 12f),
                Width = Szélesség(),
                MaxLength = MaxLength,
                Text = Tartalom
            };
            return textBox;
        }

        public int Szélesség()
        {
            int válasz = 10;
            using (Font font = new Font("Microsoft Sans Serif", 12f))
            {
                string worstCase = new string('W', MaxLength);
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

        public override object Value
        {
            get => ((TextBox)input).Text;
            set => ((TextBox)input).Text = value?.ToString() ?? "";
        }


    }
}
