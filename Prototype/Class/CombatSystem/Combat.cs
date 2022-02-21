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
                EnemyTurn();
            }

            m_Player.GainExp(m_Enemy.ExpReward);
            Game.Instance?.MainOptions();
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

            AudioManager.PlaySoundEffect(ESoundEffect.Select);

            switch(input)
            {
                case 1: m_Enemy.TakeDamage(m_Player.GetDamage()); break;
                case 2: break;
                case 3: PlayerConsumableOptions(); break;
            }

            DrawCombat();
        }

        private void EnemyTurn()
        {
            m_Player.TakeDamage(m_Enemy.GetDamage());
            DrawCombat();
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
            if (a_Item == null)
                return;

            byte input = byte.MinValue;

            Console.Clear();
            a_Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Use                    ║\n" +
                          "║                             ║\n" +
                          "║   3- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"What you wanna do with -> {a_Item.Name}: "); }
            while (input < 1 || input > 2);

            switch (input)
            {
                case 1:
                    if (a_Item is ConsumableItem)
                    {
                        ConsumableItem? cons = a_Item as ConsumableItem;
                        if (cons != null)
                        {
                            AudioManager.PlaySoundEffect(ESoundEffect.Drink);
                            m_Player?.Regen(cons.HealthAmount, cons.ManaAmount);
                            m_Player?.Inventory.RemoveItem(a_Item);
                        }
                    }
                    PlayerConsumableOptions();
                    break;
                case 2:
                    PlayerConsumableOptions();
                    break;
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