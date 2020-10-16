using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;

namespace TDDfirstSample
{
    //En priscalc som räknar ut kundpris för en varukorg

    //Systemundertest_scenario_expectedbehavior()
    public class UnitTest1
    {
        //we have a basket with items in it-----------
        //items have prices---
        //Calculate and sum of the items price
        //items can have discounts
        //three of some items can give 20% discount
        //two of same items give 15% discount
        //empty basket will cost 0kr ---------
        [Fact]
        public void CalculatePrice_EmptyBasket_ReturnsDefault()
        {
            PriceCalculator pc = CreatePriceCalculator();
            decimal price = pc.CalculatePrice(new Basket());
            Assert.Equal(0, price);
        }
        [Fact]
        public void CalculatePrice_NullBasket_ReturnsException()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = Assert.Throws<ArgumentNullException>(() => pc.CalculatePrice(null));
            Assert.Equal("Varukorgen ska inte vara tom (Parameter 'Basket')", res.Message);
        }
        [Fact]
        public void CalculatePrice_SingelItemInBasket_ReturnsTheItemprice()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = pc.CalculatePrice(CreateBasketSingelItem());
            Assert.Equal(10.50M, res);
        }

        private static Basket CreateBasketSingelItem()
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M } });
        }

        [Fact]
        public void CalculatePrice_MultiItemInBasket_ReturnsTheSumForItemprice()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = pc.CalculatePrice(CreateBasketMultiItems());
            Assert.Equal(40.50M, res);

        }

        private static Basket CreateBasketMultiItems()
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M }, new Item() { Id = 1, Price = 30.0M } });
        }

        private static PriceCalculator CreatePriceCalculator()
        {
            return new PriceCalculator();
        }

        public class PriceCalculator
        {
            public decimal CalculatePrice(Basket basket)
            {
                if (basket == null)
                    NullBasket();
                if (basket.Items != null)
                    return CustomerPrice(basket);
                return decimal.Zero;
            }

            private static void NullBasket()
            {
                throw new ArgumentNullException("Basket", "Varukorgen ska inte vara tom");
            }

            private static decimal CustomerPrice(Basket basket)
            {
                return basket.Items.Sum(x => x.Price);
            }
        }

        public class Basket
        {
            public List<Item> Items;

            public Basket()
            {
            }

            public Basket(List<Item> items)
            {
                this.Items = items;
            }
        }

        public class Item
        {
            public int Id { get; set; }
            public decimal Price { get; set; }
        }
    }
}
