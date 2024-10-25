using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projeto.Business.Interfaces;
using System.Net;

namespace Projeto.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
        {
            if (OperacaoValida())
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode)
                };

            return BadRequest(new
            {
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = ModelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception != null ? erro.Exception.Message : erro.ErrorMessage;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Business.Notificacoes.Notificacao(mensagem));
        }

    }
}
