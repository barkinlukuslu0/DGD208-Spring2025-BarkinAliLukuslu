using System;

namespace DGD208_Spring2025_BarkýnAliLüküslü
{
    public class Game
    {
        private bool _isRunning;

        public async Task GameLoop()
        {
            // Initialize the game
            Initialize();

            // Main game loop
            _isRunning = true;
            while (_isRunning)
            {
                // Display menu and get player input
                string userChoice = GetUserInput();

                // Process the player's choice
                await ProcessUserChoice(userChoice);
            }

            Console.WriteLine("Thanks for playing!");
        }

        private void Initialize()
        {
            // Use this to initialize the game
        }

        private string GetUserInput()
        {
            // Use this to display appropriate menu and get user inputs

            Console.Clear();
            Console.WriteLine("1. View Pets");
            Console.WriteLine("2. Use Item on Pet");
            Console.WriteLine("9. Credits");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            return Console.ReadLine();
        }

        private async Task ProcessUserChoice(string choice)
        {
            // Use this to process any choice user makes
            // Set _isRunning = false to exit the game

            if (choice == "0")
            {
                _isRunning = false;
            }
            else if (choice == "9")
            {
                // Display credits
                Console.WriteLine("This game created by Barkýn Ali Lüküslü");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();

                //if user press any key the game will continue
                GetUserInput();
            }
        }
    }
}
