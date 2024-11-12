using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PestTracking.Models;

namespace PestTracking.Repositorio.IRepositorio
{
    public interface IPaisRepositorio
    {
        ICollection<Pais> GetPaises();
        Pais GetPais(int paisId);
        bool ExistePais(string nombre);
        bool ExistePais(int id);    
        bool CrearPais(Pais pais);
        bool ActualizarPais(Pais pais);
        bool BorrarPais(Pais pais);
        bool Guardar(); 
    }
}