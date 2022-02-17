namespace Prototype
{
    public sealed class EquipableItem : Item
    {
        private EEquipableType m_Type;
        private EWeaponType m_WeaponType = EWeaponType.NONE;
        private int m_Armor = 0;
        private int m_Strength = 0;
        private int m_Dexterity = 0;
        private int m_Vitality = 0;
        private int m_Energy = 0;
        private int m_MinDamage = 0;
        private int m_MaxDamage = 0;

        public int Armor { get => m_Armor; }
        public int Strength { get => m_Strength; }
        public int Dexterity { get => m_Dexterity; }
        public int Vitality { get => m_Vitality; }
        public int Energy { get => m_Energy; }
        public int MinDamage { get => m_MinDamage; }
        public int MaxDamage { get => m_MaxDamage; }


        public EEquipableType EquipableType { get => m_Type; }
        public EWeaponType WeaponType { get => m_WeaponType; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="a_ItemStats">Need to enter 7 value, not more or less. </param>
        public EquipableItem(int a_Price, string a_ItemnName, EQuality a_Quality, EEquipableType a_Type, int[] a_ItemStats, EWeaponType a_WeaponType = EWeaponType.NONE) 
            : base(a_Price, a_ItemnName, a_Quality)
        {
            m_Type = a_Type;
            m_Armor = a_ItemStats[0];
            m_Strength = a_ItemStats[1];
            m_Dexterity = a_ItemStats[2];
            m_Vitality = a_ItemStats[3];
            m_Energy = a_ItemStats[4];
            m_MinDamage = a_ItemStats[5];
            m_MaxDamage = a_ItemStats[6];
            
            m_WeaponType = a_WeaponType;
        }

        public override void DrawItem()
        {
            ConsoleColor qualityColor = ConsoleColor.Black;
            switch (p_Quality)
            {
                case EQuality.Poor: qualityColor = ConsoleColor.Gray; break;
                case EQuality.Common: qualityColor = ConsoleColor.White; break;
                case EQuality.Uncommon: qualityColor = ConsoleColor.Green; break;
                case EQuality.Rare: qualityColor = ConsoleColor.DarkBlue; break;
                case EQuality.Epic: qualityColor = ConsoleColor.Cyan; break;
                case EQuality.Legendary: qualityColor = ConsoleColor.Red; break;
            }

            Console.WriteLine("¤════════════════════¤");
            Console.ForegroundColor = qualityColor;
            Console.Write($"{p_Name}\n");
            Console.Write($"{p_Quality}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{m_Type}\n");
            if (m_Armor > 0)
                Console.Write($"+{m_Armor} Armor\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (m_MinDamage > 0 && m_MaxDamage > 0)
                Console.Write($"Damage: {m_MinDamage} - {m_MaxDamage}\n");
            if (m_Strength > 0)
                Console.Write($"+{m_Strength} Strength\n");
            if (m_Dexterity > 0)
                Console.Write($"+{m_Dexterity} Dexterity\n");
            if (m_Vitality > 0)
                Console.Write($"+{m_Vitality} Vitality\n");
            if (m_Energy > 0)
                Console.Write($"+{m_Energy} Energy\n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"\nValue: {p_Price} Gold\n");
            Console.ResetColor();
            Console.WriteLine("¤════════════════════¤");
        }
    }
}