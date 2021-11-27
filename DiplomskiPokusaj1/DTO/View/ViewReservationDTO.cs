using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewReservationDTO
    {
        public string Id { get; set; }
        public string LibraryId { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> MaterialCopiesIds { get; set; }

        public string UserId { get; set; }
    }
}
