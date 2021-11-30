using DiplomskiPokusaj1.DTO.Create;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO
{
    public class CreateUserDTO
    {
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CreateAddressDTO AddressDTO { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string LibraryId { get; set; }
    }
}
