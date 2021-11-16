using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        DBContext databaseContext;

        public ReservationRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Reservation> Create(CreateReservationDTO reservation)
        {

            var copiesToReserve = await databaseContext.MaterialCopies
                .Where(copy => reservation.MaterialCopiesIds.Contains(copy.Id))
                .ToListAsync();
            var isAnyCopyAlreadyReserved = copiesToReserve.Any(copy => checkIfCopyNotAvailable(copy));

            if(isAnyCopyAlreadyReserved)
            {
                return null;
            }


            Reservation newReservation = new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Status = "reserved",
                CreatedAt = DateTime.Now,
                MaterialCopies = await databaseContext.MaterialCopies.Where(materialCopies => reservation.MaterialCopiesIds.Contains(materialCopies.Id)).ToListAsync()
                
            };

            var trackedEntity = await databaseContext.Reservations.AddAsync(newReservation);

            await databaseContext.SaveChangesAsync();

            return trackedEntity.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var entityToDelete = await Get(id);
            if (entityToDelete == null)
            {
                return false;
            }
            entityToDelete.DeletedAt = DateTime.Now;
            databaseContext.Reservations.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Reservation> Get(string id)
        {
            return await databaseContext.Reservations
                .Include(reservation => reservation.MaterialCopies)
                .ThenInclude(materialCopies => materialCopies.Material)
                .Where(reservation => reservation.Id == id && reservation.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Reservation>> GetAll()
        {
            return await databaseContext.Reservations
                .Include(reservation => reservation.MaterialCopies)
                .ThenInclude(materialCopies => materialCopies.Material)
               .Where(reservation => reservation.DeletedAt == null).ToListAsync();
        }

        public async Task<Reservation> Update(string id, UpdateReservationDTO updateReservationDTO)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            if(updateReservationDTO.Status == "reserved")
            {
                entityToUpdate.Status = "canceled";
            }
            

            var trackedEntity = databaseContext.Reservations.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }

        public bool checkIfCopyNotAvailable(MaterialCopy materialCopy)
        {
            var reservations = databaseContext.Reservations
                .Include(reservation => reservation.MaterialCopies)
                .Any(reservation => reservation.MaterialCopiesIds.Contains(materialCopy.Id) &&
                reservation.DeletedAt == null && reservation.Status == "reserved");

            var rents = databaseContext.Rents
                .Include(rent => rent.MaterialCopies)
                .Any(rent => rent.MaterialCopiesIds.Contains(materialCopy.Id) &&
                rent.DeletedAt == null && rent.CheckedIn == false);

            return reservations && rents;
        }
    }
}
