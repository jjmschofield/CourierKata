using System;
using System.Collections.Generic;
using Xunit;

namespace CourierKata.Test
{
    public class ParcelTests
    {
        public class Constructor
        {
            [Fact]
            public void Should_Set_Package_Type_To_Small_When_Largest_Dimension_Less_Than_10_CM()
            {
                var underTest = new Parcel(5, 9);
                Assert.Equal("Small", underTest.Type.Name);
                Assert.Equal(0, underTest.Type.Code);
            }

            [Fact]
            public void Should_Set_Package_Type_To_Medium_When_Largest_Dimension_Less_Than_50_CM()
            {
                var underTest = new Parcel(49, 10);
                Assert.Equal("Medium", underTest.Type.Name);
                Assert.Equal(1, underTest.Type.Code);
            }

            [Fact]
            public void Should_Set_Package_Type_To_Large_When_Largest_Dimension_Less_Than_100_CM()
            {
                var underTest = new Parcel(5, 99);
                Assert.Equal("Large", underTest.Type.Name);
                Assert.Equal(2, underTest.Type.Code);
            }

            [Fact]
            public void Should_Set_Package_Type_To_XL_When_Largest_Dimension_Greater_Than_100_CM()
            {
                var underTest = new Parcel(101, 1);
                Assert.Equal("XL", underTest.Type.Name);
                Assert.Equal(3, underTest.Type.Code);
            }
        }

        public class SetShippingCost
        {
            [Fact]
            public void Should_Calculate_The_Price_Of_Parcels_By_Type_Code_When_Given_A_Valid_Price_Dictionary()
            {
                var expectedCost = 25.00;
              
                var underTest = new Parcel(5, 9);

                var shippingRates = new Dictionary<int, ShippingRate> { { underTest.Type.Code, new ShippingRate(expectedCost, 0, 0) } };

                underTest.SetShippingCost(shippingRates);

                Assert.Equal(expectedCost, underTest.ShippingCharge);
            }

            [Fact]
            public void Should_Calculate_An_Extra_Charge_For_Each_Kg_Over_The_Weight_Limit()
            {
                var cost = 25.00;
                
                var weightLimit = 1;
                var actualWeight = 3;
                var overWeightByKg = 2;

                var underTest = new Parcel(5, 9, actualWeight);

                var shippingRates = new Dictionary<int, ShippingRate> { { underTest.Type.Code, new ShippingRate(cost, weightLimit, overWeightByKg) } };

                underTest.SetShippingCost(shippingRates);

                var expectedOverweightCharge = (actualWeight - weightLimit) * overWeightByKg;

                Assert.Equal(expectedOverweightCharge, underTest.OverweightCharge);
                Assert.Equal(underTest.TotalCost, underTest.ShippingCharge + underTest.OverweightCharge);
            }

            [Fact]
            public void Should_Throw_When_The_Code_For_A_Parcel_Is_Not_Available()
            {
                var underTest = new Parcel(5, 9);

                var shippingRates = new Dictionary<int, ShippingRate>();

                Assert.Throws<Exception>(() => underTest.SetShippingCost(shippingRates));
            }
        }
    }
}
