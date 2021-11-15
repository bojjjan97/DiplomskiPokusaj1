using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IMaterialRepository
    {
        public Task<ICollection<Material>> GetAll();
        public Task<Material> Get(string id);
        public Task<Material> Create(CreateMaterialDTO material);
        public Task<Material> Update(string id, UpdateMaterialDTO material);
        public Task<bool> Delete(string id);
    }
}
