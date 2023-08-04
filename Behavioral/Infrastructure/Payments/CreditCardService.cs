using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments;

public class CreditCardService : IPaymentService
{
    public object Process(OrderInputModel model)
    {
        return "Transação aprovada";
    }
}