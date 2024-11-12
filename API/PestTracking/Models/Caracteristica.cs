using System.ComponentModel.DataAnnotations;

namespace PestTracking.Models
{
    public class Caracteristica
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DescripcionCaracteristica { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Orden { get; set; }
    }
}
