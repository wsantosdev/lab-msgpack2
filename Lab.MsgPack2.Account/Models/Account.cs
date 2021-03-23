using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab.MsgPack2.Account.Models
{
    public static class Account
    {
        private static readonly IList<decimal> _entries = new List<decimal>();
        public static decimal Balance { get => _entries.Sum(); }

        public static void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("A positive amount must be provided.");

            _entries.Add(amount);
        }

        public static void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("A positive amount must be provided.");

            _entries.Add(amount * -1);
        }
    }
}
