using Kruger.Marketplace.Core.Business.Notificacoes;

namespace Kruger.Marketplace.Core.Business.Interfaces.Notificador
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
