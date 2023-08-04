using Behavioral.Application.Models;

namespace Behavioral.Application.ChainOfResponsibility;

public interface IOrderHandler
{
    bool Handle(OrderInputModel model);
    IOrderHandler SetNext(IOrderHandler handler);
}