using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Storage.Interface
{
    public interface IFileMenager
    {
        public Task<string> SaveFile(IFormFile formFile);
        public Task<string> SaveFile(string base64EncodedFile);
        public Task<byte[]> ReadFile(string path);
    }
}
