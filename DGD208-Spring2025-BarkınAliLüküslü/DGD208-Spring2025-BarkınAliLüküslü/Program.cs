using System;

namespace DGD208_Spring2025_BarkinAliLukuslu
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Game game = new Game();
            await game.GameLoop();
        }
    }
}