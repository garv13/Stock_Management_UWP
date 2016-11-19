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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Stock_Management_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ProductClass a = new ProductClass();
            a.Color = "Blue";
            a.Material = "PP";
            a.Name = "lol";
            a.Quality = "II";
            a.Quantity = "25";
            a.Source = "lol";
            App.MobileService.GetTable<ProductClass>().InsertAsync(a);
            this.InitializeComponent();
            mahSql.load();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Create_Page));
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search_Page));
        }

        private void Logs_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Logs_Page));
        }
    }
}
