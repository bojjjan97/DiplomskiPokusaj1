using DiplomskiPokusaj1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository
{
    public class PublisherRepository : IPublisherRepository
    {

        DBContext databaseContext;

        public PublisherRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public async Task<Publisher> Create(Publisher publisher)
        {
            Publisher newPublisher = new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                Name = publisher.Name,
                Description = publisher.Description,
                CreatedAt = DateTime.Now,
                Address = new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    Line1 = publisher.Address.Line1,
                    Line2 = publisher.Address.Line2,
                    City = publisher.Address.City,
                    PostalCode = publisher.Address.PostalCode,
                    Country = publisher.Address.Country,
                    CreatedAt = DateTime.Now

                }

            };

            var trackedEntity = await databaseContext.Publishers.AddAsync(newPublisher);

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
            databaseContext.Publishers.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Publisher> Get(string id)
        {
            return await databaseContext.Publishers
                .Include(publisher => publisher.Address)
                .Where(genre => genre.Id == id && genre.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Publisher>> GetAll()
        {
            return await databaseContext.Publishers
                .Include(publisher => publisher.Address)
                .Where(publisher => publisher.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Publisher> Update(string id, Publisher publisher)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.Name = publisher.Name;
            entityToUpdate.Description = publisher.Description;
            entityToUpdate.UpdatedAt = DateTime.Now;

            entityToUpdate.Address.Line1 = publisher.Address.Line1;
            entityToUpdate.Address.Line2 = publisher.Address.Line2;
            entityToUpdate.Address.City = publisher.Address.City;
            entityToUpdate.Address.PostalCode = publisher.Address.PostalCode;
            entityToUpdate.Address.Country = publisher.Address.Country;

            var trackedEntity = databaseContext.Publishers.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
