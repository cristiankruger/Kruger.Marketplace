﻿using Microsoft.AspNetCore.Http;

namespace Kruger.Marketplace.Business.Interfaces.Services.Arquivo
{
    public interface IArquivoService:IDisposable
    {
        Task<bool> Save(string fileName, IFormFile file);
        bool Delete(string fileName);
    }
}
