using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.Helper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository;
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
    public class PublisherController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPublisherRepository publisherRepository;

        public PublisherController(IMapper mapper, IPublisherRepository repository)
        {
            this.mapper = mapper;
            publisherRepository = repository;
        }


        // GET: api/<PublisherController>
        [HttpGet]
        public async Task<ActionResult<List<ViewPublisherDTO>>> Get()
        {
            var result = await publisherRepository.GetAll();
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewPublisherDTO>>(result));
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewPublisherDTO>> Get(string id)
        {
            var result = await publisherRepository.Get(id);

            return Ok(mapper.Map<ViewPublisherDTO>(result));
        }

        // POST api/<PublisherController>
        [HttpPost]
        public async Task<ActionResult<ViewPublisherDTO>> Post([FromBody] CreatePublisherDTO createPublisherDTO)
        {
            var publisher = mapper.Map<Publisher>(createPublisherDTO);

            var result = await publisherRepository.Create(publisher);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewPublisherDTO>(result));
            }
        }

        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewPublisherDTO>> Put(string id, [FromBody] UpdatePublisherDTO updatePublisherDTO)
        {
            var publisher = mapper.Map<Publisher>(updatePublisherDTO);
            var result = await publisherRepository.Update(id, publisher);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewPublisherDTO>(result));
            }
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await publisherRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
