using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using RailwaysDAL;
using RailwaysDBApp.Models;

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
    }
}
