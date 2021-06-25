using catalagoJogos.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalagoJogos.WebApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("238f8b55-b457-41e6-8df7-d6932f6dd182"), new Jogo{ Id = Guid.Parse("238f8b55-b457-41e6-8df7-d6932f6dd182"), Nome = "World of Warcraft", Produtora = "Blizzard", Preco = 150} },
            {Guid.Parse("f7426d4e-fa2c-4662-9e2c-6226168cfa1e"), new Jogo{ Id = Guid.Parse("f7426d4e-fa2c-4662-9e2c-6226168cfa1e"), Nome = "The Witcher® 3: Wild Hunt", Produtora = "CD Projekt Red", Preco = 79.99m} },
            {Guid.Parse("16e24073-9313-4f5a-b591-66674e4fcc16"), new Jogo{ Id = Guid.Parse("16e24073-9313-4f5a-b591-66674e4fcc16"), Nome = "Grand Theft Auto: San Andreas", Produtora = "Rockstar Games", Preco = 20.19m} },
            {Guid.Parse("8bc56af1-97e5-4541-b13d-16fc5de68a26"), new Jogo{ Id = Guid.Parse("8bc56af1-97e5-4541-b13d-16fc5de68a26"), Nome = "NARUTO TO BORUTO: SHINOBI STRIKER", Produtora = "Soleil Ltd.", Preco = 139.99m} },
            {Guid.Parse("1bfcc70d-1b57-4ab9-9a8c-8712d2cde830"), new Jogo{ Id = Guid.Parse("1bfcc70d-1b57-4ab9-9a8c-8712d2cde830"), Nome = "Counter-Strike", Produtora = "Valve", Preco = 20.69m} },
            {Guid.Parse("4260a34a-93e9-4b27-bf53-fc015ac7328b"), new Jogo{ Id = Guid.Parse("4260a34a-93e9-4b27-bf53-fc015ac7328b"), Nome = "STAR WARS™: The Old Republic™", Produtora = "BioWare", Preco = 33} },
            {Guid.Parse("8c593e1f-f2fc-4a8d-899a-5b0ad28fcf30"), new Jogo{ Id = Guid.Parse("8c593e1f-f2fc-4a8d-899a-5b0ad28fcf30"), Nome = "Counter-Strike Complete", Produtora = "Valve", Preco = 41.38m} },
            {Guid.Parse("abed1436-6ff7-4009-aaed-4477d240d276"), new Jogo{ Id = Guid.Parse("abed1436-6ff7-4009-aaed-4477d240d276"), Nome = "Valve Complete Pack PACOTE", Produtora = "Valve", Preco = 305.42m} },
            {Guid.Parse("4288e711-a07c-4e77-b9a1-46a964d49583"), new Jogo{ Id = Guid.Parse("4288e711-a07c-4e77-b9a1-46a964d49583"), Nome = "FINAL FANTASY XIV Online", Produtora = "Square Enix", Preco = 52.99m} },
            {Guid.Parse("83115fe4-2fed-4c99-b022-ceb4002b5c79"), new Jogo{ Id = Guid.Parse("83115fe4-2fed-4c99-b022-ceb4002b5c79"), Nome = "The Elder Scrolls® Online", Produtora = "Zenimax Online Studios", Preco = 61.50m} },
            {Guid.Parse("0a7eb0b1-4dd0-46b2-98af-2927d2afd692"), new Jogo{ Id = Guid.Parse("0a7eb0b1-4dd0-46b2-98af-2927d2afd692"), Nome = "EA SPORTS™ BUNDLE", Produtora = "EA", Preco = 598} },
            {Guid.Parse("ec1c896d-d5e0-450a-b3d4-1e4b62bc8498"), new Jogo{ Id = Guid.Parse("ec1c896d-d5e0-450a-b3d4-1e4b62bc8498"), Nome = "STAR WARS™: Squadrons", Produtora = "Motive | EA", Preco = 61.50m} },
            {Guid.Parse("70315556-76c5-4630-a977-7e0df7792e2d"), new Jogo{ Id = Guid.Parse("70315556-76c5-4630-a977-7e0df7792e2d"), Nome = "STAR WARS™ Battlefront™ II", Produtora = "DICE | EA", Preco = 159} },
            {Guid.Parse("801b8582-366a-422a-9180-ec711fa2e10f"), new Jogo{ Id = Guid.Parse("801b8582-366a-422a-9180-ec711fa2e10f"), Nome = "STAR WARS Jedi: Fallen Order™", Produtora = "Respawn Entertainment | EA", Preco = 199} },
            {Guid.Parse("d97ea8fb-4378-4c9f-a1fa-cb123f522191"), new Jogo{ Id = Guid.Parse("d97ea8fb-4378-4c9f-a1fa-cb123f522191"), Nome = "Trine: Ultimate Collection", Produtora = "Frozenbyte", Preco = 126.07m} }
        };

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public void Dispose() { }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return Task.FromResult<Jogo>(null);

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
    }
}
