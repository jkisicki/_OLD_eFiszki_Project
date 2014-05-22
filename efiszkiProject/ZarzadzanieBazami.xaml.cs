using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.Storage.Pickers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZarzadzanieBazami : Page
    {
        public ZarzadzanieBazami()
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DodajBaze));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DodajNoweSlowko_1));
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UsunBaze));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(UsunSlowko));
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"http://jkisicki.github.io/";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);

        }

        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".sqlite");
            filePicker.ViewMode = PickerViewMode.List;
            filePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            filePicker.CommitButtonText = "OK";

            var selectedFiles = await filePicker.PickSingleFileAsync();
            if (selectedFiles != null)
            {
                await selectedFiles.CopyAsync(ApplicationData.Current.LocalFolder);
            }
        }

        private async void Button_Click_8(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(eksport));
        }

       

        

        
    }
}
