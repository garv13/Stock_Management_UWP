using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            try
            {
                LoadingBar.Visibility = Visibility.Visible;
                LoadingBar.IsIndeterminate = true;

                List<string> lol = new List<string>();
                items = await Table.ToCollectionAsync();
                event1.ItemsSource = items;
                items2 = await Table.Select(ProductClass => ProductClass.Material).ToCollectionAsync();
                lol = items2.Distinct().ToList<string>();

                lol.Add("All");
                matBox.ItemsSource = lol;
                matBox.SelectedIndex = lol.Count - 1;

                items2 = await Table.Select(ProductClass => ProductClass.Color).ToCollectionAsync();
                lol = items2.Distinct().ToList<string>();

                lol.Add("All");
                matBox2.ItemsSource = lol;
                LoadingBar.Visibility = Visibility.Collapsed;
                matBox2.SelectedIndex = lol.Count - 1;
            }
            catch(Exception)
            {
                LoadingBar.Visibility = Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("Can't Load Now");
                await msgbox.ShowAsync();
            }


        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
        private async Task loadTable()
        {
            LoadingBar.Visibility = Visibility.Visible;
            LoadingBar.IsIndeterminate = true;

            var Quality = comboBox.SelectedItem as ComboBoxItem;
            string Qual = Quality.Content as string +" ";
            string material = matBox.SelectedItem as string;
            string color = matBox2.SelectedItem as string;
            string name = Product_Name_Box.Text;
            string source = Product_Source_Box.Text;

           
            if (material == "All")
            { material = " "; }
            if (color == "All")
            { color = " "; }
            if (name == "")
                name = " ";
            if (source == "")
                source = " ";
            //todo make if for qual=all.. match and contain

            try
            {



                if (Qual == "All ")
                {
                    Qual = " ";

                    items = await Table.Where(ProductClass =>
                    ProductClass.Name.Contains(name.ToUpper()) &&
                    ProductClass.Quality.Contains(Qual) &&
                    ProductClass.Material.Contains(material) &&
                    ProductClass.Color.Contains(color) &&
                     ProductClass.Source.Contains(source.ToUpper())).ToCollectionAsync();
                }
                else
                {
                    items = await Table.Where(ProductClass =>
                    ProductClass.Name.Contains(name.ToUpper()) &&
                    ProductClass.Quality == Qual &&
                    ProductClass.Material.Contains(material) &&
                    ProductClass.Color.Contains(color) &&
                    ProductClass.Source.Contains(source.ToUpper())).ToCollectionAsync();
                }
                LoadingBar.Visibility = Visibility.Collapsed;
                event1.ItemsSource = items;
            }
            catch(Exception)
            {
                LoadingBar.Visibility = Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("Can't Load Now");
                await msgbox.ShowAsync();
            }
        }

        private  void matBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private  void Product_Name_Box_LostFocus(object sender, RoutedEventArgs e)
        {
          
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await loadTable();
        }

        private void event1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductClass p = e.ClickedItem as ProductClass;
            Frame.Navigate(typeof(Search_Detail_Page), p);
        }
    }
}
