using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Create
{
    public class CreateMaterialDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public int PageNumber { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }


        public IEnumerable<string> CategoriesIds { get; set; }

        
        public IEnumerable<string> GenresIds { get; set; }

       
        public IEnumerable<string> PublisersIds { get; set; }
       
        public IEnumerable<string> AuthorsIds { get; set; }

       
        public IEnumerable<string> MaterialsCopiesCode { get; set; }
    }
}
