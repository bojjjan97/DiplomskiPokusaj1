using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewAuthorDTO
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Biography { get; set; }
        public ViewImageDTO Image { get; set; }
        public string ImageId { get; set; }
    }
}
