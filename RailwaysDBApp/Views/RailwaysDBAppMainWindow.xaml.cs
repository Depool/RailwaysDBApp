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
using RailwaysDBApp.Controllers;
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
            BackgroundImage.Source = TypesConverter.BitmapToWPFBitmapSource(Properties.Resources.ukrzaliznytsya);
            PermissionManager.CheckPermission(this);
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            object permission = model.CheckAuthorization(login.Text, password.Password);
            if (permission == null)
                loginResult.Property(Label.ContentProperty).SetResourceValue("MainWindow_LoginFault");
            else
            {
                try
                {
                    PermissionManager.Authorized(Convert.ToInt32(permission));
                    App.OpenWindow(WindowsFactory.MainMenu);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.go_Click(go, new RoutedEventArgs());
        }
    }
}
