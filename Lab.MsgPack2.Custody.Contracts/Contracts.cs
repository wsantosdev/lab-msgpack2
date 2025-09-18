using MessagePack;

namespace Lab.MsgPack2.Custody.Contracts
{
    [MessagePackObject]
    public class AddStockRequest 
    {
        public AddStockRequest(string symbol, int quantity)
        {
            Symbol = symbol;
            Quantity = quantity;
        }

        [Key(0)] public string Symbol { get; init; }
        [Key(1)] public int Quantity { get; init; }
    }

    [MessagePackObject]
    public class RemoveStockRequest
    {
        public RemoveStockRequest(string symbol, int quantity)
        {
            Symbol = symbol;
            Quantity = quantity;
        }

        [Key(0)] public string Symbol { get; init; }
        [Key(1)] public int Quantity { get; init; }
    }

    [MessagePackObject]
    public class CustodyStockResponse
    {
        public CustodyStockResponse(string symbol, int quantity)
        {
            Symbol = symbol;
            Quantity = quantity;
        }

        [Key(0)] public string Symbol { get; init; }
        [Key(1)] public int Quantity { get; init; }
    }
}
