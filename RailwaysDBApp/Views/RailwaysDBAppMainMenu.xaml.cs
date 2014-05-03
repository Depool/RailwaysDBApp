using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RailwaysDBApp.Models;
using RailwaysDBApp.Controllers;

namespace RailwaysDBApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RailwaysDBAppMainMenu.xaml
    /// </summary>
    public partial class RailwaysDBAppMainMenu : Window
    {
        public RailwaysDBAppMainMenu()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            RailwaysDBAppSettingsWindow settings = WindowsFactory.Settings;
            App.OpenWindow(settings);
        }

        private void Tickets_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Routes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Races_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditTables_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Resources res = new Properties.Resources();
            MainMenuBackground.Source = TypesConverter.BitmapToWPFBitmapSource(Properties.Resources.rails);
            PermissionManager.CheckPermission(this);
        }
    }
}
