using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CourierKata.Test
{
    public class PackageDimensionsTest
    {
        public class Constructor
        {
            [Fact(DisplayName = "Should set height and width when constructed")]
            public void Should_Set_Height_And_Width_When_Constructed()
            {
                // Arrange
                const int expectedWidth = 50;
                const int expectedHeight = 100;

                // Act
                var result = new ParcelDimensions(expectedWidth, expectedHeight);

                // Assert
                Assert.Equal(result.WidthCm, expectedWidth);
                Assert.Equal(result.HeightCm, expectedHeight);
            }
        }

        public class GetLargestDimension
        {
            [Fact(DisplayName = "Should return width when width is greater then height")]
            public void Should_Return_Width_When_Greater_Than_Height()
            {
                // Arrange
                const int width = 60;
                const int height = 30;
                var underTest = new ParcelDimensions(width, height);

                // Act
                var result = underTest.GetLargestDimension();

                // Assert
                Assert.Equal(result, width);
            }

            [Fact(DisplayName = "Should return height when height is greater then width")]
            public void Should_Return_Height_When_Greater_Than_Width()
            {
                // Arrange
                const int width = 50;
                const int height = 70;
                var underTest = new ParcelDimensions(width, height);

                // Act
                var result = underTest.GetLargestDimension();

                // Assert
                Assert.Equal(result, height);
            }
        }
    }
}
