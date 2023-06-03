using TicTacToe.Data.Game;
using TicTacToe.Services.Game;
using TicTacToe.Services.Game.Concrete;
using TicTacToe.Services.Repositories;

namespace TicTacToe.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.AddTransient<IGameRepository, InMemoryGameRepository>();
            services.AddTransient<IGameService, GameService>();

            var app = builder.Build();

            IGameService gameService;
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                gameService = serviceProvider
                    .GetRequiredService<IGameService>();
            }

            app.MapGet("/", () => "Hello World!");

            // create new game
            app.MapPost(
                pattern: "/api/game/",
                handler: () => gameService.Create()
                );

            app.Run();
        }
    }
}