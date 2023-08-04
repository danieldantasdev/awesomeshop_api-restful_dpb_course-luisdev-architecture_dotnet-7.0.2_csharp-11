using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Deliveries;

public interface IDeliveryService
{
    void Deliver(OrderInputModel model);
}