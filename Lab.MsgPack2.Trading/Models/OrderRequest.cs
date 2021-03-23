namespace Lab.MsgPack2.Trading.Models
{
    public record OrderRequest(bool IsBuy, string Symbol, int Quantity, decimal Price);
}
