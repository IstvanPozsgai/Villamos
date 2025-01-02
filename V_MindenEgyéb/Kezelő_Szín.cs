using System.Drawing;

namespace Villamos.V_MindenEgyéb
{
    public static  class Kezelő_Szín
    {
        public static  Szín_kódolás Szín_váltó(long színes)
        {
            Szín_kódolás ideig;
            int Piros, Kék, Zöld;
            long maradék = színes;

            if ((maradék / 65536) > 1)
            {
                Kék = (int)(színes / 65536);
                maradék = színes - (Kék * 65536);
            }
            else
            {
                Kék = 0;
            }
            if ((maradék / 256) > 1d)
            {
                Zöld = (int)(maradék / 256);
                maradék -= (Zöld * 256);
            }
            else
            {
                Zöld = 0;
            }
            Piros = (int)maradék;

            ideig = new Szín_kódolás(Piros, Kék, Zöld);

            return ideig;
        }

        public static Szín_kódolás Szín_váltó(double színes)
        {
            Szín_kódolás ideig;
            int Piros, Kék, Zöld;
            double maradék = színes;

            if ((maradék / 65536) > 1)
            {
                Kék = (int)(színes / 65536);
                maradék = színes - (Kék * 65536);
            }
            else
            {
                Kék = 0;
            }
            if ((maradék / 256) > 1d)
            {
                Zöld = (int)(maradék / 256);
                maradék -= (Zöld * 256);
            }
            else
            {
                Zöld = 0;
            }
            Piros = (int)maradék;

            ideig = new Szín_kódolás(Piros, Kék, Zöld);

            return ideig;
        }

        public static string ColorToHex(Color color)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb()));
        }

        public static Color HexToColor(string hexColorCode)
        {
            Color Válasz = Color.Green;
            object convertFromString = new ColorConverter().ConvertFromString(hexColorCode);
            if (convertFromString != null)
            {
                Válasz = (Color)convertFromString;
            }
            return Válasz;
        }
    }



    public class Szín_kódolás
    {
        public int Piros { get; set; }
        public int Kék { get; set; }
        public int Zöld { get; set; }


        public Szín_kódolás(int piros, int kék, int zöld)
        {
            Piros = piros;
            Kék = kék;
            Zöld = zöld;
        }

    }
}
