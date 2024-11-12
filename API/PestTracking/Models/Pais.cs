using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models
{
    public class Pais
    {
       [Key] 
       public int Id { get; set; }
       [Required]
       //Display(Name = "Nombre del Pais")]
       public string Descripcion { get; set; }
       [Required]
       public DateTime FechaCreacion { get; set; }
       public int Orden { get; set; }   
    }
}