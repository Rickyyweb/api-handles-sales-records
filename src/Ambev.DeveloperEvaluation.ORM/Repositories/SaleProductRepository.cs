using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Contexts;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleProductRepository : Repository<SaleProduct>, ISaleProductRepository
{
    public SaleProductRepository(DefaultContext context) : base(context)
    {
    }
}
