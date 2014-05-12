using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDBApp.Controllers;
using RailwaysDAL;
using System.Windows;

namespace RailwaysDBApp.Models
{
    //Return types for queries

    internal class CarriagesOfTrains
    {
        public int? ID { get; set; }
        public string NAME { get; set; }
        public short? AMOUNT { get; set; }

        public static List<object> ColumnsMappings()
        {
            return new List<object>(){
                                        TypesConverter.GetResource("TRAINS_ID"),
                                        TypesConverter.GetResource("CARRIAGE_TYPE_NAME"),
                                        TypesConverter.GetResource("CARRIAGES_AMOUNT")
                                     };
        }
    }

    internal class TrainDestinations
    {
        public int? ID{get;set;}
        public string start { get; set; }
        public string finish { get; set; }

        public static List<object> ColumnsMappings()
        {
            return new List<object>(){
                                        TypesConverter.GetResource("TRAINS_ID"),
                                        TypesConverter.GetResource("TRAINS_START"),
                                        TypesConverter.GetResource("TRAINS_FINISH")
                                     };
        }
    }

    internal class TrainsDestinationHelp
    {
        public int? ID{get;set;}
        public int? MIN{get;set;}
        public int? MAX { get; set; }
    }

    internal static class RailwaysDBQueries
    {
        //Queries to system tables
        public static int GetNumberOfDomainsInTable(string table)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<int> result = new List<int>();
            try
            {
                string query = String.Format("select count(RDB$FIELD_NAME) from RDB$RELATION_FIELDS\n"+
                                             "where (RDB$RELATION_NAME = '{0}')", table);
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

        public static List<string> GetNamesOfDomainsInTable(string table)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<string> result = new List<string>();
            try
            {
                string query = String.Format("select RDB$FIELD_NAME from RDB$RELATION_FIELDS\n"+ 
                                             "where (RDB$RELATION_NAME = '{0}')", table);
                result = context.ExecuteStoreQuery<string>(query).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
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


        //select TRAIN_TYPE.NAME,TRAINS.ID from TRAIN_TYPE inner join TRAINS on TRAIN_TYPE.ID=TRAINS.TRAIN_TYPE_ID
        //Resultable queries
        public static List<CarriagesOfTrains> GetCarriagesOfTrains()
        {
            RailwaysEntities context = RailwaysData.sharedContext;

            List<CarriagesOfTrains> res = context.ExecuteStoreQuery<CarriagesOfTrains>("select TRAINS.ID,CARRIAGE_TYPE.NAME,CARRIAGES.AMOUNT\n" +
                                                                                       "from TRAINS\n" +
                                                                                       "inner join CARRIAGES\n" +
                                                                                       "on TRAINS.ID=CARRIAGES.TRAIN_ID\n" +
                                                                                       "inner join CARRIAGE_TYPE\n" +
                                                                                       "on CARRIAGES.CARRIAGE_TYPE_ID=CARRIAGE_TYPE.ID\n"+
                                                                                       "order by TRAINS.ID\n").ToList();
            return res;
        }

        public static List<TrainDestinations> GetTrainDestinations()
        {
            List<TrainDestinations> res = new List<TrainDestinations>();

            RailwaysEntities context = RailwaysData.sharedContext;
            List<TrainsDestinationHelp> help = context.ExecuteStoreQuery<TrainsDestinationHelp>("select TRAINS.ID, MIN(STOPS.STOP_NUMBER),MAX(STOPS.STOP_NUMBER)\n" +
                                                                                                "from TRAINS\n" +
                                                                                                "left join STOPS\n" +
                                                                                                "on TRAINS.ID = STOPS.TRAIN_ID\n" +
                                                                                                "group by TRAINS.ID\n").ToList();
            foreach(TrainsDestinationHelp h in help)
            {
                List<int> ids = context.ExecuteStoreQuery<int>("select STOPS.STATION_ID\n" +
                                                                "from STOPS\n" +
                                                                "where STOPS.TRAIN_ID={0} And (STOPS.STOP_NUMBER={1} Or STOPS.STOP_NUMBER={2})\n", h.ID, h.MIN, h.MAX).ToList();
                int mn = ids[0];
                int mx = ids[1];
                List<string> names = context.ExecuteStoreQuery<string>("select STATIONS.NAME\n" +
                                                                        "from STATIONS\n" +
                                                                        "where STATIONS.ID={0} or STATIONS.ID={1}\n", mn, mx).ToList();
                TrainDestinations dest = new TrainDestinations();
                dest.ID = h.ID; dest.start = names[0]; dest.finish = names[1];
                res.Add(dest);
            }

            return res;
        }
        

    }
}
