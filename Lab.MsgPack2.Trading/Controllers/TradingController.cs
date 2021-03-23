using CSharpFunctionalExtensions;
using Lab.MsgPack2.Trading.Communications;
using Lab.MsgPack2.Trading.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab.MsgPack2.Trading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingController : ControllerBase
    {
        private readonly AccountClient _accountClient;
        private readonly CustodyClient _custodyClient;

        public TradingController(AccountClient accountClient, CustodyClient custodyClient) =>
            (_accountClient, _custodyClient) = (accountClient, custodyClient);

        [HttpGet]
        [Route("Balance")]
        public async Task<IActionResult> GetBalance() =>
            Ok(await _accountClient.GetBalanceAsync());

        [HttpGet]
        [Route("Custody")]
        public async Task<IActionResult> GetCustody() =>
            Ok(await _custodyClient.GetAsync());

        [HttpPost]
        public async Task<IActionResult> SendOrder(OrderRequest request)
        {
            var result = request.IsBuy
                ? await BuyAsync(request.Symbol, request.Quantity, request.Price)
                : await SellAsync(request.Symbol, request.Quantity, request.Price);

            return result.IsSuccess
                    ? Ok()
                    : result.Error.StatusCode == StatusCodes.Status400BadRequest
                        ? BadRequest(result.Error.Message)
                        : StatusCode(StatusCodes.Status500InternalServerError, result.Error.Message);
        }

        private async Task<Result<ServerSuccess, ServerError>> BuyAsync(string symbol, int quantity, decimal price)
        {
            var result = await _accountClient.WithdrawAsync(quantity * price);
            if (result.IsSuccess)
            {
                result = await _custodyClient.AddAsync(symbol, quantity);
                if (!result.IsSuccess)
                    await _accountClient.DepositAsync(quantity * price);
            }

            return result;
        }

        private async Task<Result<ServerSuccess, ServerError>> SellAsync(string symbol, int quantity, decimal price)
        {
            var result = await _custodyClient.RemoveAsync(symbol, quantity);
            if (result.IsSuccess)
            {
                result = await _accountClient.DepositAsync(quantity * price);
                if (!result.IsSuccess)
                    await _custodyClient.AddAsync(symbol, quantity);
            }

            return result;
        }
    }
}
