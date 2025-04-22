using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Contexts;

public class DefaultContext : DbContext, IEventPublisher
{
    private readonly List<IDomainEvent> _domainEvents = new();
    private readonly ILogger<DefaultContext> _logger;


    public DbSet<Sale> Sales { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SaleProduct> SaleProducts { get; set; }
    public DbSet<User> Users { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options, ILogger<DefaultContext> logger) : base(options)
    {
        _logger = logger;
        //Database.EnsureCreated();
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("DefaultConnection",
    //            b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM"));
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new SaleProductConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Sale && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var sale = (Sale)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                _domainEvents.Add(new SaleCreatedEvent(sale.Id, DateTime.UtcNow));
            }
            else if (entry.State == EntityState.Modified)
            {
                if (sale.Status == Status.Inactive)
                {
                    _domainEvents.Add(new SaleCancelledEvent(sale.Id, DateTime.UtcNow));
                }
                else
                {
                    _domainEvents.Add(new SaleModifiedEvent(sale.Id, DateTime.UtcNow));
                }
            }
        }

        // Detect item cancellations
        var modifiedItems = ChangeTracker.Entries<SaleProduct>()
            .Where(e => e.State == EntityState.Modified &&
                        e.OriginalValues[nameof(SaleProduct.Status)].ToString() !=
                        e.CurrentValues[nameof(SaleProduct.Status)].ToString());

        foreach (var entry in modifiedItems)
        {
            var item = entry.Entity;

            if (item.Status == Status.Inactive)
            {
                var saleId = entry.Property("SaleId").CurrentValue as Guid? ?? Guid.Empty;
                var productId = entry.Property("ProductId").CurrentValue as Guid? ?? Guid.Empty;

                _domainEvents.Add(new SaleProductCancelledEvent(
                    saleId,
                    productId,
                    DateTime.UtcNow
                ));
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<IDomainEvent>> GetPendingEvents()
    {
        var events = _domainEvents.ToList();
        _domainEvents.Clear();
        return events;
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IDomainEvent
    {
        //Console.WriteLine($"[Event Published] {@event.GetType().Name} - {@event}");
        //return Task.CompletedTask;

        _logger?.LogInformation("Event published: {EventType} - {Data}", typeof(TEvent).Name, @event);
        return Task.CompletedTask;
    }
}
