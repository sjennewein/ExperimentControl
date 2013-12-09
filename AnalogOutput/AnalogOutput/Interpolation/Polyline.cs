using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalogOutput.Interpolation
{
    public class Polyline
    {
        private readonly List<Point> _dataPoints;

        public Polyline(List<Point> dataPoints)
        {
            _dataPoints = dataPoints;
        }

        public double Interpolate(double x)
        {
            if (_dataPoints[0].X > x || _dataPoints.Last().X < x)
                throw new Exception("There are no data-points to interpolate this value!");

            Point upperPoint = null;
            foreach (Point dataPoint in _dataPoints)
            {
                if (dataPoint.X > x)
                {
                    upperPoint = dataPoint;
                    break;
                }

                //if the point to interpolate is identical to one in dataset
                if (Math.Abs(dataPoint.X - x) < 0.001)
                {
                    return dataPoint.Y;
                }
            }

            Point lowerPoint = _dataPoints[_dataPoints.IndexOf(upperPoint) - 1];

            double output = lowerPoint.Y + (upperPoint.Y - lowerPoint.Y) /
                                           (upperPoint.X - lowerPoint.X) * (x - lowerPoint.X);
            return output;
        }
    }
}