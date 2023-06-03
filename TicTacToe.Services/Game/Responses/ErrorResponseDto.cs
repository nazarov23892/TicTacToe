using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Game.Responses
{
    public class ErrorResponseDto : BasicResponseDto
    {
        public ErrorResponseDto(string errorMessage)
        {
            Done = false;
            Error = errorMessage;
        }

        public string Error { get; private set; } = string.Empty;

    }
}
