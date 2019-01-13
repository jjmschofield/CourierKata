using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class Parcel
    {
        public ParcelDimensions Dimensions { get; }
        public ParcelClassification Type { get; }
        public double WeightKg { get; }
        public double ShippingCost { get; private set; }

        public Parcel(double widthCm, double heightCm, double weightKg = 0)
        {
            Dimensions = new ParcelDimensions(widthCm, heightCm);
            Type = new ParcelClassification(Dimensions);
            WeightKg = weightKg;
        }

        public void SetShippingCost(Dictionary<int, double> priceByParcelType)
        {
            if (!priceByParcelType.ContainsKey(Type.Code))
            {
                throw new Exception("Type " + Type.Code + " is not available in the supplied price dictionary");
            }

            ShippingCost = priceByParcelType[Type.Code];
        }
    }
}
