using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class ShippingOrder
    {
        public List<Parcel> Parcels { get; }
        public double ParcelCost { get; } = 0;
        public double SpeedyShippingCost { get; } = 0;
        public double TotalCost { get; } = 0;
        
        public string CurrencyCode { get; } = "USD";

        public ShippingOrder(List<Parcel> parcels, Dictionary<int,ShippingCharge> shippingChargeByType, bool speedy = false)
        {
            Parcels = parcels;

            foreach (var parcel in parcels)
            {
                parcel.SetShippingCost(shippingChargeByType);
                ParcelCost += parcel.TotalCost;
            }

            if (speedy)
            {
                SpeedyShippingCost = ParcelCost;
            }

            TotalCost = ParcelCost + SpeedyShippingCost;
        }
    }
}
