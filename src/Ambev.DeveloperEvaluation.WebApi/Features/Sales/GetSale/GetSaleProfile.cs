using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<Guid, GetSaleCommand>()
           .ConstructUsing(id => new GetSaleCommand(id));

        CreateMap<GetSaleProductResult, GetSaleProductResponse>();

        CreateMap<GetSaleResult, GetSaleResponse>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.Id))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Id))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.SaleProducts.Select(item => new GetSaleProductResult
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Discount = item.Discount,
                UnitPrice = item.UnitPrice,
                FinalPrice = item.FinalPrice,
                TotalAmount = item.Quantity * item.FinalPrice
            }).ToList()))
               .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount)); 
    }
}
