using System;
using System.Collections.Generic;
using System.Text;

namespace CourierKata
{
    public class Parcel
    {
        public ParcelDimensions Dimensions { get; }
        public ParcelClassification Type { get; }
        

        public Parcel(int widthCm, int heightCm)
        {
            Dimensions = new ParcelDimensions(widthCm, heightCm);
            Type = new ParcelClassification(Dimensions);
        }
    }
}
