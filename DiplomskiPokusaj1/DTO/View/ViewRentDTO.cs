using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewRentDTO
    {
        public string Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public  ViewPublicUserDTO User { get; set; }
        public  ICollection<ViewMaterialCopyDTO> MaterialCopies { get; set; }
        public IEnumerable<string> MaterialCopiesIds { get; set; }
        public virtual ViewReservationDTO Reservation { get; set; }
    }
}
