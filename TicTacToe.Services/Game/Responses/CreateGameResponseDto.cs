﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Game.Responses
{
    public class CreateGameResponseDto
    {
        public Guid Player1_Id { get; set; }
        public Guid GameId { get; set; }
    }
}
