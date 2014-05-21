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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace efiszkiProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UsunBaze : Page
    {
        public UsunBaze()
        {
            this.InitializeComponent();
            buttonusunbaze.IsEnabled = false;
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

            comboboxusunbaze.Items.Clear();

            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;

            IReadOnlyList<StorageFile> fList = await folder.GetFilesAsync();
            foreach (var f in fList)
            {
                //Debug.WriteLine(f.DisplayName);
                if (f.DisplayName.Equals("AppData"))
                {
                }
                else
                {
                    comboboxusunbaze.Items.Add(f.DisplayName);   
                }
            };

        }

        private void comboboxusunbaze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonusunbaze.IsEnabled = true;
        }

        private async void buttonusunbaze_Click(object sender, RoutedEventArgs e)
        {
            string wybranabaza = comboboxusunbaze.SelectedValue.ToString();
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(wybranabaza + ".sqlite");
            await file.DeleteAsync();
            comboboxusunbaze.Items.Clear();
            buttonusunbaze.IsEnabled = false;
            SetCombo();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ZarzadzanieBazami));
        }
    }
}
