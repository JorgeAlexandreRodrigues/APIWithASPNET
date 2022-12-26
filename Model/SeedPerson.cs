using Microsoft.EntityFrameworkCore;

namespace RestWithASPNET.Model
{
    public static class SeedPerson
    {
        public static void Seed (this ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Jorge",
                    LastName = "Rodrigues",
                    Address = "Braga",
                    Gender = "Male",
                    Enable= true,
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Sara",
                    LastName= "Rodrigues",
                    Address = "Famalicao",
                    Gender = "Female",
                    Enable = true,
                }
                );
        }
    }
}
