using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.Helper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplomskiPokusaj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IMapper mapper, IAuthorRepository repository)
        {
            this.mapper = mapper;
            authorRepository = repository;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ViewAuthorDTO>>> GetAll([FromQuery] string authorId)
        {
            var result = await authorRepository.GetAll(authorId);
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewAuthorDTO>>(result));
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewAuthorDTO>> Get(string id)
        {
            var result = await authorRepository.Get(id);

            return Ok(mapper.Map<ViewAuthorDTO>(result));
        }

        // POST api/<AuthorController>
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<ViewAuthorDTO>> Post([FromBody] CreateAuthorDTO createAuthorDTO)
        {
            var author = mapper.Map<Author>(createAuthorDTO);

            var result = await authorRepository.Create(author);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewAuthorDTO>(result));
            }
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewAuthorDTO>> Put(string id, [FromBody] UpdateAuthorDTO updateAuthorDTO)
        {
            var author = mapper.Map<Author>(updateAuthorDTO);
            var result = await authorRepository.Update(id, author);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewAuthorDTO>(result));
            }
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await authorRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
