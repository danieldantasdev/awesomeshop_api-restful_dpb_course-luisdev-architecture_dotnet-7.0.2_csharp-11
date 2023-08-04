﻿using Behavioral.Application.Models;

namespace Behavioral.Infrastructure.Payments.Strategies;

public class PaymentContext : IPaymentContext
{
    private IPaymentStrategy _strategy;

    public object Process(OrderInputModel model)
    {
        var result = _strategy.Process(model);

        return result;
    }

    public IPaymentContext SetStrategy(IPaymentStrategy strategy)
    {
        _strategy = strategy;

        return this;
    }
}