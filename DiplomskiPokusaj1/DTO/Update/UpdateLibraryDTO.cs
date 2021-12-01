using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Update
{
    public class UpdateLibraryDTO
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ImageId { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
    
        public UpdateAddressDTO Address { get; set; }
        public IEnumerable<string> EmployeesIds { get; set; }
    }
}
