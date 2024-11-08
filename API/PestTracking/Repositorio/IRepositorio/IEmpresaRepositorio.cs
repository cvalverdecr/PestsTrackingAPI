using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PestTracking.Models;

namespace PestTracking.Repositorio.IRepositorio
{
    public interface IEmpresaRepositorio
    {
        ICollection<Empresa> GetEmpresas();
        ICollection<Empresa> GetEmpresasPorPais(int paisId);
        IEnumerable<Empresa> BuscarEmpresa(string nombre);
        Empresa GetEmpresa(int EmpresaId);
        bool ExisteEmpresa(string nombre);
        bool ExisteEmpresa(int id);    
        bool CrearEmpresa(Empresa Empresa);
        bool ActualizarEmpresa(Empresa Empresa);
        bool BorrarEmpresa(Empresa Empresa);
        bool Guardar(); 
    }
}