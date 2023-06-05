using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Game.Responses
{
    public class ResetResponseDto: BasicResponseDto
    {
        public ResetResponseDto()
        {
            Done = true;
        }
    }
}
