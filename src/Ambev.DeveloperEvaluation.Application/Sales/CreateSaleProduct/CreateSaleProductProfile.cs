using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct
{
    public class CreateSaleProductProfile : Profile
    {
        public CreateSaleProductProfile()
        {
            CreateMap<SaleProduct, CreateSaleProductResult>()
                .ForMember(x => x.Id, opt => opt.MapFrom( a => a.SaleId))
                .ReverseMap();
        }
    }
}
