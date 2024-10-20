using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Business.Models.Validations;
using System.ComponentModel;

namespace Projeto.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly string mensagemExisteFornecedorDocumento = "Já existe um fornecedor com este documento informado.";

        public FornecedorService(IFornecedorRepository fornecedorRepository,
            INotificador notificador) :base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) 
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

            if(_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar(mensagemExisteFornecedorDocumento);
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento &&
                                                  f.Id != fornecedor.Id).Result.Any())
            {
                Notificar(mensagemExisteFornecedorDocumento);
                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);  
        }       

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);

            if(fornecedor == null)
            {
                Notificar("Fornecedor não existe!");
                return;
            }

            if (fornecedor.Produtos.Any())
            {
                Notificar("Fornecedor possui produtos cadastrados!");
                return;
            }

            var endereco = await _fornecedorRepository.ObterEnderecoPorFornecedor(id);
            if (endereco != null)
                 await _fornecedorRepository.RemoverEnderecoFornecedor(endereco);

            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();   
        }
    }
}
