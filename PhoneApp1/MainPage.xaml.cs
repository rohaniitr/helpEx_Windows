using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;
using PhoneApp1.Model;
using PhoneApp1.ViewModels;
using System.Collections.ObjectModel;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using OxyPlot;

namespace PhoneApp1
{
    
    public partial class MainPage : PhoneApplicationPage
    {
        List<double> xCoordinates = new List<double>();
        List<double> yCoordinates = new List<double>();
        List<String> coordinates = new List<string>();
        public ObservableCollection<DataPoint> CollectionCoordinates = new ObservableCollection<DataPoint>();
        //private ViewModel viewModel = new ViewModel();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            CollectionCoordinates.Add(new DataPoint(0, 0));
        }
        private async void FindFrequency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SpeechRecognizerUI speechRecognition = new SpeechRecognizerUI();
                speechRecognition.Settings.ListenText = "Enter Sentence!";
                speechRecognition.Settings.ExampleText = "plot will show frequency of letters(lower case)";
                SpeechSynthesizer synth = new SpeechSynthesizer();
                await synth.SpeakTextAsync("Say something");
                SpeechRecognitionUIResult recoResult = await speechRecognition.RecognizeWithUIAsync();

                if (recoResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    int[] alpha = Enumerable.Repeat(0,26).ToArray();
                    
                    recoResult.RecognitionResult.Text.ToLower();
                    for(int i=0 ; i < recoResult.RecognitionResult.Text.Length ; i++)
                    {
                        if(Char.IsLetter(recoResult.RecognitionResult.Text[i]))
                            alpha[((int)recoResult.RecognitionResult.Text[i])-97]++;
                    }
                    for(int i=0;i<recoResult.RecognitionResult.Text.Length;i++)
                    {
                        DataPoint model = new DataPoint((char)(i + 97), alpha[i]);
                        CollectionCoordinates.Add(model);
                    }
                }
            }
            catch(Exception r)
            {

            }
        }
        private async void SpeechToText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SpeechRecognizerUI speechRecognition = new SpeechRecognizerUI();
                speechRecognition.Settings.ListenText = "Enter Observations!";
                speechRecognition.Settings.ExampleText = "Ex: X equals <value>, Y equals <value>";
                SpeechSynthesizer synth = new SpeechSynthesizer();
                await synth.SpeakTextAsync("X equals!?");
                SpeechRecognitionUIResult recoResult = await speechRecognition.RecognizeWithUIAsync();

                if (recoResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    xCoordinates.Add(Double.Parse(recoResult.RecognitionResult.Text));
                }
                double x_val = Double.Parse(recoResult.RecognitionResult.Text);
                await synth.SpeakTextAsync("Y equals!?");
                recoResult = await speechRecognition.RecognizeWithUIAsync();
                if (recoResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    yCoordinates.Add(Double.Parse(recoResult.RecognitionResult.Text));
                }
                double y_val = Double.Parse(recoResult.RecognitionResult.Text);
                coordinates.Add("X: " + x_val + "   Y: " + y_val);
                listCoordinates.ItemsSource = null;
                listCoordinates.ItemsSource = coordinates;
                DataPoint model = new DataPoint(x_val, y_val);
                CollectionCoordinates.Add(model);
            }
            catch (Exception h)
            {
                MessageBox.Show("Some error, Say Clearly!");
            }
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addToPlotButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbx1.Text == "" || tbx2.Text == "")
                SpeechToText_Click(sender , e);
            else
            {
                int xCoordinate = Convert.ToInt32(tbx1.Text);
                int yCoordinate = Convert.ToInt32(tbx2.Text);
                xCoordinates.Add(xCoordinate);
                yCoordinates.Add(yCoordinate);
                coordinates.Add("X: " + tbx1.Text + "   Y: " + tbx2.Text);
                listCoordinates.ItemsSource = null;
                listCoordinates.ItemsSource = coordinates;
                DataPoint model = new DataPoint(Convert.ToDouble(xCoordinate), Convert.ToDouble(yCoordinate));
                CollectionCoordinates.Add(model);
            }
            
        }

        private void clearCoordinates_Click(object sender, RoutedEventArgs e)
        {
            xCoordinates.Clear();
            yCoordinates.Clear();
            coordinates.Clear();
            listCoordinates.ItemsSource = null;
            CollectionCoordinates.Clear();
        }

        private void showPlot_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate("/Page1.xaml?function=" + functiontbx.Text, CollectionCoordinates);
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