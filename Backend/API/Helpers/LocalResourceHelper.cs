using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        public string GetResource(string path)
        {
            if (path == null)
            {
                return null;
            }

            string storagePath = _cnf["FileStorage:UserProfiles:Path"];
            string fullPath = Path.Combine(_env.ContentRootPath, storagePath, path);

            return fullPath;
        }

        public async Task<FilePathResult> SaveResource(IFormFile file, string userLogin)
        {
            string path = _cnf["FileStorage:UserProfiles:Path"];
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            string localPath = Path.Combine(userLogin, userLogin + "Logo" + extension);
            string directoryPath = Path.Combine(_env.ContentRootPath, path, userLogin);
            string fullPath = Path.Combine(_env.ContentRootPath, path, localPath);

            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (File.Exists(fullPath))
            {
                throw new Exception("This file was exist!");
            }

            using (var stream = File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
            }

            return new FilePathResult { LocalPath = localPath, FullPath = fullPath };
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
