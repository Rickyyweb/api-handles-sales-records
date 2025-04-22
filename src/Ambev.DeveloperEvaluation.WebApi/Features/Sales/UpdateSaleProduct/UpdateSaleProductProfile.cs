using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleProduct;

public class UpdateSaleProductProfile : Profile
{
    public UpdateSaleProductProfile()
    {
        CreateMap<UpdateSaleProductRequest, UpdateSaleProductCommand>().ReverseMap();
    }
}
