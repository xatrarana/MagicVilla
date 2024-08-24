using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Royal Villa Details",
                    ImageUrl = "https://image.com/v21/nice.png",
                    Occupancy = 5,
                    Rate = 5,
                    Sqft = 550,
                    Amenity = "",
                    CreatedAt = DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "New Villa",
                    Details = "New Villa Details",
                    ImageUrl = "https://image.com/v21/nice.png",
                    Occupancy = 45,
                    Rate = 500,
                    Sqft = 550,
                    Amenity = "",
                    CreatedAt = DateTime.Now,
                }
                );
      
        }
    }
}
