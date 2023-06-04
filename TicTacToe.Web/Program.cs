using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.DataContexts;
using TicTacToe.Data.Game;
using TicTacToe.Services.Game;
using TicTacToe.Services.Game.Concrete;
using TicTacToe.Services.Game.Requests;
using TicTacToe.Services.Repositories;

namespace TicTacToe.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IGameRepository, EfGameRepository>();
            services.AddTransient<IGameService, GameService>();
            services.AddDbContext<AppDataContext>(options => options.UseSqlite(connectionString));

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapGet("/", () => "Hello World!");

            // create new game
            app.MapPost(
                pattern: "/api/game/",
                handler: () =>
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;
                        IGameService gameService = serviceProvider
                            .GetRequiredService<IGameService>();
                        return gameService.Create();
                    }
                });

            // get game state
            app.MapGet(
                pattern: "/api/game/{id}",
                handler: (Guid id) =>
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;
                        IGameService gameService = serviceProvider
                            .GetRequiredService<IGameService>();
                        return gameService.GetStatus(gameId: id);
                    }
                });

            // connect player2 to game
            app.MapPut(
                pattern: "/api/game/connect/{id}",
                handler: (Guid id) =>
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;
                        IGameService gameService = serviceProvider
                            .GetRequiredService<IGameService>();
                        return gameService.ConnectToGame(gameId: id);
                    }
                });

            // do turn
            app.MapPut(
                pattern: "/api/game/{id}",
                handler: (Guid id, TurnRequestDto turnDto) =>
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;
                        IGameService gameService = serviceProvider
                            .GetRequiredService<IGameService>();
                        return gameService.DoTurn(
                            gameId: id,
                            turnDto: turnDto);
                    }
                });

            app.Run();
        }
    }
}