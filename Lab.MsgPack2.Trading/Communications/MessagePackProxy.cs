using CSharpFunctionalExtensions;
using MessagePack;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lab.MsgPack2.Trading.Communications
{
    public class MessagePackProxy
    {
        private const string ContentType = "application/x-msgpack";

        public async Task<T> GetAsync<T>(HttpClient httpClient, string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await httpClient.SendAsync(request);
            return MessagePackSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<Result<ServerSuccess, ServerError>> PostAsync<T>(HttpClient httpClient, string path, T payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new ByteArrayContent(MessagePackSerializer.Serialize(payload));
            request.Content.Headers.TryAddWithoutValidation(HeaderNames.ContentType, ContentType);

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                return Result.Success<ServerSuccess, ServerError>(new ServerSuccess());

            return Result.Failure<ServerSuccess, ServerError>(new ServerError((int)response.StatusCode,
                                                              await response.Content.ReadAsStringAsync()));
        }
    }
}
