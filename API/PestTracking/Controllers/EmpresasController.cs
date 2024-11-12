using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PestTracking.Models;
using PestTracking.Models.Dtos;
using PestTracking.Repositorio.IRepositorio;

namespace PestTracking.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase
    {  
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IMapper _mapper;

        public EmpresasController(IEmpresaRepositorio ctRepo, IMapper mapper)
        {
            _empresaRepositorio = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetEmpresas()
        {
            var listaEmpresas = _empresaRepositorio.GetEmpresas();
            var listaEmpresasDto = new List<EmpresaDto>();

            foreach (var lista in listaEmpresas)
            {
                listaEmpresasDto.Add(_mapper.Map<EmpresaDto>(lista));
            }

            return Ok(listaEmpresasDto);
        }

        [HttpGet("{empresaId:int}", Name = "GetEmpresa")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEmpresa(int empresaId)
        {
            var itemEmpresa = _empresaRepositorio.GetEmpresa(empresaId);
            if (itemEmpresa == null)
            {
                return NotFound();
            }
            var itemEmpresaDto = _mapper.Map<EmpresaDto>(itemEmpresa);
            return Ok(itemEmpresaDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EmpresaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearEmpresa([FromBody] CrearEmpresaDto crearEmpresaDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (crearEmpresaDto == null){
                return BadRequest(ModelState);
            }

            if (_empresaRepositorio.ExisteEmpresa(crearEmpresaDto.Nombre))
            {
                ModelState.AddModelError("", "La empresa ya existe");
                return StatusCode(404, ModelState);
            }

            var empresa = _mapper.Map<Empresa>(crearEmpresaDto);

            if (!_empresaRepositorio.CrearEmpresa(empresa))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {empresa.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEmpresa", new { empresaId = empresa.Id }, empresa);
        }   

        [HttpPatch("{empresaId:int}", Name = "ActualizarPatchEmpresa")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchEmpresa(int empresaId,[FromBody] EmpresaDto empresaDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (empresaDto == null || empresaId != empresaDto.Id){
                return BadRequest(ModelState);
            }

            var empresaExistente = _empresaRepositorio.GetEmpresa(empresaId);
            if(empresaExistente == null)
            {
                return NotFound($"No se encontro la empresa con el país {empresaId}");
            }

            var empresa = _mapper.Map<Empresa>(empresaDto);

            if (!_empresaRepositorio.ActualizarEmpresa(empresa))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {empresa.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();  
        }   


        [HttpDelete("{empresaId:int}", Name = "BorrarEmpresa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult BorrarEmpresa(int empresaId)
        {
            if (!_empresaRepositorio.ExisteEmpresa(empresaId))
            {
                return NotFound($"No se encontró la empresa");
            }

            var empresa = _empresaRepositorio.GetEmpresa(empresaId);

            if (!_empresaRepositorio.BorrarEmpresa(empresa))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {empresa.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("GetEmpresasEnPais/{paisId:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetEmpresasEnPais(int paisId)
        {
            var listaEmpresas = _empresaRepositorio.GetEmpresasPorPais(paisId);
            if (listaEmpresas == null)
            {
                return NotFound();
            }

            var itemEmpresa = new List<EmpresaDto>();
            foreach (var empresa in listaEmpresas)
            {
                itemEmpresa.Add(_mapper.Map<EmpresaDto>(empresa));
            }

            return Ok(itemEmpresa);
        }   

        [HttpGet("Buscar")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public IActionResult Buscar(string nombre)
        {

            try {
                var resultado = _empresaRepositorio.BuscarEmpresa(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicación");
            }   
        }   
        
    }
}