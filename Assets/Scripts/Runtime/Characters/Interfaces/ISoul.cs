namespace Runtime.Characters.Interfaces
{
    public interface ISoul
    {
        public float GiveSoulDamage(float damage);
        public float GetSoulGauge();
        public void ResetSoulGauge();
        public void Groggy();
    }
}