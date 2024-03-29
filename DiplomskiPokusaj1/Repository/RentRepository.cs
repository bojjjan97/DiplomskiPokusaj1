﻿using DiplomskiPokusaj1.DTO.Create;
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
    public class RentRepository : IRentRepository
    {

        DBContext databaseContext;
        private UserManager<User> userManager;

        public RentRepository(DBContext databaseContext, UserManager<User> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }

        public async Task<Rent> Create(CreateRentDTO rent)
        {

            List<MaterialCopy> listToRent= new List<MaterialCopy>();
 
            
            if (rent.ReservationId != null)
            {
                var reservation = await databaseContext.Reservations.Where(reservation => reservation.Id == rent.ReservationId)
                    .Where(reservation => reservation.Status == "reserved")
                    .Include(reservation => reservation.MaterialCopies)
                    .Include(reservation => reservation.Rent)
                    .FirstOrDefaultAsync();

                if(reservation != null)
                { 
                    listToRent.AddRange(reservation.MaterialCopies);
                    reservation.Status = "taken";
                }
                else
                {
                    return null;
                }
            }

            
           
            var copiesToAdd2 = databaseContext.MaterialCopies.Where(materialCopy => rent.MaterialCopiesIds.Contains(materialCopy.Id)).ToArray();

            if (copiesToAdd2.Count() > 0)
            { 
                listToRent.AddRange(copiesToAdd2);
            }
            
            if (listToRent.Any(copy => copy.LibraryId != rent.LibraryId))
            {
                return null;
            }
            
            Rent newRent = new Rent
            {
                Id = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                UserId = rent.UserId,
                MaterialCopies = listToRent,
                ReservationId = rent.ReservationId,
                LibraryId = rent.LibraryId
            };

            var trackedEntity = await databaseContext.Rents.AddAsync(newRent);

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
            databaseContext.Rents.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Rent> Get(string id)
        {
            return await databaseContext.Rents
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Genres)
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Categories)
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Authors)
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Publishers)


                .Include(rent => rent.Reservation)
                .Include(rent => rent.User)
                .Where(rent => rent.Id == id && rent.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Rent>> GetAll(User userRequiringAccess)
        {
            var quariable =  databaseContext.Rents
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Genres)
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Categories)
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Authors)
              .Include(rent => rent.MaterialCopies)
                   .ThenInclude(materialCopy => materialCopy.Material).ThenInclude(material => material.Publishers)
              .Include(rent => rent.Reservation)
              .Include(rent => rent.User)
              .Where(rent => rent.DeletedAt == null);

            if (userRequiringAccess != null && await userManager.IsInRoleAsync(userRequiringAccess, "Librarian") && userRequiringAccess.LibraryId != null)
            {
                quariable = quariable.Where(libaray => libaray.LibraryId == userRequiringAccess.LibraryId);
            }
            else if (await userManager.IsInRoleAsync(userRequiringAccess, "Librarian"))
            {
                return new List<Rent>();
            }

            return await quariable.ToListAsync();
        }

        public async Task<Rent> checkIn(string id)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.ReturnDate = DateTime.Now;
            entityToUpdate.UpdatedAt = DateTime.Now;

            var trackedEntity = databaseContext.Rents.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
