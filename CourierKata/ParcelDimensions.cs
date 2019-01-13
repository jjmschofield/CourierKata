using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class ParcelDimensions
    {
        public double WidthCm;
        public double HeightCm;

        public ParcelDimensions(double widthCm, double heightCm)
        {
            WidthCm = widthCm;
            HeightCm = heightCm;
        }

        public double GetLargestDimension()
        {
            return WidthCm > HeightCm ? WidthCm : HeightCm;
        }
    }
}
