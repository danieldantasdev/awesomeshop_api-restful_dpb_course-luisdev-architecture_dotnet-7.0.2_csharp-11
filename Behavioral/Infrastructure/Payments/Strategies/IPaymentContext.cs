using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments.Strategies;

public interface IPaymentContext
{
    object Process(OrderInputModel model);
    IPaymentContext SetStrategy(IPaymentStrategy strategy);
}