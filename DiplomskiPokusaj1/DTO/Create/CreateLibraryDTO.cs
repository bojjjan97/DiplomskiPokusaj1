using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Create
{
    public class CreateLibraryDTO
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public CreateAddressDTO Address { get; set; }
        public IEnumerable<string> EmployeesIds { get; set; }
    }
}
