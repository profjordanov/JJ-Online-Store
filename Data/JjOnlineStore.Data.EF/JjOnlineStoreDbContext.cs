using JjOnlineStore.Data.Entities;
using JjOnlineStore.Data.Entities.Base;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace JjOnlineStore.Data.EF
{
	public class JjOnlineStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public JjOnlineStoreDbContext(DbContextOptions<JjOnlineStoreDbContext> options)
			: base(options)
		{
		}

		private IDbContextTransaction currentTransaction;

	    public DbSet<Category> Categories { get; set; }

	    public DbSet<Product> Products { get; set; }

	    public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

	    public DbSet<OrderItem> OrderItems { get; set; }

	    public DbSet<Invoice> Invoices { get; set; }

	    public DbSet<UserViewedItems> UserViewedItems { get; set; }

        public virtual void BeginTransaction()
		{
			if(currentTransaction != null)
			{
				return;
			}

			currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public virtual async Task CommitTransactionAsync()
		{
			try
			{
				await SaveChangesAsync();

				currentTransaction?.Commit();
			}
			catch(Exception ex)
			{			    
				RollbackTransaction();
                Console.WriteLine($"Transaction Rollback because of {ex} / {ex.Message}!");
            }
			finally
			{
			    DisposeTransaction();
			}
		}

		public virtual void RollbackTransaction()
		{
			try
			{
				currentTransaction?.Rollback();
			}
			finally
			{
			    DisposeTransaction();
			}
		}

	    private void DisposeTransaction()
	    {
	        if (currentTransaction == null)
	        {
                return;
	        }
	        currentTransaction.Dispose();
	        currentTransaction = null;
	    }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			ApplyAuditInfoRules();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
			SaveChangesAsync(true , cancellationToken);

		public override Task<int> SaveChangesAsync(
			bool acceptAllChangesOnSuccess ,
			CancellationToken cancellationToken = default)
		{
			ApplyAuditInfoRules();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess , cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			ConfigureUserIdentityRelations(builder);
		    ConfigureProductCategoryRelations(builder);
		    ConfigureCartItemRelations(builder);
		    ConfigureCartUserRelations(builder);
		    ConfigureOrderRelations(builder);
		    ConfigureOrderItemRelations(builder);
		    ConfigureInvoiceOrderRelations(builder);
		    ConfigureUserViewedItemsRelations(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
		    // IDeletableEntity.IsDeleted index
            var deletableEntityTypes = entityTypes
		        .Where(et => et.ClrType != null && 
		                     typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
		    foreach (var deletableEntityType in deletableEntityTypes)
		    {
		        var method = SetIsDeletedQueryFilterMethod
		            .MakeGenericMethod(deletableEntityType.ClrType);
		        method.Invoke(null, new object[] { builder });
		    }

		    // Disable cascade delete
		    var foreignKeys = entityTypes
		        .SelectMany(e => e.GetForeignKeys()
		            .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
		    foreach (var foreignKey in foreignKeys)
		    {
		        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
		    }
        }

	    private static void ConfigureUserViewedItemsRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<UserViewedItems>()
	            .HasOne(uvi => uvi.User)
	            .WithMany(u => u.ViewedItems)
	            .HasForeignKey(uvi => uvi.UserId);

	        builder
	            .Entity<UserViewedItems>()
	            .HasOne(uvi => uvi.Product)
	            .WithMany(p => p.UserViewed)
	            .HasForeignKey(uvi => uvi.ProductId);
	    }


        private static void ConfigureInvoiceOrderRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<Invoice>()
	            .HasOne(i => i.Order)
	            .WithOne(o => o.Invoice)
	            .HasForeignKey<Order>(o => o.InvoiceId);

	        builder
	            .Entity<Order>()
	            .HasOne(o => o.Invoice)
	            .WithOne(i => i.Order)
	            .HasForeignKey<Invoice>(i => i.OrderId);
	    }

        private static void ConfigureOrderItemRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<OrderItem>()
	            .HasOne(oi => oi.Product)
	            .WithMany(p => p.OrderItems)
	            .HasForeignKey(oi => oi.ProductId);

	        builder
	            .Entity<OrderItem>()
	            .HasOne(oi => oi.Order)
	            .WithMany(o => o.OrderedItems)
	            .HasForeignKey(oi => oi.OrderId);
        }


        private static void ConfigureOrderRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<Order>()
	            .HasOne(o => o.User)
	            .WithMany(u => u.Orders)
	            .HasForeignKey(o => o.UserId);
	    }

        private static void ConfigureCartItemRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<CartItem>()
	            .HasOne(ci => ci.Product)
	            .WithMany(p => p.CartItems)
	            .HasForeignKey(ci => ci.ProductId);

	        builder
	            .Entity<CartItem>()
	            .HasOne(ci => ci.Cart)
	            .WithMany(c => c.OrderedItems)
	            .HasForeignKey(ci => ci.CartId);
	    }

	    private static void ConfigureCartUserRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<Cart>()
	            .HasOne(c => c.User)
	            .WithOne(u => u.Cart)
	            .HasForeignKey<ApplicationUser>(u => u.CartId);

	        builder
	            .Entity<ApplicationUser>()
	            .HasOne(u => u.Cart)
	            .WithOne(c => c.User)
	            .HasForeignKey<Cart>(c => c.UserId);
	    }

        private static void ConfigureProductCategoryRelations(ModelBuilder builder)
	    {
	        builder
	            .Entity<Product>()
	            .HasOne(a => a.Category)
	            .WithMany(a => a.Products)
	            .HasForeignKey(a => a.CategoryId);
        }


        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
		{
			builder.Entity<ApplicationUser>()
				.HasMany(e => e.Claims)
				.WithOne()
				.HasForeignKey(e => e.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ApplicationUser>()
				.HasMany(e => e.Logins)
				.WithOne()
				.HasForeignKey(e => e.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ApplicationUser>()
				.HasMany(e => e.Roles)
				.WithOne()
				.HasForeignKey(e => e.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		}

	    private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
	        typeof(JjOnlineStoreDbContext).GetMethod(
	            nameof(SetIsDeletedQueryFilter),
	            BindingFlags.NonPublic | BindingFlags.Static);

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
			where T : class, IDeletableEntity
		{
			builder
			    .Entity<T>()
			    .HasQueryFilter(e => !e.IsDeleted);
		}

		private void ApplyAuditInfoRules()
		{
			var changedEntries = this.ChangeTracker
				.Entries()
				.Where(e =>
					e.Entity is IAuditInfo &&
					(e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach(var entry in changedEntries)
			{
				var entity = (IAuditInfo)entry.Entity;
				if(entry.State == EntityState.Added && entity.CreatedOn == default)
				{
					entity.CreatedOn = DateTime.UtcNow;
				}
				else
				{
					entity.ModifiedOn = DateTime.UtcNow;
				}
			}
		}
	}
}