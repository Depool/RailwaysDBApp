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
using RailwaysDAL;
using RailwaysDBApp.Models;
using RailwaysDBApp.Properties;
using WpfLocalization;

namespace RailwaysDBApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class RailwaysDBAppMainWindow : Window
    {
        private RailwaysDBAppMainWindowModel model = new RailwaysDBAppMainWindowModel();

        public RailwaysDBAppMainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Resources res = new Properties.Resources();
            backgroundImage.Source = TypesConverter.BitmapToWPFBitmapSource(Properties.Resources.ukrzaliznytsya);
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            if (!model.CheckAuthorization(login.Text, password.Password))
                loginResult.Property(Label.ContentProperty).SetResourceValue("MainWindow_LoginFault");
            else
            {
                WindowsFactory.MainMenu.Show();
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.go_Click(go, new RoutedEventArgs());
        }
    }
}
