using Microsoft.EntityFrameworkCore;
using PruebaAppinit.Infrastructure.Entities;
using PruebaAppinit.Application.Interfaces;
using PruebaAppinit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Infrastructure.Repositories
{
    public class EfGameRepository : IGameRepository
    {
        private readonly AppinitDbContext _dbAppinitDbContext;
        public EfGameRepository(AppinitDbContext dbAppinitDbContext) => _dbAppinitDbContext = dbAppinitDbContext;

        public async Task SaveAsync(Game game, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _dbAppinitDbContext.Games.Include(g => g.Rounds).FirstOrDefaultAsync(g => g.Id == game.Id, cancellationToken);
                if (existing is null)
                {
                    var entityGame = MapToEntity(game);
                    _dbAppinitDbContext.Games.Add(entityGame);
                }
                else
                {
                    existing.ScoreP1 = game.ScoreP1;
                    existing.ScoreP2 = game.ScoreP2;
                    existing.Rounds.Clear();
                    int rn = 1;
                    foreach (var r in game.Rounds)
                    {
                        existing.Rounds.Add(new RoundEntity
                        {
                            GameId = existing.Id,
                            P1Move = r.P1.ToString(),
                            P2Move = r.P2.ToString(),
                            Outcome = r.Outcome.ToString(),
                            RoundNumber = rn++
                        });
                    }
                }
                await _dbAppinitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        public async Task<Game?> GetAsync(Guid idGame, CancellationToken cancellationToken = default)
        {
            try
            {
                var entityGame = await _dbAppinitDbContext.Games.Include(g => g.Rounds.OrderBy(r => r.RoundNumber)).FirstOrDefaultAsync(g => g.Id == idGame, cancellationToken);
                if (entityGame is null) return null;
                return MapToDomain(entityGame);
            }
            catch (Exception ex)
            {
                throw;                
            }
            
        }

        private static GameEntity MapToEntity(Game gameEntity)
        {
            var e = new GameEntity
            {
                Id = gameEntity.Id,
                Player1Name = gameEntity.Player1.Name,
                Player2Name = gameEntity.Player2.Name,
                ScoreP1 = gameEntity.ScoreP1,
                ScoreP2 = gameEntity.ScoreP2,
                Rounds = gameEntity.Rounds.Select((r, idx) => new RoundEntity
                {
                    GameId = gameEntity.Id,
                    P1Move = r.P1.ToString(),
                    P2Move = r.P2.ToString(),
                    Outcome = r.Outcome.ToString(),
                    RoundNumber = idx + 1
                }).ToList()
            };
            return e;
        }

        private static Game MapToDomain(GameEntity gameEntity)
        {
            var p1 = new Player { Name = gameEntity.Player1Name };
            var p2 = new Player { Name = gameEntity.Player2Name };
            var gameDomain = new Game(gameEntity.Id, p1, p2);
            foreach (var re in gameEntity.Rounds.OrderBy(r => r.RoundNumber))
            {
                var m1 = Enum.Parse<Move>(re.P1Move, true);
                var m2 = Enum.Parse<Move>(re.P2Move, true);
                gameDomain.PlayRound(m1, m2);
            }
            return gameDomain;
        }
    }

}
