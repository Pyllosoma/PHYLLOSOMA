using System;

namespace Runtime.Data.Structure
{
    [Serializable]
    public class BaseStats
    {
        public int Health = 0;
        public int Damage = 0;
        public int Defense = 0;
        public int Endurance = 0;
        public int Agility = 0;
        public int Faith = 0;
        public static BaseStats operator +(BaseStats a, BaseStats b){
            BaseStats result = new BaseStats();
            result.Health = a.Health + b.Health;
            result.Damage = a.Damage + b.Damage;
            result.Defense = a.Defense + b.Defense;
            result.Endurance = a.Endurance + b.Endurance;
            result.Agility = a.Agility + b.Agility;
            result.Faith = a.Faith + b.Faith;
            return result;
        }
        public static BaseStats operator -(BaseStats a, BaseStats b){
            BaseStats result = new BaseStats();
            result.Health = a.Health - b.Health;
            result.Damage = a.Damage - b.Damage;
            result.Defense = a.Defense - b.Defense;
            result.Endurance = a.Endurance - b.Endurance;
            result.Agility = a.Agility - b.Agility;
            result.Faith = a.Faith - b.Faith;
            return result;
        }
        public int GetTotalStat()
        {
            int result = 0;
            result += Health;
            result += Damage;
            result += Defense;
            result += Endurance;
            result += Agility;
            result += Faith;
            return result;
        }
    }
}