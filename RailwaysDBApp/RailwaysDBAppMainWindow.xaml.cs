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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RailwaysDALFirebird;

namespace RailwaysDBApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class RailwaysDBAppMainWindow : Window
    {
        public RailwaysDBAppMainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(RailwaysEntities context = new RailwaysEntities())
            {
                var itemsToShow = from rec in context.USERS select new { rec.LOGIN, rec.PASS_WORD };
                table.ItemsSource = itemsToShow.ToList();
            }
        }
    }
}
