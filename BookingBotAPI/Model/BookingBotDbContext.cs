using Microsoft.EntityFrameworkCore;

namespace BookingBotAPI.Data
{
    public class BookingBotDbContext : DbContext
    {
        public BookingBotDbContext(DbContextOptions<BookingBotDbContext> options) : base(options) { }

        public DbSet<Master> Masters { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
