using System;
using System.Threading.Tasks;

namespace DGD208_Spring2025_BarkinAliLukuslu
{
    public class Pet
    {
        public event Action<Pet> OnPetDied;
        public event Action<Pet, PetStat, int> OnStatChanged;
        public event Action<Pet, string> OnStatusChanged;

        public string Name { get; set; }
        public PetType Type { get; set; }

        private int _hunger = 50;
        private int _sleep = 50;
        private int _fun = 50;

        public int Hunger 
        { 
            get => _hunger;
            private set
            {
                int oldValue = _hunger;
                _hunger = Math.Clamp(value, 0, 100);
                if (oldValue != _hunger)
                {
                    OnStatChanged?.Invoke(this, PetStat.Hunger, _hunger);
                    CheckHealthStatus();
                }
            }
        }

        public int Sleep 
        { 
            get => _sleep;
            private set
            {
                int oldValue = _sleep;
                _sleep = Math.Clamp(value, 0, 100);
                if (oldValue != _sleep)
                {
                    OnStatChanged?.Invoke(this, PetStat.Sleep, _sleep);
                    CheckHealthStatus();
                }
            }
        }

        public int Fun 
        { 
            get => _fun;
            private set
            {
                int oldValue = _fun;
                _fun = Math.Clamp(value, 0, 100);
                if (oldValue != _fun)
                {
                    OnStatChanged?.Invoke(this, PetStat.Fun, _fun);
                    CheckHealthStatus();
                }
            }
        }

        public void ModifyStat(PetStat stat, int amount)
        {
            switch (stat)
            {
                case PetStat.Hunger:
                    Hunger += amount;
                    break;
                case PetStat.Sleep:
                    Sleep += amount;
                    break;
                case PetStat.Fun:
                    Fun += amount;
                    break;
            }
        }

        private void CheckHealthStatus()
        {
            if (Hunger <= 10 || Sleep <= 10 || Fun <= 10)
            {
                OnStatusChanged?.Invoke(this, "Critical");
            }
            else if (Hunger <= 30 || Sleep <= 30 || Fun <= 30)
            {
                OnStatusChanged?.Invoke(this, "Poor");
            }
            else if (Hunger >= 80 && Sleep >= 80 && Fun >= 80)
            {
                OnStatusChanged?.Invoke(this, "Excellent");
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