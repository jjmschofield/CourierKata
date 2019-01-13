using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class ParcelClassification
    {
        public string Name { get; }
        public int Code { get; }

    public ParcelClassification(ParcelDimensions dimensions, double weightKg)
        {
            if (weightKg >= 50)
            {
                Name = "Heavy";
                Code = 4;
                return;
            }

            var largestDimensionCm = dimensions.GetLargestDimension();

            if (largestDimensionCm < 10)
            {
                Name = "Small";
                Code = 0;
            }
            else if (largestDimensionCm < 50)
            {
                Name = "Medium";
                Code = 1;
            }
            else if (largestDimensionCm < 100)
            {
                Name = "Large";
                Code = 2;
            }
            else
            {
                Name = "XL";
                Code = 3;
            }
        }
    }
}
