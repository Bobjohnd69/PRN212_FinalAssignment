using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT
{
    public class UserSession
    {
        public static User SessionUser { get; set; }

        public static void SetSessionUser(User user)
        {
            SessionUser = user;
        }



        public static void ClearSession()
        {
            SessionUser = null;
        }
    }
}
