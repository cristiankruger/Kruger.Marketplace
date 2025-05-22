﻿using Kruger.Marketplace.Core.Business.Interfaces.Notificador;
using Kruger.Marketplace.Core.Business.Interfaces.Services.Arquivo;
using Kruger.Marketplace.Core.Business.Models.Settings;
using Kruger.Marketplace.Core.Business.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Kruger.Marketplace.Core.Business.Services.Arquivo
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

            var filePath = $"{_environment.WebRootPath}{_arquivoSettings.BasePath.Replace("~", string.Empty)}{fileName}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }

        public async Task<bool> Save(string fileName, IFormFile file)
        {
            if (fileName == _arquivoSettings.DefaultImage || !string.IsNullOrEmpty(fileName) && file is null)
                return true;

            if (file.Length == 0)
                return NotificarError("Arquivo Corrompido ou vazio.");

            var filePath = $"{_environment.WebRootPath}{_arquivoSettings.BasePath.Replace("~",string.Empty)}{fileName}";

            if (File.Exists(filePath))
                return NotificarError("Já existe um arquivo com este nome.");

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
