using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleAccelerometerSensor.Resources;

using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace SimpleAccelerometerSensor
{
    public partial class MainPage : PhoneApplicationPage
    {

        public DependencyProperty IsSpinningProperty =
            DependencyProperty.Register(
            "IsSpinning", typeof(Boolean),
            typeof(MainPage), null
            );                                         

        Accelerometer accelerometer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (!Accelerometer.IsSupported)
            {
                MessageBox.Show("Not Supported.");
            }

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            // Call UpdateUI on the UI thread and pass the AccelerometerReading.
            //Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));

            Dispatcher.BeginInvoke(() =>
                {
                    string x = e.SensorReading.Acceleration.X.ToString();
                    string y = e.SensorReading.Acceleration.Y.ToString();
                    string z = e.SensorReading.Acceleration.Z.ToString();
                    System.Diagnostics.Debug.WriteLine("X: "+ x + "Y: "+ y +"Z: " +z);
                    UpdateUI(e.SensorReading);
                    
                }
                );
        }

        //void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingEventArgs e)
        //{
        //    // Call UpdateUI on the UI thread and pass the AccelerometerReading.
        //    //Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
        //}

        private void UpdateUI(AccelerometerReading accelerometerReading)
        {           
            Vector3 acceleration = accelerometerReading.Acceleration;

            //if (acceleration.X <= -0.35)
            //{
            //    if (pivot.SelectedIndex != 4)
            //    {
            //        pivot.SelectedIndex += 1;
            //    }
            //    else
            //        pivot.SelectedIndex = 0;
            //}
            //if (acceleration.X >= 0.35)
            //{
            //    if (pivot.SelectedIndex != 0)
            //        pivot.SelectedIndex -= 1;
            //    else
            //        pivot.SelectedIndex = 5;
            //}

      

            barraX.Value = acceleration.X*100;
            barraY.Value = acceleration.Y*100;
            barraZ.Value = acceleration.Z*100; 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (accelerometer == null)
            {
                // Instantiate the Accelerometer.
                accelerometer = new Accelerometer();
                accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(40);
                accelerometer.CurrentValueChanged +=
                    new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            }


            try
            {
                MessageBox.Show("Ora accendo l'accelerometro.");
                accelerometer.Start();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("non va.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (accelerometer != null)
            {
                MessageBox.Show("disattivo l'acc");
                accelerometer.Stop();
            }
        }


        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }
    }
}