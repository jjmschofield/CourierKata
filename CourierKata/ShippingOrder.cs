﻿using System;
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
        public List<ParcelDiscounts> Discounts { get; }
        public double TotalDiscounts { get; } = 0;
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

            Discounts = CalculateDiscounts();
            TotalDiscounts = Discounts.Sum(discount => discount.Value);

            TotalPrice = ParcelPrice + SpeedyShippingPrice - TotalDiscounts;
        }

        public List<ParcelDiscounts> CalculateDiscounts()
        {
            var discounts = new List<ParcelDiscounts>();
            discounts.AddRange(GetDiscountForParcelCode(ParcelCode.Small, 4));
            discounts.AddRange(GetDiscountForParcelCode(ParcelCode.Medium, 3));
            return discounts;
        }

        List<ParcelDiscounts> GetDiscountForParcelCode(ParcelCode code, int discountSet)
        {
            var discounts = new List<ParcelDiscounts>();
            
            var groupedParcels = Parcels
                .Where(parcel => parcel.Type.Code == code)
                .OrderBy(parcel => parcel.TotalPrice)
                .ToList();
  
            var numberOfRequiredDiscounts = (int)Math.Floor((double)groupedParcels.Count / discountSet);

            var offset = 0;

            while (discounts.Count < numberOfRequiredDiscounts)
            {
                var discount = new ParcelDiscounts
                {
                    DiscountedParcel = groupedParcels[offset],
                    Parcels = groupedParcels.GetRange(offset, discountSet),
                    Value = groupedParcels[offset].TotalPrice
                };

                discounts.Add(discount);

                offset = offset + discountSet;
            }

            return discounts;
        }
    }
}
