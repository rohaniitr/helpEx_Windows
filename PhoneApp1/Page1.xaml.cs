using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.ViewModels;
using PhoneApp1.Model;
using Jace;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using System.IO;
using OxyPlot;
using OxyPlot.Series;
using System.Diagnostics;
using Windows.Storage;
namespace PhoneApp1
{
    public partial class Page1 : PhoneApplicationPage
    {
        private ViewModel viewModel = new ViewModel();
        private string base64String;
        ObservableCollection<DataPoint> list_new = new ObservableCollection<DataPoint>();
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var list = NavigationService.GetLastNavigationData();
            ObservableCollection<DataPoint> list_new = (ObservableCollection<DataPoint>)list;

            List<DataPoint> list_sorted = list_new.OrderBy(o => o.X).ToList();
            list_new = new ObservableCollection<DataPoint>(list_sorted);

            string function = string.Empty;
            CalculationEngine engine = new CalculationEngine();
            Debug.WriteLine("function");
            if (NavigationContext.QueryString.TryGetValue("function", out function))
            {
                Debug.WriteLine("inside");
                if (function != "")
                    viewModel.MyModel.Series.Add(new FunctionSeries(parameter => //Math.Sin(parameter)/parameter,
                    {
                        Dictionary<string, double> variables = new Dictionary<string, double>();
                        variables.Add("a", parameter);
                        return engine.Calculate(function, variables);
                    },
                        -10, 10, 0.1, function));
            }
            Debug.WriteLine("function outside");
            LineSeries lineSeries = new LineSeries();
            lineSeries.ItemsSource = list_new;
            viewModel.MyModel.Series.Add(lineSeries);


            this.DataContext = viewModel;
        }

        public void SaveToMediaLibrary(WriteableBitmap bitmap, string name, int quality)
        {
            using (var stream = new MemoryStream())
            {
                // Save the picture to the Windows Phone media library.
                bitmap.SaveJpeg(stream, bitmap.PixelWidth, bitmap.PixelHeight, 0, quality);
                stream.Seek(0, SeekOrigin.Begin);
                new MediaLibrary().SavePicture(name, stream);
                byte[] imageBytes = stream.ToArray();
                base64String = Convert.ToBase64String(imageBytes);
            }
        }
        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            WriteableBitmap bmpCurrentScreenImage = new WriteableBitmap((int)this.ActualWidth, (int)this.ActualHeight);
            bmpCurrentScreenImage.Render(ContentPanel, new MatrixTransform());
            bmpCurrentScreenImage.Invalidate();
            var fileName = String.Format("MyImage_{0:}.jpg", DateTime.Now.Ticks);
            SaveToMediaLibrary(bmpCurrentScreenImage, fileName, 100);
            MessageBox.Show("Image successfully Captured and saved!");
            string urlbase = String.Concat("http://searchtweets.bugs3.com/mail_image.php?to=", email.Text.Trim());

            Debug.WriteLine(email.Text + roll.Text);
            
            
            System.Diagnostics.Debug.WriteLine("Woah1!");
            var doc = new PortableDocument();
            System.Diagnostics.Debug.WriteLine("Woah2!");
            doc.Title = "Experiment";
            System.Diagnostics.Debug.WriteLine("Woah3!");
            doc.Author = "User_UnKnown";
            doc.AddPage(PageSize.A4);
            doc.SetFont("Arial", 48);
            doc.DrawText(doc.PageWidth / 4, doc.PageHeight - 50, "Experiment Info");
            doc.DrawLine(0, doc.PageHeight - 60, doc.PageWidth, doc.PageHeight - 60);

            //doc.DrawImage(portableImage);
            doc.SetFont("Arial", 24);
            System.Diagnostics.Debug.WriteLine("Woah4!");
            double xi = 50;
            double yi = doc.PageHeight - 200;
            foreach (DataPoint d in list_new)
            {
                doc.DrawText(xi, yi, "X:" + d.X.ToString() + " Y: " + d.Y.ToString());
                yi = yi - 50;
            }

            doc.DrawText(50, 400, "End of Document");
            //doc.DrawImage(portableImage);
            doc.Save(TestStream());
            System.Diagnostics.Debug.WriteLine("Woah5!");
            if (File.Exists("exampleExperiment1.pdf"))
            {
                System.Diagnostics.Debug.WriteLine("It Does Exist!");
                FileStream pdfFile2 = File.OpenRead("exampleExperiment1.pdf");
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

                System.Diagnostics.Debug.WriteLine(Path.GetFullPath("exampleExperiment1.pdf"));
                System.Diagnostics.Debug.WriteLine(installedLocation.Path);
                // Access the PDF.
                //StorageFile pdfFile = await local.GetFileAsync("C:\\Data\\Programs\\{61044A2A-FAB3-4925-BDAE-498DEB9E35EA}\\Install\\exampleExperiment1.pdf");
                //Windows.System.Launcher.LaunchFileAsync(pdfFile);
                StorageFile pdfFile = await installedLocation.GetFileAsync("exampleExperiment1.pdf");
                Windows.System.Launcher.LaunchFileAsync(pdfFile);

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Sorry Man! It Doesn't");
            }
    
        }
        private Stream TestStream()
        {
            Stream fs = File.Create("exampleExperiment1.pdf");
            return fs;
        }
        
    }  
}