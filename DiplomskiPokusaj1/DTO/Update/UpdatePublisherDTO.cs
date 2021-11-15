using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Update
{
    public class UpdatePublisherDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual UpdateAddressDTO Address { get; set; }
    }
}
