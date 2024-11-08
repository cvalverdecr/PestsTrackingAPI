using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestTracking.Models.Dtos
{
    public class CrearEmpresaDto
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Cedula { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string Notas { get; set; }
        public string Logo { get; set; }
        public int paisId { get; set; } 
        
    }
}