using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class TravellerContext : DbContext
    {
        public TravellerContext(DbContextOptions<TravellerContext> options)
            : base(options)
        {
            // Conf
        }

        public DbSet<Traveller> Travellers { get; set; }
    }
}