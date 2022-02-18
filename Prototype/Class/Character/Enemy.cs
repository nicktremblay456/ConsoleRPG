namespace Prototype
{
    public class Enemy : Character
    {
        private int m_CurrentHealth = 0;
        private int m_CurrentMana = 0;

        private bool m_IsDead = false;
        public bool IsDead {  get => m_IsDead; }

        public Enemy(string a_Name, int[] a_Stats) : base(a_Name, a_Stats)
        {
            m_CurrentHealth = p_Health;
            m_CurrentMana = p_Mana;
        }

        private void CastSpell()
        {

        }

        public int GetDamage()
        {
            Random r = new Random();
            int minDamage = 1, maxDamage = 1;

            /*
             * int minWeaponDmg = m_Inventory.GetWeapon.().GetWeapon().MinDamage;
             * int maxWeaponDmg m_Inventory.GetWeapon.().GetWeapon().MaxDamage;
             * if (m_Inventory.GetWeapon.() != null)
             * minDamage = m_Inventory.GetWeapon.().WeaponType == EWeaponType.Melee ? minWeaponDamage + (p_Strength * 0.5f) : minWeaponDamage + (p_Dexterity * 0.5f);
             * maxDamage = m_Inventory.GetWeapon.().WeaponType == EWeaponType.Melee ? maxWeaponDamage + (p_Strength * 0.5f) : maxWeaponDamage + (p_Dexterity * 0.5f);
             * return r.Next(minDamage, maxDamage + 1);
             * else return 1 + (p_Strength * 0.5f);
             */

            return r.Next(minDamage, maxDamage + 1);
        }

        #region Abstract Methods
        public override void TakeDamage(int a_Amount)
        {
            float damage = a_Amount - (p_Armor * 0.5f);
            m_CurrentHealth -= (int)damage;
            if (m_CurrentHealth <= 0)
            {
                m_IsDead = true;
                //Death
            }
        }

        public override void Regen(int a_HealthAmount = 0, int a_ManaAmount = 0)
        {
            if (a_HealthAmount > 0)
            {
                m_CurrentHealth += a_HealthAmount;
                if (m_CurrentHealth > p_Health)
                    m_CurrentHealth = p_Health;
            }
            if (a_ManaAmount > 0)
            {
                m_CurrentMana += a_ManaAmount;
                if (m_CurrentMana > p_Mana)
                    m_CurrentMana = p_Mana;
            }
        }

        public override void DrawStats()
        {
            Console.WriteLine("¤═════════════════════════════¤");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Name: {p_Name}\n" +
                          $"Level: {p_Level}\n" +
                          $"Class: {p_Class}\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Health: {m_CurrentHealth}\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"Mana: {m_CurrentMana}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"Armor: {p_Armor}\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Strength: {p_Strength}\n" +
                          $"Dexterity: {p_Dexterity}\n" +
                          $"Vitality: {p_Vitality}\n" +
                          $"Energy: {p_Energy}\n");

            Console.ResetColor();
            Console.WriteLine("¤═════════════════════════════¤");
        }
        #endregion
    }
}