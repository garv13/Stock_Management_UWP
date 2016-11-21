using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_UWP
{
    class mahSql
    {
        private static IMobileServiceTable<ProductClass> Table = App.MobileService.GetTable<ProductClass>();
        public static MobileServiceCollection<ProductClass, ProductClass> items;
        public static async void add(ProductClass a) {
            try
            {
                a = crypt.encrypt(a);
               
                a = crypt.decrypt(a);
               
            }
            catch
            {

            }
        }
        public async static void delete(ProductClass a) {
            a = crypt.encrypt(a);
            await Table.DeleteAsync(a);
            a = crypt.decrypt(a);
           
        }
        public async static void update(ProductClass a) {
            a = crypt.encrypt(a);
            await Table.UpdateAsync(a);
            a = crypt.decrypt(a);

           
        }
       
        
      
    }
}
