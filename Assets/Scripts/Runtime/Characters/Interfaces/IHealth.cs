namespace Runtime.Characters.Interfaces
{
    public interface IHealth
    {
        public float GiveDamage(float damage);
        public float GetHealth();
        public void ResetHealth();
    }
}