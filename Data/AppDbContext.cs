using Microsoft.EntityFrameworkCore;
using MvcMaxDebtProject.Models;

namespace MvcMaxDebtProject.Data
{
    // Entity Framework Core'un DbContext sınıfını genişleten bir sınıf
    
    public class AppDbContext : DbContext
    {
        // DbContext sınıfının yapılandırıcısı
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanındaki MusteriTanim tablosunu temsil eden DbSet
        public DbSet<MusteriTanim> MusteriTanim { get; set; }

        // Veritabanındaki MusteriFatura tablosunu temsil eden DbSet
        public DbSet<MusteriFatura> MusteriFatura { get; set; }

        // Model oluşturulurken tablo isimleri ve sütun ayarlarını yapılandırır
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MusteriTanim varlıklarını "MUSTERI_TANIM_TABLE" tablosuyla ilişkilendir
            modelBuilder.Entity<MusteriTanim>().ToTable("MUSTERI_TANIM_TABLE");

            // MusteriFatura varlıklarını "MUSTERI_FATURA_TABLE" tablosuyla ilişkilendir
            modelBuilder.Entity<MusteriFatura>().ToTable("MUSTERI_FATURA_TABLE");

            // MusteriFatura tablosundaki FaturaTutari sütunu için hassasiyet ve ölçek belirtir
            // decimal(18,2) = 18 toplam basamak, 2 ondalıklı basamak
            modelBuilder.Entity<MusteriFatura>()
                .Property(f => f.FaturaTutari)
                .HasColumnType("decimal(18,2)"); // FaturaTutari için uygun veri türünü ve hassasiyeti belirtir
        }
    }
}

