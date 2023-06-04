using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services.Entities;
using TicTacToe.Services.Game.Requests;
using TicTacToe.Services.Game.Responses;
using TicTacToe.Services.Repositories;

namespace TicTacToe.Services.Game.Concrete
{
    public class GameService : IGameService
    {
        private const int FieldDimension = 3;
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public BasicResponseDto ConnectToGame(Guid gameId)
        {
            GameState? game = _gameRepository.GetGame(gameId);
            if (game == null)
            {
                return new ErrorResponseDto(errorMessage: "game not found");
            }
            if (game.Status != GameStatus.WaitPlayer2_Connect)
            {
                return new ErrorResponseDto(errorMessage: "player's connection is not expected");
            }
            game.Player2_Id = Guid.NewGuid();
            game.Status = GameStatus.WaitPlayer1_Turn;
            _gameRepository.UpdateGame(game);
            return new ConnectPlayer2_ResponseDto
            {
                Player2_Id = game.Player2_Id
            };
        }

        public BasicResponseDto Create()
        {
            GameState newGame = new GameState
            {
                GameId = Guid.NewGuid(),
                Player1_Id = Guid.NewGuid(),
                Status = GameStatus.WaitPlayer2_Connect,
                Points = Enumerable.Range(0, FieldDimension * FieldDimension)
                    .Select(i => new GamePointItem
                    {
                        X = i % FieldDimension,
                        Y = i / FieldDimension,
                        Value = GamePointValue.None
                    }).ToList()
            };
            _gameRepository.AddGame(newGame);
            return new CreateGameResponseDto
            {
                GameId = newGame.GameId,
                Player1_Id = newGame.Player1_Id
            };
        }

        public BasicResponseDto DoTurn(Guid gameId, TurnRequestDto turnDto)
        {
            GameState? game = _gameRepository.GetGame(gameId);
            if (game == null)
            {
                return new ErrorResponseDto("game not found");
            }
            if (game.Status == GameStatus.WaitPlayer1_Turn)
            {
                return DoTournPlayer1(game, turnDto);
            }
            else if (game.Status == GameStatus.WaitPlayer2_Turn)
            {
                return DoTournPlayer2(game, turnDto);
            }
            return new ErrorResponseDto("the player's turn is not expected");
        }

        private BasicResponseDto DoTournPlayer1(GameState game, TurnRequestDto turnDto)
        {
            GamePointItem? point = game.Points.SingleOrDefault(p => p.X == turnDto.X && p.Y == turnDto.Y);
            if (point == null)
            {
                return new ErrorResponseDto("incorrect point value");
            }
            if (point.Value != GamePointValue.None)
            {
                return new ErrorResponseDto("the point already occupied");
            }
            if (turnDto.PlayerId == game.Player2_Id)
            {
                return new ErrorResponseDto("player's #2 turn is not expected");
            }
            if (turnDto.PlayerId != game.Player1_Id)
            {
                return new ErrorResponseDto("incorrect player id");
            }
            point.Value = GamePointValue.Player1;
            if (CheckCompletedLines(game.Points, GamePointValue.Player1))
            {
                game.Status = GameStatus.WinPlayer1;
                goto exit_point;
            }
            game.Status = GameStatus.WaitPlayer2_Turn;

        exit_point:
            _gameRepository.UpdateGame(game);
            return new TurnResponseDto();
        }

        private BasicResponseDto DoTournPlayer2(GameState game, TurnRequestDto turnDto)
        {
            GamePointItem? point = game.Points.SingleOrDefault(p => p.X == turnDto.X && p.Y == turnDto.Y);
            if (point == null)
            {
                return new ErrorResponseDto("incorrect point value");
            }
            if (point.Value != GamePointValue.None)
            {
                return new ErrorResponseDto("the point already occupied");
            }
            if (turnDto.PlayerId == game.Player1_Id)
            {
                return new ErrorResponseDto("player's #1 turn is not expected");
            }
            if (turnDto.PlayerId != game.Player2_Id)
            {
                return new ErrorResponseDto("incorrect player id");
            }
            point.Value = GamePointValue.Player2;
            if (CheckCompletedLines(game.Points, GamePointValue.Player2))
            {
                game.Status = GameStatus.WinPlayer2;
                goto exit_point;
            }
            game.Status = GameStatus.WaitPlayer1_Turn;

        exit_point:
            _gameRepository.UpdateGame(game);
            return new TurnResponseDto();
        }

        private bool CheckCompletedLines(IEnumerable<GamePointItem> points, GamePointValue pointValue)
        {
            var correspondValues = points.Where(p => p.Value == pointValue);

            // check completed rows
            if (correspondValues.GroupBy(p => p.Y)
                .Where(g => g.Count() >= FieldDimension)
                .Any())
            {
                return true;
            }

            // check completed cols
            if (correspondValues.GroupBy(p => p.X)
                .Where(g => g.Count() >= FieldDimension)
                .Any())
            {
                return true;
            }

            // check completed diagonals 
            if (correspondValues.FirstOrDefault(p => p.X == 0 && p.Y == 0) != null
                && correspondValues.FirstOrDefault(p => p.X == 1 && p.Y == 1) != null
                && correspondValues.FirstOrDefault(p => p.X == 2 && p.Y == 2) != null)
            {
                return true;
            }
            if (correspondValues.FirstOrDefault(p => p.X == 0 && p.Y == 2) != null
                && correspondValues.FirstOrDefault(p => p.X == 1 && p.Y == 1) != null
                && correspondValues.FirstOrDefault(p => p.X == 2 && p.Y == 0) != null)
            {
                return true;
            }
            return false;
        }

        public BasicResponseDto GetStatus(Guid gameId)
        {
            var game = _gameRepository.GetGame(gameId);
            if (game == null)
            {
                return new ErrorResponseDto(errorMessage: "game not found");
            }
            return new GameStateResponseDto
            {
                Status = game.Status,
                Points = game.Points
            };
        }
    }
}
