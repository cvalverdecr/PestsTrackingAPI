using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models.Dtos
{
    public class CrearCaracteristicaDto

    {
       [Required(ErrorMessage = "El campo Descripcion es requerido")]
       [MaxLength(100, ErrorMessage = "La longitud maxima del campo Descripcion es de 100 caracteres")]
       public string DescripcionCaracteristica { get; set; }
       public int orden { get; set; }
    }
}