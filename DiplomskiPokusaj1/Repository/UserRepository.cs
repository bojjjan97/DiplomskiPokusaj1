using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutoMapper.Configuration;
using DiplomskiPokusaj1.DTO;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace DiplomskiPokusaj1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly UserManager<User> userManager;
        DBContext databaseContext;
        

        public UserRepository(UserManager<User> userManager, DBContext databaseContext, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this.userManager = userManager;
            this.databaseContext = databaseContext;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> Authenticate(LoginRequestDTO requestDTO)
        {
            User appUser = await userManager.FindByNameAsync(requestDTO.Username);
            if (appUser != null && await userManager.CheckPasswordAsync(appUser, requestDTO.Password) && appUser.DeletedAt == null)
            {
                var userRoles = await userManager.GetRolesAsync(appUser);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddHours(24),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var roles = await userManager.GetRolesAsync(appUser);

                var responseDTO = new LoginResponseDTO()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenValidTo = token.ValidTo,
                    Id = appUser.Id,
                    Name = appUser.Firstname + " " + appUser.Lastname,
                    Role = roles != null && roles.Count > 0 ? roles[0] : ""
                };
                return responseDTO;
            }
            return null;
        }

        public async Task<User> Create(CreateUserDTO userDTO, User userRequesting)
        {
            var usernameExists = (await userManager.FindByNameAsync(userDTO.Username)) != null;
            var emailExists = (await userManager.FindByEmailAsync(userDTO.Email)) != null;

            if (usernameExists) throw new Exception("Username already taken!");
            if (emailExists) throw new Exception("Email already taken!");

            User appUser = new User
            {
                Firstname = userDTO.FirstName,
                Lastname = userDTO.LastName,
                UserName = userDTO.Username,
                Email = userDTO.Email,
                Role = userDTO.Role,
                LibraryId = userDTO.LibraryId,
                Address = new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    Line1 = userDTO.AddressDTO.Line1,
                    Line2 = userDTO.AddressDTO.Line2,
                    City = userDTO.AddressDTO.City,
                    PostalCode = userDTO.AddressDTO.PostalCode,
                    Country = userDTO.AddressDTO.Country,
                    CreatedAt = DateTime.Now

                }
            };

            IdentityResult result = await userManager.CreateAsync(appUser, userDTO.Password);
            await databaseContext.SaveChangesAsync();
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(appUser, userDTO.Role);
                appUser.Role = userDTO.Role;

                return appUser;
            }
            return null;
        }

        public async Task<bool> Delete(string id, User userRequesting)
        {
            User userToDelete = await userManager.FindByIdAsync(id);
            if (userToDelete != null && userToDelete.DeletedAt == null && userToDelete.Id != userRequesting.Id)
            {
                userToDelete.DeletedAt = DateTime.Now;
                IdentityResult result = await userManager.UpdateAsync(userToDelete);
                await databaseContext.SaveChangesAsync();
                return result.Succeeded;
            }
            return false;
        }

        public async Task<User> Get(string id, User userRequesting)
        {
            var result = await userManager.Users
                .Where(user => user.DeletedAt == null && user.Id == id)
                .FirstOrDefaultAsync();

            if (result != null)
            {
                var roles = await userManager.GetRolesAsync(result);
                result.Role = (roles != null && roles.Count > 0) ? roles[0] : "Client";
            }

            return result;
        }

        public async Task<ICollection<User>> GetAll(User userRequesting)
        {
            var result = await userManager.Users.Where(user => user.DeletedAt == null).ToListAsync();

            foreach (var user in result)
            {
                var roles = await userManager.GetRolesAsync(user);
                user.Role = (roles != null && roles.Count > 0) ? roles[0] : "Client";
            }

            return result;
        }

        public async Task<User> Update(string id, User user, User userRequesting)
        {
            User userToEdit = await userManager.FindByIdAsync(id);

            if (userToEdit != null && userToEdit.DeletedAt == null)
            {
                userToEdit.Firstname = user.Firstname;
                userToEdit.Lastname = user.Lastname;
                userToEdit.PhoneNumber = user.PhoneNumber;
                userToEdit.Email = user.Email;
                userToEdit.UserName = user.UserName;
                userToEdit.LibraryId = user.LibraryId;
                userToEdit.Address = user.Address;
                userToEdit.PostalCode = user.PostalCode;

                userToEdit.UpdatedAt = DateTime.Now;

                IdentityResult result = await userManager.UpdateAsync(userToEdit);
                await databaseContext.SaveChangesAsync();
                if (result.Succeeded)
                {
                    IList<string> roles = await userManager.GetRolesAsync(userToEdit);
                    await userManager.RemoveFromRolesAsync(userToEdit, roles.ToArray());

                    await userManager.AddToRoleAsync(userToEdit, user.Role);
                    userToEdit.Role = userToEdit.Role;

                    return userToEdit;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }
    }
}
