using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomskiPokusaj1.Repository.Interface;
using DiplomskiPokusaj1.DTO;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplomskiPokusaj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryController (IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/<CateController>
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetAll()
        {
            var result = await _repository.GetAll();
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(_mapper.Map<List<CategoryDTO>>(result)); 
        }

        // GET api/<CateController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CateController>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            var result =await _repository.Create(category);

            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<CategoryDTO>(result));
            }
        }

        // PUT api/<CateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
