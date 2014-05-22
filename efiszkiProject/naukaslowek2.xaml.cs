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
    public sealed partial class naukaslowek2 : Page
    {
        public String slowkopl = "";
        public String slowkoen = "";
        public String podpowiedz = "";
        public String kontekst = "";
        int licznik_losowan = 1;
        int kategoria = 0;
        public int iloscodpowiedzi = 0;
        public int iloscpoprawnychodpowiedzo = 0;
        public int passa = 0;
        public int WylosowanaPozycja = 0;
        int userpassa = 0;
        int useriloscdobrychodpowiedzi = 0;
        int useriloscodpowiedzi = 0;
        int Wylosowanyindeks = 0;
        public List<UserDefaultDataBase> PobierzDaneBazy;
        public List<UserInformation> userinf;
        public bool dynamicsave;
        public bool pressenter;
        public bool statistic;
        public bool smartlearning;

        public naukaslowek2()
        {
            this.InitializeComponent();
            clear();
            getdatabasedata();
            dynamicsave = naukaslowek1.dynamicsave;
            pressenter = naukaslowek1.pressenter;
            statistic = naukaslowek1.statistic;
            smartlearning = naukaslowek1.smartrandom;
           Debug.WriteLine("statystyki: "+statistic);
           Debug.WriteLine("enter to C&R:" + pressenter);
           Debug.WriteLine("dynamic save: " + dynamicsave);
           Debug.WriteLine("smart random: " + smartlearning);
           Debug.WriteLine("-------------------------");
            if (statistic == false)
            {
                naukabox1.IsEnabled = false;
                naukabox2.IsEnabled = false;
                naukabox3.IsEnabled = false;
                naukabox4.IsEnabled = false;
                naukabox5.IsEnabled = false;
                naukabox9.IsEnabled = false;
                naukabox10.IsEnabled = false;
                naukabox11.IsEnabled = false;
                naukabox11.IsEnabled = false;
                naukabox12.IsEnabled = false;
                naukabox13.IsEnabled = false;
                textboxuseriloscodpowiedzi.IsEnabled = false;
                textboxuseriloscdobrychodpowiedzi.IsEnabled = false;
                textboxuserpassa.IsEnabled = false;
                textboxuserprocenty.IsEnabled = false;
                userprocent.IsEnabled = false;
                slowkoiloscodpowiedzi.IsEnabled = false;
                slowkoiloscpoprawnychodpowiedzi.IsEnabled = false;
                slowkopassa.IsEnabled = false;
                textboxslowkoprocent.IsEnabled = false;
                slowkoprocent.IsEnabled = false;
                
            }


        }

        public List<int> zwrocindeksy(int kategoria)
        {
            int kat = kategoria;
            List<int> indeksy = new List<int>();
            var Query =
                                from que in PobierzDaneBazy
                                where (que.kategoria == kat)
                                select que.Id;
            foreach (var element in Query)
            {
                indeksy.Add(element);
                Debug.WriteLine("indeks: " + element);
            }

            Debug.WriteLine("ilosc indeksow: " + indeksy.Count);
            if (indeksy.Count == 0)
            {
                if (kat == 1)
                {
                    kat = 2;
                    Debug.WriteLine("Nie ma kategori 1, wybieram kat.2");
                    zwrocindeksy(kat);
                }
                else if (kat == 2)
                {
                    kat = 3;
                    Debug.WriteLine("Nie ma kategori 2, wybieram kat 3");
                    zwrocindeksy(kat);
                }
                else if (kat == 3)
                {
                    Debug.WriteLine("Nie ma kategorii 3, wybieram kat 1");
                    kat = 1;
                    zwrocindeksy(kat);
                }
                

            }
            return indeksy;


            
        }


        public async void getdatabasedata()
        {
            string wybranabaza = getbaza();
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + wybranabaza;
            var db = new SQLiteAsyncConnection(path);
            PobierzDaneBazy = await db.QueryAsync<UserDefaultDataBase>("Select * From UserDefaultDataBase");

            Debug.WriteLine("Pobrano dane z bazy slowek");
            foreach (var e in PobierzDaneBazy)
            {
                Debug.WriteLine("id: {0}, PL: {1}, EN: {2}, ilosc odp: {3}, ilosc pop odp: {4}, passa {5}", e.Id, e.SlowkoPl, e.SlowkoEn, e.IloscOdpowiedzi, e.IloscPoprawnychOdpowiedzi, e.passa);
            }
            Debug.WriteLine("-----------------------");
            var path2 = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppData.sqlite";
            var db2 = new SQLiteAsyncConnection(path2);
            userinf = await db2.QueryAsync<UserInformation>("Select * from UserInformation Where Id = 1");
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
            if (statistic == true)
            {
                if (dynamicsave == false)
                {
                    savedata();
                }
            }
        }

        private async void losuj()
        {
            TextBoxOdpowiedz.Text = "";
            TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.White);
            ButtonLosuj.IsEnabled = false;
            TextBoxOdpowiedz.IsReadOnly = false;
            SwitchPodpowiedz.IsOn = false;
            SwitchPodpowiedz.IsEnabled = true;
            TextBoxPodpowiedzLitery.Text = "";

            if (smartlearning == true) { }


                Random rnd = new Random();
                Wylosowanyindeks = rnd.Next(PobierzDaneBazy.Count);
                Debug.WriteLine("Wylosowany indeks bazy to: " + Wylosowanyindeks);

                WylosowanaPozycja = PobierzDaneBazy[Wylosowanyindeks].Id;
                Debug.WriteLine("Jego ID to :" + WylosowanaPozycja);
                Debug.WriteLine("------------------------");

                slowkopl = PobierzDaneBazy[Wylosowanyindeks].SlowkoPl;
                slowkoen = PobierzDaneBazy[Wylosowanyindeks].SlowkoEn;
                podpowiedz = PobierzDaneBazy[Wylosowanyindeks].Podpowiedz;
                kontekst = PobierzDaneBazy[Wylosowanyindeks].Kontekst;
                if (statistic == true)
                {

                    iloscodpowiedzi = PobierzDaneBazy[Wylosowanyindeks].IloscOdpowiedzi;
                    iloscpoprawnychodpowiedzo = PobierzDaneBazy[Wylosowanyindeks].IloscPoprawnychOdpowiedzi;
                    passa = PobierzDaneBazy[Wylosowanyindeks].passa;
                }
                //Debug.WriteLine(" {0} - {1} w kontekscie {2}, podpowiedz {3}", slowkopl, slowkoen, kontekst, podpowiedz );
                TextBoxWylosowaneSlowko.Text = slowkopl;

                if (SwitchKontekst.IsOn == true)
                {
                    TextBoxKontekst.Text = kontekst;
                }
                sliderpodpowiedz.Maximum = slowkoen.Length / 2;

                if (statistic == true)
                {
                    slowkoiloscodpowiedzi.Text = iloscodpowiedzi.ToString();
                    slowkoiloscpoprawnychodpowiedzi.Text = iloscpoprawnychodpowiedzo.ToString();
                    slowkopassa.Text = passa.ToString();
                    if (iloscodpowiedzi != 0)
                    {
                        slowkoprocent.Value = (double)iloscpoprawnychodpowiedzo / (double)iloscodpowiedzi * 100;
                    }


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

        private void ButtonLosuj_Click(object sender, RoutedEventArgs e)
        {
            losuj();
        }

        private async void Sprawdz()
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
                Debug.WriteLine("Poprawna odpowiedz");
                TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.GreenYellow);
                ButtonLosuj.IsEnabled = true;
                TextBoxOdpowiedz.IsReadOnly = true;
                TextBoxOdpowiedz.Text = slowkoodpowiedz + " = " + slowkoen;
                ButtonSprawdz.IsEnabled = false;
                

                if (statistic == true)
                {

                    PobierzDaneBazy[Wylosowanyindeks].passa += 1;
                    PobierzDaneBazy[Wylosowanyindeks].IloscOdpowiedzi += 1;
                    PobierzDaneBazy[Wylosowanyindeks].IloscPoprawnychOdpowiedzi += 1;

                    Debug.WriteLine("Zmiana passy dla slowka o ID "+PobierzDaneBazy[Wylosowanyindeks].Id );

                    if (PobierzDaneBazy[Wylosowanyindeks].passa > 5 & PobierzDaneBazy[Wylosowanyindeks].passa <= 10 & PobierzDaneBazy[Wylosowanyindeks].kategoria == 1)
                    {
                        PobierzDaneBazy[Wylosowanyindeks].kategoria = 2;
                        Debug.WriteLine("kategoria zmieniona na 2, poniewaz passa = " + PobierzDaneBazy[Wylosowanyindeks].passa);
                    }
                    else if (PobierzDaneBazy[Wylosowanyindeks].passa > 10 & PobierzDaneBazy[Wylosowanyindeks].kategoria == 2)
                    {
                        PobierzDaneBazy[Wylosowanyindeks].kategoria = 3;
                        Debug.WriteLine("kategoria zmieniona na 3, poniewaz passa = " + PobierzDaneBazy[Wylosowanyindeks].passa);
                    }

                    userinf[0].IloscDobrychOdpowiedzi += 1;
                    userinf[0].IloscOgolnychOdpowiedzi += 1;
                    userinf[0].passa += 1;
                   

                    if (dynamicsave == true)
                    {
                        Debug.WriteLine("Wywoluje zapisanie danych, poniewaz dynamic save ="+dynamicsave );
                        Debug.WriteLine("-------------------------");
                        savedata();
                        
                    }
                }
            }
            else
            {
                Debug.WriteLine("Odpowiedz nieprawidlowa");
                TextBoxOdpowiedz.Background = new SolidColorBrush(Colors.Red);
                ButtonLosuj.IsEnabled = true;
                TextBoxOdpowiedz.IsReadOnly = true;
                TextBoxOdpowiedz.Text = slowkoodpowiedz + " !=! " + slowkoen;
                ButtonSprawdz.IsEnabled = false;

                if (statistic == true)
                {
                    PobierzDaneBazy[Wylosowanyindeks].passa = 0;
                    PobierzDaneBazy[Wylosowanyindeks].IloscOdpowiedzi += 1;
                    PobierzDaneBazy[Wylosowanyindeks].kategoria = 1;
                   // Debug.WriteLine("Zmiana kategorii na 1, poniewaz passa = "+ PobierzDaneBazy[WylosowanaPozycja].passa.ToString());
                    userinf[0].IloscOgolnychOdpowiedzi += 1;
                    userinf[0].passa = 0;


                    if (dynamicsave == true)
                        {
                            Debug.WriteLine("Wywoluje zapisanie danych, poniewaz dynamic save =" + dynamicsave);
                            Debug.WriteLine("-------------------------");
                            savedata();
                        }
                }

            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Sprawdz();
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

        private async void savedata()
        {
            Debug.WriteLine("wywolano zapis danych");
            string wybranabaza = getbaza();
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\" + wybranabaza;
            var db = new SQLiteAsyncConnection(path);
            Debug.WriteLine("Polaczono z " + wybranabaza);

            var path2 = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppData.sqlite";
            var db2 = new SQLiteAsyncConnection(path2);
            Debug.WriteLine("Polaczono z AppData");

            Debug.WriteLine("Update danych w bazie slowek:");
            foreach (var e in PobierzDaneBazy)
            {
                Debug.WriteLine("id: {0}, PL: {1}, EN: {2}, ilosc odp: {3}, ilosc pop odp: {4}, passa {5}", e.Id, e.SlowkoPl, e.SlowkoEn, e.IloscOdpowiedzi, e.IloscPoprawnychOdpowiedzi, e.passa);
            }
            int licznik=0;
            foreach (var element in PobierzDaneBazy)
            {
                Debug.WriteLine("zapisuje dla id: " + element.Id+ " o liczniku "+ licznik);
                var update = PobierzDaneBazy.ElementAt(licznik);
                await db.UpdateAsync(update);
                licznik++;
            }
            
            var update2 = userinf.First();

            
            await db2.UpdateAsync(update2);
        }


        private void TextBoxOdpowiedz_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    if (pressenter == true && TextBoxOdpowiedz.IsReadOnly == false)
                    {
                       // ButtonSprawdz.IsEnabled = true;
                        Sprawdz();
                    }
                    else if (pressenter == true && TextBoxOdpowiedz.IsReadOnly == true)
                    {
                        losuj();
                       // ButtonSprawdz.IsEnabled = false;
                    }
                }
            

        }

        }
    }

