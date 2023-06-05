using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.DataContexts;
using TicTacToe.Services.Entities;
using TicTacToe.Services.Repositories;

namespace TicTacToe.Data.Game
{
    public class EfGameRepository : IGameRepository
    {
        private readonly AppDataContext efDbContext;

        public EfGameRepository(AppDataContext efDbContext)
        {
            this.efDbContext = efDbContext;
        }

        public void AddGame(GameState game)
        {
            efDbContext.Games.Add(game);
            efDbContext.SaveChanges();
        }

        public GameState? GetGame(Guid gameId, bool includePoints = false)
        {
            return efDbContext.Games
                    .Select(p => new GameState
                    {
                        GameId = p.GameId,
                        Player1_Id = p.Player1_Id,
                        Player2_Id = p.Player2_Id,
                        Status = p.Status,
                        Points = includePoints
                            ? p.Points.ToList()
                            : new List<GamePointItem>(),
                    })
                    .FirstOrDefault(g => g.GameId == gameId);
        }

        public void UpdateGame(GameState game)
        {
            efDbContext.Games.Update(game);
            efDbContext.SaveChanges();
        }
    }
}
