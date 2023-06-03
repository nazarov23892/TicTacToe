using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Game.Responses;

namespace TicTacToe.Services.Game
{
    public interface IGameService
    {
        public CreateGameResponseDto Create();
    }
}
