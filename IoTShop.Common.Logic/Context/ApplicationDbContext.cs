using IoTShop.Common.Logic.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<OS> OS { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public ApplicationDbContext()
            : base("BestellingenConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                        .HasMany(r => r.OS)
                        .WithMany()
                        .Map(m =>
                        {
                            m.MapLeftKey("DeviceId");
                            m.MapRightKey("OSId");
                            m.ToTable("DevicesOS");
                        });

            modelBuilder.Entity<Device>()
                        .HasMany(r => r.Framework)
                        .WithMany()
                        .Map(m =>
                        {
                            m.MapLeftKey("DeviceId");
                            m.MapRightKey("FrameworkId");
                            m.ToTable("DevicesFramework");
                        });
        }
    }
}
