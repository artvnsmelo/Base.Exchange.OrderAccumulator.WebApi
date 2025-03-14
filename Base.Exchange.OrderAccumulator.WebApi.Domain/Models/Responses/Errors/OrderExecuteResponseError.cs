namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

public class OrderExecuteResponseError : ErrorResponseBase
{
    public OrderExecuteResponseError() : base("order_execute_error") { }
    public OrderExecuteResponseError(string code, string message) : base(code, message) { }
}
