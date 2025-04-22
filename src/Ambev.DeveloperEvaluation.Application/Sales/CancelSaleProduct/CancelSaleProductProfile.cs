using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleProduct;

public class CancelSaleProductProfile : Profile
{
    public CancelSaleProductProfile()
    {
         CreateMap<(Guid saleId, Guid productId), CancelSaleProductCommand>()
            .ConstructUsing(src => new CancelSaleProductCommand(src.saleId, src.productId));
    }
}
