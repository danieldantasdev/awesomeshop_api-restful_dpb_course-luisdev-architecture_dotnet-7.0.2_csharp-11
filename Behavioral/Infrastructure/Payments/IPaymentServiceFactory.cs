using Behavioral.Core.Enums;

namespace Behavioral.Infrastructure.Payments;

public interface IPaymentServiceFactory
{
    IPaymentService GetService(PaymentMethod paymentMethod);
}