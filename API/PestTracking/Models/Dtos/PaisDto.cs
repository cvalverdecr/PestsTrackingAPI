using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace PestTracking.Models.Dtos
{
    public class PaisDto
    {
       public int Id { get; set; }
       [Required(ErrorMessage = "El campo Descripcion es requerido")]
       [MaxLength(100, ErrorMessage = "La longitud maxima del campo Descripcion es de 100 caracteres")]
       //Display(Name = "Nombre del Pais")]
       public string Descripcion { get; set; }
       public DateTime FechaCreacion { get; set; }
       public int Orden { get; set; } 
    }
}