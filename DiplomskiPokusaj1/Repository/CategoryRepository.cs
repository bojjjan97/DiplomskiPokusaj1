using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6),
                Name = category.Name,
                Description = category.Description,
                Materials = new List<Material>(),
                CreatedAt = DateTime.Now

            };

            var trackedEntity = await databaseContext.Categories.AddAsync(newCategory);
            await databaseContext.SaveChangesAsync();

            return trackedEntity.Entity;
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await databaseContext.Categories
                .Where(category => category.DeletedAt == null)
                .Include(categoty => categoty.Materials).ToListAsync();
        }

        public Task<Category> Update(string id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
