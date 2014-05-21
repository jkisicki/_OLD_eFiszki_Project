using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using efiszkiProject.Models;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class egzamin1 : Page
    {
        public static string baza;
        public static string systemoceniania;
        public egzamin1()
        {
            this.InitializeComponent();
            buttondodajnoweslowkowybranabaza.IsEnabled = false;
            SetCombo();
            oceny1.IsChecked = true;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void comboboxdostepnebazy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baza = comboboxdostepnebazy.SelectedValue.ToString();
            buttondodajnoweslowkowybranabaza.IsEnabled = true;
        
        }
        private async void SetCombo()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var files = await folder.GetFilesAsync();

            IReadOnlyList<StorageFile> fList = await folder.GetFilesAsync();
            foreach (var f in fList)
            {
                //Debug.WriteLine(f.DisplayName);
                if (f.DisplayName.Equals("AppData"))
                {
                    comboboxdostepnebazy.Items.Add("eFiszki");
                }
                else
                {
                    comboboxdostepnebazy.Items.Add(f.DisplayName);
                }
            };
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //tutaj sprawdzic czy jest powyzej 5/10 slowek w bazie
            if (baza.Equals("eFiszki"))
            {
                baza = "AppData";
            }
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + baza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);

            List<UserDefaultDataBase> listadostepnychindekow = await db.QueryAsync<UserDefaultDataBase>("Select Id From UserDefaultDataBase");
            int[] tablicaindeksow = new int[listadostepnychindekow.Count];
            int a = 0;
            foreach (var i in listadostepnychindekow)
            {
                tablicaindeksow[a] = i.Id;
                a++;
            }

            if (tablicaindeksow.Length < 20)
            {
                MessageDialog dialog = new MessageDialog(egzamin1zamalokomunikat.Text);
                await dialog.ShowAsync();
            }
            else
            {
                if (oceny1.IsChecked == true)
                {
                    systemoceniania = "procent";
                }
                else
                {
                    systemoceniania = "polski";
                }
                this.Frame.Navigate(typeof(egzamin2));
            }
            
        }

        private void oceny1_Checked(object sender, RoutedEventArgs e)
        {
            if (oceny2.IsChecked == true)
            {
                oceny2.IsChecked = false;
            }
        }

        private void oceny2_Checked(object sender, RoutedEventArgs e)
        {
            oceny1.IsChecked = false;
        }
    }
}
