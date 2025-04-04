using Kruger.Marketplace.Business.Notificacoes;

namespace Kruger.Marketplace.Business.Interfaces.Notificador
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
