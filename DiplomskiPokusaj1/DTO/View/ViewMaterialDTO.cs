using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewMaterialDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public int PageNumber { get; set; }

        public  ICollection<ViewCategoryDTO> Categories { get; set; }
        public IEnumerable<string> CategoriesIds { get; set; }

        public  ICollection<ViewGenreDTO> Genres { get; set; }
        public IEnumerable<string> GenresIds { get; set; }

        public  ICollection<ViewPublisherDTO> Publishers { get; set; }
        public IEnumerable<string> PublisersIds { get; set; }

        public  ICollection<ViewAuthorDTO> Authors { get; set; }
        public IEnumerable<string> AuthorsIds { get; set; }

    }
}
