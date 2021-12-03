using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IReservationRepository
    {
        public Task<ICollection<Reservation>> GetAll(User userRequiringAccess);
        public Task<Reservation> Get(string id);
        public Task<Reservation> Create(CreateReservationDTO author);
        public Task<Reservation> Update(string id, UpdateReservationDTO author);
        public Task<bool> Delete(string id);
    }
}
