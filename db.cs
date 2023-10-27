using Microsoft.EntityFrameworkCore;
using SmileBank.Interfaces;

public class db : DbContext
{
    public DbSet<IExtract> Extract { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("SmilesDB");
    }
}