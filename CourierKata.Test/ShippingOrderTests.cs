using System.Collections.Generic;
using Xunit;

namespace CourierKata.Test
{
    public class ShippingOrderTests
    {
        public class KataOne
        {
            Dictionary<int, double> PriceDictionary = new Dictionary<int, double>
            {
                { 0, 3 },
                { 1, 8 },
                { 2, 15 },
                { 3, 25 }
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
    }
}
