using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppVerse.AdventureWorks.DTO
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }
        
        public string ProductModelName { get; set; }


        public byte[] ThumbNailPhoto { get; set; }

        public string ThumbnailPhotoFileName { get; set; }
        
    }
}
