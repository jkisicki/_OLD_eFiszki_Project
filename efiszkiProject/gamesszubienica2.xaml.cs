using efiszkiProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class gamesszubienica2 : Page
    {
        public static int indeks;
        public static string baza;
        public static string slowko_otwarte="";
        public static string slowko_zamkniete="";
        public static bool wygrana = false;
        public string slowkoen = "";
        char[] tablica_zamknieta;
        public int ilosc_prob=1;
        public List<UserDefaultDataBase> PobierzDaneBazy;
        public gamesszubienica2()
        {
            this.InitializeComponent();
            baza = gamesszubienica1.baza;
            slowko_otwarte = "";
            slowko_zamkniete = "";
            losuj();
        }

        public async void losuj()
        {
            if (baza == "eFiszki")
            {
                baza = "AppData";
            }

            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + baza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);
            PobierzDaneBazy = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase");
            if (PobierzDaneBazy.Count == 0)
            {
                MessageDialog dialog = new MessageDialog("Error");
                await dialog.ShowAsync();
            }
            else
            {
                Random rnd = new Random();
                indeks = rnd.Next(PobierzDaneBazy.Count);
                slowkoen = PobierzDaneBazy[indeks].SlowkoEn;

               tablica_zamknieta = slowkoen.ToCharArray();
                foreach (char element in tablica_zamknieta)
               {
                  

                   if (element.ToString() == " ")
                   {
                       slowko_zamkniete = slowko_zamkniete + " " + " ";
                   }

                   else
                   {
                       slowko_zamkniete = slowko_zamkniete + "_" + " ";
                   }
               }
                foreach (char element in tablica_zamknieta)
                {
                    slowko_otwarte = slowko_otwarte + element + " ";
                }

               wylosowaneslowko.Text = slowko_zamkniete;
            }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void sprawdz(string litera)
        {
           // Debug.WriteLine(slowko_otwarte);
           // Debug.WriteLine(slowko_zamkniete);
            char[] tab = slowko_zamkniete.ToCharArray();
            bool dobrze = false;
            for (int i = 0; i < slowko_otwarte.Length; i++)
            {
                if (slowko_otwarte[i].ToString().ToUpper() == litera)
                {
                    tab[i] = litera[0];
                    dobrze = true;
                }
                
            }
            if (dobrze == false)
            {
                if (ilosc_prob > 10)
                {
                    wygrana = false;
                    this.Frame.Navigate(typeof(gamesszubienia3));
                }
                else
                {
                    szubienica.Source = new BitmapImage(new Uri(this.BaseUri, @"Assets/szub" + ilosc_prob + ".png"));
                    ilosc_prob++;
                }
            }
           
            slowko_zamkniete = new string(tab);
            wylosowaneslowko.Text = slowko_zamkniete;
            Debug.WriteLine(slowko_otwarte);
            Debug.WriteLine(slowko_zamkniete);

            if (slowko_zamkniete == slowko_otwarte.ToUpper())
            {
                wygrana = true;
                this.Frame.Navigate(typeof(gamesszubienia3));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(gamesszubienica1));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("klikniety");
            sprawdz("Q");
            _1.IsEnabled = false;
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("W");
            _2.IsEnabled = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            sprawdz("E");
            _3.IsEnabled = false;
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("R");
            _4.IsEnabled = false;
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("T");
            _5.IsEnabled = false;
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("Y");
            _6.IsEnabled = false;
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("U");
            _7.IsEnabled = false;
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("I");
            _8.IsEnabled = false;
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("O");
            _9.IsEnabled = false;
        }

        private void _10_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("P");
            _10.IsEnabled = false;
        }

        private void _11_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("A");
            _11.IsEnabled = false;
        }

        private void _12_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("S");
            _12.IsEnabled = false;
        }

        private void _13_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("D");
            _13.IsEnabled = false;
        }

        private void _14_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("F");
            _14.IsEnabled = false;
        }

        private void _15_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("G");
            _15.IsEnabled = false;
        }

        private void _16_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("H");
            _16.IsEnabled = false;
        }

        private void _17_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("J");
            _17.IsEnabled = false;
        }

        private void _18_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("K");
            _18.IsEnabled = false;
        }

        private void _19_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("L");
            _19.IsEnabled = false;
        }

        private void _20_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("Z");
            _20.IsEnabled = false;
        }

        private void _21_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("X");
            _21.IsEnabled = false;
        }

        private void _22_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("C");
            _22.IsEnabled = false;
        }

        private void _23_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("V");
            _23.IsEnabled = false;
        }

        private void _24_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("B");
            _24.IsEnabled = false;
        }

        private void _25_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("N");
            _25.IsEnabled = false;
        }

        private void _26_Click(object sender, RoutedEventArgs e)
        {
            sprawdz("M");
            _26.IsEnabled = false;
        }
    }
}
