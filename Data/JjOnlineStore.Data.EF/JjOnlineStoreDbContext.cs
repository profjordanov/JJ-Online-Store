using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Data.Entities.Base;

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
			catch(Exception)
			{
				RollbackTransaction();
			}
			finally
			{
				if(currentTransaction != null)
				{
					currentTransaction.Dispose();
					currentTransaction = null;
				}
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
				if(currentTransaction != null)
				{
					currentTransaction.Dispose();
					currentTransaction = null;
				}
			}
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