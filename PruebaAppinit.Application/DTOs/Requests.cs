using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Application.DTOs
{
    public record StartGameRequest(string Player1Name, string Player2Name);
    public record PlayRoundRequest(Guid GameId, string Player, string Move);
}
