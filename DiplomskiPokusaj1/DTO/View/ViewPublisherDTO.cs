using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewPublisherDTO
    {   
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ViewAddressDTO Address { get; set; }
    }
}
