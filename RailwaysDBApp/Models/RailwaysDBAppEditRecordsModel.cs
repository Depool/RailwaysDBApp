using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDBApp.Controllers;
using RailwaysDAL;

namespace RailwaysDBApp.Models
{
    internal class RailwaysDBAppEditRecordsModel 
    {
        private List<string> domainNames;
        private Type recordType;

        RailwaysDBAppEditRecordsModel(string tableName)
        {
            ChangeEditingTable(tableName);
        }

        public void ChangeEditingTable(string tableName)
        {
            domainNames = RailwaysDBQueries.GetNamesOfDomainsInTable(tableName);
            RailwaysEntities context = RailwaysData.sharedContext;
            context.GetType().GetProperty(tableName).GetValue(context, null);
        }
    }
}
