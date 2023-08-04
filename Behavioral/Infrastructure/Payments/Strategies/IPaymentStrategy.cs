using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments.Strategies;

public interface IPaymentStrategy
{
    object Process(OrderInputModel model);
}