using Lab.MsgPack2.Custody.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lab.MsgPack2.Custody.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustodyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() =>
            Ok(Models.Custody.GetStocks());

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(AddStockRequest request)
        {
            Models.Custody.Add(request.Symbol, request.Quantity);
            return Ok();
        }

        [HttpPost]
        [Route("Remove")]
        public IActionResult Remove(RemoveStockRequest request)
        {
            Models.Custody.Remove(request.Symbol, request.Quantity);
            return Ok();
        }
    }
}
