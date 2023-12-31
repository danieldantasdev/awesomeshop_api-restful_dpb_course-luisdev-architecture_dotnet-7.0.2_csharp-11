﻿using Behavioral.Application.Mediator;

namespace Behavioral.Application.Queries;

public class GetProductByIdQuery : IQuery
{
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}