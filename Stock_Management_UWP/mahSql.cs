using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_UWP
{
    class mahSql
    {
        public static void add(ProductClass a) {
            App.Table.Add(a);

        }
        public static void delete(ProductClass a) {
            App.Table.Remove(a);
        }
        public static void update(ProductClass a) {
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
