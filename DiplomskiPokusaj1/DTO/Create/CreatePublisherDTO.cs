using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Create
{
    public class CreatePublisherDTO
    {
       
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual CreateAddressDTO Address { get; set; }
    }
}
