using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class Material : IEntity
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public int PageNumber { get; set; }

        public virtual Image Image { get; set; }
        public string ImageId { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public IEnumerable<string> CategoriesIds => Categories.Select(c => c.Id).ToList(); 

        public virtual ICollection<Genre> Genres { get; set; }
        public IEnumerable<string> GenresIds => Genres.Select(c => c.Id).ToList();

        public virtual ICollection<Publisher> Publishers { get; set; }
        public IEnumerable<string> PublisersIds => Publishers.Select(c => c.Id).ToList();

        public virtual ICollection<Author> Authors { get; set; }
        public IEnumerable<string> AuthorsIds => Authors.Select(c => c.Id).ToList();


        public virtual ICollection<MaterialCopy> MaterialCopies { get; set; }
        public IEnumerable<string> MaterialsCopiesIds => MaterialCopies.Select(c => c.Id).ToList();

        [Required]
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }
    }
}
