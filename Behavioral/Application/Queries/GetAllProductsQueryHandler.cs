using Behavioral.Application.Models;

namespace Behavioral.Application.Queries;

public class GetAllProductsQueryHandler
{
    public Task<ProductViewModel> Handle(GetAllProductsQuery query)
    {
        return Task.FromResult(new ProductViewModel());
    }
}