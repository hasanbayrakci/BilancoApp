using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BilancoApp.Model
{
    public class GelirGider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int KalemlerId { get; set;}
        [Required]
        public decimal Tutar { get; set; }
        [Required]
        public DateTime IslemTarihi { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;

    }
}
