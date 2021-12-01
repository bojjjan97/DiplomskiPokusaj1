using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository
{
    public class ImageRepository : IImageRepository
    {
        DBContext databaseContext;

        public ImageRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Image> Create(Image image)
        {
            Image entityToCreate = new Image()
            {
                Id = Guid.NewGuid().ToString(),
                FileName = image.FileName,
                FilePath = image.FilePath,
                CreatedAt = DateTime.Now,
                
            };

            entityToCreate.PhotoUrl = "/api/Image/" + entityToCreate.Id;

            await databaseContext.Images.AddAsync(entityToCreate);
            await databaseContext.SaveChangesAsync();

            return entityToCreate;

        }

        public async Task<bool> Delete(string id)
        {
            var image = await Get(id);
            if(image == null)
            {
                return false;
            }
            image.DeletedAt = DateTime.Now;

            databaseContext.Images.Update(image);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Image> Get(string id)
        {
            return await databaseContext.Images
                .Where(image => image.Id == id)
                .FirstOrDefaultAsync();
        }

    
       
    }
}
