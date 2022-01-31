using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrderMicroservice.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.DAL
{
    public class OrderMicroserviceContext : DbContext
    {
        public OrderMicroserviceContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Order> Orders { get; set; }

        #region DB Actions
        public int Save()
        {
            SetTimestamps();
            return SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            SetTimestamps();
            return SaveChangesAsync();
        }

        public void CreateOrUpdate(IEntity entity)
        {
            var entry = Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    Add(entity);
                    break;
                case EntityState.Modified:
                    Update(entity);
                    break;
                case EntityState.Added:
                    Add(entity);
                    break;
                case EntityState.Unchanged:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IDbContextTransaction Transaction()
        {
            return Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> TransactionAsync()
        {
            return Database.BeginTransactionAsync();
        }

        private void SetTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((IEntity)entity.Entity).CreatedAt = now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((IEntity)entity.Entity).UpdatedAt = now;
                }
            }
        }
        #endregion
    }
}
