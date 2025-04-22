using Ambev.DeveloperEvaluation.Application.Sales.GetProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetProduct;
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Guid, GetProductCommand>()
           .ConstructUsing(id => new GetProductCommand(id));

        CreateMap<GetProductResult, GetProductResponse>();

    }
}
