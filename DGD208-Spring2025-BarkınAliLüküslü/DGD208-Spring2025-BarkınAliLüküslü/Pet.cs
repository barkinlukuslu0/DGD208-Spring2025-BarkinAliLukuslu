using System;

namespace DGD208_Spring2025_BarkýnAliLüküslü
{
    public class Pet
    {
        public event Action<Pet> OnPetDied;

        public string Name { get; set; }
        public PetType Type { get; set; }

        public int Hunger { get; set; } = 50;
        public int Sleep { get; set; } = 50;
        public int Fun { get; set; } = 50;

        public void ModifyStat(PetStat stat, int amount)
        {
            switch (stat)
            {
                case PetStat.Hunger:
                    Hunger = Math.Clamp(Hunger + amount, 0, 100);
                    break;
                case PetStat.Sleep:
                    Sleep = Math.Clamp(Sleep + amount, 0, 100);
                    break;
                case PetStat.Fun:
                    Fun = Math.Clamp(Fun + amount, 0, 100);
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - Hunger: {Hunger}, Sleep: {Sleep}, Fun: {Fun}";
        }

        public async Task StartLifecycleAsync()
        {
            while (true)
            {
                await Task.Delay(3000);

                ModifyStat(PetStat.Hunger, -1);
                ModifyStat(PetStat.Sleep, -1);
                ModifyStat(PetStat.Fun, -1);

                if (Hunger <= 0 || Sleep <= 0 || Fun <= 0)
                {
                    OnPetDied?.Invoke(this);
                    break;
                }
            }
        }
    }
}
