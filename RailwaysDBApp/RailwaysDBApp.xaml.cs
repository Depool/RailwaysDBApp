using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using RailwaysDAL;
using RailwaysDBApp.Models;
using RailwaysDBApp.Properties;
using System.Threading;
using System.Globalization;

namespace RailwaysDBApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            RailwaysEntities context = RailwaysData.sharedContext; //инициализация синглтона сущностей БД
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var culture = CultureInfo.GetCultureInfo(Settings.Default.Localization);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
