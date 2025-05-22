using FluentValidation.Results;
using Kruger.Marketplace.Core.Business.Notificacoes;
using Kruger.Marketplace.Core.Business.Interfaces.Notificador;
using Kruger.Marketplace.Core.Business.Models;

namespace Kruger.Marketplace.Core.Business.Services
{
    public abstract class BaseService(INotificador notificador)
    {
        private readonly INotificador _notificador = notificador;

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notificar(error.ErrorMessage);
        }

        protected void Notificar(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }

        protected bool IsValid<TE>(TE entity) where TE : Entity
        {
            if (entity.IsValid()) return true;

            Notificar(entity.ValidationResult);
            return false;
        }

        protected bool NotificarError(string errorMessage, bool ret = false)
        {
            Notificar(errorMessage);
            return ret;
        }

        protected TE NotificarError<TE>(string errorMessage)
        {
            Notificar(errorMessage);
            return default;
        }
    }
}