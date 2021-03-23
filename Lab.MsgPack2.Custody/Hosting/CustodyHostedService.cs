using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.MsgPack2.Custody.Hosting
{
    public class CustodyHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Models.Custody.Add("ITUB4", 100);
            Models.Custody.Add("PETR4", 200);
            Models.Custody.Add("VALE5", 300);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
