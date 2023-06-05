using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Entities;
using TicTacToe.Services.Repositories;

namespace TicTacToe.Data.Game
{
    public class InMemoryGameRepository : IGameRepository
    {
        private Dictionary<Guid, GameState> _games = new Dictionary<Guid, GameState>();

        public void AddGame(GameState game)
        {
            if (_games.ContainsKey(game.GameId))
            {
                throw new InvalidOperationException(message: "the game already exists");
            }
            _games[game.GameId] = game;
        }

        public GameState? GetGame(Guid gameId, bool includePoints = false)
        {
            if (!_games.ContainsKey(gameId))
            {
                return null;
            }
            return _games[gameId];
        }

        public void UpdateGame(GameState game)
        {
            if (!_games.ContainsKey(game.GameId))
            {
                throw new InvalidOperationException("game not found");
            }
            _games[game.GameId] = game;
        }
    }
}
