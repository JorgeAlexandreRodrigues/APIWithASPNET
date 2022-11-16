using Microsoft.EntityFrameworkCore;

namespace RestWithASPNET.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Seedbook();
        }

    }
}
