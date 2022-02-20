namespace Prototype
{
    public sealed class WizardNpc : NPC
    {
        private Spell[] m_Spells;
        public int SpellsCount { get => m_Spells.Length; }

        public WizardNpc(string a_Name, Spell[] a_Spells, Product[] a_Products) : base(a_Name, a_Products)
        {
            m_Spells = a_Spells;
        }

        public Spell GetSpell(int a_Index)
        {
            return m_Spells[a_Index];
        }

        private Spell[] GetSpellByType(ESpellType a_Type)
        {
            List<Spell> spells = new List<Spell>();
            foreach (Spell spell in m_Spells)
            {
                if (spell.Type == a_Type)
                    spells.Add(spell);
            }
            return spells.ToArray();
        }

        public void DrawSpellMarket()
        {
            Spell[] damageSpells = GetSpellByType(ESpellType.Damage);
            Spell[] buffSpells = GetSpellByType(ESpellType.Buff);
            Spell[] healSpells = GetSpellByType(ESpellType.Heal);
            int counter = 0;

            Console.WriteLine("¤═════════════════════════════¤\n" +
                              "         ¤ Spells ¤           \n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nDamage Spells\n");
            if (damageSpells.Length > 0)
            {
                Console.ResetColor();
                for (int i = 0; i < damageSpells.Length; i++)
                {
                    Console.WriteLine($"\n{counter}: ");
                    Console.Write($"Price: {damageSpells[i].Price} Gold\n");
                    damageSpells[i].DrawSpell();
                    counter++;
                }
            }
            else
                Console.WriteLine("<Empty>\n");
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("\n\nBuff Spells\n");
            if (buffSpells.Length > 0)
            {
                Console.ResetColor();
                for (int i = 0; i < buffSpells.Length; i++)
                {
                    Console.WriteLine($"\n{counter}: ");
                    Console.Write($"Price: {buffSpells[i].Price} Gold\n");
                    buffSpells[i].DrawSpell();
                    counter++;
                }
            }
            else
                Console.WriteLine("<Empty>\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nHealing Spells\n");
            if (healSpells.Length > 0)
            {
                Console.ResetColor();
                for (int i = 0; i < healSpells.Length; i++)
                {
                    Console.WriteLine($"\n{counter}: ");
                    Console.Write($"Price: {healSpells[i].Price} Gold\n");
                    healSpells[i].DrawSpell();
                    counter++;
                }
            }
            else
                Console.WriteLine("<Empty>\n");
            Console.ResetColor();
            Console.Write($"\n{counter}: Back\n");
            Console.WriteLine("\n¤═════════════════════════════¤");
        }
    }
}