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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dataToShow = from rec in RailwaysData.sharedContext.USERS select new { rec.LOGIN, rec.PASS_WORD, rec.PERMISSION };
            table.ItemsSource = dataToShow.ToList();
        }
    }
}
