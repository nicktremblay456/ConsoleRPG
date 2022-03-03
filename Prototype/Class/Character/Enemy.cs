namespace Prototype
{
    public class Enemy : Character
    {
        private int m_MinDamage = 0;
        private int m_MaxDamage = 0;
        private int m_CurrentHealth = 0;
        private int m_CurrentMana = 0;
        private int m_ExpReward = 0;
        private int m_GoldReward = 0;

        private bool m_IsDead = false;

        public int CurrentHealth { get => m_CurrentHealth; }
        public int CurrentMana { get => m_CurrentMana; }
        public int ExpReward { get => m_ExpReward; }
        public int GoldReward 
        { 
            get
            {
                Random r = new Random();
                m_GoldReward = 2 * p_Level;
                return r.Next(0, m_GoldReward + 1);
            }
        }
        //public bool IsDead {  get => m_IsDead; }

        public Enemy(string a_Name, int a_ExpReward, int a_MinDamage, int a_MaxDamage, int[] a_Stats, int a_Level) : base(a_Name, a_Stats)
        {
            m_MinDamage = a_MinDamage;
            m_MaxDamage = a_MaxDamage;
            m_CurrentHealth = p_Health;
            m_CurrentMana = p_Mana;
            p_Level = a_Level;

            m_ExpReward = a_ExpReward;
            m_GoldReward = 2 * p_Level;
        }

        private void CastSpell()
        {

        }

        public int GetDamage()
        {
            Random r = new Random();
            int minDamage = m_MinDamage + (p_Strength / 2), maxDamage = m_MaxDamage + (p_Strength / 2);

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
                m_CurrentHealth = 0;
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
                          $"Level: {p_Level}\n");
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