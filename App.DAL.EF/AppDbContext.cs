using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Feedback> Feedbacks { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Pc>? Pcs { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<ProductInOrder> ProductInOrders { get; set; } = default!;
    public DbSet<ProductInPc> ProductInPcs { get; set; } = default!;
    
    public DbSet<ProductType> ProductTypes { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;
    public DbSet<ServiceType> ServiceTypes { get; set; } = default!;
    
    public DbSet<ShippingInfo> ShippingInfos { get; set; } = default!;
    public DbSet<ShippingType> ShippingTypes { get; set; } = default!;
    public DbSet<Specification> Specifications { get; set; } = default!;
    public DbSet<SpecificationType> SpecificationTypes { get; set; } = default!;

    public DbSet<PaymentType> PaymentTypes { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Remove cascade delete
        foreach (var relationShip in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationShip.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }


    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }


    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }

}