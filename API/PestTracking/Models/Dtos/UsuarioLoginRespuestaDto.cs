using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models.Dtos
{
    public class UsuarioLoginRespuestaDto
    {
        public Usuario Usuario { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
    }
}