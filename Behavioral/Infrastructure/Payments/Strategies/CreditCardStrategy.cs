using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments.Strategies;

public class CreditCardStrategy : IPaymentStrategy
{
    public object Process(OrderInputModel model)
    {
        return "Transação aprovada.";
    }
}