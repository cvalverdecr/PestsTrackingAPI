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
            pais.FechaCreacion = DateTime.UtcNow;
            //arreglar problema del put con el tracking
            var paisExistente = _context.Pais.Find(pais.Id);
            if (paisExistente != null)
            {
                _context.Entry(paisExistente).CurrentValues.SetValues(pais); 
            }else{
                _context.Pais.Update(pais);
            }
            return Guardar();
        }

        public bool BorrarPais(Pais pais)
        {
            _context.Pais.Remove(pais);
            return Guardar();
        }

        public bool CrearPais(Pais pais)
        {
            pais.FechaCreacion = DateTime.UtcNow;
            _context.Add(pais);
            return Guardar();
        }

        public bool ExistePais(string nombre)
        {
            bool valor = _context.Pais.Any(p => p.Descripcion.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExistePais(int id)
        {
            return _context.Pais.Any(p => p.Id == id);
        }

        public Pais GetPais(int paisId)
        {
        return _context.Pais.FirstOrDefault(p => p.Id == paisId);
        }

        public ICollection<Pais> GetPaises()
        {
            return _context.Pais.OrderBy(p => p.Orden).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}