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
        private TicketDisplayForm tickets = null;
        private reportDisplayForm reports = null;

        public RailwaysDBAppMainMenu()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.Settings);
        }

        private void Routes_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.AddTrainWindow);
        }

        private void EditTables_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.EditDB);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Resources res = new Properties.Resources();
            MainMenuBackground.Source = TypesConverter.BitmapToWPFBitmapSource(Properties.Resources.rails);
            PermissionManager.CheckPermission(this);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.AddNewUser);
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            App.OpenMainWindow(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowsFactory.CloseAllWindows();
        }

        private void Queries_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.QueriesWindow);
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            if (reports == null || !reports.Visible)
            {
                reports = new reportDisplayForm();
                reports.Show();
            }
            else
                reports.Focus();
        }

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.BuyTicketWindow);
        }

        private void RemoveTicket_Click(object sender, RoutedEventArgs e)
        {
            if (tickets == null || !tickets.Visible)
            {
                tickets = new TicketDisplayForm();
                tickets.Show();
            }
            else
                tickets.Focus();
        }

        private void Logging_Click(object sender, RoutedEventArgs e)
        {
            App.OpenWindow(WindowsFactory.LogWindow);
        }
    }
}
