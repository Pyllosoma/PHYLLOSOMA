namespace Runtime.Characters.Interfaces
{
    public interface IHealth
    {
        public bool IsDead { get; }
        public float MaxHealth { get; }
        public float Health { get; }
        /// <summary>
        /// Add value to current health
        /// </summary>
        /// <param name="health">Value that will be added to current health</param>
        public void GiveDamage(float health,string attackerTag = null);
        /// <summary>
        /// Death
        /// </summary>
        public void Death(); 
        /// <summary>
        /// Reset HealthComponent
        /// </summary>
        public void Reset();
    }
}