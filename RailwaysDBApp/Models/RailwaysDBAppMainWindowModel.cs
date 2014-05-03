using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDAL;
using RailwaysDBApp.Controllers;

namespace RailwaysDBApp.Models
{
    internal class RailwaysDBAppMainWindowModel
    {
        public object CheckAuthorization(String login, String password)
        {
            List<USER> user = (from rec in RailwaysData.sharedContext.USERS
                       where (rec.LOGIN == login)
                       select rec).ToList();
            if (user.Count == 0)
                return null;
            else
            {
                string hashed = TypesConverter.StringToSHA256(password);
                if (hashed == user[0].PASS_WORD)
                    return ((object)user[0].PERMISSION);
                else
                    return null;
            }
        }
    }
}
