using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMaxDebtProject.Models
{
    [Table("MUSTERI_TANIM_TABLE")]
    public class MusteriTanim
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("UNVAN")]
        public string? Unvan { get; set; }
    }
}
