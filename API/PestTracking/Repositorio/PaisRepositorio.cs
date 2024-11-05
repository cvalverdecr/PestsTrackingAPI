using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PestTracking.Data;
using PestTracking.Models;
using PestTracking.Repositorio.IRepositorio;

namespace PestTracking.Repositorio
{
    public class PaisRepositorio : IPaisRepositorio
    {
        private readonly ApplicationDbContext _context;
        public PaisRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool ActualizarPais(Pais pais)
        {
            pais.FechaCreacion = DateTime.Now;
            _context.Pais.Update(pais);
            return Guardar();
        }

        public bool BorrarPais(Pais pais)
        {
            _context.Pais.Remove(pais);
            return Guardar();
        }

        public bool CrearPais(Pais pais)
        {
            pais.FechaCreacion = DateTime.Now;
            _context.Add(pais);
            return Guardar();
        }

        public bool ExistePais(string nombre)
        {
            bool valor = _context.Pais.Any(p => p.Descripcion.ToLower().Trim() == nombre.ToLower().Trim());
            

        }

        public bool ExistePais(int id)
        {
            return _context.Pais.Any(p => p.Id == id);
        }

        public Pais GetPais(int paisId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Pais> GetPaises()
        {
            throw new NotImplementedException();
        }

        public bool Guardar()
        {
            throw new NotImplementedException();
        }
    }
}