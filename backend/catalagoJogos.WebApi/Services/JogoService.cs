using catalagoJogos.WebApi.Entities;
using catalagoJogos.WebApi.Exceptions;
using catalagoJogos.WebApi.InputModel;
using catalagoJogos.WebApi.Repositories;
using catalagoJogos.WebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalagoJogos.WebApi.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            this.jogoRepository = jogoRepository;
        }

        public async Task Atualizar(Guid id, JogoInputModel jogoUpdate)
        {
            var jogo = await jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            jogo.Nome = jogoUpdate.Nome;
            jogo.Produtora = jogoUpdate.Produtora;
            jogo.Preco = jogoUpdate.Preco;

            await jogoRepository.Atualizar(jogo);

        }

        public async Task Atualizar(Guid id, decimal preco)
        {
            var jogo = await jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            jogo.Preco = preco;

            await jogoRepository.Atualizar(jogo);
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var verificarJogo = await jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (verificarJogo.Count() > 0)
                throw new JogoJaCadastradoException();

            var novoJogo = new Jogo {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await jogoRepository.Inserir(novoJogo);

            return new JogoViewModel
            {
                Id = novoJogo.Id,
                Nome = novoJogo.Nome,
                Produtora = novoJogo.Produtora,
                Preco = novoJogo.Preco
            };

        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid Id)
        {
            var jogo = await jogoRepository.Obter(Id);

            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = await jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            jogoRepository?.Dispose();
        }
    }
}
