using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<User> userManager;
        public ReservationRepository(DBContext databaseContext, UserManager<User> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
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

            if(copiesToReserve.Any(copy => copy.LibraryId != reservation.LibraryId))
            {
                return null;
            }

            Reservation newReservation = new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                Status = "reserved",
                CreatedAt = DateTime.Now,
                UserId = reservation.UserId,
                MaterialCopies = await databaseContext.MaterialCopies.Where(materialCopies => reservation.MaterialCopiesIds.Contains(materialCopies.Id)).ToListAsync(),
                LibraryId = reservation.LibraryId
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
                .Include(reservation => reservation.Rent)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Reservation>> GetAll(User userRequiringAccess)
        {
            var quariable = databaseContext.Reservations
                .Include(reservation => reservation.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Genres)
                .Include(reservation => reservation.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Categories)
              .Include(reservation => reservation.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Authors)
              .Include(reservation => reservation.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Publishers)
                .Include(reservation => reservation.Rent)
               .Where(reservation => reservation.DeletedAt == null);

            if (userRequiringAccess != null && await userManager.IsInRoleAsync(userRequiringAccess, "Librarian") && userRequiringAccess.LibraryId != null)
            {
                quariable = quariable.Where(libaray => libaray.LibraryId == userRequiringAccess.LibraryId);
            }
            else if (await userManager.IsInRoleAsync(userRequiringAccess, "Librarian"))
            {
                return new List<Reservation>();
            }

            return await quariable.ToListAsync();
        }

        public async Task<Reservation> Update(string id, UpdateReservationDTO updateReservationDTO)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            
                entityToUpdate.Status = "canceled";
            
            

            var trackedEntity = databaseContext.Reservations.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }

        public bool checkIfCopyNotAvailable(MaterialCopy materialCopy)
        {
            var reservations = databaseContext.Reservations
                .Include(reservation => reservation.MaterialCopies)
                .Where(reservation => reservation.DeletedAt == null && reservation.Status == "reserved")
                .ToList()
                .Any(reservation => reservation.MaterialCopiesIds.Contains(materialCopy.Id));

            var rents = databaseContext.Rents
                .Include(rent => rent.MaterialCopies)
                .Where(rent => rent.DeletedAt == null && rent.ReturnDate == null)
                .ToList()
                .Any(rent => rent.MaterialCopiesIds.Contains(materialCopy.Id));

            return reservations && rents;
        }
    }
}
