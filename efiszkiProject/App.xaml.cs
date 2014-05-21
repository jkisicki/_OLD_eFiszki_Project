using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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
using System.Threading.Tasks;
using Windows.Storage;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace efiszkiProject
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>


        public string DBPath { get; set; }
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            int sprawdz = 0;
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var files = await folder.GetFilesAsync();

            IReadOnlyList<StorageFile> fList = await folder.GetFilesAsync();
            foreach (var f in fList)
            {
                //Debug.WriteLine(f.DisplayName);
                if (f.DisplayName.Equals("AppData"))
                {
                    sprawdz = 1;
                }
            };
            Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
            if (sprawdz == 0)
            {
                this.DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "AppData.sqlite"); 
                //inicjalizacja bazy
                using (var db = new SQLite.SQLiteConnection(this.DBPath))
                {
                    db.CreateTable<UserInformation>();
                    db.CreateTable<UserDefaultDataBase>();

                    var uzytkownik = new UserInformation
                    {
                        Id = 1,
                        IloscDobrychOdpowiedzi = 0,
                        IloscOgolnychOdpowiedzi = 0,
                        passa = 0,
                        NauczucielTest = 1,
                        NauczycielPin = 0000,
                        IloscLogowan = 0

                    };

                    var slowko1 = new UserDefaultDataBase
                    {
                        SlowkoPl = "Monitor",
                        SlowkoEn = "Screen",
                        Podpowiedz = "You watching move on yours PC's s...",
                        Kontekst = "Monitor komputera",
                        IloscOdpowiedzi = 0,
                        passa = 0,
                        IloscPoprawnychOdpowiedzi = 0,
                        kategoria = 1
                    };


                    db.Insert(uzytkownik);
                    db.Insert(slowko1);
                }
            }

  
            //this.DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "AppData.sqlite");
            //Debug.WriteLine("App path: {0}", DBPath);
            ////inicjalizacja bazy
            //using (var db = new SQLite.SQLiteConnection(this.DBPath))
            //{
            //    db.CreateTable<UserInformation>();
            //    db.CreateTable<UserDefaultDataBase>();

            //    var asd = new UserDefaultDataBase
            //    {
            //        SlowkoPl = "Monitor",
            //        SlowkoEn = "Screen",
            //        Podpowiedz = "You watching move on yours PC's s...",
            //        Kontekst = "Monitor komputera",
            //        IloscOdpowiedzi = 0,
            //        passa = 0,
            //        IloscPoprawnychOdpowiedzi = 0
            //    };

            //    db.Insert(asd);
            //}

            

            
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        
    }
}
