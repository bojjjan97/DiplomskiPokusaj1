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
        public string UniqueCode { get; set; }
        [NotMapped]
        public bool Available { get; set; }  

        public Library Library { get; set; }
        public string LibraryId { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Rent>  Rents { get; set; }
        public virtual Material Material { get; set; }
        public string MaterialId { get; set; }
        [Required]
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public DateTime? DeletedAt { get; set ; }
    }
}
