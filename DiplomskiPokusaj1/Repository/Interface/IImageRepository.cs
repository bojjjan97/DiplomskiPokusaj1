using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IImageRepository
    {
        public Task<Image> Get(string id);
        public Task<Image> Create(Image image);
        public Task<bool> Delete(string id);
    }
}
