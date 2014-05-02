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
using System.Globalization;
using WpfLocalization;
using RailwaysDBApp.Properties;

namespace RailwaysDBApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RailwaysDBAppSettingsWindow.xaml
    /// </summary>
    public partial class RailwaysDBAppSettingsWindow : Window
    {
        public RailwaysDBAppSettingsWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = comboBoxLanguage.SelectedItem as ComboBoxItem;

            if ((selectedItem.Tag as string) != Dispatcher.Thread.CurrentCulture.Name)
            {
                var culture = CultureInfo.GetCultureInfo(selectedItem.Tag as string);

                Dispatcher.Thread.CurrentCulture = culture;
                Dispatcher.Thread.CurrentUICulture = culture;

                LocalizationManager.UpdateValues();

                Settings.Default.Localization = selectedItem.Tag as string;
                Settings.Default.Save();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (ComboBoxItem item in comboBoxLanguage.Items)
                if ((item.Tag as string) == Settings.Default.Localization)
                    item.IsSelected = true;
        }

    }
}
