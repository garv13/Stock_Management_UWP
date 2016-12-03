using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_UWP
{
    class Logs
    {
     
        public static List<Logs> log = new List<Logs>();
        public string Id { get; set; }
        public string ProductId { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }

        public static void createLog(Logs a)
        {
            a= crypt.encryptLog(a);
            a.DateTime = System.DateTime.Now;
            App.MobileService.GetTable<Logs>().InsertAsync(a);

        }
       


    }
}
