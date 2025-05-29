using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGD208_Spring2025_BarkinAliLukuslu
{
    public class UseItemOnPet
    {
        private List<Pet> _pets;

        public event Action<string> OnItemUsed;

        public UseItemOnPet(List<Pet> pets)
        {
            _pets = pets;
        }

        public async Task UseItemAsync()
        {
            if (_pets.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You have no pets to use items on.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            var petMenu = new Menu<Pet>(
                "Select a pet to use item on",
                _pets,
                pet => $"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger} Sleep: {pet.Sleep} Fun: {pet.Fun}"
            );

            Pet selectedPet = petMenu.ShowAndGetSelection();
            if (selectedPet == null) return;

            var compatibleItems = ItemDatabase.AllItems
                .Where(item => item.CompatibleWith.Contains(selectedPet.Type))
                .ToList();

            if (compatibleItems.Count == 0)
            {
                Console.Clear();
                Console.WriteLine($"No items available for {selectedPet.Type}s.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            var itemMenu = new Menu<Item>(
                $"Select an item to use on {selectedPet.Name}",
                compatibleItems,
                item => $"{item.Name} (+{item.EffectAmount} {item.AffectedStat}) - {item.Duration}s"
            );

            Item selectedItem = itemMenu.ShowAndGetSelection();
            if (selectedItem == null) return;

            await ProcessItemUsage(selectedPet, selectedItem);
        }

        private async Task ProcessItemUsage(Pet pet, Item item)
        {
            Console.Clear();
            Console.WriteLine($"Using {item.Name} on {pet.Name}...");
            Console.WriteLine($"This will take {item.Duration} seconds.");

            int totalSteps = (int)(item.Duration * 10);
            for (int i = 0; i <= totalSteps; i++)
            {
                Console.SetCursorPosition(0, 3);
                Console.Write($"Progress: [{new string('=', i * 30 / totalSteps)}{new string(' ', 30 - (i * 30 / totalSteps))}] {i * 100 / totalSteps}%");
                await Task.Delay(100);
            }

            pet.ModifyStat(item.AffectedStat, item.EffectAmount);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Successfully used {item.Name} on {pet.Name}!");
            Console.WriteLine($"{pet.Name}'s {item.AffectedStat} increased by {item.EffectAmount}!");
            Console.WriteLine($"Current stats: {pet}");

            OnItemUsed?.Invoke($"{item.Name} used on {pet.Name}");

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}