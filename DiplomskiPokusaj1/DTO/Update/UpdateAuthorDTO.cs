using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Update
{
    public class UpdateAuthorDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Biography { get; set; }

        public string ImageId { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
    }
}
