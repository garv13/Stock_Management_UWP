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
        private static MobileServiceCollection<string, string> items3;


        public report()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {

            string content = "";
            List<Logs> l = new List<Logs>();
            List<ProductClass> pro = new List<ProductClass>();

            int i = 0;
            DateTime start = Start_date.Date.DateTime;
            DateTime end = End_date.Date.DateTime;
            List<string> ids = new List<string>();
            try {
                while (true)
                {
                    items = await Table.Where(Logs => Logs.DateTime >= start && Logs.DateTime<= end && !Logs.Content.Contains("new")).OrderByDescending(Logs=> Logs.ProductId).Skip(i*50).Take(50).ToCollectionAsync();
                    items3 = await Table.Where(Logs => Logs.DateTime >= start && Logs.DateTime <= end && !Logs.Content.Contains("new")).Select(Logs => Logs.ProductId).Skip(i * 50).Take(50).ToCollectionAsync();
                    
                    i++;
                    foreach (Logs t in items)
                    {
                        l.Add(t);
                    }

                    foreach (string t in items3)
                    {
                        ids.Add(t);
                    }
                    ids = ids.Distinct().ToList<string>();
                    i++;
                    if (items.Count < 50)
                    {
                        break;
                    }
                }

            }
            catch (Exception)
            { //do someothing 
                return;
            }
            ProductClass p = new ProductClass();
            string[,] con = new string[ids.Count, end.DayOfYear - start.DayOfYear+3];
            for (i = 0; i < ids.Count; i++)
            {
                for (int j = 0; j < end.DayOfYear - start.DayOfYear + 2; j++)
                {
                    con[i, j] = "0";
                }
            }


            for (i = 0; i < l.Count; i++)
            {
                items2 = await Table2.Where(ProductClass => ProductClass.Id == l[i].ProductId).ToCollectionAsync();
                p = items2[0];
                con[ids.IndexOf(l[i].ProductId), 0] = p.Material + " " + p.Quality + " " + p.Color + " " + p.Name + " " + p.Source;
                con[ids.IndexOf(l[i].ProductId), end.DayOfYear - start.DayOfYear + 2] = p.Quantity;
                if (l[i].Content.Split(' ')[0] == "Added")
                {
                    int t = int.Parse(con[ids.IndexOf(l[i].ProductId), l[i].DateTime.DayOfYear - start.DayOfYear + 1]);
                    t = t + int.Parse(l[i].Content.Split(' ')[1]);
                    con[ids.IndexOf(l[i].ProductId), l[i].DateTime.DayOfYear - start.DayOfYear + 1] = t.ToString();

                }
                else
                {
                    int t = int.Parse(con[ids.IndexOf(l[i].ProductId), l[i].DateTime.DayOfYear - start.DayOfYear + 1]);
                    t = t - int.Parse(l[i].Content.Split(' ')[1]);
                    con[ids.IndexOf(l[i].ProductId), l[i].DateTime.DayOfYear - start.DayOfYear + 1] = t.ToString();


                }
                  

            }
            content = "Material";
            for (i = 0; i <= end.DayOfYear - start.DayOfYear; i++)
            {
                content += "," + start.AddDays(i).Date.ToString().Split(' ')[0];
            }
            content += ", Quantity \n";

            for (i = 0; i < ids.Count; i++)
            {
                for (int j = 0; j < end.DayOfYear - start.DayOfYear + 3; j++)
                {
                    if (con[i, j] == "0")
                    {
                        content += ",";
                    }
                    else
                    {
                        content += con[i, j] + ",";
                    }
                }
                content = content.Substring(0, content.Length - 1);
                content += "\n";
            }
            //we have of unchanged ready
            if (toggleSwitch.IsOn)
            {
                try
                {
                    i = 0;
                    while (true)
                    {
                        items2 = await Table2.Where(ProductClass => !ids.Contains(ProductClass.Id)).Skip(i * 50).Take(50).ToCollectionAsync();
                        i++;
                        foreach (ProductClass t in items2)
                        {
                            pro.Add(t);
                        }
                        i++;
                        if (items2.Count < 50)
                        {
                            break;
                        }

                    }
                    con = new string[pro.Count, end.DayOfYear - start.DayOfYear + 3];
                    for (i = 0; i < pro.Count; i++)
                    {
                        for (int j = 0; j < end.DayOfYear - start.DayOfYear + 2; j++)
                        {
                            con[i, j] = "0";
                        }
                    }

                    for (int j = 0; j < pro.Count; j++)
                    {
                        p = pro[j];
                        con[j, 0] = p.Material + " " + p.Quality + " " + p.Color + " " + p.Name + " " + p.Source;
                        con[j, end.DayOfYear - start.DayOfYear + 2] = p.Quantity;
                    }
                    for (i = 0; i < pro.Count; i++)
                    {
                        for (int j = 0; j < end.DayOfYear - start.DayOfYear + 3; j++)
                        {
                            if (con[i, j] == "0")
                            {
                                content += ",";
                            }
                            else
                            {
                                content += con[i, j] + ",";
                            }
                        }
                        content = content.Substring(0, content.Length - 1);
                        content += "\n";
                    }

                }
                catch (Exception)
                { }
            }

                //we got all the logs for the dates and ids of products

                //content = "Material";
                DateTime d = new DateTime();
            //for (i =0; i <= end.DayOfYear-start.DayOfYear; i++)
            //{
            //    content += "," + start.AddDays(i).Date.ToString().Split(' ')[0];
            //}
            //content += ", Quantity";
            //string id = "lol";
            //    int k = 0;
            //for (i = 0; i < l.Count; i++)
            //{

            //    if (id != l[i].ProductId)
            //    {
            //        for (; k <= end.DayOfYear - start.DayOfYear; k++)
            //        {
            //            content += ",";
            //        }
            //        content += "," + p.Quantity;
            //        k = 0;
            //        items2 = await Table2.Where(ProductClass => ProductClass.Id == l[i].ProductId).ToCollectionAsync();
            //        p = items2[0];
            //        content += "\n" + p.Material + " " + p.Quality + " " + p.Color + " " + p.Name + " " + p.Source;
            //        id = l[i].ProductId;
            //        ids.Add(id);
            //        d = start;
            //    }

            //    k = 0;
            //        while(true)
            //        {
            //            if (l[i].DateTime.DayOfYear == d.DayOfYear)
            //            {
            //                content += ",";
            //                if (l[i].Content.Split(' ')[0] == "Added")
            //                {
            //                    content+= "+"+ l[i].Content.Split(' ')[1];
            //                k++;

            //                }
            //                else
            //                {
            //                    content += "-" + l[i].Content.Split(' ')[1];
            //                k++;
            //                }
            //                break;
            //            }
            //            else
            //            {
            //                content += ",";
            //            k++;
            //                d= d.AddDays(1);
            //            }

            //        }

            //}
            //for (; k <= end.DayOfYear - start.DayOfYear; k++)
            //{
            //    content += ",";
            //}
            //content += "," + p.Quantity;
            //if (toggleSwitch.IsOn)
            //{
            //    try
            //    {
            //        i = 0;
            //        while (true)
            //        {
            //            items2 = await Table2.Where(ProductClass=> !ids.Contains(ProductClass.Id)).Skip(i * 50).Take(50).ToCollectionAsync();
            //            i++;
            //            foreach (ProductClass t in items2)
            //            {
            //                pro.Add(t);
            //            }
            //            i++;
            //            if (items2.Count < 50)
            //            {
            //                break;
            //            }
            //        }
            //        for (int j = 0; j < pro.Count; j++)
            //        {
            //            content += "\n" + pro[j].Material + " " + pro[j].Quality + " " + pro[j].Color + " " + pro[j].Name + " " + pro[j].Source;

            //            for (i = 0; i <= end.DayOfYear - start.DayOfYear; i++)
            //            {
            //                content += ",";
            //            }
            //            content += "," + pro[j].Quantity;
            //        }

            //    }
            //    catch (Exception)
            //    { //do someothing here
            //    }
            //    //we need all entries

            //}

            //TODO: garv write content to a csv file
        }
    }
}
