using Microsoft.EntityFrameworkCore;

namespace RestWithASPNET.Model
{
    public static class SeedUser
    {
        public static void Seeduser (this ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                   Id= 1,
                   UserName = "Test",
                   FullName = "Test",
                   Password = "24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9",
                   RefreshToken = null,
                   RefreshTokenExpiryTime = default
                }
                );
        }
    }
}
