﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Create_Page : Page
    {

        public List<string> lol;
            private static IMobileServiceTable<ProductClass> Table = App.MobileService.GetTable<ProductClass>();
        public static MobileServiceCollection<string, string> items;
        
        public Create_Page()
        {
                lol = new List<string>();
            Loaded += Create_Page_Loaded;
           
            this.InitializeComponent();
            
        }

        private async void Create_Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                items = await Table.Select(ProductClass => ProductClass.Material).ToCollectionAsync();
                lol = items.Distinct().ToList<string>();

                lol.Add("Other");
                matBox.ItemsSource = lol;
            }
            catch(Exception)
            {

            }
        }

        private async void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            int lol2;   
            if (Product_Name_Box.Text == "")
            {
                await (new MessageDialog("Enter Valid Name")).ShowAsync();
                return;
            }

            if (Product_Quantity_Box.Text == "" && !int.TryParse(Product_Quantity_Box.Text, out lol2))
            {
                await (new MessageDialog("Enter Valid Quantity")).ShowAsync();
                return;
            }

            if (Product_Color_Box.Text == "")
            {
                await (new MessageDialog("Enter Valid Color")).ShowAsync();
                return;
            }
           


            try
            {
                ProductClass p = new ProductClass();
                p.Name = Product_Name_Box.Text.ToUpper()+" ";
                p.Color = Product_Color_Box.Text.ToUpper() + " ";
                p.Quantity = Product_Quantity_Box.Text.ToUpper() + " ";
                var Quality = comboBox.SelectedItem as ComboBoxItem;
                p.Quality = Quality.Content as string + " ";

                var material = matBox.SelectedItem as string ;
                string mat = material + " ";
                if (mat == "Other ")
                {
                    mat = Product_Mat_Box.Text.ToUpper() ;
                    if (lol.Contains(mat))
                    {
                        await (new MessageDialog("Material Already exists. Kindly select from drop down menu")).ShowAsync();
                    }
                }
                p.Material = mat;
               
                p.Source = Product_Source_Box.Text.ToUpper() + " ";

                    await App.MobileService.GetTable<ProductClass>().InsertAsync(p);
                    Logs l = new Logs();
                    l.ProductId = p.Id;
                    l.Content = "Added new product " + p.Name + ". " + p.Quantity + " Bags";
                    Logs.createLog(l);               

                await (new MessageDialog("Record Added")).ShowAsync();
            }

            catch (Exception)
            {
                await (new MessageDialog("Record Not Added")).ShowAsync();
            }
        }

        private void matBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as ComboBox;
            string Quality = a.SelectedItem as string;
            //todo something here
            if(Quality == "Other")
            { Product_Mat_Box.Visibility = Visibility.Visible; }
            else
                Product_Mat_Box.Visibility = Visibility.Collapsed;
        }
    }
}
