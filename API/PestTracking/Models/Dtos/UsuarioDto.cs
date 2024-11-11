using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}