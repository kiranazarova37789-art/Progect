using Microsoft.EntityFrameworkCore;
using Проект;

namespace Folivora.Scaffold;

public class CartrigDbContext : DbContext
{
    private readonly IConfiguration _conf;

    public CartrigDbContext()
    {
    }

    public CartrigDbContext(IConfiguration conf)
    {
        _conf = conf;
    }

    public DbSet<Office> Offices { get; set; }

    public DbSet<Cartridg> Cartridgs { get; set; }
    public DbSet<Zaiavka> Zaiavkas { get; set; }
    public DbSet<Printer> Printers { get; set; }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_conf["DBConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Office>()
            .ToTable("office", "public").HasKey("id_off");

        modelBuilder.Entity<Cartridg>()
            .ToTable("cartridg", "public").HasKey("id_cr");

        modelBuilder.Entity<Zaiavka>()
          .ToTable("zaiavka", "public").HasKey("id_zv");

        modelBuilder.Entity<Printer>()
            .ToTable("printer", "public").HasKey("id_print");
    }
}