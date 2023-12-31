using Behavioral.Application.Models;
using Behavioral.Infrastructure.Payments;

namespace Behavioral.Application.ChainOfResponsibility;

public class CheckForFraudHandler : OrderHandlerBase, IOrderHandler
{
    private readonly IPaymentFraudCheckService _paymentFraudCheckService;

    public CheckForFraudHandler(IPaymentFraudCheckService paymentFraudCheckService)
    {
        _paymentFraudCheckService = paymentFraudCheckService;
    }

    public override bool Handle(OrderInputModel model)
    {
        Console.WriteLine($"Invoking CheckForFraudHandler.Handle");

        var isFraud = _paymentFraudCheckService.IsFraud(model);

        if (isFraud)
            return false;

        return base.Handle(model);
    }
}