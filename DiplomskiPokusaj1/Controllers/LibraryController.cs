using AutoMapper;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.Helper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using DiplomskiPokusaj1.Storage.Interface;
using Microsoft.AspNetCore.Http;
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
    public class LibraryController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly ILibraryRepository libraryRepository;
        IImageRepository imageRepository;
        IFileMenager fileMenager;



        public LibraryController(IMapper mapper, ILibraryRepository repository, IImageRepository imageRepository, IFileMenager fileMenager)
        {
            this.mapper = mapper;
            libraryRepository = repository;
            this.imageRepository = imageRepository;
            this.fileMenager = fileMenager;
        }
        // GET: api/<LibraryController>
        [HttpGet]
        public async Task<ActionResult<List<ViewLibraryDTO>>> Get()
        {
            var result = await libraryRepository.GetAll();
            ControllerHelper.IncludeContentRange("client", 0, result.Count, result.Count, Request);
            return Ok(mapper.Map<List<ViewLibraryDTO>>(result));
        }

        // GET api/<LibraryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewLibraryDTO>> Get(string id)
        {
            var result = await libraryRepository.Get(id);

            return Ok(mapper.Map<ViewLibraryDTO>(result));
        }

        // POST api/<LibraryController>
        [HttpPost]
        public async Task<ActionResult<ViewLibraryDTO>> Post([FromBody] CreateLibraryDTO createLibraryDTO)
        {

            Image newImage = new Image();
            if (createLibraryDTO.File != null)
            {
                try
                {
                    newImage.FilePath = await fileMenager.SaveFile(createLibraryDTO.File);
                    newImage.FileName = createLibraryDTO.FileName;
                }
                catch (Exception e)
                {
                    _ = Console.Error.WriteLineAsync(e.Message);
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }

            var image = await imageRepository.Create(newImage);
            createLibraryDTO.ImageId = image.Id;


            var result = await libraryRepository.Create(createLibraryDTO);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewLibraryDTO>(result));
            }
        }

        // PUT api/<LibraryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ViewLibraryDTO>> Put(string id, [FromBody] UpdateLibraryDTO updateLibraryDTO)
        {
            var result = await libraryRepository.Update(id, updateLibraryDTO);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<ViewLibraryDTO>(result));
            }
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await libraryRepository.Delete(id);
            return result ? Ok() : BadRequest();
        }
    }
}
