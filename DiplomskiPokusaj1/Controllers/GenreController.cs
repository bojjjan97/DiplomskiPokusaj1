using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomskiPokusaj1.Repository;
using DiplomskiPokusaj1.Repository.Interface;
using DiplomskiPokusaj1.DTO;
using DiplomskiPokusaj1.Helper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplomskiPokusaj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMapper mapper;
        private IGenreRepository genreRepository;

        public GenreController(IMapper _mapper, IGenreRepository _genreRepository)
        {
            mapper = _mapper;
            genreRepository = _genreRepository;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<ActionResult<List<ViewGenreDTO>>> Get()
        {
            var result = await genreRepository.GetAll();
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewGenreDTO>>(result));
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewGenreDTO>> Get(string id)
        {
            var result = await genreRepository.Get(id);

            return Ok(mapper.Map<ViewGenreDTO>(result));
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<ActionResult<ViewGenreDTO>> Post([FromBody] CreateGenreDTO genreDTO)
        {
            var genre = mapper.Map<Genre>(genreDTO);

            var result = await genreRepository.Create(genre);

            if(result != null)
            {
                return Ok(mapper.Map<ViewGenreDTO>(result));
            }
            else
            {
                return BadRequest();
            }

        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewGenreDTO>> Put(string id, UpdateGenreDTO updateGenreDTO)
        {
            var genre = mapper.Map<Genre>(updateGenreDTO);
            var result = await genreRepository.Update(id, genre);

            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewGenreDTO>(result));
            }
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await genreRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
