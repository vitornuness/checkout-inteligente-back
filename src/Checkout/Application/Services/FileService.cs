namespace Application.Services;

using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Http;

public class FileService : IFileService
{
    public async Task SaveFile(IFormFile file, string path)
    {
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    public async Task RemoveFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
