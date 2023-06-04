using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Entities
{
    public class GameState
    {
        [Key]
        public Guid GameId { get; set; } = Guid.Empty;
        public Guid Player1_Id { get; set; } = Guid.Empty;
        public Guid Player2_Id { get; set; } = Guid.Empty;
        public GameStatus Status { get; set; } = GameStatus.WaitPlayer2_Connect;
        public ICollection<GamePointItem> Points{get;set;} = new List<GamePointItem>();
    }

    public enum GameStatus
    {
        WaitPlayer2_Connect = 0,
        WaitPlayer1_Turn,
        WaitPlayer2_Turn,
        Draw,
        WinPlayer1,
        WinPlayer2
    }
}
