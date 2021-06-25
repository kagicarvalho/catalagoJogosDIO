using catalagoJogos.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalagoJogos.WebApi.Repositories
{
    public interface IJogoRepository
    {
        Task<Jogo> Obter(Guid id);
        Task<List<Jogo>> Obter(int pagina, int quantidade);
        Task<List<Jogo>> Obter(string nome, string produtora);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);
        void Dispose();
    }
}
