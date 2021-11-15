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
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;

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
        public async Task<ActionResult<List<ViewCategoryDTO>>> GetAll()
        {
            var result = await _repository.GetAll();
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(_mapper.Map<List<ViewCategoryDTO>>(result)); 
        }

        // GET api/<CateController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewCategoryDTO>> Get(string id)
        {
            var result = await _repository.Get(id);

            return Ok(_mapper.Map<ViewCategoryDTO>(result));
        }

        // POST api/<CateController>
        [HttpPost]
        public async Task<ActionResult<ViewCategoryDTO>> Post([FromBody] CreateCategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            var result =await _repository.Create(category);

            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<ViewCategoryDTO>(result));
            }
        }

        // PUT api/<CateController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewCategoryDTO>> Put(string id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = _mapper.Map<Category>(updateCategoryDTO);

            var result = await _repository.Update(id,category);

            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<ViewCategoryDTO>(result)); 
            }

        }

        // DELETE api/<CateController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _repository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
