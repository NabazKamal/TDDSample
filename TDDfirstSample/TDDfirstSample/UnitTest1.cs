using System;
using System.Dynamic;
using Xunit;

namespace TDDfirstSample
{
    //En priscalc som räknar ut kundpris för en varukorg

    //Systemundertest_scenario_expectedbehavior()
    public class UnitTest1
    {
        //we have a basket with items in it
        //items have prices
        //items can have discounts
        //three of some items can give 20% discount
        //two of same items give 15% discount
        //empty basket will cost 0kr
        [Fact]
        public void CalculatePrice_EmptyBasket_ReturnsDefault()
        {
            PriceCalculator pc = new PriceCalculator();
            decimal price = pc.CalculatePrice(new Basket());
            Assert.Equal(0, price);

        }
        [Fact]
        public void CalculatePrice_NullBasket_ReturnsException()
        {
            PriceCalculator pc = new PriceCalculator();
            //decimal price = pc.CalculatePrice(null);
            var res = Assert.Throws<ArgumentNullException>(()=> pc.CalculatePrice(null));
            Assert.Equal("Value cannot be null.", res.Message);

        }

        private class PriceCalculator
        {
            internal decimal CalculatePrice(Basket basket)
            {
                if (basket == null)
                    throw new ArgumentNullException();
                return decimal.Zero;
            }
        }

        private class Basket
        {
            public Basket()
            {
            }
        }
    }
}
