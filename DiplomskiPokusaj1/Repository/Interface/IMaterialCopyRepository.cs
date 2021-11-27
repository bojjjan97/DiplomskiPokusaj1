using DiplomskiPokusaj1.DTO.Filter;
using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IMaterialCopyRepository
    {
        public Task<ICollection<MaterialCopy>> GetAll(FilterMaterialCopyDTO filter);
        public Task<MaterialCopy> Get(string id);
        public Task<MaterialCopy> Create(MaterialCopy materialCopy);
        public Task<MaterialCopy> Update(string id, MaterialCopy materialCopy);
        public Task<bool> Delete(string id);
    }
}
