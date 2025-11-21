using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Infrastructure.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string Player1Name { get; set; } = "";
        public string Player2Name { get; set; } = "";
        public int ScoreP1 { get; set; }
        public int ScoreP2 { get; set; }
        public List<RoundEntity> Rounds { get; set; } = new();
    }
}
