using Microsoft.EntityFrameworkCore;
using BulkyWeb.Models;

namespace BulkyWeb.Data
{
    // DB schema
    // To create a DB schema we neet to get methord from Microsoft.EntityFrameworkCore  
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option) : base(option){
        }
        public DbSet<Category> Categories{ set; get; }

        // this is default function is use to over ride the default model using modelBuiler
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scific", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
            );   
        }

    }
}
 