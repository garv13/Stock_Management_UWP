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
        private MobileServiceCollection<ProductClass, ProductClass> items;
        public static async void add(ProductClass a) {
            try
            {
                a = crypt.encrypt(a);
                await App.MobileService.GetTable<ProductClass>().InsertAsync(a);
                a = crypt.decrypt(a);
                App.Table.Add(a);
            }
            catch
            {

            }
        }
        public async static void delete(ProductClass a) {
            a = crypt.encrypt(a);
            await Table.DeleteAsync(a);
            a = crypt.decrypt(a);
            App.Table.Remove(a);
        }
        public async static void update(ProductClass a) {
            a = crypt.encrypt(a);
            await Table.UpdateAsync(a);
            a = crypt.decrypt(a);

            for (int i = 0; i < App.Table.Count; i++)
            {
                if (App.Table[i].Id == a.Id)
                {
                    App.Table[i] = a;
                    break;
                }
            }
        }
        public static List<ProductClass> query(ProductClass p) {
            List<ProductClass> temp = new List<ProductClass>();
            if (p.Color != "")
            {
                var a = App.Table.Where(ProductClass => ProductClass.Color==p.Color);
                foreach (ProductClass l in a)
                {
                    temp.Add(l);
                }
            }
            if (p.Name != "")
            {
                var a = temp.Where(ProductClass => ProductClass.Name.Contains(p.Name));
                temp.Clear();
                foreach (ProductClass l in a)
                {
                    temp.Add(l);
                }
            }
            if (p.Quality != "")
            {
                var a = temp.Where(ProductClass => ProductClass.Quality == p.Quality);
                temp.Clear();
                foreach (ProductClass l in a)
                {
                    temp.Add(l);
                }
            }
            if (p.Source != "")
            {
                var a = temp.Where(ProductClass => ProductClass.Source.Contains(p.Source));
                temp.Clear();
                foreach (ProductClass l in a)
                {
                    temp.Add(l);
                }
            }

            return temp;
        }
        public static void load() {

        }
      
    }
}
