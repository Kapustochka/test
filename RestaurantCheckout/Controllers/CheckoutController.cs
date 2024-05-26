using Microsoft.AspNetCore.Mvc;
using RestaurantCheckout.Models;
using RestaurantCheckout.Services;

namespace RestaurantCheckout.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("addOrder")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            _checkoutService.AddOrder(order);
            return Ok(new { message = "Order added successfully." });
        }

        [HttpPost("removeOrder")]
        public IActionResult RemoveOrder([FromBody] Order order)
        {
            _checkoutService.RemoveOrder(order);
            return Ok(new { message = "Order removed successfully." });
        }

        [HttpGet("finalBill")]
        public IActionResult GetFinalBill()
        {
            double total = _checkoutService.GetFinalTotal();
            return Ok(new { total });
        }
        
        [HttpGet("currentOrders")] // New endpoint to get current orders
        public IActionResult GetCurrentOrders()
        {
            List<Order> orders = _checkoutService.GetCurrentOrders();
            return Ok(orders);
        }
        
        [HttpDelete("clearOrders")] // New endpoint to get current orders
        public IActionResult ClearAllOrders()
        {
            _checkoutService.ClearOrders();
            return NoContent();
        }
    }
}