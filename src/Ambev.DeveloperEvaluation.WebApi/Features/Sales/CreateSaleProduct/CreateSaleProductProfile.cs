using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleProduct
{

    public class CreateSaleProductProfile : Profile
    {
        public CreateSaleProductProfile()
        {
            // Mapeamento principal com customizações para nomes diferentes
            CreateMap<CreateSaleProductRequest, CreateSaleProductCommand>().ReverseMap();   

            // Mapeamento do resultado
            CreateMap<CreateSaleProductResult, CreateSaleProductResponse>();
        }
    }
}
