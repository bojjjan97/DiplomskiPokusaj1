using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository
{
    public interface IPublisherRepository
    {
        public Task<ICollection<Publisher>> GetAll();
        public Task<Publisher> Get(string id);
        public Task<Publisher> Create(Publisher publisher);
        public Task<Publisher> Update(string id, Publisher publisher);
        public Task<bool> Delete(string id);
    }
}
