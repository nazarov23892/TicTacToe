using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Game.Responses
{
    public class TurnResponseDto: BasicResponseDto
    {
        public TurnResponseDto()
        {
            Done = true;
        }
    }
}
