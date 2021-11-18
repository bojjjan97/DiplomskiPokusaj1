using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IRentRepository
    {
        public Task<ICollection<Rent>> GetAll();
        public Task<Rent> Get(string id);
        public Task<Rent> Create(CreateRentDTO rent);
        public Task<bool> Delete(string id);
        public  Task<Rent> checkIn(string id);
    }
}
