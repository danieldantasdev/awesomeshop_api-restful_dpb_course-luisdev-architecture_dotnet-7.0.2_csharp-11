using Behavioral.Core.Enums;
using Behavioral.Infrastructure.Deliveries;
using Behavioral.Infrastructure.Payments;

namespace Behavioral.Infrastructure;

public interface IOrderAbstractFactory
{
    IPaymentService GetPaymentService(PaymentMethod method);
    IDeliveryService GetDeliveryService();
}