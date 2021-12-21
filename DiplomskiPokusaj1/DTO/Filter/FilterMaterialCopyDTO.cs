using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.Filter
{
    public class FilterMaterialCopyDTO
    {
        public string LibraryId { get; set; }
        public string MaterialId { get; set; }
        public bool? Available { get; set; }
    }
}
