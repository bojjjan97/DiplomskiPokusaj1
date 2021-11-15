using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiPokusaj1.Repository
{
    public class GenreRepository : IGenreRepository
    {

        DBContext databaseContext;

        public GenreRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Genre> Create(Genre genre)
        {
            Genre newGenre = new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = genre.Name,
                Description = genre.Description,
                CreatedAt = DateTime.Now,
            };

            var trackedEntity = await databaseContext.Genres.AddAsync(newGenre);

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
            databaseContext.Genres.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;

        }

        public async Task<Genre> Get(string id)
        {
            return await databaseContext.Genres
                .Where(genre => genre.Id == id && genre.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Genre>> GetAll()
        {
            return await databaseContext.Genres
                .Where(genre => genre.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Genre> Update(string id, Genre genre)
        {
            var entityToUpdate = await Get(id);
            if(entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.Name = genre.Name;
            entityToUpdate.Description = genre.Description;
            entityToUpdate.UpdatedAt = DateTime.Now;

            var trackedEntity = databaseContext.Genres.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
