using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailwaysDBApp.Views
{
    internal static class WindowsFactory
    {
        private static RailwaysDBAppMainMenu mainMenu = null;
        private static RailwaysDBAppSettingsWindow settings = null;
        private static RailwaysDBAppEditDBWindow editDB = null;

        public static RailwaysDBAppMainMenu MainMenu
        {
            get
            {
                if (mainMenu == null)
                    mainMenu = new RailwaysDBAppMainMenu();
                return mainMenu;
            }
        }

        public static RailwaysDBAppSettingsWindow Settings
        {
            get
            {
                if (settings == null || !settings.IsVisible)
                    settings = new RailwaysDBAppSettingsWindow();
                return settings;
            }
        }

        public static RailwaysDBAppEditDBWindow EditDB
        {
            get
            {
                if (editDB == null || !editDB.IsVisible)
                    editDB = new RailwaysDBAppEditDBWindow();
                return editDB;
            }
        }
    }
}
