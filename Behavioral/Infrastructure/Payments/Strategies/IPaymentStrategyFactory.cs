using Behavioral.Core.Enums;

namespace Behavioral.Infrastructure.Payments.Strategies;

public interface IPaymentStrategyFactory
{
    IPaymentStrategy GetStrategy(PaymentMethod paymentMethod);
}