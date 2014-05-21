using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using efiszkiProject.Models;
using SQLite;
using Windows.UI.Popups;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class NaukaSlowek : Page
    {
        public String slowkopl = "";
        public String slowkoen = "";
        public String podpowiedz = "";
        public String kontekst = "";
        public int iloscodpowiedzi = 0;
        public int iloscpoprawnychodpowiedzo = 0;
        public int passa = 0;
        public int WylosowanaPozycja = 0;
        int userpassa = 0;
        int useriloscdobrychodpowiedzi = 0;
        int useriloscodpowiedzi = 0;
        int licznik_losowan = 0;
        public static bool smartrandom;

        public NaukaSlowek()
        {
            this.InitializeComponent();
            clear();
            smartrandom = naukaslowek1.smartrandom;
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
            this.Frame.Navigate(typeof(naukaslowek1));
        }



        private async void ButtonLosuj_Click(object sender, RoutedEventArgs e)
        {

            string wybranabaza = getbaza();
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + wybranabaza;
            var db = new SQLiteAsyncConnection(path);

            var path2 = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppData.sqlite";
            var db2 = new SQLiteAsyncConnection(path2);

            List<UserDefaultDataBase> PobierzDaneBazy = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase");
            if (PobierzDaneBazy.Count == 0)
            {
                MessageDialog dialog = new MessageDialog("error");
                await dialog.ShowAsync();
            }
            else
            {
                TextBoxOdpowiedz.Text = "";
                TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.White);
                ButtonLosuj.IsEnabled = false;
                TextBoxOdpowiedz.IsReadOnly = false;
                SwitchPodpowiedz.IsOn = false;
                SwitchPodpowiedz.IsEnabled = true;
                TextBoxPodpowiedzLitery.Text = "";

                if (smartrandom == true)
                {
                    Debug.WriteLine("tutaj");
                    var Query =
                        from que in PobierzDaneBazy
                        where (que.kategoria == 1)
                        select que.Id;
                    foreach (var element in Query)
                    {
                        Debug.WriteLine(element);
                    }

                }
                else if (smartrandom == false)
                {

                Random rnd = new Random();
                int Wylosowanyindeks = rnd.Next(PobierzDaneBazy.Count);



                WylosowanaPozycja = PobierzDaneBazy[Wylosowanyindeks].Id;

                slowkopl = PobierzDaneBazy[Wylosowanyindeks].SlowkoPl;
                slowkoen = PobierzDaneBazy[Wylosowanyindeks].SlowkoEn;
                podpowiedz = PobierzDaneBazy[Wylosowanyindeks].Podpowiedz;
                kontekst = PobierzDaneBazy[Wylosowanyindeks].Kontekst;
                iloscodpowiedzi = PobierzDaneBazy[Wylosowanyindeks].IloscOdpowiedzi;
                iloscpoprawnychodpowiedzo = PobierzDaneBazy[Wylosowanyindeks].IloscPoprawnychOdpowiedzi;
                passa = PobierzDaneBazy[Wylosowanyindeks].passa;

                //Debug.WriteLine(" {0} - {1} w kontekscie {2}, podpowiedz {3}", slowkopl, slowkoen, kontekst, podpowiedz );
                TextBoxWylosowaneSlowko.Text = slowkopl;

                if (SwitchKontekst.IsOn == true)
                {
                    TextBoxKontekst.Text = kontekst;
                }
                sliderpodpowiedz.Maximum = slowkoen.Length / 2;
                slowkoiloscodpowiedzi.Text = iloscodpowiedzi.ToString();
                slowkoiloscpoprawnychodpowiedzi.Text = iloscpoprawnychodpowiedzo.ToString();
                slowkopassa.Text = passa.ToString();
                if (iloscodpowiedzi != 0)
                {
                    slowkoprocent.Value = (double)iloscpoprawnychodpowiedzo / (double)iloscodpowiedzi * 100;
                }
                List<UserInformation> userinf = await db2.QueryAsync<UserInformation>("Select * from UserInformation Where Id = 1");
                foreach (var item in userinf)
                {
                    userpassa = item.passa;
                    useriloscdobrychodpowiedzi = item.IloscDobrychOdpowiedzi;
                    useriloscodpowiedzi = item.IloscOgolnychOdpowiedzi;
                }

                textboxuserpassa.Text = userpassa.ToString();
                textboxuseriloscodpowiedzi.Text = useriloscodpowiedzi.ToString();
                textboxuseriloscdobrychodpowiedzi.Text = useriloscdobrychodpowiedzi.ToString();

                if (useriloscodpowiedzi != 0)
                {
                    userprocent.Value = (double)useriloscdobrychodpowiedzi / (double)useriloscodpowiedzi * 100;
                }




                }
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            String slowkoodpowiedz = TextBoxOdpowiedz.Text;
            slowkoodpowiedz = slowkoodpowiedz.ToLower();
            slowkoen = slowkoen.ToLower();
            TextBoxPodpowiedzLitery.Text = "";
            sliderpodpowiedz.Value = 0;
            //Debug.WriteLine(slowkoodpowiedz);
            //Debug.WriteLine(slowkoen);


            if (slowkoen.Equals(slowkoodpowiedz))
            {
                //Debug.WriteLine("OK");
                TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.GreenYellow);
                ButtonLosuj.IsEnabled = true;
                TextBoxOdpowiedz.IsReadOnly = true;
                TextBoxOdpowiedz.Text = slowkoodpowiedz + " = " + slowkoen;
                

                string wybranabaza = getbaza();
                var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + wybranabaza;
                var db = new SQLiteAsyncConnection(path);

                List<UserDefaultDataBase> slowko = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase Where Id = ?", new object[] { WylosowanaPozycja });
                var update = slowko.First();
                update.passa += 1;
                update.IloscOdpowiedzi += 1;
                update.IloscPoprawnychOdpowiedzi += 1;
                await db.UpdateAsync(update);

                var path2 = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppData.sqlite";
                var db2 = new SQLiteAsyncConnection(path2);

                List<UserInformation> userinf = await db2.QueryAsync<UserInformation>("Select * from UserInformation Where Id = 1");
                var updateuser = userinf.First();
                updateuser.IloscDobrychOdpowiedzi += 1;
                updateuser.IloscOgolnychOdpowiedzi += 1;
                updateuser.passa += 1;
                await db2.UpdateAsync(updateuser);

                ButtonSprawdz.IsEnabled = false;
            }
            else
            {
                TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.Red);
                ButtonLosuj.IsEnabled = true;
                TextBoxOdpowiedz.IsReadOnly = true;
                TextBoxOdpowiedz.Text = slowkoodpowiedz + " !=! " + slowkoen;
                

                string wybranabaza = getbaza();
                var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + wybranabaza;
                var db = new SQLiteAsyncConnection(path);

                List<UserDefaultDataBase> slowko = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase Where Id = ?", new object[] { WylosowanaPozycja });
                var update = slowko.First();
                update.IloscOdpowiedzi += 1;
                update.passa = 0;
                await db.UpdateAsync(update);

                var path2 = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppData.sqlite";
                var db2 = new SQLiteAsyncConnection(path2);

                List<UserInformation> userinf = await db2.QueryAsync<UserInformation>("Select * from UserInformation Where Id = 1");
                var updateuser = userinf.First();
                updateuser.IloscOgolnychOdpowiedzi += 1;
                updateuser.passa = 0;
                await db2.UpdateAsync(updateuser);

                ButtonSprawdz.IsEnabled = false;

            }

        }

        private void TextBoxOdpowiedz_TextChanged(object sender, TextChangedEventArgs e)
        {
            String data = TextBoxOdpowiedz.Text;
            if (data.Length > 0)
            {
                ButtonSprawdz.IsEnabled = true;
            }
            else
            {
                ButtonSprawdz.IsEnabled = false;
            }
        }


        private void SwitchKontekst_Toggled(object sender, RoutedEventArgs e)
        {
            if (SwitchKontekst.IsOn == true)
            {
                TextBoxKontekst.Text = kontekst;
            }
            else
            {
                TextBoxKontekst.Text = "";
            }
        }

        private void SwitchPodpowiedz_Toggled(object sender, RoutedEventArgs e)
        {
            if (SwitchPodpowiedz.IsOn)
            {
                TextBoxPodpowiedz.Text = podpowiedz;
                sliderpodpowiedz.IsEnabled = true;
            }
            else
            {
                TextBoxPodpowiedz.Text = "";
                sliderpodpowiedz.IsEnabled = false;
            }
        }

        private void sliderpodpowiedz_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            TextBoxPodpowiedzLitery.Text = slowkoen.Substring(0, (int)sliderpodpowiedz.Value);
        }

        public void clear()
        {
            ButtonLosuj.IsEnabled = true;
            ButtonSprawdz.IsEnabled = false;
            TextBoxOdpowiedz.IsReadOnly = true;
            TextBoxOdpowiedz.Text = "";
            TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.White);
            TextBoxWylosowaneSlowko.Text = "";
            TextBoxKontekst.Text = "";
            SwitchPodpowiedz.IsOn = false;
            TextBoxPodpowiedzLitery.Text = "";
            SwitchPodpowiedz.IsEnabled = false;
            sliderpodpowiedz.IsEnabled = false;
            sliderpodpowiedz.Value = 0;
            userprocent.Value = 0;
            slowkoprocent.Value = 0;
            slowkoiloscodpowiedzi.Text = "";
            slowkoiloscpoprawnychodpowiedzi.Text = "";
            slowkopassa.Text = "";



        }

        public string getbaza()
        {
            string wybranabaza = "";
            wybranabaza = naukaslowek1.baza;
            if (wybranabaza.Equals("eFiszki"))
            {
                wybranabaza = "AppData.sqlite";
            }
            else
            {
                wybranabaza = wybranabaza + ".sqlite";
            }

            return wybranabaza;
        }

        private void userprocent_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            textboxuserprocenty.Text = userprocent.Value.ToString();
        }

        private void slowkoprocent_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            textboxslowkoprocent.Text = slowkoprocent.Value.ToString();
        }
       

        }
    }
