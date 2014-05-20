using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace RailwaysDBApp.Views
{
    internal static class WindowsFactory
    {
        private static List<Window> windows = new List<Window>();

        private static RailwaysDBAppMainMenu mainMenu = null;
        private static RailwaysDBAppSettingsWindow settings = null;
        private static RailwaysDBAppEditDBWindow editDB = null;
        private static RailwaysDBAppAddNewUser addNewUser = null;
        private static RailwaysDBAppMainWindow loginWindow = null;
        private static RailwaysDBAppQueriesWindow queriesWindow = null;
        private static RailwaysDBAppAddTrainWindow addTrainWindow = null;
        private static RailwaysDBAppBuyTicketWindow buyTicketWindow = null;
        

        //static properties
        public static RailwaysDBAppMainMenu MainMenu
        {
            get
            {
                if (mainMenu == null || !mainMenu.IsVisible)
                {
                    if (mainMenu != null)
                    {
                        //mainMenu.Close();
                        windows.Remove(mainMenu);
                    }
                    mainMenu = new RailwaysDBAppMainMenu();
                    windows.Add(mainMenu);
                }
                return mainMenu;
            }
        }

        public static RailwaysDBAppSettingsWindow Settings
        {
            get
            {
                if (settings == null || !settings.IsVisible)
                {
                    if (settings != null)
                    {
                        //settings.Close();
                        windows.Remove(settings);
                    }
                    settings = new RailwaysDBAppSettingsWindow();
                    windows.Add(settings);
                }
                return settings;
            }
        }

        public static RailwaysDBAppEditDBWindow EditDB
        {
            get
            {
                if (editDB == null || !editDB.IsVisible)
                {
                    if (editDB != null)
                    {
                        //editDB.Close();
                        windows.Remove(editDB);
                    }
                    editDB = new RailwaysDBAppEditDBWindow();
                    windows.Add(editDB);
                }
                return editDB;
            }
        }

        public static RailwaysDBAppAddNewUser AddNewUser
        {
            get
            {
                if (addNewUser == null || !addNewUser.IsVisible)
                {
                    if (addNewUser != null)
                    {
                        //addNewUser.Close();
                        windows.Remove(addNewUser);
                    }
                    addNewUser = new RailwaysDBAppAddNewUser();
                    windows.Add(addNewUser);
                }
                return addNewUser;
            }
        }

        public static RailwaysDBAppMainWindow LoginWindow
        {
            get
            {
                if (loginWindow == null || !loginWindow.IsVisible)
                {
                    if (loginWindow != null)
                    {
                        //loginWindow.Close();
                        windows.Remove(loginWindow);
                    }
 
                    loginWindow = new RailwaysDBAppMainWindow();
                    windows.Add(loginWindow);
                }
                return loginWindow;
            }
        }

        public static RailwaysDBAppQueriesWindow QueriesWindow
        {
            get
            {
                if (queriesWindow == null || !queriesWindow.IsVisible)
                {
                    if (queriesWindow != null)
                    {
                        //queriesWindow.Close();
                        windows.Remove(queriesWindow);
                    }
                    queriesWindow = new RailwaysDBAppQueriesWindow();
                    windows.Add(queriesWindow);
                }
                return queriesWindow;
            }
        }

        public static RailwaysDBAppAddTrainWindow AddTrainWindow
        {
            get
            {
                if (addTrainWindow == null || !addTrainWindow.IsVisible)
                {
                    if (addTrainWindow != null)
                    {
                        windows.Remove(addTrainWindow);
                    }
                    addTrainWindow = new RailwaysDBAppAddTrainWindow();
                    windows.Add(addTrainWindow);
                }
                return addTrainWindow;
            }
        }

        public static RailwaysDBAppBuyTicketWindow BuyTicketWindow
        {
            get
            {
                if (buyTicketWindow == null || !buyTicketWindow.IsVisible)
                {
                    if (buyTicketWindow != null)
                    {
                        windows.Remove(buyTicketWindow);
                    }
                    buyTicketWindow = new RailwaysDBAppBuyTicketWindow();
                    windows.Add(buyTicketWindow);
                }
                return buyTicketWindow;
            }
        }


        //static methods
        public static void HideOpenedWindows()
        {
            foreach (Window window in windows)
                if (window.IsVisible)
                    window.Hide();
        }

        public static void CloseHiddenWindows()
        {
            foreach (Window window in windows)
                if (!window.IsVisible)
                    window.Close();
        }

        public static void CloseAllWindows()
        {
            foreach (Window window in windows)
                if (window.IsVisible && window.GetType() != typeof(RailwaysDBAppMainWindow))
                    window.Close();
        }

    }
}
