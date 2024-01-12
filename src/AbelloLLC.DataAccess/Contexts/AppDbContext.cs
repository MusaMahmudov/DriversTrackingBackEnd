using AbelloLLC.Core.Entities;
using AbelloLLC.Core.Entities.Common;
using AbelloLLC.Core.Entities.Identity;
using AbelloLLC.DataAccess.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AppDbContext(IHttpContextAccessor contextAccessor, DbContextOptions<AppDbContext> options) : base(options) 
        {
            _contextAccessor = contextAccessor;
         
        }
      public  DbSet<Driver> Drivers { get; set; } = null!;
        DbSet<VehicleType> VehicleTypes { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            string userName = "Admin2";
            var identity = _contextAccessor?.HttpContext?.User.Identity;
            if (identity is not null)
            {
                userName = identity.IsAuthenticated ? identity.Name : "Admin2";
            }

            var entities = ChangeTracker.Entries<BaseSectionEntity>();


            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                            entity.Entity.CreatedAt = DateTime.UtcNow;
                            entity.Entity.UpdatedAt = DateTime.UtcNow;
                            entity.Entity.CreatedBy = userName;
                            entity.Entity.UpdatedBy = userName;
                        break;
                        case EntityState.Modified:
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        entity.Entity.UpdatedBy = userName;
                        break;
                    

                }
            }



            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DriverConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

    }
}
