using System;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Bills;
using AspNet.Tutorial.Core.Interfaces.IServices;
using AspNet.Tutorial.WebApi.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Tutorial.WebApi.Controllers
{
    [ApiController]
    [Route("api/bills")]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token, [FromBody] BillCreationDto billCreationDto)
        {
            var userId = JwtUtil.GetGuid(token);
            await _billService.CreateBillOrder(userId, billCreationDto);
            return Ok("Order successfully");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMany([FromHeader(Name = "Authorization")] string token, [FromQuery] int page, [FromQuery] int size)
        {
            var userId = JwtUtil.GetGuid(token);
            return Ok(await _billService.GetBillOrders(userId, page, size));
        }
    }
}
