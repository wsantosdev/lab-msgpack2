using CSharpFunctionalExtensions;
using Lab.MsgPack2.Custody.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lab.MsgPack2.Trading.Communications
{
    public class CustodyClient
    {
        private readonly HttpClient _httpClient;
        private readonly MessagePackProxy _messagePackProxy;

        public CustodyClient(HttpClient httpClient, MessagePackProxy messagePackProxy) =>
            (_httpClient, _messagePackProxy) = (httpClient, messagePackProxy);

        public async Task<IEnumerable<CustodyStockResponse>> GetAsync() =>
            await _messagePackProxy.GetAsync<IEnumerable<CustodyStockResponse>>(_httpClient, "/api/custody");

        public async Task<Result<ServerSuccess, ServerError>> AddAsync(string symbol, int quantity) =>
            await _messagePackProxy.PostAsync(_httpClient, "/api/custody/add", new AddStockRequest(symbol, quantity));

        public async Task<Result<ServerSuccess, ServerError>> RemoveAsync(string symbol, int quantity) =>
            await _messagePackProxy.PostAsync(_httpClient, "/api/custody/remove", new RemoveStockRequest(symbol, quantity));
    }
}
