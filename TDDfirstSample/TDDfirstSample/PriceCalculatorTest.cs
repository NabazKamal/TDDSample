using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;

namespace TDDfirstSample
{
    //En priscalc som räknar ut kundpris för en varukorg

    //Systemundertest_scenario_expectedbehavior()
    public class PriceCalculatorTest
    {
        //we have a basket with items in it-----------
        //items have prices---
        //Calculate and sum of the items price
        //items can have discounts
        //three of some items can give 20% discount
        //two of same items give 15% discount
        //valid voucher will give the discount
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
            var res = pc.CalculatePrice(CreateBasketSingelItem(1));
            Assert.Equal(10.50M, res);
        }

        [Fact]
        public void CalculatePrice_TwoOfSameSingleItemInBasket_WillGive20Discount()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = pc.CalculatePrice(CreateBasketSingelItem(2));
            Assert.Equal(10.50M * 2 * 0.8M, res);
        }

        [Fact]
        public void CalculatePrice_TwoOfSameMultiItemInBasket_WillGive20Discount()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = pc.CalculatePrice(CreateBasketMultiItems(2));
            Assert.Equal(40.50M * 2 * 0.8M, res);
        }

        private static Basket CreateBasketSingelItem(int quantity)
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M, Quantity = quantity } });
        }

        [Fact]
        public void CalculatePrice_MultiItemInBasket_ReturnsTheSumForItemprice()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = pc.CalculatePrice(CreateBasketMultiItems(1));
            Assert.Equal(40.50M , res);

        }

        private static Basket CreateBasketMultiItems(int quantity)
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M, Quantity = quantity }, new Item() { Id = 1, Price = 30.0M, Quantity = quantity } });
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
                TwoOfSameItemDiscount(basket);
                return basket.Items.Sum(x => x.Price);
            }

            private static void TwoOfSameItemDiscount(Basket basket)
            {
                foreach (var i in basket.Items)
                    if (i.Quantity > 1)
                        i.Price = i.Price * i.Quantity * 0.8M;
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
            public int Quantity { get;  set; }
        }
    }
}
