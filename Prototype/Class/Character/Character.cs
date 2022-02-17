namespace Prototype
{
    public abstract class Character
    {
        protected string p_Name = "";

        protected int p_Health = 100;
        protected int p_Mana = 100;
        protected int p_Armor = 0;
        protected int p_Strength = 0;
        protected int p_Dexterity = 0;
        protected int p_Vitality = 0;
        protected int p_Energy = 0;

        protected EClass p_Class = EClass.NONE;

        protected int p_Exp = 0;
        protected int p_Level = 1;

        private int m_MaxExp = 100;

        //public string Name { get => p_Name; }
        //public int Health { get => p_Health; }
        //public int Mana { get => p_Mana;}
        //public int Armor { get => p_Armor;}
        //public int Strength { get => p_Strength; }
        //public int Dexterity { get => p_Dexterity; }
        //public int Vitality { get => p_Vitality; }
        //public int Energy { get => p_Energy; }
        //public int Level { get => p_Level; }
        //public EClass Class { get => p_Class; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="a_Stats">Need to enter 7 value, not more or less. </param>
        public Character(string a_Name, int[] a_Stats)
        {
            p_Name = a_Name;
            p_Health = a_Stats[0];
            p_Mana = a_Stats[1];
            p_Armor = a_Stats[2];
            p_Strength = a_Stats[3];
            p_Dexterity = a_Stats[4];
            p_Vitality = a_Stats[5];
            p_Energy = a_Stats[6];
        }

        protected void GainExp(int a_Amount)
        {
            p_Exp += a_Amount;
            if (p_Exp >= m_MaxExp)
            {
                int remaining = p_Exp - m_MaxExp;
                p_Exp = 0;
                p_Exp += remaining;
                m_MaxExp *= 2;
                p_Level++;
            }
        }

        protected void AddStats(int[] a_Stats)
        {
            p_Armor += a_Stats[0];
            p_Strength += a_Stats[1];
            p_Dexterity += a_Stats[2];
            p_Vitality += a_Stats[3];
            p_Energy += a_Stats[4];

            p_Health += p_Vitality / 2;
            p_Mana += p_Energy / 2;
        }

        protected void RemoveStats(int[] a_Stats)
        {
            p_Armor -= a_Stats[0];
            p_Strength -= a_Stats[1];
            p_Dexterity -= a_Stats[2];
            p_Vitality -= a_Stats[3];
            p_Energy -= a_Stats[4];

            p_Health -= a_Stats[3] / 2;
            p_Mana -= a_Stats[4] / 2;
        }

        public abstract void DrawStats();
    }
}