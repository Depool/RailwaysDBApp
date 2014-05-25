using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDBApp.Controllers;
using RailwaysDAL;
using System.Data.Objects;
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

    internal class StationAndTrains
    {
        public string StationName{get;set;}
        public int? ID { get; set; }
        public string start { get; set; }
        public string finish { get; set; }
    }

    internal class StationName
    {
        public string NAME { get; set; }

        public static List<object> ColumnsMappings()
        {
            return new List<object>(){
                                        TypesConverter.GetResource("STATIONS_NAME"),
                                     };
        }
    }


    internal class TrainsDestinationHelp
    {
        public int? ID{get;set;}
        public Int16? MIN{get;set;}
        public Int16? MAX{get;set;} //!!!
    }

    internal class RacesForTrain
    {
        public int? ID { get; set; }
        public DateTime START_TIME { get; set; }
        public short? FORWARD { get; set; }
        public static List<object> ColumnsMappings()
        {
            return new List<object>(){
                                        TypesConverter.GetResource("RACES_ID"),
                                        TypesConverter.GetResource("RACES_START_TIME"),
                                        TypesConverter.GetResource("RACES_FORWARD")
                                     };
        }

    }

    internal class TrainIDAndStopNum
    {
        public int? TRAIN_ID { get; set; }
        public short? STOP_NUMBER { get; set; }
        public short? sn { get; set; }
    }

    internal class TicketsForRace
    {
        public string start { get; set; }
        public string finish { get; set; }
        public short carriageNumber { get; set; }
        public short placeNumber { get; set; }
        public static List<object> ColumnsMappings()
        {
            return new List<object>(){
                                        TypesConverter.GetResource("TRAINS_START"),
                                        TypesConverter.GetResource("TRAINS_FINISH"),
                                        TypesConverter.GetResource("TICKETS_CARRIAGE_NUMBER"),
                                        TypesConverter.GetResource("TICKETS_PLACE_NUMBER")
                                     };
        }
    }

    internal class ProfitRecord
    {
        public int raceID { get; set; }
        public int trainID { get; set; }
        public string start { get; set; }
        public string finish { get; set; }
        public int ticketsSold { get; set; }
        public float profit { get; set; }

        public static List<object> ColumnsMappings()
        {
            return new List<object>(){
                                        TypesConverter.GetResource("RACES_ID"),
                                        TypesConverter.GetResource("TRAINS_ID"),
                                        TypesConverter.GetResource("TRAINS_START"),
                                        TypesConverter.GetResource("TRAINS_FINISH"),
                                        TypesConverter.GetResource("ProfitRecord_Amount"),
                                        TypesConverter.GetResource("ProfitRecord_Profit")
                                     };
        }
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
                string query = String.Format("select count(COLUMN_NAME)\n" +
                                             "from\n" +
                                             "INFORMATION_SCHEMA.columns \n" +
                                             "where TABLE_NAME='{0}'", table);
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
                string query = String.Format("select COLUMN_NAME\n" +
                                             "from\n" +
                                             "INFORMATION_SCHEMA.columns \n" +
                                             "where TABLE_NAME='{0}'\n"+
                                             "order by\n" +
                                             "ORDINAL_POSITION asc", table);
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
                tables = context.ExecuteStoreQuery<string>("select name from sys.tables where type_desc = 'USER_TABLE'\n" +
                                                           "order by name").ToList();
                
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

        private static List<TrainsDestinationHelp> GetTrainDestinationsHelp(string filter)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<TrainsDestinationHelp> result = new List<TrainsDestinationHelp>();
            try
            {
                result = context.ExecuteStoreQuery<TrainsDestinationHelp>("select TRAINS.ID, min(st.STOP_NUMBER) as MIN, max(st.STOP_NUMBER) as MAX\n" +
                                                                                                    "from TRAINS\n" +
                                                                                                    "inner join STOPS as st\n" +
                                                                                                    "on TRAINS.ID = st.TRAIN_ID " + filter +
                                                                                                    "group by TRAINS.ID\n").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        private static List<TrainDestinations> GetTrainDestinationsByHelpInfo(List<TrainsDestinationHelp> help)
        {
            List<TrainDestinations> res = new List<TrainDestinations>();
            RailwaysEntities context = RailwaysData.sharedContext;
            foreach (TrainsDestinationHelp h in help)
            {
                List<int> ids = context.ExecuteStoreQuery<int>("select STOPS.STATION_ID\n" +
                                                                "from STOPS\n" +
                                                                "where STOPS.TRAIN_ID={0} And (STOPS.STOP_NUMBER={1} Or STOPS.STOP_NUMBER={2})\n"+
                                                                "order by STOPS.STOP_NUMBER\n", h.ID, h.MIN, h.MAX).ToList();
                int mn = ids[0];
                int mx = ids[1];
                List<string> names = context.ExecuteStoreQuery<string>("select STATIONS.NAME\n" +
                                                                        "from STATIONS\n" +
                                                                        "where STATIONS.ID={0}\n", mn).ToList();
                TrainDestinations dest = new TrainDestinations();
                dest.ID = h.ID; dest.start = names[0];
                names = context.ExecuteStoreQuery<string>("select STATIONS.NAME\n" +
                                                          "from STATIONS\n" +
                                                          "where STATIONS.ID={0}\n", mx).ToList();
                dest.finish = names[0];
                res.Add(dest);
            }
            return res;
        }

        public static List<TrainDestinations> GetTrainDestinations()
        {
            List<TrainDestinations> res = new List<TrainDestinations>();
            List<TrainsDestinationHelp> help = GetTrainDestinationsHelp(String.Empty);
            res = GetTrainDestinationsByHelpInfo(help); 

            return res;
        }

        public static List<StationName> GetCorrespondingStationsFor(string name)
        {
            List<StationName> res = new List<StationName>();

            RailwaysEntities context = RailwaysData.sharedContext;
            string query = String.Format("select st.ID " +
                                                          "from STATIONS as st " +
                                                          "where st.NAME='{0}'", name);
            List<int> id = context.ExecuteStoreQuery<int>(query).ToList();
            if (id.Count > 0)
            {
                res = context.ExecuteStoreQuery<StationName>("select STATIONS.NAME\n" +
                                                             "from EDGES\n" +
                                                             "inner join STATIONS\n" +
                                                             "on (STATIONS.ID!={0}) And\n" +
                                                             "(EDGES.START_ID={0} Or EDGES.FIN_ID={0}) And\n" +
                                                             "((EDGES.START_ID=STATIONS.ID) Or (EDGES.FIN_ID=STATIONS.ID))\n", id[0]).ToList();
            }
            return res;
        }

        public static List<RacesForTrain> GetRacesForTrainId(int id)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<RacesForTrain> res = context.ExecuteStoreQuery<RacesForTrain>("select ID,START_TIME,FORWARD\n" +
                                                                                "from RACES\n" +
                                                                                "where TRAIN_ID={0}\n", id).ToList();
            return res;
        }

        public static short? GetStopNumberByTrainAndStationId(int trainId, int stationId)
        {
            RailwaysEntities context = RailwaysData.sharedContext;

            List<short> result = context.ExecuteStoreQuery<short>("SELECT STOP_NUMBER\n" +
                                                                  "FROM STOPS\n" +
                                                                  "WHERE TRAIN_ID={0} AND STATION_ID={1}", trainId, stationId).ToList();
            if (result.Count == 0)
                return null;
            else
                return result[0];
        }

        public static List<TicketsForRace> GetTicketsForRace(int id)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<TICKET> tickets = context.ExecuteStoreQuery<TICKET>("select *\n"+
                                                                     "from TICKETS\n"+
                                                                     "where RACE_ID={0}",id).ToList();
            List<TicketsForRace> res = new List<TicketsForRace>();
            if (tickets.Count > 0)
            {
                List<int> trainId = context.ExecuteStoreQuery<int>("select TRAIN_ID\n"+
                                                                   "from RACES\n"+
                                                                   "where RACES.ID={0}",id).ToList();
                foreach (TICKET ticket in tickets)
                {
                    TicketsForRace r = new TicketsForRace();
                    r.placeNumber = ticket.PLACE_NUMBER;
                    r.carriageNumber = ticket.CARRIAGE_NUMBER;

                    //будет ровно одна запись в ответе при условии корректности БД
                    try
                    {
                        r.start = ticket.START_STATION; 
                        /*context.ExecuteStoreQuery<string>("select STATIONS.NAME\n" +
                                                                    "from STOPS\n" +
                                                                    "inner join STATIONS\n" +
                                                                    "on STOPS.TRAIN_ID={0} And STOPS.STOP_NUMBER={1}\n" +
                                                                    "And STOPS.STATION_ID=STATIONS.ID",trainId[0],ticket.START_STATION_NUMBER).ToList()[0];*/
                        r.finish = ticket.FINISH_STATION;
                        /*context.ExecuteStoreQuery<string>("select STATIONS.NAME\n" +
                                                                    "from STOPS\n" +
                                                                    "inner join STATIONS\n" +
                                                                    "on STOPS.TRAIN_ID={0} And STOPS.STOP_NUMBER={1}\n" +
                                                                    "And STOPS.STATION_ID=STATIONS.ID", trainId[0], ticket.FIN_STATION_NUMBER).ToList()[0];*/
                        res.Add(r);
                    }
                    catch { }

                }
            }
            return res;
        }

        public static List<int> GetCityIdByName(string city)
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            List<int> res = context.ExecuteStoreQuery<int>("SELECT ID " +
                                                           "FROM STATIONS as a " +
                                                           "where a.NAME={0}", city).ToList();
            return res;
        }

        public static List<RACE> GetProperRaces(string startCity, string finCity)
        {
            List<RACE> result = new List<RACE>();

            RailwaysEntities context = RailwaysData.sharedContext;
            List<int> sID = GetCityIdByName(startCity);
            if (sID.Count == 0)
            {
                MessageBox.Show(String.Format(TypesConverter.GetResource("Queries_CityNotExist"), startCity));
                return result;
            }
            List<int> fID = GetCityIdByName(finCity);
            if (fID.Count == 0)
            {
                MessageBox.Show(String.Format(TypesConverter.GetResource("Queries_CityNotExist"), finCity));
                return result;
            }

            List<TrainIDAndStopNum> trains = context.ExecuteStoreQuery<TrainIDAndStopNum>("select one.TRAIN_ID,one.STOP_NUMBER,sn from(\n" +
                                                                                          "select STOPS.TRAIN_ID,STOPS.STOP_NUMBER from STOPS where STATION_ID={0}) one\n" +
                                                                                           "inner join\n" +
                                                                                           "(select two.TRAIN_ID,two.STOP_NUMBER as sn from(\n" +
                                                                                           "select STOPS.TRAIN_ID,STOPS.STOP_NUMBER from STOPS where STATION_ID={1}) two) three\n" +
                                                                                           "on one.TRAIN_ID=three.TRAIN_ID", sID[0], fID[0]).ToList();
            foreach (TrainIDAndStopNum tr in trains)
            {
                string condition = String.Empty;
                if (tr.STOP_NUMBER < tr.sn)
                    condition = " And FORWARD=0";
                if (tr.STOP_NUMBER > tr.sn)
                    condition = " And FORWARD=1";
                result.AddRange(context.ExecuteStoreQuery<RACE>("select * from RACES\n"+
                                                                "where TRAIN_ID={0}"+condition,tr.TRAIN_ID).ToList());
            }

            return result;
        }

        public static List<TrainDestinations> GetTrainsThroughStation(string name)
        {
            List<TrainDestinations> result = new List<TrainDestinations>();
            RailwaysEntities context = RailwaysData.sharedContext;
            List<int> ID = GetCityIdByName(name);
            if (ID.Count == 0)
            {
                MessageBox.Show(String.Format(TypesConverter.GetResource("Queries_CityNotExist"), name));
                return result;
            }
            string filter = String.Format("And TRAINS.ID in (select STOPS.TRAIN_ID from STOPS where STOPS.STATION_ID={0})\n",ID[0]);
            List<TrainsDestinationHelp> help = GetTrainDestinationsHelp(filter);
            result = GetTrainDestinationsByHelpInfo(help);
            return result;
        }

        public static TrainDestinations GetOneTrainDestination(int id)
        {
            List<TrainsDestinationHelp> help = GetTrainDestinationsHelp(String.Format("And TRAINS.ID={0}",id));
            List<TrainDestinations> result = GetTrainDestinationsByHelpInfo(help);

            if (result.Count != 1)
                throw new ApplicationException("Something wrong. One train can`t have not one destinations value");
            else
                return result[0];
        }


        //comparer
        private static int profitRecordComparer(ProfitRecord a, ProfitRecord b)
        {
            return a.raceID - b.raceID;
        }

        public static List<ProfitRecord> GetProfits()
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            Dictionary<int, ProfitRecord> groups = new Dictionary<int, ProfitRecord>();
            List<TICKET> tickets = context.TICKETS.ToList();
            foreach (TICKET ticket in tickets)
            {
                int? id = ticket.RACE_ID;
                if (!groups.ContainsKey((int)id))
                {
                    ProfitRecord record = new ProfitRecord();
                    TrainDestinations dest = GetOneTrainDestination((int)ticket.RACE.TRAIN_ID);

                    record.raceID = (int)id;
                    record.trainID = (int)dest.ID;
                    record.start = dest.start;
                    record.finish = dest.finish;
                    if (ticket.RACE.FORWARD == 1)
                    {
                        string x = record.start;
                        record.start = record.finish;
                        record.finish = x;
                    }
                    record.ticketsSold = 0; record.profit = 0;
                    groups.Add((int)id, record);
                }

                ProfitRecord profRec = groups[(int)ticket.RACE_ID];
                profRec.profit += (float)ticket.PRICE;
                profRec.ticketsSold++;

                context.SaveChanges();
            }

            List<ProfitRecord> res = groups.Values.ToList();
            res.Sort(profitRecordComparer);
            return res;
        }
    }
}
