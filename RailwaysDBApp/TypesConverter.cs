using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Reflection;
using System.Data;

namespace RailwaysDBApp
{

    internal static class TypesConverter
    {
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource BitmapToWPFBitmapSource(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty, 
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        public static string StringToSHA256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }

        public static object CreateGenericList(Type typeX) 
        { 
            Type listType = typeof(List<>); 
            Type[] typeArgs = {typeX}; 
            Type genericType = listType.MakeGenericType(typeArgs); 
            object o = Activator.CreateInstance(genericType); 
            return o;
        }

        public static string GetResource(string key)
        {
            string value = null;
            string curResourceFile = "RailwaysDBApp.Properties.Resources";
            string localization = Properties.Settings.Default.Localization;

            System.Resources.ResourceManager RM = new System.Resources.ResourceManager(curResourceFile, Assembly.GetExecutingAssembly());
            try
            {
                value = RM.GetString(key).ToString();
            }
            catch
            {
            }

            if (value != null)
                return value;
            else
                return key;
        }

        public static void EntityToDataSet(DataSet dataSet, object entity)
        {
            string tableName = String.Empty;
            DataRow newRow = null;
            PropertyInfo[] props = entity.GetType().GetProperties();

            tableName = entity.GetType().Name;
            newRow = dataSet.Tables[tableName].NewRow();

            foreach (PropertyInfo field in props)
            {
                if (newRow.Table.Columns.Contains(field.Name))
                    newRow[field.Name] = field.GetValue(entity, null);
            }
            dataSet.Tables[tableName].Rows.Add(newRow);
        }
    }
}
