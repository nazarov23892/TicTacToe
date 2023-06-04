using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Entities
{
    public  class GamePointItem
    {
        [Key]
        public int PointId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public GamePointValue Value { get; set; } = GamePointValue.None;

    }

    public enum GamePointValue
    {
        None = 0,
        Player1,
        Player2
    }
}
