using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PestTracking.Models;

namespace PestTracking.Repositorio.IRepositorio
{
    public interface ICaracteristicaRepositorio
    {
        ICollection<Caracteristica> GetCaracteristicas();
        Caracteristica GetCaracteristica(int caracteristicaId);
        bool ExisteCaracteristica(string nombre);
        bool ExisteCaracteristica(int id);    
        bool CrearCaracteristica(Caracteristica caracteristica);
        bool ActualizarCaracteristica(Caracteristica caracteristica);
        bool BorrarCaracteristica(Caracteristica caracteristica);
        bool Guardar(); 
    }
}