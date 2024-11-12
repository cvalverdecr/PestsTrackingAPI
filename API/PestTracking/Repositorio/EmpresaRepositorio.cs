using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PestTracking.Data;
using PestTracking.Models;
using PestTracking.Repositorio.IRepositorio;

namespace PestTracking.Repositorio
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
         private readonly ApplicationDbContext _context;
        public EmpresaRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool ActualizarEmpresa(Empresa empresa)
        {
            empresa.FechaCreacion = DateTime.UtcNow;
            //arreglar problema del put con el tracking
            var empresaExistente = _context.Empresa.Find(empresa.Id);
            if (empresaExistente != null)
            {
                _context.Entry(empresaExistente).CurrentValues.SetValues(empresa); 
            }else{
                _context.Empresa.Update(empresa);
            }
            return Guardar();
        }

        public bool BorrarEmpresa(Empresa empresa)
        {
            _context.Empresa.Remove(empresa);
            return Guardar();
        }

        public bool CrearEmpresa(Empresa empresa)
        {
            empresa.FechaCreacion = DateTime.UtcNow;
            _context.Add(empresa);
            return Guardar();
        }

        public bool ExisteEmpresa(string nombre)
        {
            bool valor = _context.Empresa.Any(p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteEmpresa(int id)
        {
            return _context.Empresa.Any(p => p.Id == id);
        }

        public Empresa GetEmpresa(int empresaId)
        {
        return _context.Empresa.FirstOrDefault(p => p.Id == empresaId);
        }

        public ICollection<Empresa> GetEmpresas()
        {
            return _context.Empresa.OrderBy(p => p.Nombre).ToList();
        }

         public ICollection<Empresa> GetEmpresasPorPais(int paisId)
        {
            return _context.Empresa.Include(pa =>pa.Pais).Where(pa => pa.paisId == paisId).ToList();
        }
            IEnumerable<Empresa> IEmpresaRepositorio.BuscarEmpresa(string nombre)
        {
            IQueryable<Empresa> query = _context.Empresa;
            if(!string.IsNullOrEmpty(nombre)){
                query = query.Where(e => e.Nombre.Contains(nombre) );
            }
            return query.ToList();
        }
     
        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    
    }
}