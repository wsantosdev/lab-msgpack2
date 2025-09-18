using MessagePack;

namespace Lab.MsgPack2.Account.Contracts
{

    [MessagePackObject]
    public class DepositRequest
    {
        public DepositRequest(decimal amount)
        {
            Amount = amount;
        }

        [Key(0)]
        public decimal Amount { get; init; }
    }

    [MessagePackObject]
    public class WithdrawRequest 
    {
        public WithdrawRequest(decimal amount)
        {
            Amount = amount;
        }

        [Key(0)]
        public decimal Amount { get; init; }
    }

    [MessagePackObject]
    public class BalanceResponse
    { 
        public BalanceResponse(decimal amount)
        {
            Amount = amount;
        }

        [Key(0)]
        public decimal Amount { get; init; }
    }
}