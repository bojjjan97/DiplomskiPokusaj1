using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
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
    public class ReservationController : ControllerBase
    {


        private readonly IMapper mapper;
        private readonly IReservationRepository reservationRepository;
        private UserManager<User> userManager;

        public ReservationController(IMapper mapper, IReservationRepository repository, UserManager<User> userManager)
        {
            this.mapper = mapper;
            reservationRepository = repository;
            this.userManager = userManager;
        }


        // GET: api/<ReservationController>
        [HttpGet]
        public async Task<ActionResult<List<ViewReservationDTO>>> Get()
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);

            var result = await reservationRepository.GetAll(userRequiringAccess);
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewReservationDTO>>(result));
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewReservationDTO>> Get(string id)
        {
            var result = await reservationRepository.Get(id);

            return Ok(mapper.Map<ViewReservationDTO>(result));
        }

        // POST api/<ReservationController>
        [HttpPost]
        public async Task<ActionResult<ViewReservationDTO>> Post([FromBody] CreateReservationDTO createReservationDTO)
        {

            var result = await reservationRepository.Create(createReservationDTO);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewReservationDTO>(result));
            }
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewReservationDTO>> Put(string id, [FromBody] UpdateReservationDTO updateReservationDTO)
        {
            var result = await reservationRepository.Update(id, updateReservationDTO);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewReservationDTO>(result));
            }
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await reservationRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
