using Lab.MsgPack2.Account.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lab.MsgPack2.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() =>
            Ok(new BalanceResponse(Models.Account.Balance));

        [HttpPost]
        [Route("Deposit")]
        public IActionResult Deposit(DepositRequest request)
        {
            Models.Account.Deposit(request.Amount);
            return Ok();
        }

        [HttpPost]
        [Route("Withdraw")]
        public IActionResult Withdraw(WithdrawRequest request)
        {
            Models.Account.Withdraw(request.Amount);
            return Ok();
        }
    }
}
