using FluentValidation;
using FluentValidation.Results;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Business.Notificacoes;

namespace Projeto.Business.Services
{
    public abstract class BaseService
    {
        protected INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificar(item.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV,TE>(TV validacao, TE entidade) 
            where TV : AbstractValidator<TE> 
            where TE : Entity
        {

            //return validacao.Validate(entidade).IsValid;

            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
