using System;
using System.Collections.Generic;
using System.Linq;

namespace DGD208_Spring2025_BarkinAliLukuslu
{
    public class AdoptPet
    {
        private List<Pet> _pets;

        public AdoptPet(List<Pet> pets)
        {
            _pets = pets;
        }

        public async Task AdoptAsync()
        {
            Console.Clear();
            Console.Write("Enter a name for your new pet: ");
            string name = Console.ReadLine();

            var petTypeMenu = new Menu<PetType>(
                "Choose a pet type",
                Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList(),
                pt => pt.ToString()
            );

            PetType chosenType = petTypeMenu.ShowAndGetSelection();

            if (!string.IsNullOrWhiteSpace(name))
            {
                _pets.Add(new Pet { Name = name, Type = chosenType });
                Console.WriteLine($"You adopted a {chosenType} named {name}!");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}
