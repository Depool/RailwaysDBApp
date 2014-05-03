using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDAL;

namespace RailwaysDBApp.Controllers
{
    internal static class RailwaysData
    {
        private static RailwaysEntities context = null;

        public static RailwaysEntities sharedContext
        {
            get
            {
                if (context == null)
                    context = new RailwaysEntities();
                return context;
            }
        }

    }
}
