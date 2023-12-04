using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BilancoApp.Model
{
    public class Kalemler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Type { get; set; } //0:Gider, 1:Gelir
        public DateTime Tarih {  get; set; } = DateTime.Now;
    }
}
