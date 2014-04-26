using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp2.Resources;
using Windows.Devices.Geolocation;

namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            if (App.Geolocator == null)
            {
                // Use the app's global Geolocator variable
                App.Geolocator = new Geolocator();
            }

            App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
            App.Geolocator.MovementThreshold = 3; // The units are meters.

            App.Geolocator.StatusChanged += geolocator_StatusChanged;
            App.Geolocator.PositionChanged += geolocator_PositionChanged;
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (!App.RunningInBackground)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    txtBlock.Text += "Lat: " + args.Position.Coordinate.Latitude.ToString("0.000000") + "\n";
                    txtBlock.Text += "Long: " + args.Position.Coordinate.Longitude.ToString("0.000000") + "\n";
                    txtBlock.Text += "Alt: " + args.Position.Coordinate.Altitude.ToString() + "\n\n";


                });
            }
            else
            {
                Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                toast.Content = args.Position.Coordinate.Latitude.ToString("0.000");
                toast.Title = "Location: ";
                toast.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);
                toast.Show();

            }
        }
        

        private void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            //throw new NotImplementedException();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}