using Phase1;

namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
        FactionType Faction { get; }
    }
}