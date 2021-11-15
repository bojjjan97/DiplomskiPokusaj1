using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class Reservation : IEntity
    {
        public string Id { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MaterialCopy> MaterialCopies { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        [Required]
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }

    }
}
