using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using DiplomskiPokusaj1.Storage.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplomskiPokusaj1.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        IImageRepository imageRepository;
        IFileMenager fileMenager;

        public ImageController(IImageRepository imageRepository, IFileMenager fileMenager)
        {
            this.imageRepository = imageRepository;
            this.fileMenager = fileMenager;
        }



        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<byte[]>> Get(string id)
        {
            var result = await imageRepository.Get(id);

            if (result != null && result.FilePath != null && result.FileName != null)
            {
                var content = await fileMenager.ReadFile(result.FilePath);
                var file =  File(content, "image/jpeg" , result.FileName);
                return Ok(content);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ImageController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateImageDTO createImageDTO)
        {
            Image newImage = new Image();
            if (createImageDTO.File != null)
            {
                try
                {
                    newImage.FilePath = await fileMenager.SaveFile(createImageDTO.File);
                    newImage.FileName = createImageDTO.FileName;
                }
                catch (Exception e)
                {
                    _ = Console.Error.WriteLineAsync(e.Message);
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }

            var result = await imageRepository.Create(newImage);

            if (result != null)
            {

                return Ok(result.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
