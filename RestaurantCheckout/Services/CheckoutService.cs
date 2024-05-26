using RestaurantCheckout.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantCheckout.Services
{
    public interface ICheckoutService
    {
        double GetFinalTotal();
        void AddOrder(Order order);
        void RemoveOrder(Order order);
        void ClearOrders();

        List<Order> GetCurrentOrders();
    }

    public class CheckoutService : ICheckoutService
    {
        private readonly List<Order> _orders = new List<Order>();

        private const double StarterCost = 4.00;
        private const double MainCost = 7.00;
        private const double DrinkCost = 2.50;
        private const double ServiceChargeRate = 0.10;
        private const double DrinkDiscountRate = 0.30;
        private readonly TimeSpan _discountEndTime = new TimeSpan(19, 0, 0);

        public double GetFinalTotal()
        {
            
            var totalDrinks = _orders.Sum(orderItem => orderItem.Drinks);
            var discountedDrinksCost = totalDrinks == 0 ? 0 : 
                _orders.Where(orderItem => orderItem.OrderTime < _discountEndTime).Sum(orderItem =>
                    orderItem.Drinks) * (1 - DrinkDiscountRate) * DrinkCost 
                + _orders.Where(orderItem => orderItem.OrderTime >= _discountEndTime).Sum(orderItem =>
                    orderItem.Drinks) * DrinkCost;
            

            var foodCost = _orders.Sum(orderItem => orderItem.Starters) * StarterCost +
                              _orders.Sum(orderItem => orderItem.Mains) * MainCost;
            
            var total = foodCost + discountedDrinksCost + foodCost * ServiceChargeRate;
            return total;
        }
        
        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            order.Drinks = order.Drinks * -1;
            order.Mains = order.Mains * -1;
            order.Starters = order.Starters * -1;
            _orders.Add(order);
        }
        
        public void ClearOrders()
        {
            _orders.Clear();
        }
        
        public List<Order> GetCurrentOrders()
        {
            return _orders;
        }
    }
}