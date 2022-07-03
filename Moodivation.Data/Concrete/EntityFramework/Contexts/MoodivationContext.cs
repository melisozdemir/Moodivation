using Microsoft.EntityFrameworkCore;
using Moodivation.Data.Concrete.EntityFramework.Mappings;
using Moodivation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Data.Concrete.EntityFramework.Contexts
{
    public class MoodivationContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> ProductCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //    @"Server=DESKTOP-8TQASI2\SQLEXPRESS;Database=Moodivation;Trusted_Connection=True;");
        }

        public MoodivationContext(DbContextOptions<MoodivationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
