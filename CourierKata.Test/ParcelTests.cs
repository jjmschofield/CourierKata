using Xunit;

namespace CourierKata.Test
{
    public class ParcelTests
    {
        public class KataOne
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
    }
}
