using AutoMapper;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Categoria;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using System.Diagnostics.CodeAnalysis;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Produto;

namespace Kruger.Marketplace.Application.Configurations
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