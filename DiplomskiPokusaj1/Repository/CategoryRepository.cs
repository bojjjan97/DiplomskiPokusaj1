using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiplomskiPokusaj1.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        DBContext databaseContext;

        public CategoryRepository(DBContext _databaseContext)
        {
            databaseContext = _databaseContext;
        }

        public async Task<Category> Create(Category category)
        {
            Category newCategory = new Category()
            {
                Id = Guid.NewGuid().ToString(),
                Name = category.Name,
                Description = category.Description,
                CreatedAt = DateTime.Now

            };

            var trackedEntity = await databaseContext.Categories.AddAsync(newCategory);
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
            databaseContext.Categories.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Category> Get(string id)
        {
            return await databaseContext.Categories
                .Where(category => category.Id == id && category.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await databaseContext.Categories
                .Where(category => category.DeletedAt == null).ToListAsync();
        }

        public async Task<Category> Update(string id, Category category)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.Name = category.Name;
            entityToUpdate.Description = category.Description;
            entityToUpdate.UpdatedAt = DateTime.Now;

            var trackedEntity = databaseContext.Categories.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
