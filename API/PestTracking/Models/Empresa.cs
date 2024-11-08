using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Cedula { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string Notas { get; set; }
        public string Logo { get; set; }
        public DateTime FechaCreacion { get; set; }
        //Relacion con pais
        public int paisId { get; set; } 
        [ForeignKey("paisId")]
        public Pais Pais { get; set; }
    }
}