using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Services;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;

using QuickFix;
using QuickFix.Fields;

namespace Base.Exchange.OrderAccumulator.WebApi.Service.Services
{
    public class FixAcceptor : MessageCracker, IApplication
    {
        private readonly IOrderSingleService _orderSingleService;

        public FixAcceptor(IOrderSingleService orderSingleService)
        {
            _orderSingleService = orderSingleService;  
        }   
 
        public void OnCreate(SessionID sessionID)
        => Console.WriteLine($"[OnCreate] SessionID: {sessionID}");

        public void OnLogon(SessionID sessionID)
            => Console.WriteLine($"[OnLogon] SessionID: {sessionID}");

        public void OnLogout(SessionID sessionID)
            => Console.WriteLine($"[OnLogout] SessionID: {sessionID}");

        public void ToAdmin(Message message, SessionID sessionID)
            =>  Console.WriteLine($"[ToAdmin] {message}");

        public void FromAdmin(Message message, SessionID sessionID)
            => Console.WriteLine($"[FromAdmin] {message}");

        public void ToApp(Message message, SessionID sessionID)
            => Console.WriteLine($"[ToApp] {message}");

        public async void FromApp(Message message, SessionID sessionId)
        {
            Console.WriteLine($"🚀 Received message from session {sessionId}: {message}");

            try
            {
                var request = new ExecutedNewOrderSingleRequest()
                {
                    Price = message.GetDecimal(Price.TAG),
                    Symbol = message.GetString(Symbol.TAG),
                    Quantity = message.GetInt(OrderQty.TAG)
                };

                var result = await _orderSingleService.ExecuteNewOrderSingleAsync(request, (OrderSideEnum)message.GetInt(Side.TAG));
                SendExecutionReport(request.Symbol, request.Quantity, sessionId, result, message.GetInt(Side.TAG));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to execute order: ", ex);
            }     
        }

        private void SendExecutionReport(string symbol, decimal qty, SessionID sessionID, bool hasSuccess, int sideId)
        {
            var execType = hasSuccess ? ExecType.NEW : ExecType.REJECTED;

            var executionReport = new QuickFix.FIX44.ExecutionReport(
                new OrderID(Guid.NewGuid().ToString()),
                new ExecID(Guid.NewGuid().ToString()),
                new ExecType(execType),
                new OrdStatus(execType),
                new Symbol(symbol),
                new Side((char)sideId),
                new LeavesQty(qty),
                new CumQty(0),
                new AvgPx(0)                
            );                        
            
            Session.SendToTarget(executionReport, sessionID);
        }
    }
}
