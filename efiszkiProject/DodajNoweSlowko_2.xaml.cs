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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DodajNoweSlowko_2 : Page
    {
        public string aktualnabaza;
        public DodajNoweSlowko_2()
        {
            aktualnabaza = DodajNoweSlowko_1.baza;
            if (aktualnabaza.Equals("eFiszki"))
            {
                aktualnabaza = "AppData";
            }
            this.InitializeComponent();
            buttonDodajSlowko.IsEnabled = false;
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
            this.Frame.Navigate(typeof(DodajNoweSlowko_1));
        }

        private void TextBoxDodajSlowkoPL_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dlugos = TextBoxDodajSlowkoPL.Text;
            string dlugos2 = TextBoxDodajSlowkoEN.Text;

            if (dlugos.Length > 0 & dlugos2.Length > 0)
            {
                buttonDodajSlowko.IsEnabled = true;
            }
            else
            {
                buttonDodajSlowko.IsEnabled = false;
            }
        }

        private void TextBoxDodajSlowkoEN_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dlugos = TextBoxDodajSlowkoPL.Text;
            string dlugos2 = TextBoxDodajSlowkoEN.Text;

            if (dlugos.Length > 0 & dlugos2.Length > 0)
            {
                buttonDodajSlowko.IsEnabled = true;
            }
            else
            {
                buttonDodajSlowko.IsEnabled = false;
            }
        }

        private void buttonDodajSlowko_Click(object sender, RoutedEventArgs e)
        {
            string slowkopl = TextBoxDodajSlowkoPL.Text.ToLower();
            string slowkoen = TextBoxDodajSlowkoEN.Text.ToLower();
            string kontekst = TextBoxDodajKontekst.Text;
            if (kontekst.Length == 0)
            {
                kontekst = " ";
            }
            string podpowiedz = TextBoxDodajSlowkoPodpowiedz.Text;
            if (podpowiedz.Length == 0)
            {
                podpowiedz = " ";
            }

            string DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, aktualnabaza + ".sqlite");
            var db = new SQLite.SQLiteConnection(DBPath);
            var newfisz = new UserDefaultDataBase
            {
                SlowkoPl = slowkopl,
                SlowkoEn = slowkoen,
                Kontekst = kontekst,
                Podpowiedz = podpowiedz,
                IloscOdpowiedzi = 0,
                IloscPoprawnychOdpowiedzi = 0,
                passa = 0,
                kategoria = 1

            };

            db.Insert(newfisz);

            TextBoxDodajSlowkoEN.Text = "";
            TextBoxDodajSlowkoPL.Text = "";
            TextBoxDodajKontekst.Text = "";
            TextBoxDodajSlowkoPodpowiedz.Text = "";
        }

      
        

    }
}
