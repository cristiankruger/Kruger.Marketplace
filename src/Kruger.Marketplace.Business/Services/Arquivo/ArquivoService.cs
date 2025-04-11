using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Services.Arquivo;
using Kruger.Marketplace.Business.Models.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Kruger.Marketplace.Business.Services.Arquivo
{
    //TODO: IN PRODUCTION SHOULD SAVE FILE TO AWS S3, Azure BlobStorage Or Server Folder!
    public class ArquivoService(INotificador notificador,
                                IWebHostEnvironment environment,
                                IOptions<ArquivoSettings> arquivoSettings) : BaseService(notificador), IArquivoService
    {
        private readonly ArquivoSettings _arquivoSettings = arquivoSettings.Value;
        private readonly IWebHostEnvironment _environment = environment;

        public bool Delete(string fileName)
        {
            if (fileName == _arquivoSettings.DefaultImage) return true;

            var filePath = $"{_arquivoSettings.BasePath}{_arquivoSettings.Container}{fileName}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }

        public async Task<bool> Save(string fileName, IFormFile file)
        {
            if (fileName == _arquivoSettings.DefaultImage || (!string.IsNullOrEmpty(fileName) && file is null))
                return true;

            if (file.Length == 0)
            {
                NotificarError("Arquivo Corrompido ou vazio.");
                return false;
            }

            var filePath = $"{_environment.WebRootPath}{_arquivoSettings.BasePath.Replace("~", string.Empty)}{_arquivoSettings.Container}{fileName}";

            using Stream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
