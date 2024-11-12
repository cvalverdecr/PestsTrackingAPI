using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PestTracking.Models;
using PestTracking.Models.Dtos;
using PestTracking.Repositorio.IRepositorio;

namespace PestTracking.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
         private readonly IUsuarioRepositorio _usuarioRepositorio;
         protected RespuestaAPI _respuestaApi;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepositorio ctRepo, IMapper mapper)
        {
            _usuarioRepositorio = ctRepo;
            _mapper = mapper;
            this._respuestaApi = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _usuarioRepositorio.GetUsuarios();
            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }

            return Ok(listaUsuariosDto);
        }


        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _usuarioRepositorio.GetUsuario(usuarioId);
            if (itemUsuario == null)
            {
                return NotFound();
            }
            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }


        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>Registro([FromBody]UsuarioRegistroDto usuarioRegistroDto)
        {
           bool validarNombreUsuarioUnico = _usuarioRepositorio.IsUniqueUser(usuarioRegistroDto.Username);
           if(!validarNombreUsuarioUnico)
              {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.isSuccess = false;
                _respuestaApi.ErrorMessages.Add("El nombre de usuario ya existe");
                return BadRequest(_respuestaApi);
              }
              var usuario = await _usuarioRepositorio.Registro(usuarioRegistroDto);
              if(usuario == null)
              {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.isSuccess = false;
                _respuestaApi.ErrorMessages.Add("Error al registrar el usuario");
                return StatusCode(500, _respuestaApi);
              }
              _respuestaApi.StatusCode = HttpStatusCode.OK;
              _respuestaApi.isSuccess = true;
              return Ok(_respuestaApi);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>Login([FromBody]UsuarioLoginDto usuarioLoginDto)
        {
           var respuestaLogin = await _usuarioRepositorio.Login(usuarioLoginDto);
           if(respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
              {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.isSuccess = false;
                _respuestaApi.ErrorMessages.Add("Usuario o contraseña incorrecta");
                return BadRequest(_respuestaApi);
              }
              _respuestaApi.StatusCode = HttpStatusCode.OK;
              _respuestaApi.isSuccess = true;
              _respuestaApi.Result = respuestaLogin;
              return Ok(_respuestaApi);
        }
        
    }
}