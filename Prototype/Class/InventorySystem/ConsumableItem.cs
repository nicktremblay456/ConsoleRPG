namespace Prototype
{
    public sealed class ConsumableItem : Item
    {
        private int m_HealthAmount = 0;
        private int m_ManaAmount = 0;

        public int HealthAmount { get => m_HealthAmount; }
        public int ManaAmount { get => m_ManaAmount; }

        public ConsumableItem(int a_Price, string a_Name, EQuality a_Quality, int a_HealthAmount, int a_ManaAmount) : base(a_Price, a_Name, a_Quality)
        {
            m_HealthAmount = a_HealthAmount;
            m_ManaAmount = a_ManaAmount;
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
            Console.WriteLine("¤════════════════════¤\n");
            Console.ForegroundColor = qualityColor;
            Console.Write($"{p_Name}\n");
            Console.Write($"{p_Quality}\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (m_HealthAmount > 0)
                Console.Write($"+{m_HealthAmount} Health Points\n");
            if (m_ManaAmount > 0)
                Console.Write($"+{m_ManaAmount} Mana Points\n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"\nValue: {p_Price} Gold\n");
            Console.ResetColor();
            Console.WriteLine("\n¤════════════════════¤");
        }
    }
}