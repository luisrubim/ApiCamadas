using Microsoft.AspNetCore.Mvc;
using Projeto.Api.ViewModel;

namespace Projeto.Api.Controllers
{
    [Route("api/produtos")]
    public class FornecedoresController : MainController
    {
        public FornecedoresController()
        {
            
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
        }

        [HttpGet("{id:guid}")]
        public async Task<IEnumerable<FornecedorViewModel>> ObterPorId(Guid id)
        {

        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {

        }

        [HttpPut("{id: guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {

        }

        [HttpDelete("{id: guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {

        }
    }
}
