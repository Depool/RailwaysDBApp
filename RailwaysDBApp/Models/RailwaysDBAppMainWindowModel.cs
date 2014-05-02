using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailwaysDAL;

namespace RailwaysDBApp.Models
{
    internal class RailwaysDBAppMainWindowModel
    {

        public bool CheckAuthorization(String login, String password)
        {
            List<USER> user = (from rec in RailwaysData.sharedContext.USERS
                       where (rec.LOGIN == login)
                       select rec).ToList();
            if (user.Count == 0)
                return false;
            else
            {
                string hashed = TypesConverter.StringToSHA256(password);
                return (hashed == user[0].PASS_WORD);     
            }
        }
    }
}
