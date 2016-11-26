using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Search_Page : Page
    {
        private static IMobileServiceTable<ProductClass> Table = App.MobileService.GetTable<ProductClass>();
        public static MobileServiceCollection<ProductClass, ProductClass> items;
        public static MobileServiceCollection<String, String> items2;

        public Search_Page()
        {
            this.InitializeComponent();
            Loaded += Search_Page_Loaded;
        }

        private async void Search_Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> lol = new List<string>();
            items = await Table.ToCollectionAsync();
            event1.ItemsSource = items;
            items2 = await Table.Select(ProductClass => ProductClass.Material).ToCollectionAsync();
            lol = items2.Distinct().ToList<string>();

            lol.Add("All");
            matBox.ItemsSource = lol;
            matBox.SelectedIndex = 0;

            items2 = await Table.Select(ProductClass => ProductClass.Color).ToCollectionAsync();
            lol = items2.Distinct().ToList<string>();

            lol.Add("All");
            matBox2.ItemsSource = lol;
            matBox2.SelectedIndex = 0;



        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
        private async Task loadTable()
        {
            var Quality = comboBox.SelectedItem as ComboBoxItem;
            string Qual = Quality.Content as string;
            var material = matBox.SelectedItem as string;
            var color = matBox2.SelectedItem as string;
            

            items = await Table.Where(ProductClass =>
            Product_Name_Box.Text != "" ? ProductClass.Name.Contains(Product_Name_Box.Text.ToUpper()) : true &&
            ProductClass.Material==Qual && 
            material!="All"?ProductClass.Material.Contains(material):true &&
            color!= "All" ? ProductClass.Color.Contains(color) : true &&
            Product_Source_Box.Text != "" ? ProductClass.Source.Contains(Product_Source_Box.Text.ToUpper()) : true).ToCollectionAsync();

            event1.ItemsSource = items;
        }

        private async void matBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await loadTable();
        }

        private async void Product_Name_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            await loadTable();
        }
    }
}
