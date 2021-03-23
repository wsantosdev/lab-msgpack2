using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.MsgPack2.Account.Hosting
{
    public class AccountHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Models.Account.Deposit(100_000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
