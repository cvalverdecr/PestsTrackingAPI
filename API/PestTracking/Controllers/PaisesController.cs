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
    [Route("api/[controller]")]
    public class PaisesController : ControllerBase
    {
        private readonly IPaisRepositorio _paisRepositorio;
        private readonly IMapper _mapper;

        public PaisesController(IPaisRepositorio ctRepo, IMapper mapper)
        {
            _paisRepositorio = ctRepo;
            _mapper = mapper;
        }
        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPaises()
        {
            var listaPaises = _paisRepositorio.GetPaises();
            var listaPaisesDto = new List<PaisDto>();

            foreach (var lista in listaPaises)
            {
                listaPaisesDto.Add(_mapper.Map<PaisDto>(lista));
            }

            return Ok(listaPaisesDto);
        }



        [HttpGet("{paisId:int}", Name = "GetPais")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPais(int paisId)
        {
            var itemPais = _paisRepositorio.GetPais(paisId);
            if (itemPais == null)
            {
                return NotFound();
            }
            var itemPaisDto = _mapper.Map<PaisDto>(itemPais);
            return Ok(itemPaisDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearCategoria([FromBody] CrearPaisDto crearPaisDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (crearPaisDto == null){
                return BadRequest(ModelState);
            }

            if (_paisRepositorio.ExistePais(crearPaisDto.Descripcion))
            {
                ModelState.AddModelError("", "El país ya existe");
                return StatusCode(404, ModelState);
            }

            var pais = _mapper.Map<Pais>(crearPaisDto);

            if (!_paisRepositorio.CrearPais(pais))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {pais.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPais", new { paisId = pais.Id }, pais);
            
        }   


        [HttpPatch("{paisId:int}", Name = "ActualizarPatchPais")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchPais(int paisId,[FromBody] PaisDto paisDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (paisDto == null || paisId != paisDto.Id){
                return BadRequest(ModelState);
            }

            var pais = _mapper.Map<Pais>(paisDto);

            if (!_paisRepositorio.ActualizarPais(pais))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {pais.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }   


        [HttpPut("{paisId:int}", Name = "ActualizarPutPais")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ActualizarPutPais(int paisId,[FromBody] PaisDto paisDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (paisDto == null || paisId != paisDto.Id){
                return BadRequest(ModelState);
            }

            var paisExistente = _paisRepositorio.GetPais(paisId);
            if (paisExistente == null)
            {
                return NotFound($"No se encontró el país con el id {paisId}");
            }

            var pais = _mapper.Map<Pais>(paisDto);

            if (!_paisRepositorio.ActualizarPais(pais))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {pais.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }   


        [HttpDelete("{paisId:int}", Name = "BorrarPais")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult BorrarPais(int paisId)
        {
            if (!_paisRepositorio.ExistePais(paisId))
            {
                return NotFound($"No se encontró el país");
            }

            var pais = _paisRepositorio.GetPais(paisId);

            if (!_paisRepositorio.BorrarPais(pais))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {pais.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }   
    }
}