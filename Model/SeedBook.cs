using Microsoft.EntityFrameworkCore;

namespace RestWithASPNET.Model
{
    public static class SeedBook
    {
        public static void Seedbook (this ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Amor de Predição",
                    Author = "Camilo Castelo Branco",
                    Price = 59,
                    LaunchDate = new DateTime(1990,08,01)
                },
                new Book
                {
                    Id = 2,
                    Title = "Lusiadas",
                    Author = "Luis de Camoes",
                    Price = 24,
                    LaunchDate = new DateTime(1955,10,11)
                }
                );
        }
    }
}
