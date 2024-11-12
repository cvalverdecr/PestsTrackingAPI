using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PestTracking.Data;
using PestTracking.Models;
using PestTracking.Models.Dtos;
using PestTracking.Repositorio.IRepositorio;
using XSystem.Security.Cryptography;

namespace PestTracking.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly ApplicationDbContext _context;
        private readonly string claveSecreta;
        public UsuarioRepositorio(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }



        public Usuario GetUsuario(int UsuarioId)
        {
            return _context.Usuario.FirstOrDefault(u => u.Id == UsuarioId);

        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _context.Usuario.OrderBy(u => u.Nombre).ToList();
        }

        public bool IsUniqueUser(string nombre)
        {
            var usuarioBd = _context.Usuario.FirstOrDefault(u => u.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            if (usuarioBd == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenerMd5(usuarioLoginDto.Password);
            var usuario = _context.Usuario.FirstOrDefault(
                u => u.Username.ToLower() == usuarioLoginDto.Username.ToLower() 
                && u.Password == passwordEncriptado);
            //validamos si el usuario no existe con la combinacion de usuario y password
            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
             
            //aqui existe e usuario entonces podemos procesar el login
            var manejoToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);
             
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    //new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejoToken.CreateToken(tokenDescriptor);
            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejoToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRespuestaDto;

        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptado = obtenerMd5(usuarioRegistroDto.Password);

            Usuario usuario = new Usuario
            {
                Username = usuarioRegistroDto.Username,
                Password = passwordEncriptado,
                Nombre = usuarioRegistroDto.Nombre,
                Rol = usuarioRegistroDto.Rol,
            };
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return usuario;
        }


        public static string obtenerMd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for(int i = 0 ; i <data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;    
        }    
}
}