using FluentValidation;

namespace Projeto.Business.Models.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
         public EnderecoValidation() 
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(2,200).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(2, 100).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(8).WithMessage(MensagemValidation.mensagemValidationQuantidadeCaracteres);

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(2, 100).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(2, 50).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage(MensagemValidation.mensagemValidationCampoVazio)
                .Length(1, 50).WithMessage(MensagemValidation.mensagemValidationQuantidadeEntreCaracteres);

        }
    }
}
