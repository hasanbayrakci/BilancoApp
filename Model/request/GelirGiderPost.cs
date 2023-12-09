using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BilancoApp.Model.request
{
    public class GelirGiderPost
    {
        public int Id { get; set; }
        public int Kalem { get; set; }
        public decimal Tutar { get; set; }
        public DateTime IslemTarihi { get; set; }

    }
}
