using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class Library : IEntity
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public virtual Address Address { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Material> Materials { get; set; }

        [Required]
        public DateTime CreatedAt { get ; set; }
        public DateTime? UpdatedAt { get ; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
