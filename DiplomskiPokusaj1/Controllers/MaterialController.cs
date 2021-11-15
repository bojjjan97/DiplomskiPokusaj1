﻿using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.Helper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
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
    public class MaterialController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMaterialRepository materialRepository;

        public MaterialController(IMapper mapper, IMaterialRepository repository)
        {
            this.mapper = mapper;
            materialRepository = repository;
        }

        // GET: api/<MaterialController>
        [HttpGet]
        public async Task<ActionResult<List<ViewMaterialDTO>>> Get()
        {
            var result = await materialRepository.GetAll();
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewMaterialDTO>>(result));
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewMaterialDTO>> Get(string id)
        {
            var result = await materialRepository.Get(id);

            return Ok(mapper.Map<ViewMaterialDTO>(result));
        }

        // POST api/<MaterialController>
        [HttpPost]
        public async Task<ActionResult<ViewMaterialDTO>> Post([FromBody] CreateMaterialDTO createMaterialDTO)
        {

            var result = await materialRepository.Create(createMaterialDTO);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewMaterialDTO>(result));
            }
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}