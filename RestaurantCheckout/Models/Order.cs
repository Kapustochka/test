namespace RestaurantCheckout.Models
{
    public class Order
    {
        public Order(int starters=0, int mains=0, int drinks=0, TimeSpan orderTime = default(TimeSpan))
        {
            Starters = starters;
            Mains = mains;
            Drinks = drinks;
            if (orderTime == default(TimeSpan))
            {
                orderTime = DateTime.Now.TimeOfDay;
            }
            OrderTime = orderTime;
        }
        public int Starters { get; set; }
        public int Mains { get; set; }
        public int Drinks { get; set; }
        public TimeSpan OrderTime { get; set; }
    }
}