namespace Projeto.Business.Models.Validations
{
    public static class MensagemValidation
    {
        public static string mensagemValidationCampoVazio = 
            "O campo {PropertyName} precisa ser fornecido(a)";

        public static string mensagemValidationQuantidadeEntreCaracteres = 
            "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";

        public static string mensagemValidationQuantidadeCaracteres =
            "O campo {PropertyName} precisa ter {MaxLength} caracteres";

        public static string mensagemValidationMaiorQue = 
            "O campo {PropertyName} precisa ser maior que {ComparisonValue}";

    }
}
