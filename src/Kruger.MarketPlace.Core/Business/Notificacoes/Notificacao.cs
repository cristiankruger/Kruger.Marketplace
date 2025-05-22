namespace Kruger.Marketplace.Core.Business.Notificacoes
{
    public class Notificacao(string mensagem)
    {
        public string Mensagem { get; } = mensagem;
    }
}
