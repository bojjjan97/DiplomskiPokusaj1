using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
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
    public class RentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRentRepository rentRepository;
        private UserManager<User> userManager;

        public RentController(IMapper mapper, IRentRepository repository, UserManager<User> userManager)
        {
            this.mapper = mapper;
            rentRepository = repository;
            this.userManager = userManager;
        }

        // GET: api/<RentController>
        [HttpGet]
        public async Task<ActionResult<List<ViewRentDTO>>> Get()
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);

            var result = await rentRepository.GetAll(userRequiringAccess);
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewRentDTO>>(result));
        }

        // GET api/<RentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewRentDTO>> Get(string id)
        {
            var result = await rentRepository.Get(id);

            return Ok(mapper.Map<ViewRentDTO>(result));
        }

        // POST api/<RentController>
        [HttpPost]
        public async Task<ActionResult<ViewRentDTO>> Post([FromBody] CreateRentDTO createRentDTO)
        {

            var result = await rentRepository.Create(createRentDTO);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewRentDTO>(result));
            }
        }

        // PUT api/<RentController>/5
        [Route("{id}/checkin")]
        [HttpPost]
        public async Task<ActionResult<ViewRentDTO>> CheckIn(string id)
        {

            var result = await rentRepository.checkIn(id);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewRentDTO>(result));
            }
        }

        // DELETE api/<RentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await rentRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
