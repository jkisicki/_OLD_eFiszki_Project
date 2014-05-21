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
    public sealed partial class eksport : Page
    {
        public eksport()
        {
            this.InitializeComponent();
            buttondodajnoweslowkowybranabaza.IsEnabled = false;
            SetCombo();
            getinfo();
            friendmeil.IsEnabled = false;
            radio1.IsChecked = true;
           
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

        private void comboboxdostepnebazy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttondodajnoweslowkowybranabaza.IsEnabled = true;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string meiladdress;
            string wiadomosc = "";
            string baza = comboboxdostepnebazy.SelectedValue.ToString();
            if (baza == "eFiszki")
            {
                baza = "AppData";
            }    

            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + baza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);
            List<UserDefaultDataBase> PobierzDaneBazy = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase");
            if (PobierzDaneBazy.Count == 0)
            {
                MessageDialog dialog = new MessageDialog("Error");
                await dialog.ShowAsync();
            }
            else
            {
                
                foreach (var element in PobierzDaneBazy)
                {
                    wiadomosc = wiadomosc + "(" + element.SlowkoPl + " - " + element.SlowkoEn + " - " + element.Kontekst + " - " + element.Podpowiedz + ") \n";
                }
            }
            if (radio1.IsChecked == true)
            {
                meiladdress = friendmeil.Text;
            }
            else
            {
                meiladdress = "jozef.kisicki@outlook.com";
            }
            var mailto = new Uri("mailto:?to="+ meiladdress +"&subject="+userinfo.Text+ "_db_"+baza+":"+jezyk1.Text+"-"+jezyk2.Text+" &body=" + wiadomosc);
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }

        private async void getinfo()
        {
            string firstname = await Windows.System.UserProfile.UserInformation.GetFirstNameAsync();
            string lastname = await Windows.System.UserProfile.UserInformation.GetLastNameAsync();
            
                if (string.IsNullOrEmpty(firstname) & string.IsNullOrEmpty(lastname))
                {
                    userinfo.Text = "Anonym";
                }
                else
                {
                    userinfo.Text = firstname + "_" + lastname;
                }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ZarzadzanieBazami));
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            friendmeil.IsEnabled = true;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            friendmeil.IsEnabled = false;
        }

       



        
    }
}
