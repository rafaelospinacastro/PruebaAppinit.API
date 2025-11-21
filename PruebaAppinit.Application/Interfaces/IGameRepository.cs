using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaAppinit.Domain.Entities;

namespace PruebaAppinit.Application.Interfaces
{
    
    public interface IGameRepository
    {
        Task SaveAsync(Game game, System.Threading.CancellationToken cancellationToken = default);
        Task<Game?> GetAsync(System.Guid id, System.Threading.CancellationToken cancellationToken = default);

    }
}
