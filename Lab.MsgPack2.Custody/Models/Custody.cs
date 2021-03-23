using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab.MsgPack2.Custody.Models
{
    public static class Custody
    {
        private static readonly IDictionary<string, int> _entries = new Dictionary<string, int>();

        public static IEnumerable<dynamic> GetStocks() =>
            _entries.Select(e => new { Symbol = e.Key, Quantity = e.Value }).ToArray();

        public static void Add(string symbol, int quantity)
        {
            if(string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("A symbol must be provided");

            if(quantity <= 0)
                throw new ArgumentException("A positive quantity must be provided");

            if (_entries.ContainsKey(symbol))
            {
                _entries[symbol] += quantity;
                return;
            }

            _entries.Add(symbol, quantity);
        }

        public static void Remove(string symbol, int quantity)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("A symbol must be provided");

            if (quantity <= 0)
                throw new ArgumentException("A positive quantity must be provided");

            if (!_entries.ContainsKey(symbol))
                throw new InvalidOperationException($"{symbol} is unavailable in the stock custody");

            if(quantity > _entries[symbol])
                throw new InvalidOperationException($"Invalid quantity to remove. The limit is {_entries[symbol]}");

            if (quantity == _entries[symbol])
            {
                _entries.Remove(symbol);
                return;
            }

            _entries[symbol] -= quantity;
        }
    }
}
