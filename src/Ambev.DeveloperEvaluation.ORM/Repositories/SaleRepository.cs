using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(Contexts.DefaultContext context) : base(context)
        {
        }

        public async Task CancelAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(saleId, cancellationToken);
            if (sale == null) throw new KeyNotFoundException("Sale not found");

            sale.Cancel();
            await UpdateAsync(sale, cancellationToken);
        }
    }
}
