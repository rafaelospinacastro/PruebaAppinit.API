using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Domain.Entities
{
    public enum RoundOutcome { Tie, Player1Wins, Player2Wins }
    public record RoundResult(Move P1, Move P2, RoundOutcome Outcome);

}
