using AutoMapper;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Categoria;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.CrossCutting.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        }
    }
}