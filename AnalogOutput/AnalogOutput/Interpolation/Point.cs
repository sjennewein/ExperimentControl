using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Interpolation
{
    public class Point
    {
        public readonly double X;
        public readonly double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
