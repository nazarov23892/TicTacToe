using TicTacToe.Data.Game;
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

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}