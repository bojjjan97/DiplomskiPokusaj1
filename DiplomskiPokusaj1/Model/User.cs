using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class User : IdentityUser, IEntity
    {   
        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public string ClientId { get; set; }

        public virtual Image Image { get; set; }
        public string ImageId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        public virtual Library Library { get; set; }
        public string LibraryId { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        [Required]
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }
    }
}
