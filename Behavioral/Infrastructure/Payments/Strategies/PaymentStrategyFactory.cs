using Behavioral.Core.Enums;

namespace Behavioral.Infrastructure.Payments.Strategies;

public class PaymentStrategyFactory : IPaymentStrategyFactory
{
    public IPaymentStrategy GetStrategy(PaymentMethod paymentMethod)
    {
        IPaymentStrategy strategy;

        if (paymentMethod == PaymentMethod.PaymentSlip)
            strategy = new PaymentSlipStrategy();
        else
            strategy = new CreditCardStrategy();

        return strategy;
    }
}