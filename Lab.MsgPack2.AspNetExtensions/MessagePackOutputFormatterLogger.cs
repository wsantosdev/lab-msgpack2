using MessagePack;
using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using System;
using System.Text;
using System.Text.Json;
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
            var messagePack = MessagePackSerializer.Serialize(context.Object, MessagePackSerializerOptions.Standard);
            var messagePackAsString = Encoding.UTF8.GetString(messagePack);

            var json = JsonSerializer.Serialize(context.Object);

            Console.WriteLine($"MessagePack output on {context.HttpContext.Request.Path}: {messagePackAsString} - {messagePackAsString.Length} bytes");
            Console.WriteLine($"JSON output on {context.HttpContext.Request.Path}: {json} - {json.Length} bytes");

            await _messagePackOutputFormatter.WriteAsync(context);
        }
    }
}
