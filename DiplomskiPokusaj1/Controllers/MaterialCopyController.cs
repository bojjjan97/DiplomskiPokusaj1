using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Filter;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.Helper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.AspNetCore.Identity;
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
    public class MaterialCopyController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMaterialCopyRepository materialCopyRepository;
        private UserManager<User> userManager;

        public MaterialCopyController(IMapper mapper, IMaterialCopyRepository repository, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            this.mapper = mapper;
            materialCopyRepository = repository;
            this.userManager = userManager;
        }
        // GET: api/<MaterialCopyController>
        [HttpGet]
        public async Task<ActionResult<List<ViewMaterialCopyDTO>>> Get([FromQuery] FilterMaterialCopyDTO filter)
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);

            var result = await materialCopyRepository.GetAll(filter, userRequiringAccess);
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewMaterialCopyDTO>>(result));
        }

        // GET api/<MaterialCopyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewMaterialCopyDTO>> Get(string id)
        {
            var result = await materialCopyRepository.Get(id);

            return Ok(mapper.Map<ViewMaterialCopyDTO>(result));
        }

        // POST api/<MaterialCopyController>
        [HttpPost]
        public async Task<ActionResult<ViewMaterialCopyDTO>> Post([FromBody] CreateMaterialCopyDTO createMaterialCopyDTO )
        {
            var materialCopy = mapper.Map<MaterialCopy>(createMaterialCopyDTO);

            var result = await materialCopyRepository.Create(materialCopy);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewMaterialCopyDTO>(result));
            }
        }

        // PUT api/<MaterialCopyController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewMaterialCopyDTO>> Put(string id, [FromBody] UpdateMaterialCopyDTO updateMaterialCopyDTO)
        {
            var materialCopy = mapper.Map<MaterialCopy>(updateMaterialCopyDTO);
            var result = await materialCopyRepository.Update(id, materialCopy);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewMaterialCopyDTO>(result));
            }
        }

        // DELETE api/<MaterialCopyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await materialCopyRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
