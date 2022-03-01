namespace Prototype
{
    public class SpellBook
    {
        private List<Spell> m_Spells;
        public int SpellCount { get => m_Spells.Count; }

        public SpellBook()
        {
            m_Spells = new List<Spell>();
        }

        public Spell GetSpell(int a_Index)
        {
            return m_Spells[a_Index];
        }

        public void LearnSpell(Spell a_Spell)
        {
            if (!m_Spells.Contains(a_Spell))
                m_Spells.Add(a_Spell);
        }

        public Spell[] GetSpellByType(ESpellType a_Type)
        {
            List<Spell> spells = new List<Spell>();
            foreach (Spell spell in m_Spells)
            {
                if (spell.Type == a_Type)
                    spells.Add(spell);
            }
            return spells.ToArray();
        }

        public void DrawSpellBook()
        {
            // Seperate spell by type
            Spell[] damageSpells = GetSpellByType(ESpellType.Damage);
            Spell[] buffSpells = GetSpellByType(ESpellType.Buff);
            Spell[] healSpells = GetSpellByType(ESpellType.Heal);
            int counter = 0;
            
            Console.WriteLine("¤═════════════════════════════¤\n" +
                              "        ¤ Spell Book ¤         \n");
            Console.Write("\n0: Back\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nDamage Spells\n");
            if (damageSpells.Length > 0)
            {
                Console.ResetColor();
                for (int i = 0; i < damageSpells.Length; i++)
                {
                    counter++;
                    Console.WriteLine($"{counter}: ");
                    damageSpells[i].DrawSpell();
                }
            }
            else
                Console.WriteLine("<Empty>\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\nBuff Spells\n");
            if (buffSpells.Length > 0)
            {
                Console.ResetColor();
                for (int i = 0; i < buffSpells.Length; i++)
                {
                    counter++;
                    Console.WriteLine($"{counter}: ");
                    buffSpells[i].DrawSpell();
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
                    counter++;
                    Console.WriteLine($"{counter}: ");
                    healSpells[i].DrawSpell();
                }
            }
            else
                Console.WriteLine("<Empty>\n");
            Console.ResetColor();
            Console.WriteLine("\n¤═════════════════════════════¤");
        }
    }
}