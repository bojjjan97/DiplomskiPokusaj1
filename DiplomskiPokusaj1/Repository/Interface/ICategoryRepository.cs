using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Task<ICollection<Category>> GetAll();
        public Task<Category> Get(string id);
        public Task<Category> Create(Category category);
        public Task<Category> Update(string id, Category category);
        public Task<bool> Delete(string id);


    }
}
