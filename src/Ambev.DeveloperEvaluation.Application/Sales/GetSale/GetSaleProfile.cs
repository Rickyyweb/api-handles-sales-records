using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResult>()
               .ForMember(dest => dest.Client, opt => opt.MapFrom(src => new SaleClientResult
               {
                   Id = src.CustomerId,
                   Name = src.CustomerName
               }))
               .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => new SaleBranchResult
               {
                   Id = src.BranchId,
                   Name = src.BranchName
               }))
               .ForMember(dest => dest.SaleProducts, opt => opt.MapFrom(src => src.SaleProducts.Select(item => new GetSaleProductResult
               {
                   ProductId = item.Id,
                   ProductName = item.Product.ProductName,
                   Quantity = item.Quantity,
                   Discount = item.Discount,
                   UnitPrice = item.Product.UnitPrice,
                   FinalPrice = item.FinalPrice,
                   TotalAmount = item.Quantity * item.FinalPrice
               }).ToList()))
               .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));
    }
}
