using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO
{
    public class CategoryDTO
    {
        public string Id { get; set; }
       
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MaterialsDTO> Materials { get; set; }
        public virtual ICollection<string> MaterialsIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
