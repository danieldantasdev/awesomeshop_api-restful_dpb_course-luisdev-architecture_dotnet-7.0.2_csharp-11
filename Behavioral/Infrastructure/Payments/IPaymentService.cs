using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments;

public interface IPaymentService
{
    object Process(OrderInputModel model);
}