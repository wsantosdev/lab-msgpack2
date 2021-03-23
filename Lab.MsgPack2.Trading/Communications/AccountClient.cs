using CSharpFunctionalExtensions;
using Lab.MsgPack2.Account.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lab.MsgPack2.Trading.Communications
{
    public class AccountClient
    {
        private readonly HttpClient _httpClient;
        private readonly MessagePackProxy _messagePackProxy;

        public AccountClient(HttpClient httpClient, MessagePackProxy messagePackProxy) =>
            (_httpClient, _messagePackProxy) = (httpClient, messagePackProxy);

        public async Task<BalanceResponse> GetBalanceAsync() =>
            await _messagePackProxy.GetAsync<BalanceResponse>(_httpClient, "/api/account");

        public async Task<Result<ServerSuccess, ServerError>> WithdrawAsync(decimal amount) =>
            await _messagePackProxy.PostAsync(_httpClient, "/api/account/withdraw", new WithdrawRequest(amount));

        public async Task<Result<ServerSuccess, ServerError>> DepositAsync(decimal amount) =>
            await _messagePackProxy.PostAsync(_httpClient, "/api/account/deposit", new DepositRequest(amount));
    }
}
