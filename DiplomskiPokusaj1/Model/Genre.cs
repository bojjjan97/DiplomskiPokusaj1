using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class Genre : IEntity
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
        [NotMapped]
        public IEnumerable<string> MaterialsIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get ; set ; }
        public DateTime? DeletedAt { get; set; }
    }
}
