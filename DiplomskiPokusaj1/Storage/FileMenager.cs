
using DiplomskiPokusaj1.Storage.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Storage
{
    public class FileMenager : IFileMenager
    {
        public IConfiguration configuration;

        public FileMenager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<byte[]> ReadFile(string path)
        {
            if (path != null)
            {
                return await File.ReadAllBytesAsync(path);
            }
            return null;
        }

        public async Task<string> SaveFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var fileDirectory = configuration["StoredFilesPath"];
                var fileName = Path.GetRandomFileName();
                var filePath = Path.Combine(fileDirectory, fileName);

                Directory.CreateDirectory(fileDirectory);
                using (var stream = File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                return filePath;
            }
            return null;
        }

        public async Task<string> SaveFile(string base64EncodedFile)
        {
            if (base64EncodedFile != null && base64EncodedFile.Length > 0)
            {
                var fileDirectory = configuration["StoredFilesPath"];
                var fileName = Path.GetRandomFileName();
                var filePath = Path.Combine(fileDirectory, fileName);

                Directory.CreateDirectory(fileDirectory);
                await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(base64EncodedFile));

                return filePath;
            }
            return null;
        }
    }
}
