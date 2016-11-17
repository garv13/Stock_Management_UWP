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
        public Create_Page()
        {
            this.InitializeComponent();
        }

        StorageFile file = null;
        List<string> dataList = new List<string>();
        private async void filePicker_Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            try
            {
                file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    // Application now has read/write access to the picked file
                    filePicker_Button.Content = "Picked photo: " + file.Name;

                }
                else
                {
                    filePicker_Button.Content = "Operation canceled.";
                }
            }
            catch (Exception)
            {
                await (new MessageDialog("Image Not Selected")).ShowAsync();
            }
        }
        private async void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                await (new MessageDialog("Image Not Selected")).ShowAsync();
                return;
            }
            if (Product_Name_Box.Text == " ")
            {
                await (new MessageDialog("Enter Valid Name")).ShowAsync();
                return;
            }

            if (Product_Quan_Box.Text == " ")
            {
                await (new MessageDialog("Enter Valid Quantity")).ShowAsync();
                return;
            }

            if (Product_Color_Box.Text == " ")
            {
                await (new MessageDialog("Enter Valid Color")).ShowAsync();
                return;
            }
            try
            {
                ProductClass p = new ProductClass();
                p.Name = Product_Name_Box.Text;
                p.Color = Product_Color_Box.Text;
                p.Quantity = int.Parse(Product_Quan_Box.Text);

                StorageFolder mainFolder = ApplicationData.Current.LocalFolder;
                StorageFolder localFolder = await mainFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
                StorageFile imageFile = await localFolder.CreateFileAsync(file.Name, CreationCollisionOption.GenerateUniqueName);
                
                p.Image = imageFile.Name;

                string str = JsonConvert.SerializeObject(p);
                dataList.Add(str);
                StorageFile sf = await mainFolder.CreateFileAsync("data.txt", CreationCollisionOption.OpenIfExists);
                await FileIO.AppendLinesAsync(sf, dataList);

                await (new MessageDialog("Record Added")).ShowAsync();
            }

            catch (Exception)
            {
                await (new MessageDialog("Record Not Added")).ShowAsync();
            }
        }
    }
}
