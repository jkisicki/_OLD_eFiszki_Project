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
using SQLite;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UsunSlowko : Page
    {
        public UsunSlowko()
        {
            this.InitializeComponent();
            comboboxslowkodousuniecia.IsEnabled = false;
            buttonusunslowko.IsEnabled = false;
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

        private async void SetCombo()
        {

            comboboxusunzbazy.Items.Clear();

            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;

            IReadOnlyList<StorageFile> fList = await folder.GetFilesAsync();
            foreach (var f in fList)
            {
                //Debug.WriteLine(f.DisplayName);
                if (f.DisplayName.Equals("AppData"))
                {
                    comboboxusunzbazy.Items.Add("eFiszki");
                }
                else
                {
                    comboboxusunzbazy.Items.Add(f.DisplayName);
                }
            };

        }

        private async void comboboxusunzbazy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setcomboslowko();   
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string baza = comboboxusunzbazy.SelectedValue.ToString();
            string slowko = comboboxslowkodousuniecia.SelectedValue.ToString();
            string[] tablica = slowko.Split(new[] {'.'});
            if (baza.Equals("eFiszki"))
            {
                baza = "AppData";
            }

            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + baza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);

            var maxid = 0;
            List<UserDefaultDataBase> result = await db.QueryAsync<UserDefaultDataBase>("Select max(id) as Id From UserDefaultDataBase");
            foreach (var item in result)
            {
                maxid = item.Id;
            }

            int indeksdousuniecia = Convert.ToInt32(tablica[0]);
            var all  = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase");
            var del = await db.FindAsync<UserDefaultDataBase>(u => u.Id == indeksdousuniecia);
            await db.DeleteAsync(del);

            //Debug.WriteLine("maksymalny indeks: " + maxid + ", indeks do usuniecia:" + indeksdousuniecia);
            //if (maxid != indeksdousuniecia)
            //{
            //    List<UserDefaultDataBase> updateid = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase Where Id = ?", new object[] { maxid });
            //    var update = updateid.First();
            //    update.Id += 100;
            //    update.SlowkoEn = "toz to dziala";
            //    await db.UpdateAsync(update);
            //    Debug.WriteLine("Update indeksu:" + maxid + " na " + indeksdousuniecia);

            //    //foreach (var i in updateid)
            //    //{
            //    //    Debug.WriteLine(i.Id);
            //    //}

            //    //await db.QueryAsync<UserDefaultDataBase>("Update UserDefaultDataBase set id= ? where id=?", new object[] {maxid, indeksdousuniecia });
            //    //foreach (var i in updateid)
            //    //{
            //    //    var update = updateid.First();
            //    //    update.Id = indeksdousuniecia;
            //    //    await db.UpdateAsync(update);
            //    //} 
            //}


            comboboxslowkodousuniecia.Items.Clear();
            setcomboslowko();

            

        }

        private void comboboxslowkodousuniecia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonusunslowko.IsEnabled = true;
        }


        public async void setcomboslowko()
        {
            comboboxslowkodousuniecia.IsEnabled = true;
            buttonusunslowko.IsEnabled = false;
            comboboxslowkodousuniecia.Items.Clear();

            String Result = "";
            string wybranabaza = comboboxusunzbazy.SelectedValue.ToString();
            if (wybranabaza.Equals("eFiszki"))
            {
                wybranabaza = "AppData";
            }
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + wybranabaza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);
            List<UserDefaultDataBase> allUsers = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase");
            var count = allUsers.Any() ? allUsers.Count : 0;
            foreach (var item in allUsers)
            {
                Result = item.Id + ". " + item.SlowkoPl + " - " + item.SlowkoEn;
                comboboxslowkodousuniecia.Items.Add(Result);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ZarzadzanieBazami));
        }
    }
}
