using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class ParcelType
    {
        public string Name { get; }
        public ParcelCode Code { get; }

    public ParcelType(ParcelDimensions dimensions, double weightKg)
        {
            if (weightKg >= 50)
            {
                Name = "Heavy";
                Code = ParcelCode.Heavy;
                return;
            }

            var largestDimensionCm = dimensions.GetLargestDimension();

            if (largestDimensionCm < 10)
            {
                Name = "Small";
                Code = ParcelCode.Small;
            }
            else if (largestDimensionCm < 50)
            {
                Name = "Medium";
                Code = ParcelCode.Medium;
            }
            else if (largestDimensionCm < 100)
            {
                Name = "Large";
                Code = ParcelCode.Large;
            }
            else
            {
                Name = "XL";
                Code = ParcelCode.XL;
            }
        }
    }
}
