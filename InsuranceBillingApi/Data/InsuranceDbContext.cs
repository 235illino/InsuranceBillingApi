using InsuranceBillingApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InsuranceBillingApi.Data
{
    public class InsuranceDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<InsurancePolicy> Policies { get; set; }

        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
        }
    }
}
