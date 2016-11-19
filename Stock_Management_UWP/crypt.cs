using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_UWP
{
    class crypt
    {
        public static ProductClass encrypt(ProductClass a)
        {
            //encrypt everythin except the id
            return a;
        }
        public static ProductClass decrypt(ProductClass a)
        {
            return a;
        }
        public static Logs decryptLog(Logs a)
        {
            return a;
        }
        public static Logs encryptLog(Logs a)
        {
            //only escrypt contect thats it

            return a;
        }
    }
}
