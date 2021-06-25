using catalagoJogos.WebApi.Exceptions;
using catalagoJogos.WebApi.InputModel;
using catalagoJogos.WebApi.Services;
using catalagoJogos.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catalagoJogos.WebApi.Controllers.v1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService jogoService;

        public JogosController(IJogoService jogoService)
        {
            this.jogoService = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, int.MaxValue)] int quantidade = 5)
        {
            var jogos = await jogoService.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid IdJogo)
        {
            try
            {
                var jogo = await jogoService.Obter(IdJogo);

                if (jogo == null)
                    return NoContent();

                return Ok(jogo);
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este Jogo: " + ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody]JogoInputModel jogoInputModel)
        {
            try
            {
                var novoJogo = await jogoService.Inserir(jogoInputModel);

                return Ok(novoJogo);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora: " + ex);
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromBody]JogoInputModel jogoInputModel)
        {
            try
            {
                await jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este Jogo: " + ex);
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:decimal}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute]decimal preco)
        {
            try
            {
                await jogoService.Atualizar(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este Jogo: " + ex);
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute]Guid idJogo)
        {
            try
            {
                await jogoService.Remover(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este Jogo: " + ex);
            }
        }

    }
}
