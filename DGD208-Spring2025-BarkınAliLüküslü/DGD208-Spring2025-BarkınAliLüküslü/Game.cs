using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGD208_Spring2025_BarkinAliLukuslu
{
    public class Game
    {
        private bool _isRunning;

        /// List of pets that the player has been adopted
        private List<Pet> _pets = new List<Pet>();

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
            Console.WriteLine("=== Pet Care Game ===");
            Console.WriteLine();
            Console.WriteLine("1. Adopt a Pet");
            Console.WriteLine("2. View Pets");
            Console.WriteLine("3. Use Item on a Pet");
            Console.WriteLine("9. Credits");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Your choice: ");
            return Console.ReadLine();
        }

        private async Task ProcessUserChoice(string choice)
        {
            // Use this to process any choice user makes
            // Set _isRunning = false to exit the game

            switch (choice)
            {
                case "0":
                    _isRunning = false;
                    break;

                case "1":
                    await AdoptPet();
                    break;

                case "2":
                    ShowPets();
                    break;

                case "3":
                    await UseItemOnPet();
                    break;

                case "9":
                    ShowCredits();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private async Task AdoptPet()
        {
            var adopter = new AdoptPet(_pets);
            await adopter.AdoptAsync();

            var latestPet = _pets.LastOrDefault();
            if (latestPet != null)
            {
                latestPet.OnPetDied += HandlePetDeath;
                _ = latestPet.StartLifecycleAsync();
            }
        }

        private void HandlePetDeath(Pet pet)
        {
            _pets.Remove(pet);
            Console.WriteLine($"\n\n\n Notify: Sadly, your pet {pet.Name} has died due to neglect.");
            Console.ReadKey();
        }

        private void ShowPets()
        {
            var shower = new ShowPets(_pets);
            shower.Display();
        }

        private async Task UseItemOnPet()
        {
            var itemUser = new UseItemOnPet(_pets);
            await itemUser.UseItemAsync();
        }

        private void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("=== CREDITS ===");
            Console.WriteLine();
            Console.WriteLine("This game created by Barkın Ali Lüküslü");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}