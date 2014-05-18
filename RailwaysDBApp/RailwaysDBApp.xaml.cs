using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using RailwaysDAL;
using RailwaysDBApp.Controllers;
using RailwaysDBApp.Properties;
using RailwaysDBApp.Views;
using System.Threading;
using System.Globalization;

namespace RailwaysDBApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        //private
        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            RailwaysEntities context = RailwaysData.sharedContext; //инициализация синглтона сущностей БД
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string s = Environment.CurrentDirectory;

            AppDomain.CurrentDomain.SetData("DataDirectory", s);
            var culture = CultureInfo.GetCultureInfo(Settings.Default.Localization);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            PermissionManager.LoadPermissionList();
        }


        //static
        public static void OpenWindow(Window window)
        {
            if (!window.IsVisible)
                window.Show();
            else
                window.Activate();
        }

        public static void OpenMainWindow()
        {
            WindowsFactory.HideOpenedWindows();
            RailwaysDBAppMainWindow main = WindowsFactory.LoginWindow;
            App.OpenWindow(main);
            WindowsFactory.CloseHiddenWindows();
            WindowsFactory.MainMenu.Close();
        }

    }
}
