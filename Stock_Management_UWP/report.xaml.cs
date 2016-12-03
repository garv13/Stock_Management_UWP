using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Stock_Management_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class report : Page
    {
        private static IMobileServiceTable<Logs> Table = App.MobileService.GetTable<Logs>();
        private static MobileServiceCollection<Logs, Logs> items;
        private static IMobileServiceTable<ProductClass> Table2 = App.MobileService.GetTable<ProductClass>();
        private static MobileServiceCollection<ProductClass, ProductClass> items2;

        public report()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string content = "";
            List<Logs> l = new List<Logs>();
            int i = 0;
            DateTime start = Start_date.Date.DateTime;
            DateTime end = End_date.Date.DateTime;

            try {
                while (true)
                {
                    items = await Table.Where(Logs => Logs.DateTime >= start && Logs.DateTime<= end).OrderByDescending(Logs=> Logs.ProductId).Skip(i*50).Take(50).ToCollectionAsync();
                    i++;
                    foreach (Logs t in items)
                    {
                        l.Add(t);
                    }
                    i++;
                    if (items.Count < 50)
                    {
                        break;
                    }
                }

            }
            catch (Exception)
            { //do someothing here
            }
            //we got all the logs for the dates
            ProductClass p = new ProductClass();
            content = "Material";
            DateTime d = new DateTime();
            for (i =0; i <= end.DayOfYear-start.DayOfYear; i++)
            {
                content += "," + start.AddDays(i).Date.ToString().Split(' ')[0];
            }
           
            string id = "lol";
            for (i = 0; i < l.Count; i++)
            {
                if (id != l[i].ProductId)
                {
                    items2 = await Table2.Where(ProductClass => ProductClass.Id == l[i].ProductId).ToCollectionAsync();
                    p = items2[0];
                    content += "\n" + p.Material + " " + p.Quality + " " + p.Color + " " + p.Name + " " + p.Source;
                    id = l[i].ProductId;
                    d = start;
                }
                else
                {
                    while(true)
                    {
                        if (l[i].DateTime.DayOfYear == d.DayOfYear)
                        {
                            content += ",";
                            break;
                        }
                        else
                        {
                            content += ",";
                        }

                    }
                }
            }

            if (toggleSwitch.IsOn)
            {
                //we need all entries
            }
            else
            {

            }
           
        }
    }
}
