using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ViewAddressDTO Address { get; set; }
        public string LibraryId { get; set; }

        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
