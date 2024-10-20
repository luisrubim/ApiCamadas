using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Business.Models.Validations;

namespace Projeto.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly string mensagemExisteProdutoMesmoNome = "Já existe um produto com este Nome informado.";


        public ProdutoService(IProdutoRepository produtoRepository
            , INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            if(_produtoRepository.Buscar(p => p.Nome.Equals(produto.Nome)).Result.Any())
            {
                Notificar(mensagemExisteProdutoMesmoNome);
                return;
            }

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            if (_produtoRepository.Buscar(p => p.Nome.Equals(produto.Nome) && p.Id != produto.Id).Result.Any())
            {
                Notificar(mensagemExisteProdutoMesmoNome);
                return;
            }

            await _produtoRepository.Atualizar(produto);  
        }       

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();   
        }
    }
}
