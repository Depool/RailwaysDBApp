using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDBApp.Controllers;
using RailwaysDAL;
using System.Windows;

namespace RailwaysDBApp.Models
{
    internal static class RailwaysDBQueries
    {
        public static int GetNumberOfDomainsInTable(string table)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<int> result = new List<int>();
            try
            {
                string query = String.Format("select count(RDB$FIELD_NAME) from RDB$RELATION_FIELDS where (RDB$RELATION_NAME = '{0}')", table);
                result = context.ExecuteStoreQuery<int>(query).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (result.Count == 0)
                return 0;
            else
                return result[0];
        }

        public static List<string> GetUserTablesNames()
        {
            List<string> tables = null;
            RailwaysEntities context = RailwaysData.sharedContext;
            try
            {
                tables = context.ExecuteStoreQuery<string>("select RDB$RELATION_NAME from RDB$RELATIONS" +
                                                           " where (RDB$SYSTEM_FLAG = 0) AND (RDB$RELATION_TYPE = 0)" +
                                                           " order by RDB$RELATION_NAME").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return tables;
        }

    }
}
