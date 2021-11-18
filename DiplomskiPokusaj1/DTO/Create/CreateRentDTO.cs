using DiplomskiPokusaj1.DTO.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Create
{
    public class CreateRentDTO
    {
        public string UserId { get; set; }
        public IEnumerable<string> MaterialCopiesIds { get; set; }
        public  string ReservationId { get; set; }
    }
}
