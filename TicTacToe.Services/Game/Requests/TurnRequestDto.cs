using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Game.Responses;

namespace TicTacToe.Services.Game.Requests
{
    public class TurnRequestDto
    {
        public Guid PlayerId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
