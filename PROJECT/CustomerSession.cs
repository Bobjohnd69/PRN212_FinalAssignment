using Repo.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT
{
    public class CustomerSession
    {
        public static Customer SessionCustomer { get; set; }

        public static void SetSessionCustomer(Customer Customer)
        {
            SessionCustomer = Customer;
        }



        public static void ClearSession()
        {
            SessionCustomer = null;
        }
    }
}
