using MessagePack;

namespace Lab.MsgPack2.Account.Contracts
{

    [MessagePackObject(true)]
    public record DepositRequest(decimal Amount);
    [MessagePackObject(true)]
    public record WithdrawRequest(decimal Amount);

    [MessagePackObject(true)]
    public record BalanceResponse(decimal Amount);
}