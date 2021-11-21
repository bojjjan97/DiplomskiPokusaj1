using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Create
{
    public class CreateReservationDTO
    {
        public string UserId { get; set; }
        public string LibraryId { get; set; }
        public IEnumerable<string> MaterialCopiesIds { get; set; }
    }
}
