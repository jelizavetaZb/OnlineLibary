using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OnlineLibary.Managers.Managers;

namespace OnlineLibary.Managers.Helpers
{
    public class FileHelper : BaseManager
    {
        private readonly IConfiguration _config;
        public FileHelper(IConfiguration config, IMapper mapper) : base(mapper)
        {
            _config = config;
        }

        public async Task<string> UploadFile(IFormFile file, string folderName)
        {
            var path = _config["System:SiteFilesPath"] + folderName + @"\\";
            Directory.CreateDirectory(path);
            var virtualPath = _config["System:VirtualSiteFilesPath"] + folderName + @"/";
            var fileName = Guid.NewGuid() + file.FileName;

            var upload = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(upload, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Path.Combine(virtualPath, fileName);

        }
    }
}

