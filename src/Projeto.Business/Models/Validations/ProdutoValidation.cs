using FluentValidation;

namespace Projeto.Business.Models.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>  
    {
 
        public ProdutoValidation() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(2,200).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(2, 1000).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage(MensagemValidation.mensagemValidationMaiorQue);

        }
    }
}
