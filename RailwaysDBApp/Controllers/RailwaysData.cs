using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDAL;
using RailwaysDBApp.Models;

namespace RailwaysDBApp.Controllers
{
    internal static class RailwaysData
    {
        private static RailwaysEntities context = null;
        private static Dictionary<string, int> ids = new Dictionary<string, int>();
        private static bool filled = false;

        public static RailwaysEntities sharedContext
        {
            get
            {
                if (context == null)
                    context = new RailwaysEntities();
                return context;
            }
        }

        public static void FillIds()
        {
            if (!filled)
            {
                List<string> tables = RailwaysDBQueries.GetUserTablesNames();
                
                foreach (string table in tables)
                {
                    dynamic d = context.GetType().GetProperty(table).GetValue(context, null);

                    int? ans = null;
                    if (context.GetType().GetProperty(table).PropertyType.GetGenericArguments()[0].GetProperty("ID") != null)
                        ans = 0;

                    foreach (dynamic q in d)
                    {
                        try
                        {
                            int id = (int)context.GetType().GetProperty(table).PropertyType.GetGenericArguments()[0].GetProperty("ID").GetValue(q, null);
                            if (id > ans)
                                ans = id;
                        }
                        catch { }
                    }
                    if (ans != null)
                        ids.Add(table, (int)ans + 1);
                }
                filled = true;
              }
            }

        public static int GetIndexForTable(string tableName)
        {
            if (!ids.ContainsKey(tableName))
                throw new ApplicationException(String.Format("Table {0} doesn`t have autoincrement field ID",tableName));
            ids[tableName]++;
            return ids[tableName] - 1;
        }
    }
}
