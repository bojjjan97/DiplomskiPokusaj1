using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Model
{
    public class Library : IEntity
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public virtual Image Image { get; set; }
        public string ImageId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<User> Employees { get; set; }
        public IEnumerable<string> EmployeesIds => Employees.Select(employee => employee.Id).ToList();
        public virtual ICollection<Material> Materials { get; set; }

        [Required]
        public DateTime CreatedAt { get ; set; }
        public DateTime? UpdatedAt { get ; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
