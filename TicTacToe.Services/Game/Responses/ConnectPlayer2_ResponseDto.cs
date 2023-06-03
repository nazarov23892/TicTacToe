using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Game.Responses
{
    public class ConnectPlayer2_ResponseDto:BasicResponseDto
    {
        public ConnectPlayer2_ResponseDto()
        {
            Done = true;
        }

        public Guid Player2_Id { get; set; }
    }
}
