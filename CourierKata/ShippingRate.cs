using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class ShippingRate
    {
        public double Charge { get; }
        public double WeightLimitKg { get; }
        public double OverweightChargePerKg { get; }

        public ShippingRate(double charge, double weightLimitKg, double overweightChargePerKg)
        {
            Charge = charge;
            WeightLimitKg = weightLimitKg;
            OverweightChargePerKg = overweightChargePerKg;
        }
    }
}
