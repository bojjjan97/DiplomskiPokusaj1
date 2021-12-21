using DiplomskiPokusaj1.DTO.Filter;
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
    public class MaterialCopyRepository : IMaterialCopyRepository
    {
        DBContext databaseContext;
        private UserManager<User> userManager;

        public MaterialCopyRepository(DBContext databaseContext, UserManager<User> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }

        public async Task<MaterialCopy> Create(MaterialCopy materialCopy)
        {
            MaterialCopy materialCopy1 = new MaterialCopy
            {
                Id = Guid.NewGuid().ToString(),
                UniqueCode = materialCopy.UniqueCode,
                MaterialId = materialCopy.MaterialId,
                LibraryId = materialCopy.LibraryId
            };

            var trackedEntity = await databaseContext.MaterialCopies.AddAsync(materialCopy1);

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
            databaseContext.MaterialCopies.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<MaterialCopy> Get(string id)
        {   

            MaterialCopy copy = await databaseContext.MaterialCopies
               .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Genres)
               .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Categories)
               .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Publishers)
               .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Authors)
               .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Image)
               .Where(materialCopy => materialCopy.Id == id && materialCopy.DeletedAt == null)
               .FirstOrDefaultAsync();

            copy.Available = !checkIfCopyNotAvailable(copy);

            return copy;
        }

        public async Task<ICollection<MaterialCopy>> GetAll(FilterMaterialCopyDTO filter, User userRequiringAccess)
        {

            var quariable = databaseContext.MaterialCopies
              .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Genres)
              .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Categories)
              .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Publishers)
              .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Authors)
              .Include(materialCopy => materialCopy.Material).ThenInclude(material => material.Image)
              .Where(materialCopy => materialCopy.DeletedAt == null && materialCopy.Material.DeletedAt == null);

            if(filter.LibraryId != null)
            {
                quariable = quariable.Where(libaray => libaray.LibraryId == filter.LibraryId);
            }

            if (filter.MaterialId != null)
            {
                quariable = quariable.Where(material => material.MaterialId == filter.MaterialId);
            }
            
            if (userRequiringAccess != null && await userManager.IsInRoleAsync(userRequiringAccess, "Librarian") && userRequiringAccess.LibraryId != null)
            {
                quariable = quariable.Where(libaray => libaray.LibraryId == userRequiringAccess.LibraryId);
            }
            else if(userRequiringAccess != null &&  await userManager.IsInRoleAsync(userRequiringAccess, "Librarian"))
            {
                return new List<MaterialCopy>();
            }

            var resultList = await quariable
               .ToListAsync();

            foreach(var c in resultList)
            {
                c.Available = !checkIfCopyNotAvailable(c);
            }

            if (filter.Available != null)
            {
                resultList = resultList.Where(material => material.Available == filter.Available).ToList();
            }


            return resultList;
        }

        public async Task<MaterialCopy> Update(string id, MaterialCopy materialCopy)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.UniqueCode = materialCopy.UniqueCode;
            entityToUpdate.UpdatedAt = DateTime.Now;

            var trackedEntity = databaseContext.MaterialCopies.Update(entityToUpdate);
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
