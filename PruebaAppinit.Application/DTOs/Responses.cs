using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Application.DTOs
{
    public record StartGameResponse(Guid GameId, string Player1Name, string Player2Name);
    public record PlayRoundResponse(Guid GameId, string Player1Move, string Player2Move, string Outcome, int ScoreP1, int ScoreP2);
    public record GameStatusResponse(Guid GameId, string Player1Name, string Player2Name, int ScoreP1, int ScoreP2, IEnumerable<object> Rounds);

}
