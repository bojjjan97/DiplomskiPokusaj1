using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Filter
{
    public class FilterItemDTO
    {
        public string Query { get; set; }
        public IEnumerable<string> AuthorIds { get; set; }
        public IEnumerable<string> CategoryIds { get; set; }
        public IEnumerable<string> GenreIds { get; set; }
        public IEnumerable<string> PublisherIds { get; set; }
        public IEnumerable<string> LibraryIds { get; set; }
        public bool Available { get; set; }
    }
}
