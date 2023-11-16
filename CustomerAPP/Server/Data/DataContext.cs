

using System.Security.Principal;

namespace CustomerAPP.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Antony", LastName = "Hoppikins", Email = "ahoppikins@gmail.com", CompanyId = 1 },
                new Customer { Id = 2, FirstName = "John", LastName = "Wayne", Email = "jwayne@gmail.com", CompanyId = 2 },
                new Customer { Id = 3, FirstName = "Marcus", LastName = "Sunny", Email = "msunnys@gmail.com", CompanyId = 3 },
                new Customer { Id = 4, FirstName = "Peter", LastName = "Austin", Email = "paustin@gmail.com", CompanyId = 2 },
                new Customer { Id = 5, FirstName = "Gregory", LastName = "Capilano", Email = "gcapilano@gmail.com", CompanyId = 3 }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "Microsoft" },
                new Company { CompanyId = 2, CompanyName = "Apple" },
                new Company { CompanyId = 3, CompanyName = "IBM" }
            );
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
