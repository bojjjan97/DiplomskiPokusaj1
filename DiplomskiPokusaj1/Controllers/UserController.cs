using AutoMapper;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.DTO;
using DiplomskiPokusaj1.Helper;
using Microsoft.AspNetCore.Authorization;
using DiplomskiPokusaj1.DTO.View;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplomskiPokusaj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;

        public UserController(ILogger<UserController> logger, IMapper mapper, UserManager<User> userManager, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Administrator,Librarian")]
        public async Task<ActionResult<List<ViewUserDTO>>> Get()
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);
            var result = await userRepository.GetAll(userRequiringAccess);

            ControllerHelper.IncludeContentRange("user", 0, result.Count, result.Count, Request);

            if (userRequiringAccess != null && (await userManager.IsInRoleAsync(userRequiringAccess, "Administrator")))
            {
                var dto = _mapper.Map<List<ViewUserDTO>>(result);
                return Ok(dto);
            }
            else
            {
                var dto = _mapper.Map<List<ViewPublicUserDTO>>(result);
                return Ok(dto);
            }

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewPublicUserDTO>> Get(string id)
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);

            var result = await userRepository.Get(id, userRequiringAccess);
            if (result == null)
            {
                return NotFound();
            }

            if (userRequiringAccess != null &&
                (userRequiringAccess.Id == result.Id
                || await userManager.IsInRoleAsync(userRequiringAccess, "Administrator")
                || await userManager.IsInRoleAsync(userRequiringAccess, "Secretary")
                ))
            {
                return Ok(_mapper.Map<ViewPublicUserDTO>(result));
            }
            else
            {
                return Ok(_mapper.Map<ViewPublicUserDTO>(result));
            }

        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ViewUserDTO>> Post([FromBody] CreateUserDTO userDTO)
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userRepository.Create(userDTO, userRequiringAccess);
                    if (user != null)
                    {
                        return Ok(_mapper.Map<ViewUserDTO>(user));
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return BadRequest("Unknown error!");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ViewUserDTO>> Patch(string id, [FromBody] ViewUserDTO userDTO)
        {
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);

            var user = _mapper.Map<User>(userDTO);
            User result = await userRepository.Update(id, user, userRequiringAccess);

            if (result != null)
            {
                return Ok(_mapper.Map<ViewUserDTO>(result));
            }
            else
            {
                return BadRequest("User Not Found");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            User userToDelete = await userManager.FindByIdAsync(id);
            User userRequiringAccess = await userManager.GetUserAsync(HttpContext.User);

            if (userToDelete.Id == userRequiringAccess.Id)
            {
                return BadRequest("You cannot delete your account.");
            }

            var result = await userRepository.Delete(id, userRequiringAccess);

            if (result)
            {
                return Ok(true);
            }

            return BadRequest("User not found!");
        }

        [HttpPost]
        [Route("authorize")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginDTO)
        {
            var result = await userRepository.Authenticate(loginDTO);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized("Login Failed: Invalid Email or password");
            }
        }
    }
}
