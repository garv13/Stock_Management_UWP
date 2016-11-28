using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
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
    public sealed partial class Search_Detail_Page : Page
    {

        private static IMobileServiceTable<Logs> Table = App.MobileService.GetTable<Logs>();
        private static MobileServiceCollection<Logs, Logs> items;
        private static IMobileServiceTable<ProductClass> Table2 = App.MobileService.GetTable<ProductClass>();
        private static MobileServiceCollection<ProductClass, ProductClass> items2;
        ProductClass p;
        public Search_Detail_Page()
        {
            this.InitializeComponent();
            p = new ProductClass();
            Loaded += Search_Detail_Page_Loaded;
        }

        private async void Search_Detail_Page_Loaded(object sender, RoutedEventArgs e)
        {
            items = await Table.Where(Logs => Logs.ProductId == p.Id).ToCollectionAsync();
            event1.ItemsSource = items;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            p = e.Parameter as ProductClass;
            
            Product_Name_Box.Text = p.Name;
            Product_Quantity_Box.Text = p.Quantity;
            Product_Mat_Box.Text = p.Quality;
             matBox.Text = p.Material;
            Product_Source_Box.Text = p.Source;
            Product_Color_Box.Text = p.Color;
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //add stock button
            int a = int.Parse(p.Quantity);
            int b;
            if (int.TryParse(Number_bags.Text, out b))
            {
                p.Quantity = (a + b).ToString();
                await Table2.UpdateAsync(p);
                Logs l = new Logs();
                l.ProductId = p.Id;
                l.Content = "Added " + b.ToString() + " Bags of " + p.Name;
                Logs.createLog(l);
                MessageDialog mess = new MessageDialog("Stock Added", "Stock Updated");
                await mess.ShowAsync();
            }
            else
            {
                MessageDialog mess = new MessageDialog("Please enter a valid number", "Bad input");
                await mess.ShowAsync();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //subtract stock button
            int a = int.Parse(p.Quantity);
            int b;
            if (int.TryParse(Number_bags.Text, out b))
            {
                if (a - b > 0)
                {
                    p.Quantity = (a - b).ToString();
                    await Table2.UpdateAsync(p);
                    Logs l = new Logs();
                    l.ProductId = p.Id;
                    l.Content = "Subtracted " + b.ToString() + " Bags of " + p.Name;
                    Logs.createLog(l);
                    MessageDialog mess = new MessageDialog("Stock subtracted", "Stock Updated");
                    await mess.ShowAsync();
                }
                else
                {
                    MessageDialog mess = new MessageDialog("Entered Value exceeds stock", "Bad input");
                    await mess.ShowAsync();

                }
            }
            else
            {
                MessageDialog mess = new MessageDialog("Please enter a valid number", "Bad input");
                await mess.ShowAsync();
            }
        }
    }
}
