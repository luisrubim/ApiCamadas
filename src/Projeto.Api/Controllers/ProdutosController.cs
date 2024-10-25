using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projeto.Api.ViewModel;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using System.Net;

namespace Projeto.Api.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService,
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(
                await _produtoRepository.ObterProdutosFornecedores());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produtoViewModel = await this.ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return produtoViewModel;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(HttpStatusCode.Created, produtoViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse(ModelState);
            }

            var produtoAtualizacao = await ObterProduto(id);

            //Não mapear automaticamente pois pode não querer atualizar todos os dados enviados
            //Poderia por essa parte na service também, fica a critério
            produtoAtualizacao.FornecedorId = produtoViewModel.FornecedorId;
            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return  _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
        }
    }
}
