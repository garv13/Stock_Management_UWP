using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
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
    public sealed partial class LoginPage : Page
    {
        private IMobileServiceTable<User> Table = App.MobileService.GetTable<User>();
        private MobileServiceCollection<User, User> items;
        public LoginPage()
        {
            this.InitializeComponent();
        }

          private async void Password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await lol();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           // await UserEntry(UserName.Text, Password.Password); // function to add user
            await lol();
        }
        private async Task lol()
        { 
            LoadingBar.Visibility = Visibility.Visible;
            LoadingBar.IsIndeterminate = true;
            try
            {
                items = await Table.Where(User
                                => User.username == UserName.Text).ToCollectionAsync();
                if (items.Count != 0)
                {
                    if (ComputeMD5(Password.Password) == items[0].md5)
                    {
                        LoadingBar.Visibility = Visibility.Collapsed;
                        MessageDialog msgbox = new MessageDialog("Welcome " + UserName.Text);
                        await msgbox.ShowAsync();
                        Frame.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        LoadingBar.Visibility = Visibility.Collapsed;
                        MessageDialog msgbox = new MessageDialog("Password or Username incorrect");
                        await msgbox.ShowAsync();
                    }
                }
                else
                {
                    LoadingBar.Visibility = Visibility.Collapsed;
                    MessageDialog msgbox = new MessageDialog("Password or Username incorrect");
                    await msgbox.ShowAsync();
                }
            }
                catch (Exception)
                {
                    LoadingBar.Visibility = Visibility.Collapsed;
                    MessageDialog msgbox = new MessageDialog("Sorry Can't connect");
                    await msgbox.ShowAsync();
                }
        }

        public static string ComputeMD5(string str)
        {

            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            string res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }


        private static async Task UserEntry(string name,string password)
        {
            string md5 = ComputeMD5(password);
            char[] arr = password.ToCharArray();
            Array.Reverse(arr);
            string reversepass = new string(arr);
            string reversemd5 = ComputeMD5(reversepass);
            User a = new User();
            a.username = name;
            a.md5 = md5;
            a.md5reverse = reversemd5;
            await App.MobileService.GetTable<User>().InsertAsync(a);
        }
    }
}
