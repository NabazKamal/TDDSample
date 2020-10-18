using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDDfirstLib
{
    public class PriceCalculator
    {
        public decimal CalculatePrice(Basket basket)
        {
            if (basket == null)
                NullBasket();
            if (basket.Items != null)
                return this.GetCustomerPrice(basket);
            return decimal.Zero;
        }

        private static void NullBasket()
        {
            throw new ArgumentNullException("Basket", "Varukorgen ska inte vara tom");
        }

        private decimal GetCustomerPrice(Basket basket)
        {
            ApplyDiscounts(basket);
            return CustomerPrice(basket);
        }

        private decimal CustomerPrice(Basket basket)
        {
            basket.CustomerPrice = basket.Items.Sum(x => x.Price);
            return basket.CustomerPrice;
        }

        private void ApplyDiscounts(Basket basket)
        {
            ApplyTwoOfSameItem(basket);
            ApplyThreeItemsDiscount(basket);
        }

        private static void ApplyTwoOfSameItem(Basket basket)
        {
            foreach (var i in basket.Items)
                if (i.Quantity > 1)
                    i.Price = i.Price * i.Quantity * 0.8M;
        }

        private static void ApplyThreeItemsDiscount(Basket basket)
        {
            var items = basket.Items.Where(x => x.CampainCode != null && x.CampainCode.Code == 3).ToList();

            if (items.Count > 2)
            {
                foreach (var i in items)
                    i.Price = i.Price * 0.8M;
            }
        }
    }
}
