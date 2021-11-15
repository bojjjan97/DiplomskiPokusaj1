using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class Author : IEntity
    {
        public string Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Biography { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
        public DateTime CreatedAt { get; set ; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
