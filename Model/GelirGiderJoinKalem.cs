using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BilancoApp.Model
{
    public class GelirGiderJoinKalem
    {
        public int Id { get; set; }
        public string? Kalem { get; set;}
        public decimal Tutar { get; set; }
        public string? IslemTarihi { get; set; }

    }
}
