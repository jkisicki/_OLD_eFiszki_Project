using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class egzamin2 : Page
    {
        public int[] tablicawylosowanychindeksow = new int[20];
        public List<UserDefaultDataBase> query;
        int wynik = 0;
        public static string systemoceniania;
        public egzamin2()
        {
            this.InitializeComponent();
            wroc.IsEnabled = false;
            Sprawdz.IsEnabled = false;
            systemoceniania = egzamin1.systemoceniania;
            StartExam();

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


        }

        public async void StartExam()
        {
            sn0.IsReadOnly = false;
            sn1.IsReadOnly = false;
            sn2.IsReadOnly = false;
            sn3.IsReadOnly = false;
            sn4.IsReadOnly = false;
            sn5.IsReadOnly = false;
            sn6.IsReadOnly = false;
            sn7.IsReadOnly = false;
            sn8.IsReadOnly = false;
            sn9.IsReadOnly = false;

            string baza = egzamin1.baza;
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + baza + ".sqlite";
            var db = new SQLiteAsyncConnection(path);

            List<UserDefaultDataBase> listadostepnychindekow = await db.QueryAsync<UserDefaultDataBase>("Select id From UserDefaultDataBase");
            query = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase order by SlowkoPl");

            //Debug.WriteLine("Dlugos tablicy przed losowaniem:" + listadostepnychindekow.Count);
            Random rnd = new Random();
            for (int i = 0; i < tablicawylosowanychindeksow.Length; i++)
            {
                int wylosowanyindeks = rnd.Next(listadostepnychindekow.Count - 1);
                tablicawylosowanychindeksow[i] = listadostepnychindekow[wylosowanyindeks].Id;
                listadostepnychindekow.RemoveAt(wylosowanyindeks);

            }
            //foreach (int i in tablicawylosowanychindeksow)
            //{
            //    Debug.WriteLine(i);
            //}
            //Debug.WriteLine("Dlugos tablicy po losowaniem:" + listadostepnychindekow.Count);

            sl0.Text = query[tablicawylosowanychindeksow[0]].SlowkoPl;
            sl1.Text = query[tablicawylosowanychindeksow[1]].SlowkoPl;
            sl2.Text = query[tablicawylosowanychindeksow[2]].SlowkoPl;
            sl3.Text = query[tablicawylosowanychindeksow[3]].SlowkoPl;
            sl4.Text = query[tablicawylosowanychindeksow[4]].SlowkoPl;
            sl5.Text = query[tablicawylosowanychindeksow[5]].SlowkoPl;
            sl6.Text = query[tablicawylosowanychindeksow[6]].SlowkoPl;
            sl7.Text = query[tablicawylosowanychindeksow[7]].SlowkoPl;
            sl8.Text = query[tablicawylosowanychindeksow[8]].SlowkoPl;
            sl9.Text = query[tablicawylosowanychindeksow[9]].SlowkoPl;
            sl10.Text = query[tablicawylosowanychindeksow[10]].SlowkoEn;
            sl11.Text = query[tablicawylosowanychindeksow[11]].SlowkoEn;
            sl12.Text = query[tablicawylosowanychindeksow[12]].SlowkoEn;
            sl13.Text = query[tablicawylosowanychindeksow[13]].SlowkoEn;
            sl14.Text = query[tablicawylosowanychindeksow[14]].SlowkoEn;
            sl15.Text = query[tablicawylosowanychindeksow[15]].SlowkoEn;
            sl16.Text = query[tablicawylosowanychindeksow[16]].SlowkoEn;
            sl17.Text = query[tablicawylosowanychindeksow[17]].SlowkoEn;
            sl18.Text = query[tablicawylosowanychindeksow[18]].SlowkoEn;
            sl19.Text = query[tablicawylosowanychindeksow[19]].SlowkoEn;

            foreach (var slowka in query)
            {
                csn10.Items.Add(slowka.SlowkoPl);
                csn10.SelectedIndex = 0;
                csn11.Items.Add(slowka.SlowkoPl);
                csn11.SelectedIndex = 0;
                csn12.Items.Add(slowka.SlowkoPl);
                csn12.SelectedIndex = 0;
                csn13.Items.Add(slowka.SlowkoPl);
                csn13.SelectedIndex = 0;
                csn14.Items.Add(slowka.SlowkoPl);
                csn14.SelectedIndex = 0;
                csn15.Items.Add(slowka.SlowkoPl);
                csn15.SelectedIndex = 0;
                csn16.Items.Add(slowka.SlowkoPl);
                csn16.SelectedIndex = 0;
                csn17.Items.Add(slowka.SlowkoPl);
                csn17.SelectedIndex = 0;
                csn18.Items.Add(slowka.SlowkoPl);
                csn18.SelectedIndex = 0;
                csn19.Items.Add(slowka.SlowkoPl);
                csn19.SelectedIndex = 0;
            }

            Sprawdz.IsEnabled = true;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Sprawdz.IsEnabled = false;
            wroc.IsEnabled = true;

            wynik += sprawdzSlowkoEN(sn0, 0);
            wynik += sprawdzSlowkoEN(sn1, 1);
            wynik += sprawdzSlowkoEN(sn2, 2);
            wynik += sprawdzSlowkoEN(sn3, 3);
            wynik += sprawdzSlowkoEN(sn4, 4);
            wynik += sprawdzSlowkoEN(sn5, 5);
            wynik += sprawdzSlowkoEN(sn6, 6);
            wynik += sprawdzSlowkoEN(sn7, 7);
            wynik += sprawdzSlowkoEN(sn8, 8);
            wynik += sprawdzSlowkoEN(sn9, 9);

            wynik += sprawdzSlowkoPL(csn10, 10);
            wynik += sprawdzSlowkoPL(csn11, 11);
            wynik += sprawdzSlowkoPL(csn12, 12);
            wynik += sprawdzSlowkoPL(csn13, 13);
            wynik += sprawdzSlowkoPL(csn14, 14);
            wynik += sprawdzSlowkoPL(csn15, 15);
            wynik += sprawdzSlowkoPL(csn16, 16);
            wynik += sprawdzSlowkoPL(csn17, 17);
            wynik += sprawdzSlowkoPL(csn18, 18);
            wynik += sprawdzSlowkoPL(csn19, 19);

            double wynik2 = (double)wynik / 30 * 100;
            wynik2 = Math.Round(wynik2);

            if (wynik2 < 30)
            {
                if (systemoceniania == "procent")
                {
                    pokazwynik.Text = wynik2.ToString() + "%";
                }
                else
                {
                    pokazwynik.Text = "1";
                }
                
                pokazwynik.Background = new SolidColorBrush(Colors.Red);
            }
            else if (wynik2 >= 30 & wynik2 <= 50)
            {
                if (systemoceniania == "procent")
                {
                    pokazwynik.Text = wynik2.ToString() + "%";
                }
                else
                {
                    pokazwynik.Text = "2";
                }
                pokazwynik.Background = new SolidColorBrush(Colors.Yellow);
            }
            else if (wynik2 > 50 & wynik2 <= 70)
            {
                if (systemoceniania == "procent")
                {
                    pokazwynik.Text = wynik2.ToString() + "%";
                }
                else
                {
                    pokazwynik.Text = "3";
                }
                pokazwynik.Background = new SolidColorBrush(Colors.Yellow);
            }
            else if (wynik2 > 70 & wynik2 <= 89)
            {
                if (systemoceniania == "procent")
                {
                    pokazwynik.Text = wynik2.ToString() + "%";
                }
                else
                {
                    pokazwynik.Text = "4";
                }
                pokazwynik.Background = new SolidColorBrush(Colors.GreenYellow);
            }
            else if (wynik2 > 70)
            {
                if (systemoceniania == "procent")
                {
                    pokazwynik.Text = wynik2.ToString() + "%";
                }
                else
                {
                    pokazwynik.Text = "5";
                }
                pokazwynik.Background = new SolidColorBrush(Colors.GreenYellow);
            }
        }

        public int sprawdzSlowkoEN(TextBox box, int indeks)
        {

            if (query[tablicawylosowanychindeksow[indeks]].SlowkoEn.Equals(box.Text.ToLower()))
            {
                box.Background = new SolidColorBrush(Colors.GreenYellow);
                return 2;
            }
            else
            {
             box.Background = new SolidColorBrush(Colors.Red);
             return 0;
            }
            
        }

        public int sprawdzSlowkoPL(ComboBox box, int indeks)
        {
            if (query[tablicawylosowanychindeksow[indeks]].SlowkoPl.Equals(box.SelectedValue.ToString()))
            {
                box.Background = new SolidColorBrush(Colors.GreenYellow);
                return 1;
            }
            else
            {
                box.Background = new SolidColorBrush(Colors.Red);
                return 0;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(egzamin1));
        }

        }
        
    }



