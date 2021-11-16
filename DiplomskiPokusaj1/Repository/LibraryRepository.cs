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
    public class LibraryRepository : ILibraryRepository
    {

        DBContext databaseContext;

        public LibraryRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Library> Create(CreateLibraryDTO library)
        {

            
            Library newLibrary = new Library
            {
                Id = Guid.NewGuid().ToString(),
                Name = library.Name,
                CreatedAt = DateTime.Now,
                Telephone = library.Telephone,
                Email = library.Email,
                Employees = await databaseContext.Users.Where(user => library.EmployeesIds.Contains(user.Id)).ToListAsync(),
                Address = new Address
                {
                    Line1 = library.Address.Line1,
                    Line2 = library.Address.Line2,
                    City = library.Address.City,
                    PostalCode = library.Address.PostalCode,
                    Country = library.Address.Country,
                }

            };


            var trackedEntity = await databaseContext.Libraries.AddAsync(newLibrary);

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
            databaseContext.Libraries.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Library> Get(string id)
        {
            return await databaseContext.Libraries
                .Include(library => library.Employees)
                .Include(library => library.Address)
                .Where(library => library.Id == id && library.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Library>> GetAll()
        {
            return await databaseContext.Libraries
               .Include(library => library.Employees)
               .Include(library => library.Address)
               .Where(library => library.DeletedAt == null)
               .ToListAsync();
        }

        public async Task<Library> Update(string id, UpdateLibraryDTO updateLibraryDTO)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.Name = updateLibraryDTO.Name;
            entityToUpdate.Telephone = updateLibraryDTO.Telephone;
            entityToUpdate.Email = updateLibraryDTO.Email;
            entityToUpdate.Address.Line1 = updateLibraryDTO.Address.Line1;
            entityToUpdate.Address.Line2 = updateLibraryDTO.Address.Line2;
            entityToUpdate.Address.City = updateLibraryDTO.Address.City;
            entityToUpdate.Address.PostalCode = updateLibraryDTO.Address.PostalCode;
            entityToUpdate.Address.Country = updateLibraryDTO.Address.Country;
            entityToUpdate.Employees = await databaseContext.Users.Where(user => updateLibraryDTO.EmployeesIds.Contains(user.Id)).ToListAsync();

            entityToUpdate.UpdatedAt = DateTime.Now;

            var trackedEntity = databaseContext.Libraries.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
