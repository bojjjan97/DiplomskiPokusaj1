using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IAuthorRepository
    {
        public Task<ICollection<Author>> GetAll(string authorId = null);
        public Task<Author> Get(string id);
        public Task<Author> Create(Author author);
        public Task<Author> Update(string id, Author author);
        public Task<bool> Delete(string id);
    }
}
