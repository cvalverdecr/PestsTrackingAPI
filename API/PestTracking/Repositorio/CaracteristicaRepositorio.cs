using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PestTracking.Data;
using PestTracking.Models;
using PestTracking.Repositorio.IRepositorio;

namespace PestTracking.Repositorio
{
    public class CaracteristicaRepositorio : ICaracteristicaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public CaracteristicaRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool ActualizarCaracteristica(Caracteristica caracteristica)
        {
            caracteristica.FechaCreacion = DateTime.UtcNow;
            //arreglar problema del put con el tracking
            //alternativamente, hacer que este campo se llene por defecto usando triggers en la base de datos

            var caracteristicaExistente = _context.Caracteristica.Find(caracteristica.Id);
            if (caracteristicaExistente != null)
            {
                _context.Entry(caracteristicaExistente).CurrentValues.SetValues(caracteristica); 
            }else{
                _context.Caracteristica.Update(caracteristica);
            }
            return Guardar();
        }

        public bool BorrarCaracteristica(Caracteristica caracteristica)
        {
            _context.Caracteristica.Remove(caracteristica);
            return Guardar();
        }

        public bool CrearCaracteristica(Caracteristica caracteristica)
        {
            caracteristica.FechaCreacion = DateTime.UtcNow;
            _context.Add(caracteristica);
            return Guardar();
        }

        public bool ExisteCaracteristica(string descripcion)
        {
            var test = _context.Caracteristica;
            bool valor = test.Any(p => p.DescripcionCaracteristica.ToLower().Trim() == descripcion.ToLower().Trim());


            return valor;
        }

        public bool ExisteCaracteristica(int id)
        {
            return _context.Caracteristica.Any(p => p.Id == id);
        }

        public Caracteristica GetCaracteristica(int caracteristicaId)
        {
        return _context.Caracteristica.FirstOrDefault(p => p.Id == caracteristicaId);
        }

        public ICollection<Caracteristica> GetCaracteristicas()
        {
            return _context.Caracteristica.OrderBy(p => p.Orden).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}