using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Entities;
using TicTacToe.Services.Game.Responses;
using TicTacToe.Services.Repositories;

namespace TicTacToe.Services.Game.Concrete
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public ConnectPlayer2_ResponseDto? ConnectToGame(Guid gameId)
        {
            GameState? game = _gameRepository.GetGame(gameId);
            if (game == null)
            {
                return null;
            }
            if (game.Status != GameStatus.WaitPlayer2_Connect)
            {
                return null;
            }
            if (game.Player2_Id != Guid.Empty)
            {
                return null;
            }
            Guid player2_Id = Guid.NewGuid();
            game.Player2_Id = player2_Id;
            _gameRepository.UpdateGame(game);
            return new ConnectPlayer2_ResponseDto
            {
                Player2_Id = player2_Id
            };
        }

        public CreateGameResponseDto Create()
        {
            GameState newGame = new GameState
            {
                GameId = Guid.NewGuid(),
                Player1_Id = Guid.NewGuid(),
                Status = GameStatus.WaitPlayer2_Connect
            };
            _gameRepository.AddGame(newGame);
            return new CreateGameResponseDto
            {
                GameId = newGame.GameId,
                Player1_Id = newGame.Player1_Id
            };
        }

        public GameStateResponseDto? GetStatus(Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            if (game == null)
            {
                return null;
            }
            return new GameStateResponseDto
            {
                Status = game.Status,
                Points = game.Points
            };

        }
    }
}
