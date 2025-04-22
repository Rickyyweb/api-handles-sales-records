using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale operation
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignora o ID pois já está no comando
            .ForMember(dest => dest.Date, opt => opt.Ignore()) // Mantém a data original
            .ForMember(dest => dest.SaleProducts, opt => opt.Ignore()); // Mantém os itens originais

        CreateMap<Sale, UpdateSaleResult>();
    }
}
