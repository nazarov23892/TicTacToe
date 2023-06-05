using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Entities;

namespace TicTacToe.Services.Repositories
{
    public interface IGameRepository
    {
        GameState? GetGame(Guid gameId, bool includePoints = false);
        void AddGame(GameState game);
        void UpdateGame(GameState game);
    }
}
