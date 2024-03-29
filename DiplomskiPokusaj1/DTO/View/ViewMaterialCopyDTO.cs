﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO.View
{
    public class ViewMaterialCopyDTO
    {
        public string Id { get; set; }
        public string UniqueCode { get; set; }
        public string LibraryId {get;set;}
        public bool Available { get; set; }

        public ViewMaterialDTO Material { get; set; }
        public string MaterialId { get; set; }
    }
}
