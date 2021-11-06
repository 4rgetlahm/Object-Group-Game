using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public struct Coordinate
    {
        public Coordinate(double Latitutde, double Longtitude)
        {
            this.Latitude = Latitutde;
            this.Longtitude = Longtitude;
        }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}
