using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime TokenValidTo { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
