using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Game.Requests
{
    public class ResetRequestDto
    {
        public Guid PlayerId { get; set; }
    }
}
