using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace CourierKata.Test
{
    public class ShippingOrderTests
    {
        static readonly Dictionary<ParcelCode, ShippingRate> shippingRatesByCode = new Dictionary<ParcelCode, ShippingRate>
        {
            {ParcelCode.Small, new ShippingRate(3, 1, 2)},
            {ParcelCode.Medium, new ShippingRate(8, 3, 2)},
            {ParcelCode.Large, new ShippingRate(15, 6, 2)},
            {ParcelCode.XL, new ShippingRate(25, 10, 2)},
            {ParcelCode.Heavy, new ShippingRate(50, 50, 1)}
        };

        public class KataOne
        {
            [Fact]
            public void Should_Correctly_Total_The_Price_Of_An_Order()
            {
                var expectedCost = 51.00;

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9),
                    new Parcel(11, 49),
                    new Parcel(5, 51),
                    new Parcel(101, 9),

                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode);

                Assert.Equal(expectedCost, underTest.TotalPrice);
            }
        }

        public class KataTwo
        {
            [Fact]
            public void Should_Double_The_Price_Of_An_Order_When_Speedy_Shipping()
            {
                var expectedCost = 102.00;

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9),
                    new Parcel(11, 49),
                    new Parcel(5, 51),
                    new Parcel(101, 9),

                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode, true);

                Assert.Equal(expectedCost, underTest.TotalPrice);
            }

            [Fact]
            public void Should_Include_The_Speedy_Shipping_Charge_When_Speedy_Shipping()
            {
                var expectedCost = 51.00;

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9),
                    new Parcel(11, 49),
                    new Parcel(5, 51),
                    new Parcel(101, 9),

                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode, true);

                Assert.Equal(expectedCost, underTest.SpeedyShippingPrice);
            }
        }

        public class KataThree
        {
            [Fact]
            public void It_Should_Charge_An_Extra_2kg_For_A_Package_When_It_Is_Overweight()
            {
                var expectedCost = 59.00;

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9, 2),
                    new Parcel(11, 49, 4),
                    new Parcel(5, 51, 7),
                    new Parcel(101, 9, 11),

                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode);

                Assert.Equal(expectedCost, underTest.TotalPrice);
            }
        }

        public class KataFour
        {
            [Fact]
            public void It_Should_Only_Charge_1kg_Per_Kg_Overweight_For_Heavy_Parcels()
            {
                var expectedCost = 51.00;

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9, 51)
                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode);

                Assert.Equal(expectedCost, underTest.TotalPrice);
            }
        }

        public class KataFive
        {
            [Fact]
            public void It_Should_Discount_Cheapest_Small_Parcel_When_At_Lest_Four_Small_Parcels()
            {
                var expectedDiscountParcel = new Parcel(5, 9, 1);

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9, 10),
                    new Parcel(5, 9, 30),
                    expectedDiscountParcel,
                    new Parcel(5, 9, 10),
                    new Parcel(5, 9, 31),
                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode);


                Assert.Equal(expectedDiscountParcel.TotalPrice, underTest.TotalDiscounts);
                Assert.Contains(parcels[0], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[1], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[2], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[3], underTest.Discounts[0].Parcels);
                Assert.DoesNotContain(parcels[4], underTest.Discounts[0].Parcels);

                var expectedOrderTotal = parcels.Sum(parcel => parcel.TotalPrice) - expectedDiscountParcel.TotalPrice;
                Assert.Equal(expectedOrderTotal, underTest.TotalPrice);
            }


            [Fact]
            public void It_Should_Discount_Cheapest_Medium_Parcel_When_At_Lest_Three_Medium_Parcels()
            {
                var expectedDiscountParcel = new Parcel(30, 30, 1);

                var parcels = new List<Parcel>
                {
                    new Parcel(30, 30, 10),
                    new Parcel(30, 30, 30),
                    expectedDiscountParcel,
                    new Parcel(30, 30, 10),
                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode);

                Assert.Equal(expectedDiscountParcel.TotalPrice, underTest.TotalDiscounts);
                Assert.Contains(parcels[0], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[2], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[3], underTest.Discounts[0].Parcels);
                Assert.DoesNotContain(parcels[1], underTest.Discounts[0].Parcels);
            }

            [Fact]
            public void It_Should_Apply_Multiple_Discounts_Wiht_Parcels_Used_Only_Once_Per_Discount()
            {
                var discountedParcelOne = new Parcel(30, 30, 1);
                var discountedParcelTwo = new Parcel(30, 30, 4);

                var parcels = new List<Parcel>
                {
                    discountedParcelOne,
                    discountedParcelTwo,
                    new Parcel(30, 30, 5),
                    new Parcel(30, 30, 6),
                    new Parcel(30, 30),
                    new Parcel(30, 30),
                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode);

                var expectedTotalDiscount = discountedParcelOne.TotalPrice + discountedParcelTwo.TotalPrice;

                Assert.Equal(expectedTotalDiscount, underTest.TotalDiscounts);
                Assert.Contains(parcels[0], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[4], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[5], underTest.Discounts[0].Parcels);
                Assert.Contains(parcels[1], underTest.Discounts[1].Parcels);
                Assert.Contains(parcels[2], underTest.Discounts[1].Parcels);
                Assert.Contains(parcels[3], underTest.Discounts[1].Parcels);
            }

            [Fact]
            public void It_Should_Apply_The_Discount_Before_Speedy_Shipping_Is_Applied()
            {
                var expectedDiscountParcel = new Parcel(5, 9, 1);

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9, 10),
                    new Parcel(5, 9, 30),
                    expectedDiscountParcel,
                    new Parcel(5, 9, 10),
                    new Parcel(5, 9, 31),
                };

                var underTest = new ShippingOrder(parcels, shippingRatesByCode, true);

                var expectedOrderTotal = ( parcels.Sum(parcel => parcel.TotalPrice) * 2) - ( expectedDiscountParcel.TotalPrice * 2);
                Assert.Equal(expectedOrderTotal, underTest.TotalPrice);
            }
        }
    }
}
