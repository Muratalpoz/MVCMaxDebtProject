using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMaxDebtProject.Models
{
    [Table("MUSTERI_FATURA_TABLE")]
    public class MusteriFatura
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("MUSTERI_ID")]
        public int MusteriId { get; set; }

            [Column("FATURA_TARIHI")]
        public DateTime FaturaTarihi { get; set; }

        [Column("FATURA_TUTARI")]
        public decimal FaturaTutari { get; set; }

        [Column("ODEME_TARIHI")]
        public DateTime? OdemeTarihi { get; set; }
    }
}
