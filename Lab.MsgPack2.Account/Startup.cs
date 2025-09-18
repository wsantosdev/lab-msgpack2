using Lab.MsgPack2.Account.Filters;
using Lab.MsgPack2.Account.Hosting;
using Lab.MsgPack2.AspNetExtensions;
using MessagePack;
using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Lab.MsgPack2.Account
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<AccountHostedService>();

            services.AddControllers().AddMvcOptions(options =>
            {
                options.Filters.Add<ExceptionFilter>();

                options.InputFormatters.Add(new MessagePackInputFormatterLogger(
                    new MessagePackInputFormatter(StandardResolver.Options.WithSecurity(MessagePackSecurity.UntrustedData))
                ));

                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new MessagePackOutputFormatterLogger(
                    new MessagePackOutputFormatter(StandardResolver.Options)
                ));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
