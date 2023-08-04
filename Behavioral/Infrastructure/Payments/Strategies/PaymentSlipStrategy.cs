using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments.Strategies;

public class PaymentSlipStrategy : IPaymentStrategy
{
    public object Process(OrderInputModel model)
    {
        return "Dados do Boleto: xyz";
    }
}