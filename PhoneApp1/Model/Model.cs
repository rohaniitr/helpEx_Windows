using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp1.Model
{
    public class Models
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Models(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
