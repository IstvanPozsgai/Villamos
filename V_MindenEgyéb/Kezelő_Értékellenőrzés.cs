using System;
using System.Globalization;
using System.Windows.Forms;
using Villamos;

public partial class Függvénygyűjtemény
{
    public static int Érték_INT(string rekord) => int.TryParse(rekord, out int válasz) ? válasz : 0;
    public static long Érték_LONG(string rekord) => long.TryParse(rekord, out long válasz) ? válasz : 0;
    public static double Érték_DOUBLE(string rekord) => double.TryParse(rekord, out double válasz) ? válasz : 0.0;
    public static DateTime Érték_DATETIME(string rekord) => DateTime.TryParse(rekord, out DateTime válasz) ? válasz : new DateTime(1900, 1, 1);
    public static bool Érték_BOOL(object rekord) => bool.TryParse(rekord?.ToString(), out bool válasz) && válasz;


}

public static class FVGyűjtemény
{
    /// <summary>
    /// Objectumot alakít át szöveggé amit megtrimmel
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToStrTrim(this object value)
    {
        if (value == DBNull.Value || value == null)
            return string.Empty; // Visszaad egy üres stringet, ha az érték DBNull vagy null
        return value.ToString().Trim(); // Egyébként visszaadja a trimmelt stringet
    }


    /// <summary>
    /// Objectumot alakít át INT abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToÉrt_Int(this object str)
    {
        if (!int.TryParse(str.ToStrTrim(), out int válasz))
            válasz = 0;
        return válasz;
    }
    /// <summary>
    ///  Szöveget alakít át INT abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToÉrt_Int(this string str)
    {
        if (!int.TryParse(str, out int válasz))
            válasz = 0;
        return válasz;
    }
    /// <summary>
    /// Objectumot alakít át LONG abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long ToÉrt_Long(this object str)
    {
        if (!long.TryParse(str.ToStrTrim(), out long válasz))
            válasz = 0;
        return válasz;
    }
    /// <summary>
    ///     Szöveget alakít át LONG abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long ToÉrt_Long(this string str)
    {
        if (!long.TryParse(str, out long válasz))
            válasz = 0;
        return válasz;
    }
    /// <summary>
    /// Objectumot alakít át DOUBLE abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double ToÉrt_Double(this object str)
    {
        if (!double.TryParse(str.ToStrTrim(), out double válasz))
            válasz = 0;
        return válasz;
    }
    /// <summary>
    /// Szöveget alakít át DOUBLE abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double ToÉrt_Double(this string str)
    {
        if (!double.TryParse(str, out double válasz))
            válasz = 0;
        return válasz;
    }
    /// <summary>
    /// Objectumot alakít át DATETIME abban az esetben,
    /// ha nem alakítható át dátummá akkor 1900.01.01 00:00:00-el tér vissza
    /// Ha régebbi mint 1900.01.01, akkor az időt visszi magával
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime ToÉrt_DaTeTime(this object str)
    {
        DateTime Válasz = new DateTime(1900, 1, 1, 0, 0, 0);
        if (DateTime.TryParse(str.ToStrTrim(), out DateTime dátum))
            if (dátum < new DateTime(1900, 1, 1, 0, 0, 0))
                Válasz = new DateTime(1900, 1, 1, dátum.Hour, dátum.Minute, dátum.Second);
            else
                Válasz = dátum;
        return Válasz;
    }
    /// <summary>
    ///  Szöveget alakít át DATETIME abban az esetben, ha nem jó értéket kap 1900.01.01-el tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime ToÉrt_DaTeTime(this string str)
    {
        DateTime Válasz = new DateTime(1900, 1, 1, 0, 0, 0);
        if (DateTime.TryParse(str.ToStrTrim(), out DateTime dátum))
            if (dátum < new DateTime(1900, 1, 1, 0, 0, 0))
                Válasz = new DateTime(1900, 1, 1, dátum.Hour, dátum.Minute, dátum.Second);
            else
                Válasz = dátum;
        return Válasz;
    }
    /// <summary>
    /// Objectumot alakít át BOOL abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ToÉrt_Bool(this object str)
    {
        if (!bool.TryParse(str.ToStrTrim(), out bool válasz))
            válasz = false;
        return válasz;
    }
    /// <summary>
    ///  Szöveget alakít át BOOL abban az esetben, ha nem jó értéket kap 0-val tér vissza
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ToÉrt_Bool(this string str)
    {
        if (!bool.TryParse(str, out bool válasz))
            válasz = false;
        return válasz;
    }

    /// <summary>
    /// A double értékeket alakítja át szöveggé úgy, hogy a tizedesvessző helyett pontot használjon, így biztosítva a helyes formátumot például adatbázisba vagy fájlba íráskor.
    /// </summary>
    /// <param name="szám"></param>
    /// <returns></returns>
    public static string ToStringCult(this double szám)
    {
        return szám.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// a decimal értékeket alakítja át szöveggé úgy, hogy a tizedesvessző helyett pontot használjon, így biztosítva a helyes formátumot például adatbázisba vagy fájlba íráskor.
    /// </summary>
    /// <param name="szám"></param>
    /// <returns></returns>
    public static string ToStringCult(this decimal szám)
    {
        return szám.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// A float értékeket alakítja át szöveggé úgy, hogy a tizedesvessző helyett pontot használjon, így biztosítva a helyes formátumot például adatbázisba vagy fájlba íráskor.
    /// </summary>
    /// <param name="szám"></param>
    /// <returns></returns>
    public static string ToStringCult(this float szám)
    {
        return szám.ToString(CultureInfo.InvariantCulture);
    }

    public static string ToTypeString(this Type type)
    {
        try
        {
            if (type == typeof(string)) return "TEXT";
            if (type == typeof(int) || type == typeof(long) || type == typeof(bool)) return "INTEGER";
            if (type == typeof(double) || type == typeof(decimal)) return "REAL";
            if (type == typeof(DateTime)) return "TEXT";
            if (type == typeof(byte[])) return "BLOB";
            if (type.Name == "Int16") return "INTEGER";

            throw new HibásBevittAdat($"A {type.Name} nincs még beazonosítva, hogy mire legyen konvertálva.");
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "ConvertType", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return "HIBA";
    }

}
