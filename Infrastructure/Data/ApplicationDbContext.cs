using Domain.Entities;
using Domain.Interfaces.Entities;
using Infrastructure.Data.OnConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurations = typeof(IEntityTypeConfiguration<>).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(configurations);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=CleanApi;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");
        }
    }
}