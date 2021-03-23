namespace Lab.MsgPack2.Trading.Communications
{
    public record ServerSuccess();
    public record ServerError(int StatusCode, string Message);
}
