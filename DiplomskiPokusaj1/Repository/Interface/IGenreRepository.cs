using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IGenreRepository
    {
        public Task<ICollection<Genre>> GetAll();
        public Task<Genre> Get(string id );
        public Task<Genre> Create(Genre genre);
        public Task<Genre> Update(string id,Genre genre);
        public Task<bool> Delete(string id);
    }
}
