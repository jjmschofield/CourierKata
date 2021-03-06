﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class Parcel
    {
        public ParcelDimensions Dimensions { get; }
        public ParcelType Type { get; }
        public double WeightKg { get; }
        public double ShippingCharge { get; private set; }
        public double OverweightCharge { get; private set; }
        public double TotalPrice { get; private set; }

        public Parcel(double widthCm, double heightCm, double weightKg = 0)
        {
            Dimensions = new ParcelDimensions(widthCm, heightCm);
            Type = new ParcelType(Dimensions, weightKg);
            WeightKg = weightKg;
        }

        public void SetShippingPrice(Dictionary<ParcelCode, ShippingRate> shippingRatesByCode)
        {
            if (!shippingRatesByCode.ContainsKey(Type.Code))
            {
                throw new Exception("Type " + Type.Code + " is not available in the supplied price dictionary");
            }

            ShippingCharge = shippingRatesByCode[Type.Code].Charge;
            OverweightCharge = CalculateOverweightCharge(shippingRatesByCode[Type.Code]);
            TotalPrice = ShippingCharge + OverweightCharge;
        }

        private double CalculateOverweightCharge(ShippingRate shippingRate)
        {
            if (WeightKg > shippingRate.WeightLimitKg)
            {
                return (WeightKg - shippingRate.WeightLimitKg) * shippingRate.OverweightChargePerKg;
            }
            
            return 0;
        }
    }
}
