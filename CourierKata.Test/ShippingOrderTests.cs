using System.Collections.Generic;
using Xunit;

namespace CourierKata.Test
{
    public class ShippingOrderTests
    {
        public class KataOne
        {
            readonly Dictionary<int, ShippingRate> PriceDictionary = new Dictionary<int, ShippingRate>
            {
                {0, new ShippingRate(3, 1, 2)},
                {1, new ShippingRate(8, 3, 2)},
                {2, new ShippingRate(15, 6, 2)},
                {3, new ShippingRate(25, 10, 2)}
            };

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

                var underTest = new ShippingOrder(parcels, PriceDictionary);

                Assert.Equal(expectedCost, underTest.TotalCost);
            }
        }

        public class KataTwo
        {
            readonly Dictionary<int, ShippingRate> PriceDictionary = new Dictionary<int, ShippingRate>
            {
                {0, new ShippingRate(3, 1, 2)},
                {1, new ShippingRate(8, 3, 2)},
                {2, new ShippingRate(15, 6, 2)},
                {3, new ShippingRate(25, 10, 2)}
            };

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

                var underTest = new ShippingOrder(parcels, PriceDictionary, true);

                Assert.Equal(expectedCost, underTest.TotalCost);
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

                var underTest = new ShippingOrder(parcels, PriceDictionary, true);

                Assert.Equal(expectedCost, underTest.SpeedyShippingCost);
            }
        }

        public class KataThree
        {
            readonly Dictionary<int, ShippingRate> PriceDictionary = new Dictionary<int, ShippingRate>
            {
                {0, new ShippingRate(3, 1, 2)},
                {1, new ShippingRate(8, 3, 2)},
                {2, new ShippingRate(15, 6, 2)},
                {3, new ShippingRate(25, 10, 2)}
            };

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

                var underTest = new ShippingOrder(parcels, PriceDictionary);

                Assert.Equal(expectedCost, underTest.TotalCost);
            }
        }

        public class KataFour
        {
            readonly Dictionary<int, ShippingRate> PriceDictionary = new Dictionary<int, ShippingRate>
            {
                {0, new ShippingRate(3, 1, 2)},
                {1, new ShippingRate(8, 3, 2)},
                {2, new ShippingRate(15, 6, 2)},
                {3, new ShippingRate(25, 10, 2)},
                {4, new ShippingRate(50, 50, 1)}
            };

            [Fact]
            public void It_Should_Only_Charge_1kg_Per_Kg_Overweight_For_Heavy_Parcels()
            {
                var expectedCost = 51.00;

                var parcels = new List<Parcel>
                {
                    new Parcel(5, 9, 51)
                };

                var underTest = new ShippingOrder(parcels, PriceDictionary);

                Assert.Equal(expectedCost, underTest.TotalCost);
            }
        }
    }
}
