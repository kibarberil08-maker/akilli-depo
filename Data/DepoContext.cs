using AkilliDepo.Models;
using Microsoft.EntityFrameworkCore;

namespace AkilliDepo.Data
{
    public class DepoContext : DbContext
    {
        public DepoContext(DbContextOptions<DepoContext> options)
            : base(options)
        {
        }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Hareket> Hareketler { get; set; }

        public DbSet<Komut> Komutlar { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }

    }
}