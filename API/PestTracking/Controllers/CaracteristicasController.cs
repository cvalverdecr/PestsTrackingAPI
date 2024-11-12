using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PestTracking.Models.Dtos;
using PestTracking.Models;
using PestTracking.Repositorio.IRepositorio;

namespace PestTracking.Controllers
{
    [Route("api/caracteristica")]
    [ApiController]
    public class CaracteristicasController : ControllerBase
    {
        private readonly ICaracteristicaRepositorio _caracteristicasRepositorio;
        private readonly IMapper _mapper;

        public CaracteristicasController(ICaracteristicaRepositorio ctRepo, IMapper mapper)
        {
            _caracteristicasRepositorio = ctRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCaracteristicas()
        {
            var listaCaracteristicases = _caracteristicasRepositorio.GetCaracteristicas();
            var listaCaracteristicasesDto = new List<CaracteristicaDto>();

            foreach (var lista in listaCaracteristicases)
            {
                listaCaracteristicasesDto.Add(_mapper.Map<CaracteristicaDto>(lista));
            }

            return Ok(listaCaracteristicasesDto);
        }



        [HttpGet("{caracteristicasId:int}", Name = "GetCaracteristicas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCaracteristica(int caracteristicasId)
        {
            var itemCaracteristicas = _caracteristicasRepositorio.GetCaracteristica(caracteristicasId);
            if (itemCaracteristicas == null)
            {
                return NotFound();
            }
            var itemCaracteristicaDto = _mapper.Map<CaracteristicaDto>(itemCaracteristicas);
            return Ok(itemCaracteristicaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearCategoria([FromBody] CrearCaracteristicaDto crearCaracteristicaDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (crearCaracteristicaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_caracteristicasRepositorio.ExisteCaracteristica(crearCaracteristicaDto.DescripcionCaracteristica))
            {
                ModelState.AddModelError("", "Esta caracteristica ya existe");
                return StatusCode(404, ModelState);
            }

            var caracteristicas = _mapper.Map<Caracteristica>(crearCaracteristicaDto);

            if (!_caracteristicasRepositorio.CrearCaracteristica(caracteristicas))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {caracteristicas.DescripcionCaracteristica}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCaracteristicas", new { caracteristicasId = caracteristicas.Id }, caracteristicas);

        }


        [HttpPatch("{caracteristicasId:int}", Name = "ActualizarPatchCaracteristicas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchCaracteristicas(int caracteristicasId, [FromBody] CaracteristicaDto caracteristicasDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (caracteristicasDto == null || caracteristicasId != caracteristicasDto.Id)
            {
                return BadRequest(ModelState);
            }

            var caracteristicas = _mapper.Map<Caracteristica>(caracteristicasDto);

            if (!_caracteristicasRepositorio.ActualizarCaracteristica(caracteristicas))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {caracteristicas.DescripcionCaracteristica}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpPut("{caracteristicasId:int}", Name = "ActualizarPutCaracteristicas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ActualizarPutCaracteristicas(int caracteristicasId, [FromBody] CaracteristicaDto caracteristicasDto)
        {
            if (!ModelState.IsValid == null)
            {
                return BadRequest(ModelState);
            }

            if (caracteristicasDto == null || caracteristicasId != caracteristicasDto.Id)
            {
                return BadRequest(ModelState);
            }

            var caracteristicasExistente = _caracteristicasRepositorio.GetCaracteristica(caracteristicasId);
            if (caracteristicasExistente == null)
            {
                return NotFound($"No se encontró la caracteristica con el id {caracteristicasId}");
            }

            var caracteristicas = _mapper.Map<Caracteristica>(caracteristicasDto);

            if (!_caracteristicasRepositorio.ActualizarCaracteristica(caracteristicas))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {caracteristicas.DescripcionCaracteristica}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{caracteristicasId:int}", Name = "BorrarCaracteristicas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult BorrarCaracteristicas(int caracteristicasId)
        {
            if (!_caracteristicasRepositorio.ExisteCaracteristica(caracteristicasId))
            {
                return NotFound($"No se encontró la caracteristica con el id ${caracteristicasId}");
            }

            var caracteristicas = _caracteristicasRepositorio.GetCaracteristica(caracteristicasId);

            if (!_caracteristicasRepositorio.BorrarCaracteristica(caracteristicas))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {caracteristicas.DescripcionCaracteristica}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
