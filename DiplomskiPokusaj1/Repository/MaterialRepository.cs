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

        public async Task<bool> Delete(string id)
        {
            var entityToDelete = await Get(id);
            if (entityToDelete == null)
            {
                return false;
            }
            entityToDelete.DeletedAt = DateTime.Now;
            databaseContext.Materials.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
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

        public async Task<ICollection<Material>> GetAll(string authorId = null)
        {   

            if(authorId == null)
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
            else
            {
                return await databaseContext.Materials
               .Include(material => material.Authors)
                    .Where( x => x.Authors.Select( y => y.Id).Contains(authorId))
               .Include(material => material.Categories)
               .Include(material => material.Genres)
               .Include(material => material.Publishers)
               .Include(material => material.MaterialCopies)
               .Where(publisher => publisher.DeletedAt == null )
               .ToListAsync();
            }
        }

        public async Task<Material> Update(string id, UpdateMaterialDTO material)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.Title = material.Title;
            entityToUpdate.Description = material.Description;
            entityToUpdate.Isbn = material.Isbn;
            entityToUpdate.PageNumber = material.PageNumber;
            entityToUpdate.UpdatedAt = DateTime.Now;

            entityToUpdate.Authors = await databaseContext.Authors.Where(author => material.AuthorsIds.Contains(author.Id)).ToListAsync();
            entityToUpdate.Categories = await databaseContext.Categories.Where(category => material.CategoriesIds.Contains(category.Id)).ToListAsync();
            entityToUpdate.Genres = await databaseContext.Genres.Where(genre => material.GenresIds.Contains(genre.Id)).ToListAsync();
            entityToUpdate.Publishers = await databaseContext.Publishers.Where(publisher => material.PublisersIds.Contains(publisher.Id)).Include(publisher => publisher.Address).ToListAsync();


            var trackedEntity = databaseContext.Materials.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
