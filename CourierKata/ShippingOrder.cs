using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourierKata
{
    public class ShippingOrder
    {
        public List<Parcel> Parcels { get; }
        public double ParcelPrice { get; } = 0;
        public double SpeedyShippingPrice { get; } = 0;
        public double TotalPrice { get; } = 0;
        
        public string CurrencyCode { get; } = "USD";

        public ShippingOrder(List<Parcel> parcels, Dictionary<ParcelCode, ShippingRate> shippingRatesByCode, bool speedy = false)
        {
            Parcels = parcels;

            foreach (var parcel in parcels)
            {
                parcel.SetShippingPrice(shippingRatesByCode);
                ParcelPrice += parcel.TotalPrice;
            }

            if (speedy)
            {
                SpeedyShippingPrice = ParcelPrice;
            }

            TotalPrice = ParcelPrice + SpeedyShippingPrice;
        }

        //public List<ParcelDiscounts> CalculateDiscounts()
        //{
        //   // Calculate number of small parcel discounts
        //    var smallParcels = Parcels.Where(parcel => parcel.Type.Code == 0);

        //    var mediumParcel

        //    // Calculate number of medium parcel discounts
        //    // Mixed parcel discount
        //}
    }
}
