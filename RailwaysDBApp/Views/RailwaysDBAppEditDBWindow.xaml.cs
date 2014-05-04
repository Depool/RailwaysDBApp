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
using RailwaysDBApp.Controllers;
using RailwaysDBApp.Models;
using RailwaysDAL;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using System.Collections;


namespace RailwaysDBApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RailwaysDBAppEditDBWindow.xaml
    /// </summary>
    public partial class RailwaysDBAppEditDBWindow : Window
    {
        public RailwaysDBAppEditDBWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> tables = RailwaysDBQueries.GetUserTablesNames();
            if (tables != null)
                foreach (string name in tables)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = name;
                    item.Tag = name;
                    TablesComboBox.Items.Add(item);
                };
        }

        private void TablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = TablesComboBox.SelectedItem as ComboBoxItem;
            RailwaysEntities context = RailwaysData.sharedContext;
            string tableName = selectedItem.Tag as string;
            if (tableName!= null)
            {
                tableName = tableName.Replace(" ", string.Empty);
                dynamic query = context.GetType().GetProperty(tableName).GetValue(context, null);
                DataTable.ItemsSource = null;
                object collection = null;
                foreach (dynamic q in query)
                {
                    collection = TypesConverter.CreateGenericList(q.GetType());
                    break;
                }

                if (collection != null)
                {
                    MethodInfo mListAdd = collection.GetType().GetMethod("Add");
                    foreach (dynamic q in query)
                    {
                        mListAdd.Invoke(collection, new object[] { q });
                    }

                    DataTable.ItemsSource = (IEnumerable)collection;
                    int numberOfDomains = RailwaysDBQueries.GetNumberOfDomainsInTable(tableName);

                    for (int i = DataTable.Columns.Count - 1; i >= numberOfDomains; --i)
                         DataTable.Columns.RemoveAt(i);

                }
            }
        }
    }
}
