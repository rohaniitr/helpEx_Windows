using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp1.Model;
using OxyPlot;
using OxyPlot.Series;
using System.Windows.Media.Imaging;

namespace PhoneApp1.ViewModels
{
    public class ViewModel
    {
        public ObservableCollection<Models> Collection { get; set; }
        public ViewModel()
        {
        //    Collection = new ObservableCollection<Models>();
        //    GenerateDatas();
            this.MyModel = new PlotModel { Title = "Example 1" };
        //    this.MyModel.Series.Add(new FunctionSeries( Math.Cos, 0, 10, 0.1, "cos(x)"));
            
        }

        public PlotModel MyModel { get; private set; }
        private void GenerateDatas()
        {
            this.Collection.Add(new Models(0, 1));
            this.Collection.Add(new Models(1, 2));
            this.Collection.Add(new Models(2, 3));
            this.Collection.Add(new Models(3, 4));
        }
    }
}
