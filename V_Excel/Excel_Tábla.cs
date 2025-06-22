namespace Villamos
{
    public static partial class Module_Excel
    {
        /// <summary>
        /// A táblázatot rögzíti a beállított sornak megfelelően
        /// </summary>
        /// <param name="sor">sor</param>
        public static void Tábla_Rögzítés(int sor)
        {
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = sor;
            xlApp.ActiveWindow.FreezePanes = true;
        }

    }
}
