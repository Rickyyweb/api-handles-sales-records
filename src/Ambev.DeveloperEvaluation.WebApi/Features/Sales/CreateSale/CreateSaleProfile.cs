using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{

    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            // Mapeamento principal com customizações para nomes diferentes
            CreateMap<CreateSaleRequest, CreateSaleCommand>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.ClientName));

            // Mapeamento do resultado
            CreateMap<CreateSaleResult, CreateSaleResponse>();
            CreateMap<CreateSaleProductRequest, CreateSaleProductCommand>().ReverseMap();
        }
    }
}
