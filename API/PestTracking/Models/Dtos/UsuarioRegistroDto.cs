using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models.Dtos
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "El password es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public string Rol { get; set; }

    }
}