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
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class naukaslowek1 : Page
    {
        public static string baza;
        public static bool dynamicsave;
        public static bool pressenter;
        public static bool statistic;
        public static bool smartrandom;

        public naukaslowek1()
        {
            this.InitializeComponent();
            buttonnaukaslowekwybranabaza.IsEnabled = false;
            togglestatistic.IsOn = true;
            SetCombo();
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
            if (baza.Equals("eFiszki"))
            {
                baza = "AppData";
            }
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + baza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);

            List<UserDefaultDataBase> listadostepnychindekow = await db.QueryAsync<UserDefaultDataBase>("Select Id From UserDefaultDataBase");
          
            if (listadostepnychindekow.Count < 1)
            {
                MessageDialog dialog = new MessageDialog(egzamin1zamalokomunikat.Text);
                await dialog.ShowAsync();
            }
            else
            {
                if (toggledynamicsave.IsOn)
                {
                    dynamicsave = true;
                    //this.Frame.Navigate(typeof(NaukaSlowek));
                }
                else
                {
                    dynamicsave = false;
                    //this.Frame.Navigate(typeof(naukaslowek2));
                }

                if (toggleentertocheck.IsOn)
                {
                    pressenter = true;
                }
                else
                {
                    pressenter = false;
                }

                if (togglestatistic.IsOn)
                {
                    statistic = true;
                }
                else
                {
                    statistic = false;
                }
                if (togglesmartrandom.IsOn)
                {
                    smartrandom = true;
                }
                else
                {
                    smartrandom = false;
                }

                this.Frame.Navigate(typeof(naukaslowek2));

                
            }
        }

        private void comboboxdostepnebazy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            baza = comboboxdostepnebazy.SelectedValue.ToString();
            buttonnaukaslowekwybranabaza.IsEnabled = true;

        }

        private void togglestatistic_Toggled(object sender, RoutedEventArgs e)
        {
            if (togglestatistic.IsOn)
            {
                toggledynamicsave.IsEnabled = true;
                togglesmartrandom.IsEnabled = true;
            }
            else
            {
                toggledynamicsave.IsEnabled = false;
                togglesmartrandom.IsEnabled = false;
            }

        }


    }
}
