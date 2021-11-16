using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewLibraryDTO
    { 
        public string Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public ViewAddressDTO Address { get; set; }
        public IEnumerable<string> EmployeesIds { get; set; }
    }
}
