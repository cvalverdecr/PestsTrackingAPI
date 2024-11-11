using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PestTracking.Models;
using PestTracking.Models.Dtos;

namespace PestTracking.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool IsUniqueUser(string nombre);
        Task<UsuarioLoginRespuestaDto>Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario>Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}