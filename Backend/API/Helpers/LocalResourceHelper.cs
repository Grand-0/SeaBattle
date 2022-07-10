using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using FileResult = API.Models.FileModel;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class LocalResourceHelper
    {
        private IWebHostEnvironment _env;
        private IConfiguration _cnf;
        public LocalResourceHelper(IWebHostEnvironment env, IConfiguration cnf)
        {
            _env = env;
            _cnf = cnf;
        }

        public async Task<FileResult> GetResource(string path)
        {
            if (path == null)
            {
                return null;
            }

            string storagePath = _cnf["FileStorage:UserProfiles:Path"];
            string fullPath = Path.Combine(_env.ContentRootPath, storagePath, path);

            byte[] content = await File.ReadAllBytesAsync(fullPath);
            string type = "application/" + Path.GetExtension(fullPath);
            string name = Path.GetFileNameWithoutExtension(fullPath);

            return new FileResult { Content = content, Type = type, Name = name };
        }

        public async Task<string> SaveResource(IFormFile file, string userLogin)
        {
            string path = _cnf["FileStorage:UserProfiles:Path"];
            string localPath = Path.Combine(userLogin, Path.GetRandomFileName());

            string fullPath = Path.Combine(_env.ContentRootPath, path, localPath);

            if (File.Exists(fullPath))
            {
                throw new Exception("This file was exist!");
            }

            using (var stream = File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
            }

            return localPath;
        }

        public bool IsResourceValid(IFormFile file)
        {
            if (file.Length > 100000) 
            {
                return false;
            }

            return true;
        }
    }
}
