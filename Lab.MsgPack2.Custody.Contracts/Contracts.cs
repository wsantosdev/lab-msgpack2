using MessagePack;

namespace Lab.MsgPack2.Custody.Contracts
{
    [MessagePackObject(true)]
    public record AddStockRequest(string Symbol, int Quantity);
    [MessagePackObject(true)]
    public record RemoveStockRequest(string Symbol, int Quantity);

    [MessagePackObject(true)]
    public record CustodyStockResponse(string Symbol, int Quantity);
}
