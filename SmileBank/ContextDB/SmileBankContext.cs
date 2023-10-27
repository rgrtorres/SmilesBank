using Microsoft.EntityFrameworkCore;
using SmileBank.Interfaces;

namespace SmileBank.ContextDB
{
    public class SmileBankContext : DbContext
    {
        public DbSet<IExtract> Extract { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SmileDB");
        }
    }
}
