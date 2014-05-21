using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using efiszkiProject.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DodajBaze : Page
    {
        public static string baza;
        public DodajBaze()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            baza = TextBoxNazwaNowejBazy.Text;
            string komunikat = textboxkomunikatistniejacabaza.Text;
            string komunikat2 = TextBoxKomunikatNazwaZastrzezona.Text;
            string komunikat3 = TextBoxZaKrotkaNazwa.Text;
            
            if (baza.Equals("AppData") | baza.Equals("eFiszki") | baza.Equals("efiszki"))
                {
                    MessageDialog dialog = new MessageDialog(komunikat2);
                    await dialog.ShowAsync();
                }
            else if (baza.Length < 2)
            {
                MessageDialog dialog = new MessageDialog(komunikat3);
                await dialog.ShowAsync();

            }

            else
            {
                StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                IReadOnlyList<StorageFile> fList = await folder.GetFilesAsync();
                int sprawdz = 0;
                foreach (var f in fList)
                {
                    if (baza.Equals(f.DisplayName))
                    {
                        sprawdz = 1;
                    }
                };

                if (sprawdz == 1)
                {
                    MessageDialog dialog = new MessageDialog(komunikat);
                    await dialog.ShowAsync();
                }
                else
                {
                    string DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, baza + ".sqlite");
                    using (var db = new SQLite.SQLiteConnection(DBPath))
                    {
                        db.CreateTable<UserDefaultDataBase>();
                    }


                    this.Frame.Navigate(typeof(DodajSlowko));

                }


            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ZarzadzanieBazami));
        }

       
    }
}
