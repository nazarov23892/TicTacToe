using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Entities;

namespace TicTacToe.Services.Game.Responses
{
    public class GameStateResponseDto
    {
        public GameStatus Status { get; set; }
        public IEnumerable<GamePointItem> Points { get; set; } = Enumerable.Empty<GamePointItem>();

    }
}
