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
        private static IMobileServiceTable<Logs> Table = App.MobileService.GetTable<Logs>();
        private static MobileServiceCollection<Logs, Logs> items;
        public static List<Logs> log = new List<Logs>();
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string DateTime { get; set; }
        public string Content { get; set; }

        public static void createLog(Logs a)
        {
            a= crypt.encryptLog(a);
            App.MobileService.GetTable<Logs>().InsertAsync(a);

        }
        public async static void load()
        {
           
            items = await Table.ToCollectionAsync();
            log.Clear();
            foreach (Logs p in items)
            {
                Logs q = crypt.decryptLog(p);
                log.Add(q);
            }
            
        }


    }
}
