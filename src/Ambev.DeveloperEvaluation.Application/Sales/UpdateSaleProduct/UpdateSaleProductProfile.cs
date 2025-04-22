using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProduct;

public class UpdateSaleProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale operation
    /// </summary>
    public UpdateSaleProductProfile()
    {
        CreateMap<UpdateSaleProductCommand, SaleProduct>().ReverseMap();
        CreateMap<SaleProduct, UpdateSaleProductResult>();
    }
}
