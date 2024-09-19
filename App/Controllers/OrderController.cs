using App.DTOs;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(
            OrderService orderService
        )
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}/current")]
        public async Task<ActionResult<OrderDTO?>> GetCurrentUserOrder(int userId)
        {
            Order order = await _orderService.GetCurrentUserOrder(userId);

            return Ok(new OrderDTO(order));
        }

        [HttpPost("{id}/add-product")]
        public async Task<ActionResult> AddProductInOrder(int id, int productId)
        {
            await _orderService.AddProduct(id, productId);

            return NoContent();
        }

        [HttpPost("{id}/remove-product")]
        public async Task<ActionResult> RemoveProductInOrder(int id, int productId)
        {
            await _orderService.RemoveProduct(id, productId);

            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult> CompleteOrder(int id)
        {
            await _orderService.CompleteOrder(id);

            return NoContent();
        }
    }
}
