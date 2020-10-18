using System;

namespace TDDfirstLib
{
    public class Item
    {
        public int Id { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public CampainCode CampainCode { get; set; }
    }
}
