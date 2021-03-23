using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Threading.Tasks;

namespace Lab.MsgPack2.AspNetExtensions
{
    public class MessagePackInputFormatterLogger : IInputFormatter
    {
        private const string ContentType = "application/x-msgpack";
        private readonly MessagePackInputFormatter _messagePackInputFormatter;

        public MessagePackInputFormatterLogger(MessagePackInputFormatter messagePackInputFormatter) =>
            _messagePackInputFormatter = messagePackInputFormatter;

        public bool CanRead(InputFormatterContext context) =>
            context.HttpContext.Request.ContentType == ContentType;

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            Console.WriteLine($"MessagePack Input Formatter Called on {context.HttpContext.Request.Path}.");

            return await _messagePackInputFormatter.ReadAsync(context);
        }
    }
}
