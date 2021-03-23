using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Lab.MsgPack2.AspNetExtensions
{
    public class MessagePackOutputFormatterLogger : IOutputFormatter
    {
        private const string ContentType = "application/x-msgpack";
        private readonly MessagePackOutputFormatter _messagePackOutputFormatter;

        public MessagePackOutputFormatterLogger(MessagePackOutputFormatter messagePackOutputFormatter) =>
            _messagePackOutputFormatter = messagePackOutputFormatter;

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (!context.ContentType.HasValue)
            {
                context.ContentType = new StringSegment(ContentType);
                return true;
            }

            return context.ContentType.Value == ContentType;
        }

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            Console.WriteLine($"MessagePack Output Formatter Called on {context.HttpContext.Request.Path}");

            await _messagePackOutputFormatter.WriteAsync(context);
        }
    }
}
