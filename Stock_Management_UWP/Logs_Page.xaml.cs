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
    public sealed partial class Logs_Page : Page
    {
        private static IMobileServiceTable<Logs> Table = App.MobileService.GetTable<Logs>();
        private static MobileServiceCollection<Logs, Logs> items;

        public Logs_Page()
        {
            this.InitializeComponent();
            Loaded += Logs_Page_Loaded;

        }

        private async void Logs_Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
            items = await Table.ToCollectionAsync();
            event1.ItemsSource = items;

            //            items = await Table.ToCollectionAsync();
            //            foreach(ProductClass p in items)
            //            {
            //                p.Color = p.Color + " ";
            //                p.Material = p.Material + " ";
            //                p.Name = p.Name + " ";
            //                p.Source = p.Source + " ";
            //                p.Quality = p.Quality + " ";
            //                await Table.UpdateAsync(p);
            //;

                //            }
            }
            catch(Exception)
            {

            }
        }

    }
        
}
