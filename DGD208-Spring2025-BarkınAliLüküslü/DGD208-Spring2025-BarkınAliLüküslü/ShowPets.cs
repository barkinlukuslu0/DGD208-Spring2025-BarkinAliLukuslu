using System;
using System.Collections.Generic;

namespace DGD208_Spring2025_BarkýnAliLüküslü
{
    public class ShowPets
    {
        private List<Pet> _pets;

        public ShowPets(List<Pet> pets)
        {
            _pets = pets;
        }

        public void Display()
        {
            Console.Clear();

            if (_pets.Count == 0)
            {
                Console.WriteLine("You have no pets yet.");
            }
            else
            {
                foreach (var pet in _pets)
                {
                    Console.WriteLine(pet);
                }
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}
