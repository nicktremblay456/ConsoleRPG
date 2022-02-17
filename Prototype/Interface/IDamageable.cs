namespace Prototype
{
    public interface IDamageable
    {
        void TakeDamage(int a_Amount);
        void Regen(int a_HealthAmount = 0, int a_ManaAmount = 0);
    }
}