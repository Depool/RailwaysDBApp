using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailwaysDBApp.Views
{
    internal static class WindowsFactory
    {
        private static RailwaysDBAppMainMenu mainMenu = null;

        public static RailwaysDBAppMainMenu MainMenu
        {
            get
            {
                if (mainMenu == null)
                    mainMenu = new RailwaysDBAppMainMenu();
                return mainMenu;
            }
        }
    }
}
