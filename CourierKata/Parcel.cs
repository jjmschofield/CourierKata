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
        public double ShippingCharge { get; private set; }
        public double OverweightCharge { get; private set; }
        public double TotalCost { get; private set; }

        public Parcel(double widthCm, double heightCm, double weightKg = 0)
        {
            Dimensions = new ParcelDimensions(widthCm, heightCm);
            Type = new ParcelClassification(Dimensions);
            WeightKg = weightKg;
        }

        public void SetShippingCost(Dictionary<int, ShippingCharge> shippingChargeByType)
        {
            if (!shippingChargeByType.ContainsKey(Type.Code))
            {
                throw new Exception("Type " + Type.Code + " is not available in the supplied price dictionary");
            }

            ShippingCharge = shippingChargeByType[Type.Code].Charge;
            OverweightCharge = CalculateOverweightCharge(shippingChargeByType[Type.Code]);
            TotalCost = ShippingCharge + OverweightCharge;
        }

        private double CalculateOverweightCharge(ShippingCharge shippingCharge)
        {
            if (WeightKg > shippingCharge.WeightLimitKg)
            {
                return (WeightKg - shippingCharge.WeightLimitKg) * shippingCharge.OverweightChargePerKg;
            }
            
            return 0;
        }
    }
}
