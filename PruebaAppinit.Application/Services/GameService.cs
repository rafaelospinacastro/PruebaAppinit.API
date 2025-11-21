using PruebaAppinit.Application.DTOs;
using PruebaAppinit.Application.Interfaces;
using System;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using PruebaAppinit.Domain.Entities;

namespace PruebaAppinit.Application.Services
{
    public class GameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository) => _gameRepository = gameRepository;

        public async Task<StartGameResponse> StartGameAsync(StartGameRequest startGameRequest, CancellationToken cancellationToken = default)
        {
            try 
            {
                var objGame = new Game(new Player { Name = startGameRequest.Player1Name }, new Player { Name = startGameRequest.Player2Name });
                await _gameRepository.SaveAsync(objGame, cancellationToken);
                return new StartGameResponse(objGame.Id, objGame.Player1.Name, objGame.Player2.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; 
            }
           
        }

        public async Task<PlayRoundResponse> PlayRoundAsync(PlayRoundRequest playRoundRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var gameObj = await _gameRepository.GetAsync(playRoundRequest.GameId, cancellationToken) ?? throw new KeyNotFoundException("Juego no encontrado");
                var parts = playRoundRequest.Move.Split('|', System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) throw new ArgumentException("No se leen los movimientos");

                var p1Move = Enum.Parse<Move>(parts[0], true);
                var p2Move = Enum.Parse<Move>(parts[1], true);

                var resultRound = gameObj.PlayRound(p1Move, p2Move);
                await _gameRepository.SaveAsync(gameObj, cancellationToken);

                string outcomeRounds = resultRound.Outcome switch
                {
                    RoundOutcome.Tie => "Tie",
                    RoundOutcome.Player1Wins => "Player1Wins",
                    RoundOutcome.Player2Wins => "Player2Wins",
                    _ => "Unknown"
                };

                return new PlayRoundResponse(gameObj.Id, resultRound.P1.ToString(), resultRound.P2.ToString(), outcomeRounds, gameObj.ScoreP1, gameObj.ScoreP2);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            
        }

        public async Task<GameStatusResponse> GetStatusAsync(Guid gameId, CancellationToken cancellationToken = default)
        {          

            try
            {
                var gameObj = await _gameRepository.GetAsync(gameId, cancellationToken) ?? throw new KeyNotFoundException("No se encontró el juego");
                var rounds = gameObj.Rounds.Select(r => new { P1 = r.P1.ToString(), P2 = r.P2.ToString(), Outcome = r.Outcome.ToString() });
                return new GameStatusResponse(gameObj.Id, gameObj.Player1.Name, gameObj.Player2.Name, gameObj.ScoreP1, gameObj.ScoreP2, rounds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }       

    }
}