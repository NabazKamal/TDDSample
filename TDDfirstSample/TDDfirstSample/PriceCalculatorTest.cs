using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using TDDfirstLib;
using Xunit;

namespace TDDfirstSample
{
    //En priscalc som räknar ut kundpris för en varukorg

    //Systemundertest_scenario_expectedbehavior()
    public class PriceCalculatorTest
    {
        //we have a basket with items in it-----------
        //items have prices---
        //Calculate and sum of the items price-----
        //items can have discounts
        //three of some items can give 20% discount
        //two of same items give 15% discount
        //valid voucher will give the discount
        //Not allowed to combine discounts
        //If there are several discounts take the one which gives most
        //empty basket will cost 0kr ---------
        
        [Fact]
        public void CalculatePrice_ThreeItemsDiscount_Give20Discount()
        {
            PriceCalculator pc = CreatePriceCalculator();
            decimal price = pc.CalculatePrice(CreateBasketMultiItemsWithDiscountflagga());
            Assert.Equal((51.0M * 0.8M) + 10.0M, price);
        }



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


        [Fact]
        public void CalculatePrice_MultiItemInBasket_ReturnsTheSumForItemprice()
        {
            PriceCalculator pc = CreatePriceCalculator();
            var res = pc.CalculatePrice(CreateBasketMultiItems(1));
            Assert.Equal(40.50M , res);
        }

        private static Basket CreateBasketSingelItem(int quantity)
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M, Quantity = quantity } });
        }
        private static Basket CreateBasketMultiItems(int quantity)
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M, Quantity = quantity }, new Item() { Id = 1, Price = 30.0M, Quantity = quantity } });
        }
        private Basket CreateBasketMultiItemsWithDiscountflagga()
        {
            return new Basket(new List<Item>() { new Item() { Id = 1, Price = 10.50M, CampainCode = new CampainCode {Code = 3 } }, new Item() { Id = 2, Price = 10.50M, CampainCode = new CampainCode { Code = 3 } }, new Item() { Id = 3, Price = 30.0M, CampainCode = new CampainCode { Code = 3 } }, new Item() { Id = 4, Price = 10.0M, CampainCode = new CampainCode { Code = 0 } } });
        }
        private static PriceCalculator CreatePriceCalculator()
        {
            return new PriceCalculator();
        }

        

    }
}
