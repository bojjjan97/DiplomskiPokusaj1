using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO
{
    public class MaterialsDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public int PageNumber { get; set; }
        /*public virtual Library Library { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<MaterialCopy> MaterialCopies { get; set; }
        */
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
