namespace Prototype
{
    public class Combat
    {
        private Player m_Player;
        private Enemy m_Enemy;

        public Combat(ref Player a_Player, ref Enemy a_Enemy)
        {
            m_Player = a_Player;
            m_Enemy = a_Enemy;
        }

        public void Fight()
        {
            Console.Clear();
            DrawCombat();

            while (m_Player.CurrentHealth > 0 && m_Enemy.CurrentHealth > 0)
            {
                PlayerTurn();
                if (m_Enemy.CurrentHealth <= 0)
                    continue;
                Thread.Sleep(1000);// With 1 sec between each turn.
                EnemyTurn();
            }


            if (m_Enemy.CurrentHealth <= 0)
            {
                m_Player.GainExp(m_Enemy.ExpReward);
                Console.WriteLine($"\nYou gain {m_Enemy.ExpReward} exp\n");
            }
            Thread.Sleep(2000);// Wait 2 sec before ending combat.
            Game.Instance?.ArenaOptions();
        }

        private void PlayerTurn()
        {
            byte input = byte.MinValue;

            Console.Write("╔═════════════════════════════╗\n" +
                          "║         ¤ Action ¤          ║\n" +
                          "║                             ║\n" +
                          "║   1- Attack                 ║\n" +
                          "║   2- Spell                  ║\n" +
                          "║   3- Inventory              ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, "Enter your action choice: "); }
            while (input < 1 || input > 3);

            AudioManager.Instance?.PlaySoundEffect(ESoundEffect.Select);

            switch(input)
            {
                case 1:
                    EquipableItem weapon = m_Player.Equipment?.GetEquipedWeapon();
                    if (weapon != null)
                    {
                        if (weapon.WeaponType == EWeaponType.MELEE || weapon.WeaponType == EWeaponType.MAGICAL)
                            AudioManager.Instance?.PlaySoundEffect(ESoundEffect.Swing); 
                        else if (weapon.WeaponType == EWeaponType.RANGED)
                            AudioManager.Instance?.PlaySoundEffect(ESoundEffect.ArrowHit);
                    }
                    m_Enemy.TakeDamage(m_Player.GetDamage()); 
                    break;
                case 2: PlayerSpellOptions(); break;
                case 3: PlayerConsumableOptions(); break;
            }

            DrawCombat();
        }

        private void EnemyTurn()
        {
            int damage = m_Enemy.GetDamage();
            m_Player.TakeDamage(damage);
            DrawCombat();
        }

        private void PlayerSpellOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            m_Player.SpellBook.DrawSpellBook();

            do { GetInput(ref input, "Select a spell: "); }
            while(input > m_Player.SpellBook.SpellCount);

            if (input == 0)
                return;

            SelectedSpellOptions(m_Player.SpellBook.GetSpell(input - 1));
        }

        private void SelectedSpellOptions(Spell? a_Spell)
        {
            if (a_Spell == null) return;

            byte input = byte.MinValue;

            Console.Clear();
            a_Spell.DrawSpell();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║      ¤ Spell options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Cast                   ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"Do you want to cast -> {a_Spell.Name}: "); }
            while (input < 1 || input > 2);

            if (input == 1)
            {
                m_Player.CastSpell(a_Spell);
                switch (a_Spell.Type)
                {
                    case ESpellType.Damage: AudioManager.Instance?.PlaySoundEffect(ESoundEffect.SpellDamage); m_Enemy.TakeDamage(m_Player.GetSpellDamage(a_Spell)); break;
                    case ESpellType.Buff: break;
                    case ESpellType.Heal:
                        int healthRegenValue = m_Player.GetHealthRegenPower(a_Spell), manaRegenValue = m_Player.GetManaRegenPower(a_Spell);
                        AudioManager.Instance?.PlaySoundEffect(ESoundEffect.SpellHeal);
                        m_Player.Regen(healthRegenValue, manaRegenValue);
                        break;
                }
            }
            else if (input == 2)
                PlayerSpellOptions();
        }

        private void PlayerConsumableOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            m_Player.Inventory.DrawConsumable();

            do { GetInput(ref input, "Select an item: "); }
            while (input > m_Player.Inventory.ConsumableCount);

            if (input == 0)
                return;

            SelectedConsumableOptions(m_Player.Inventory.GetConsumable(input - 1));
        }

        private void SelectedConsumableOptions(Item? a_Item)
        {
            if (a_Item == null) return;

            byte input = byte.MinValue;

            Console.Clear();
            a_Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Use                    ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"What you wanna do with -> {a_Item.Name}: "); }
            while (input < 1 || input > 2);

            if (input == 1)
            {
                if (a_Item is ConsumableItem)
                {
                    ConsumableItem? cons = a_Item as ConsumableItem;
                    if (cons != null)
                    {
                        AudioManager.Instance?.PlaySoundEffect(ESoundEffect.Drink);
                        m_Player?.Regen(cons.HealthAmount, cons.ManaAmount);
                        m_Player?.Inventory.RemoveItem(a_Item);
                    }
                }
            }
        }

        private void GetInput(ref byte a_Input, string a_Text)
        {
            Console.Write(a_Text);
            try { a_Input = byte.Parse(Console.ReadLine()); }
            catch { a_Input = 99; }// Just a random number to make sure it will be refused, to prompt user again.
        }

        private void DrawCombat()
        {
            Console.Clear();
            m_Player.DrawStats();
            m_Enemy.DrawStats();
        }
    }
}