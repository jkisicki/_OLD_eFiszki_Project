using SQLite;
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
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamesStandardLeaning2 : Page
    {
        public string baza;
        public int licznik = 0;
        public List<UserDefaultDataBase> PobierzDaneBazy;
        public GamesStandardLeaning2()
        {
            this.InitializeComponent();
            baza = GamesStandardLeaning1.baza;

            if (baza.Equals("eFiszki"))
            {
                baza = "AppData";
            }

            gamesnextbutton.IsEnabled = false;
            gamespreviousbutton.IsEnabled = false;
            zacznij();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void zacznij()
        {
           // StandardLearningStart.IsEnabled = false;
            gamesnextbutton.IsEnabled = true;
            gamespreviousbutton.IsEnabled = true;


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
                gamespreviousbutton.IsEnabled = false;
                StandatdLearningslowkoPL.Text = PobierzDaneBazy[licznik].SlowkoPl;
                StandatdLearningslowkoEN.Text = PobierzDaneBazy[licznik].SlowkoEn;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamesStandardLeaning1));
        }

        private void nextprevious(int licznik)
        {
            try
            {
                if (licznik == 0)
                {
                    gamespreviousbutton.IsEnabled = false;
                }
                else if (licznik == PobierzDaneBazy.Count - 1)
                {
                    gamesnextbutton.IsEnabled = false;
                }

                StandatdLearningslowkoPL.Text = PobierzDaneBazy[licznik].SlowkoPl;
                StandatdLearningslowkoEN.Text = PobierzDaneBazy[licznik].SlowkoEn;
            }

            catch (ArgumentOutOfRangeException e)
            {
                if (licznik > 0)
                {
                    gamesnextbutton.IsEnabled = false;
                }

                else
                {
                    gamespreviousbutton.IsEnabled = false;
                }
            }
        } 

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            gamesnextbutton.IsEnabled = true;
            licznik --;
            nextprevious(licznik);
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            gamespreviousbutton.IsEnabled = true;
            licznik++;
            nextprevious(licznik);
            
        }
    }
}
