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
       
       
        public static  void add(ProductClass a) {
            try
            {
                a = crypt.encrypt(a);
               
                a = crypt.decrypt(a);
               
            }
            catch
            {

            }
        }
        public  static void delete(ProductClass a) {
            a = crypt.encrypt(a);
            
            a = crypt.decrypt(a);
           
        }
        public  static void update(ProductClass a) {
            a = crypt.encrypt(a);
            
            a = crypt.decrypt(a);

           
        }
       
        
      
    }
}
