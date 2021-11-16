using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;
using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface ILibraryRepository
    {
        public Task<ICollection<Library>> GetAll();
        public Task<Library> Get(string id);
        public Task<Library> Create(CreateLibraryDTO library);
        public Task<Library> Update(string id, UpdateLibraryDTO library);
        public Task<bool> Delete(string id);
    }
}
