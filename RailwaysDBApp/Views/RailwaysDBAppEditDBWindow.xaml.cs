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
        private Type editingType;
        private string curTableName;
        private bool isDataTabEditing;
        private List<string> columnsOriginalHeaders = new List<string>();
        private int generatorValueForCurTable = 0;
        private List<string> tables = null;

        public RailwaysDBAppEditDBWindow()
        {
            InitializeComponent();
            isDataTabEditing = false;
            DataTab.PreviewKeyDown += DT_PreviewKeyDown; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tables = RailwaysDBQueries.GetUserTablesNames();
            for (int i = 0; i < tables.Count; ++i)
                tables[i] = tables[i].Replace(" ", String.Empty);

            if (tables != null)
                foreach (string name in tables)
                {
                    if (name != "USERS" && name != "PERMISSION_TYPE")
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = TypesConverter.GetResource(name);
                        item.Tag = name;
                        TablesComboBox.Items.Add(item);
                    }
                };
        }

        private void TablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = TablesComboBox.SelectedItem as ComboBoxItem;
            RailwaysEntities context = RailwaysData.sharedContext;
            string tableName = selectedItem.Tag as string;

            if (tableName!= null)
            {
                curTableName = tableName;
                editingType = context.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0];
                dynamic query = context.GetType().GetProperty(tableName).GetValue(context, null);
                DataTab.ItemsSource = null;
                object collection = TypesConverter.CreateGenericList(editingType);

                generatorValueForCurTable = 0;
                MethodInfo mListAdd = collection.GetType().GetMethod("Add");
                foreach (dynamic q in query)
                {
                     try
                     {
                         int id = (int)context.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0].GetProperty("ID").GetValue(q, null);
                         if (id > generatorValueForCurTable)
                             generatorValueForCurTable = id;
                     }
                     catch{}
                     mListAdd.Invoke(collection, new object[] { q });
                }
                
                DataTab.ItemsSource = (IEnumerable)collection;
                DataTab.DataContext = context.GetType().GetProperty(tableName).GetValue(context, null);

                int numberOfDomains = RailwaysDBQueries.GetNumberOfDomainsInTable(tableName);
                    
                for (int i = DataTab.Columns.Count - 1; i >= numberOfDomains; --i)
                         DataTab.Columns.RemoveAt(i);
                for (int i = DataTab.Columns.Count - 1;i >= 0; --i)
                    if ((DataTab.Columns[i].Header as string) == "ID")
                        DataTab.Columns.RemoveAt(i);

                columnsOriginalHeaders.Clear();
                foreach (DataGridColumn column in DataTab.Columns)
                {
                     columnsOriginalHeaders.Add((string)column.Header);
                     column.Header = TypesConverter.GetResource(curTableName + "_" + (column.Header as string));
                }
              }
              
          }

        private void DataTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteItems.IsEnabled = DataTab.SelectedItems.Count > 0;
        }

        private void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            DeleteItems.IsEnabled = false;
            removeRecords();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                DeleteItems_Click(sender, new RoutedEventArgs());
        }

        private void removeRecords()
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            object entities = context.GetType().GetProperty(curTableName).GetValue(context, null);

            MethodInfo mListDelete = entities.GetType().GetMethod("DeleteObject");
            for (int i = DataTab.SelectedItems.Count - 1; i >= 0; --i)
            {
                object row = DataTab.SelectedItems[i];
                try
                {
                    mListDelete.Invoke(entities, new object[] { row });
                }
                catch { };
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                /*if (ex.InnerException == null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.InnerException.Message);*/
            }
        }

        private void DT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isDataTabEditing)
            {
                removeRecords();
            }
                    
        }

        private void DataTab_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isDataTabEditing = true;
            DeleteItems.IsEnabled = false;
        }

        private void DataTab_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox t = e.EditingElement as TextBox;
            string newValue = t.Text;
            object entity = e.Row.DataContext;

            Type convertToType = entity.GetType().GetProperty(columnsOriginalHeaders[e.Column.DisplayIndex]).PropertyType;
            TypeConverter tc = TypeDescriptor.GetConverter(convertToType);
            object castedValue = tc.ConvertFromString(newValue);

            entity.GetType().GetProperty(columnsOriginalHeaders[e.Column.DisplayIndex]).SetValue(entity, castedValue, null);
            isDataTabEditing = false;
            DeleteItems.IsEnabled = isDataTabEditing;
        }

        private void DataTab_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            try
            {
                object entities = context.GetType().GetProperty(curTableName).GetValue(context, null);
                object dataContext = e.Row.DataContext;
                try{
                    object obj = dataContext.GetType().GetProperty("ID").GetValue(dataContext, null);
                    int isNewId;
                    if (obj.GetType() == typeof(short))
                    {
                        isNewId = (short)obj;
                        if (isNewId == 0)
                            dataContext.GetType().GetProperty("ID").SetValue(dataContext, (short)(++generatorValueForCurTable), null);
                    }
                    else
                    {
                        isNewId = (int)obj;
                        //0 - значение id вместо null. Все валиные id в базе > 0
                        if (isNewId == 0)
                            dataContext.GetType().GetProperty("ID").SetValue(dataContext, ++generatorValueForCurTable, null);
                    }
                }
                catch{}

                MethodInfo mListAdd = entities.GetType().GetMethod("AddObject");
                mListAdd.Invoke(entities, new object[] { dataContext });
            }
            catch { }
           
            try
            {
                context.SaveChanges();
                context.AcceptAllChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                RailwaysData.sharedContext.SaveChanges();
            }
            catch
            {

            }
        }


        public void LanguageChanged()
        {
            int i = 0;
            foreach (ComboBoxItem item in TablesComboBox.Items)
            {
                string name = tables[i++];
                if (name != "USERS" && name != "PERMISSION_TYPE")
                    item.Content = TypesConverter.GetResource(name);
            }
        }
     }
}
