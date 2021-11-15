using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class MaterialCopy : IEntity
    {   
        [Required]
        public string Id { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Rent>  Rents { get; set; }
        public virtual Material Material { get; set; }
        [Required]
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public DateTime? DeletedAt { get; set ; }
    }
}
