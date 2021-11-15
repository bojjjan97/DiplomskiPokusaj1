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
    public class MaterialRepository : IMaterialRepository
    {
        DBContext databaseContext;

        public MaterialRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Material> Create(CreateMaterialDTO material)
        {
            Material newMaterial = new Material
            {
                Id = Guid.NewGuid().ToString(),
                Title = material.Title,
                Isbn = material.Isbn,
                Description = material.Description,
                PageNumber = material.PageNumber,
                CreatedAt = DateTime.Now,
                Authors = await databaseContext.Authors.Where(author => material.AuthorsIds.Contains(author.Id)).ToListAsync(),
                Categories = await databaseContext.Categories.Where(category => material.CategoriesIds.Contains(category.Id)).ToListAsync(),
                Genres = await databaseContext.Genres.Where(genre => material.GenresIds.Contains(genre.Id)).ToListAsync(),
                Publishers = await databaseContext.Publishers.Where(publisher => material.PublisersIds.Contains(publisher.Id)).Include(publisher => publisher.Address).ToListAsync(),
                MaterialCopies = new List<MaterialCopy>()

            };

            foreach (var materialCopyCode in material.MaterialsCopiesCode)
            {
                newMaterial.MaterialCopies.Add(new MaterialCopy()
                {
                    Id = Guid.NewGuid().ToString(),
                    UniqueCode = materialCopyCode,
                    CreatedAt = DateTime.Now

                });
            }

            var trackedEntity = await databaseContext.Materials.AddAsync(newMaterial);

            await databaseContext.SaveChangesAsync();

            return trackedEntity.Entity;
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Material> Get(string id)
        {
            return await databaseContext.Materials
               .Include(material => material.Authors)
               .Include(material => material.Categories)
               .Include(material => material.Genres)
               .Include(material => material.Publishers)
               .Include(material => material.MaterialCopies)
               .Where(material => material.Id == id && material.DeletedAt == null)
               .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Material>> GetAll()
        {
            return await databaseContext.Materials
               .Include(material => material.Authors)
               .Include(material => material.Categories)
               .Include(material => material.Genres)
               .Include(material => material.Publishers)
               .Include(material => material.MaterialCopies)
               .Where(publisher => publisher.DeletedAt == null)
               .ToListAsync();
        }

        public Task<Material> Update(string id, UpdateMaterialDTO material)
        {
            throw new NotImplementedException();
        }
    }
}
