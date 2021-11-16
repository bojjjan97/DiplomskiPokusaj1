using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
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

        public MaterialCopyRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<MaterialCopy> Create(MaterialCopy materialCopy)
        {
            MaterialCopy materialCopy1 = new MaterialCopy
            {
                Id = Guid.NewGuid().ToString(),
                UniqueCode = materialCopy.UniqueCode,
                MaterialId = materialCopy.MaterialId
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
            return await databaseContext.MaterialCopies
               .Include(materialCopy => materialCopy.Material)
               .Where(materialCopy => materialCopy.Id == id && materialCopy.DeletedAt == null)
               .FirstOrDefaultAsync();
        }

        public async Task<ICollection<MaterialCopy>> GetAll()
        {
            return await databaseContext.MaterialCopies
              .Include(materialCopy => materialCopy.Material)
              .Where(materialCopy => materialCopy.DeletedAt == null)
              .ToListAsync();
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
    }
}
