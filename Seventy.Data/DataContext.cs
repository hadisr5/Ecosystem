using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Seventy.Common.Utilities;
using Seventy.Data.Seeding;
using Seventy.DomainClass;
using Seventy.DomainClass.Core;

namespace Seventy.Data
{
    public class DataContext : DbContext
    {
        private readonly IOptions<PublicConfiguration> _AppSetting;
        private readonly bool _testMode = false;
        public DataContext() : base()
        {
        }
        public DataContext(IOptions<PublicConfiguration> options)
        {
            _AppSetting = options;
        }

        public DataContext(DbContextOptions<DataContext> options, bool testMode) : base(options)
        {
            _testMode = testMode;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(ICoreBase).Assembly;
            modelBuilder.RegisterAllEntities<ICoreBase>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddSequentialGuidForIdConvention();

            //modelBuilder.Seed();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_testMode)
                return;
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=185.97.118.69,1433;Initial Catalog=HAFTAD_CF7;User Id=sa;Password=1qazZAQ!@WSX;MultipleActiveResultSets=True");
            }

            //optionsBuilder
            //.UseSqlServer(_AppSetting.Value.ConnectionString, providerOptions => providerOptions.CommandTimeout(60));
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                {
                    continue;
                }

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                        {
                            continue;
                        }

                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

    }
}
