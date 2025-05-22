using AutoMapper;
using Kruger.Marketplace.Core.Application.ViewModels.CadastroBasico.Categoria;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;
using System.Diagnostics.CodeAnalysis;
using Kruger.Marketplace.Core.Application.ViewModels.CadastroBasico.Produto;

namespace Kruger.Marketplace.Core.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome));
        }
    }
}