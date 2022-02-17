namespace Prototype
{
    public class Spell
    {
        private int m_Price = 0;
        private string m_Name = "";
        private ESpellType m_Type;

        private int m_ArmorBuff = 0;
        private int m_StrengthBuff = 0;
        private int m_DexterityBuff = 0;
        private int m_VitalityBuff = 0;
        private int m_EnergyBuff = 0;
        private int m_MinDamage = 0;
        private int m_MaxDamage = 0;
        private int m_HealAmount = 0;
        private int m_ManaAmount = 0;
        private int m_ManaCost = 0;

        public int Price { get => m_Price; }
        public string Name { get => m_Name; }
        public ESpellType Type { get => m_Type; }
        public int ArmorBuff { get => m_ArmorBuff; }
        public int StrengthBuff { get => m_StrengthBuff; }
        public int DexterityBuff { get => m_DexterityBuff; }
        public int VitalityBuff { get => m_VitalityBuff; }
        public int EnergyBuff { get => m_EnergyBuff; }
        public int MinDamage { get => m_MinDamage; }
        public int MaxDamage { get => m_MaxDamage; }
        public int HealAmount { get => m_HealAmount; }
        public int ManaAmount { get => m_ManaAmount; }
        public int ManaCost { get => m_ManaCost; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="a_BuffStats">Need to enter 10 value, not more or less. </param>
        public Spell(int a_Price, string a_Name, ESpellType a_Type, int[] a_BuffStats)
        {
            m_Price = a_Price;
            m_Name = a_Name;
            m_Type = a_Type;

            m_ArmorBuff = a_BuffStats[0];
            m_StrengthBuff = a_BuffStats[1];
            m_DexterityBuff= a_BuffStats[2];
            m_VitalityBuff= a_BuffStats[3];
            m_EnergyBuff = a_BuffStats[4];
            m_MinDamage = a_BuffStats[5];
            m_MaxDamage = a_BuffStats[6];
            m_HealAmount = a_BuffStats[7];
            m_ManaAmount = a_BuffStats[8];
            m_ManaCost = a_BuffStats[9];
        }

        public void DrawSpell()
        {
            Console.WriteLine("¤════════════════════¤");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Name: {m_Name}\n" +
                          $"Type: {m_Type}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            if (m_ArmorBuff != 0)
                Console.Write($"+{m_ArmorBuff} Armor\n");
            if (m_StrengthBuff != 0)
                Console.Write($"+{m_StrengthBuff} Strength\n");
            if (m_DexterityBuff != 0)
                Console.Write($"+{m_DexterityBuff} Dexterity\n");
            if (m_VitalityBuff != 0)
                Console.Write($"+{m_VitalityBuff} Vitality\n");
            if (m_EnergyBuff != 0)
                Console.Write($"+{m_EnergyBuff} Energy\n");
            if (m_HealAmount != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\nHeal +{m_HealAmount} Health Points\n");
            }
            if (m_ManaAmount != 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Regen +{m_ManaAmount} Mana Points\n");
            }
            Console.ResetColor();
            if (m_MinDamage != 0 && m_MaxDamage != 0)
            {
                Console.Write($"\nDamage: {m_MinDamage}-{m_MaxDamage}\n");
            }
            Console.Write($"\nCost: {m_ManaCost} Mana\n");
            Console.WriteLine("¤════════════════════¤");
        }
    }
}