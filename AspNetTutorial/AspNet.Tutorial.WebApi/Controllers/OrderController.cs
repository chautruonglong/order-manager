using AspNet.Tutorial.Core.Interfaces.IServices;

using Microsoft.AspNetCore.Mvc;

namespace AspNet.Tutorial.WebApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
    }
}
