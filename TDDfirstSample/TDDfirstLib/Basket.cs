using System;
using System.Collections.Generic;
using System.Text;

namespace TDDfirstLib
{
    public class Basket
    {
        public List<Item> Items;
        public decimal CustomerPrice { get; set; }

        public Basket()
        {
        }

        public Basket(List<Item> items)
        {
            this.Items = items;
        }
    }
}
