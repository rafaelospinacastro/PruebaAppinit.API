using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Domain.Entities
{
    using System.Collections.Generic;
    using System.Numerics;

    public class Game
    {
        public Guid Id { get; init; }
        public Player Player1 { get; init; }
        public Player Player2 { get; init; }
        public List<RoundResult> Rounds { get; } = new();
        public int ScoreP1 { get; private set; }
        public int ScoreP2 { get; private set; }

        public Game(Guid id, Player p1, Player p2)
        {
            Id = id;
            Player1 = p1;
            Player2 = p2;
        }

        public Game(Player p1, Player p2) : this(Guid.NewGuid(), p1, p2) { }

        public RoundResult PlayRound(Move p1Move, Move p2Move)
        {
            var outcome = DetermineOutcome(p1Move, p2Move);
            var r = new RoundResult(p1Move, p2Move, outcome);
            Rounds.Add(r);
            if (outcome == RoundOutcome.Player1Wins) ScoreP1++;
            if (outcome == RoundOutcome.Player2Wins) ScoreP2++;
            return r;
        }

        private static RoundOutcome DetermineOutcome(Move p1, Move p2)
        {
            if (p1 == p2) return RoundOutcome.Tie;
            if ((p1 == Move.Rock && p2 == Move.Scissors) ||
                (p1 == Move.Scissors && p2 == Move.Paper) ||
                (p1 == Move.Paper && p2 == Move.Rock))
                return RoundOutcome.Player1Wins;
            return RoundOutcome.Player2Wins;
        }
    }
}
