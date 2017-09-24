using HamburgerMenuApp.Models;
using HamburgerMenuApp.Views;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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

namespace HamburgerMenuApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        //Todo lo que esté aquí esta para toda la aplicacion disponible, como la conexion a la base de datos.


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            //Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
            // Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
            // Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            /*
             *La creación de las tablas las hago en OnLaunched de App.xaml.cs. Se crearan si no existen. 
             * Como la conexión a la base de datos implementa la interfaz IDisposable, 
             * cuando la llamamos debemos hacerlo de forma directa cerrando finalizándola con Dispose manualmente, 
             * o de forma indirecta usando el using. Yo prefiero usar los usings de C# 
             * por cada vez que realicemos acciones contra la base de datos verán el código:
             */
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbConnectionString))
            {
                conn.CreateTable<b_Persona>();
                conn.CreateTable<bx_Talla>();
            }

        
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
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
        /*
         *Voy a crear la propiedad DbConnectionString que voy a utilizar para abrir las conexiones mas adelante. 
         * Se puede dejar como una propiedad publica en App.xaml.cs, 
         * dejarla en un resource file o como prefieran.
         */

        public static string DbConnectionString
        {
            get
            {
                return Path.Combine(ApplicationData.Current.LocalFolder.Path, "integrat.sqlite");
            }
        }

    }
}
