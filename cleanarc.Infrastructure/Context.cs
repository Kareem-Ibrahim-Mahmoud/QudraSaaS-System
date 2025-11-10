using QudraSaaS.Dmain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QudraSaaS.Infrastructure
{
    public class Context: IdentityDbContext<applicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //local Database
            optionsBuilder.UseSqlServer(@"Data source=.;Initial catalog=QudraSaaSDatabase;Integrated security=true;TrustServerCertificate=true;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Customer>()
            //    .HasOne(c => c.workShopId)
            //    .WithMany() // أو .WithMany(w => w.Customers) لو عندك ICollection
            //    .HasForeignKey(c => c.workShop)
            //    .OnDelete(DeleteBehavior.Restrict); // منع cascade delete

            // Customer → Workshop
 // يمنع cascade delete
            //------------------------------
// يمنع cascade delete

        }



        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<ServiceSession> ServiceSessions { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<OTPCode> oTPCodes { get; set; }
        public DbSet<ServiceType> serviceTypes { get; set; }



    }

}
