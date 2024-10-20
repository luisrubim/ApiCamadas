using Projeto.Business.Models;

namespace Projeto.Business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task Remover(Guid id);

        //Remover Endereco...
        //Atualizar Endereco...
    }
}
