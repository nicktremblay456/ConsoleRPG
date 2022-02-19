using WMPLib;

namespace Prototype
{
    public sealed class Game
    {
        private static Game? m_Instance;
        public static Game? Instance { get => m_Instance; }

        private GameMusic m_Music;

        private MainMenu m_MainMenu;

        private NPC m_Alchemist;
        private NPC m_Blacksmith;
        private WizardNpc m_Wizard;

        private Player? m_Player = null;
        
        public Player? Player { get => m_Player; }


        public Game()
        {
            m_Instance = this;

            m_MainMenu = new MainMenu();

            // Fetch Data for NPC
            m_Alchemist = new NPC("Alchemist", GameData.GetAlchemistItems("AlchemistMarketData.json"));
            m_Blacksmith = new NPC("Blacksmith", GameData.GetMarketProducts("BsMarketData.json"));
            m_Wizard = new WizardNpc("Wizard", GameData.GetWizardSpells("WizardSpellMarket.json"), 
                                               GameData.GetMarketProducts("WizardItemMarket.json"));

            m_Music = new GameMusic();
        }

        public void ShowMainMenu()
        {
            m_MainMenu.ShowMainMenu();
        }

        private void MainOptions()
        {
            m_Music.PlayMusic(EMusic.Town);
            Console.Clear();
            //m_Player?.DrawStats();

            byte input = byte.MinValue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("╔═════════════════════════════╗\n" +
                          "║          ¤ Town ¤           ║\n" +
                          "║                             ║\n" +
                          "║   1- Enter Arena            ║\n" +
                          "║   2- Enter alchemist lab    ║\n" +
                          "║   3- Enter the blacksmith   ║\n" +
                          "║      shop                   ║\n" +
                          "║   4- Enter wizard academy   ║\n" +
                          "║                             ║\n" +
                          "║   5- Inventory              ║\n" +
                          "║   6- Equipment              ║\n" +
                          "║   7- Spell Book             ║\n" +
                          "║                             ║\n" +
                          "║   8- Back to main menu      ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");
            Console.ResetColor();

            do { GetInput(ref input, "Enter your choice: "); }
            while (input < 1 || input > 8);

            switch (input)
            {
                case 1: break;// Arena
                case 2: AlchemistOptions(); break;
                case 3: m_Music.PlayMusic(EMusic.Market); BlacksmithOptions(); break;
                case 4: WizardOptions(); break;
                case 5: InventoryOptions(); break;
                case 6: EquipmentOptions(); break;
                case 7: SpellBookOptions(); break;
                case 8: ShowMainMenu(); break;
            }
        }

        private void AlchemistOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            m_Alchemist.DrawMarket();

            do { GetInput(ref input, "Select an item to buy: "); }
            while (input > m_Alchemist.ProductsCount + 2);

            if (input == m_Alchemist.ProductsCount + 1)
                MainOptions();
            else
                SelectedAlchItemOptions(m_Alchemist.GetProduct(input - 1));
        }

        private void SelectedAlchItemOptions(Product a_Product)
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            a_Product.Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Buy                    ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"Do you want to buy? -> {a_Product.Item.Name}: "); }
            while (input > 2);

            if (input == 1)
            {
                if (m_Player?.Inventory.Gold >= a_Product.Price)
                {
                    m_Player.Inventory.BuyItem(a_Product.Price);
                    m_Player.Inventory.AddItem(a_Product.Item);
                }
                AlchemistOptions();
            }
            else if (input == 2)
                AlchemistOptions();
            
        }

        private void BlacksmithOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            m_Blacksmith.DrawMarket();

            do { GetInput(ref input, "Select an item to buy: "); }
            while (input > m_Blacksmith.ProductsCount + 1);

            //if (input == m_Blacksmith.ProductsCount + 1)
            //    SellingItemOptions(BlacksmithOptions);
            if (input == m_Blacksmith.ProductsCount + 1)
                MainOptions();
            else
                SelectedBsItemOptions(m_Blacksmith.GetProduct(input - 1));
        }

        private void SelectedBsItemOptions(Product a_Product)
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            a_Product.Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Buy                    ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"Do you want to buy? -> {a_Product.Item.Name}: "); }
            while (input < 1 || input > 2);

            switch(input)
            {
                case 1:
                    if (m_Player?.Inventory.Gold >= a_Product.Price)
                    {
                        m_Player.Inventory.BuyItem(a_Product.Price);
                        m_Player.Inventory.AddItem(a_Product.Item);
                    }
                    BlacksmithOptions();
                    break;
                case 2: BlacksmithOptions(); break;
            }
        }

        private void WizardOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("╔═════════════════════════════╗\n" +
                          "║      ¤ Wizard Academy ¤     ║\n" +
                          "║                             ║\n" +
                          "║   1- Buy Equipments         ║\n" +
                          "║   2- Buy Spells             ║\n" +
                          "║                             ║\n" +
                          "║   3- Leave                  ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");
            Console.ResetColor();

            do { GetInput(ref input, "Enter your choice: "); }
            while (input < 1 || input > 3);

            switch (input)
            {
                case 1: WizardEquipmentOptions(); break;
                case 2: WizardSpellOptions(); break;
                case 3: MainOptions(); break;
            }
        }

        private void WizardEquipmentOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            m_Wizard.DrawMarket();

            do { GetInput(ref input, "Select an item to buy: "); }
            while (input > m_Wizard.ProductsCount + 2);

            if (input == m_Wizard.ProductsCount + 1)
                WizardOptions();
            else
                SelectedWizardItemOptions(m_Wizard.GetProduct(input - 1));
        }

        private void SelectedWizardItemOptions(Product a_Product)
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            a_Product.Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Buy                    ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"Do you want to buy? -> {a_Product.Item.Name}: "); }
            while (input < 1 || input > 2);

            switch (input)
            {
                case 1:
                    m_Player?.Inventory.BuyItem(a_Product.Price);
                    m_Player?.Inventory.AddItem(a_Product.Item);
                    WizardOptions();
                    break;
                case 2: WizardOptions(); break;
            }
        }

        private void WizardSpellOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            m_Wizard.DrawSpellMarket();

            do { GetInput(ref input, "Select a spell to buy: "); }
            while (input > m_Wizard.SpellsCount);

            if (input == m_Wizard.SpellsCount)
                WizardOptions();
            else
                SelectedWizardSpellOptions(m_Wizard.GetSpell(input));
        }

        private void SelectedWizardSpellOptions(Spell a_Spell)
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            a_Spell.DrawSpell();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Learn                  ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"Do you want to learn? -> {a_Spell.Name}: "); }
            while (input < 1 || input > 2);

            switch (input)
            {
                case 1:
                    if (m_Player?.Inventory.Gold >= a_Spell.Price)
                    {
                        m_Player.SpellBook.LearnSpell(a_Spell);
                    }
                    WizardSpellOptions();
                    break;
                case 2: WizardSpellOptions(); break;
            }
        }

        private void InventoryOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.Write($"Your gold -> {m_Player?.Inventory.Gold}\n\n");
            m_Player?.Inventory.DrawInventory();

            do { GetInput(ref input, "Select an item: "); }
            while (input > m_Player?.Inventory.InventoryCount);

            if (input == 0)
                MainOptions();
            else
            {
                try { SelectedItemOptions(m_Player?.Inventory.GetItem(input - 1)); }
                catch { }
            }
        }

        private void SelectedItemOptions(Item? a_Item)
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
                          "║   2- Drop                   ║\n" +
                          "║                             ║\n" +
                          "║   3- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"What you wanna do with -> {a_Item.Name}: "); }
            while (input < 1 || input > 3);

            switch (input)
            {
                case 1:
                    if (a_Item is EquipableItem)
                        m_Player?.EquipItem(a_Item as EquipableItem);
                    else if (a_Item is ConsumableItem)
                    {
                        ConsumableItem? cons = a_Item as ConsumableItem;
                        if (cons != null)
                        {
                            m_Player?.Regen(cons.HealthAmount, cons.ManaAmount);
                            m_Player?.Inventory.RemoveItem(a_Item);
                        }
                    }
                    InventoryOptions();
                    break;
                case 2:
                    m_Player?.Inventory.RemoveItem(a_Item);
                    InventoryOptions();
                    break;
                case 3:
                    InventoryOptions();
                    break;
            }
        }

        private void EquipmentOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            m_Player?.DrawStats();
            m_Player?.Equipment.DrawEquipment();

            do { GetInput(ref input, "Select an item: "); }
            while (input > 8);

            if (input == 8)
                MainOptions();
            else
            {
                try
                {
                    if (m_Player?.Equipment.GetItem(input) != null)
                        SelectedEquipmentOptions(m_Player?.Equipment.GetItem(input));
                    else
                        EquipmentOptions();
                }
                catch { }
            }
        }

        private void SelectedEquipmentOptions(Item? a_Item)
        {
            if (a_Item == null)
                return;

            byte input = byte.MinValue;

            Console.Clear();
            a_Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║    ¤ Equipment options ¤    ║\n" +
                          "║                             ║\n" +
                          "║   1- Unequip                ║\n" +
                          "║   2- Drop                   ║\n" +
                          "║                             ║\n" +
                          "║   3- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"What you wanna do with -> {a_Item.Name}: "); }
            while (input < 1 || input > 3);

            switch (input)
            {
                case 1:
                    if (a_Item is EquipableItem)
                    {
                        m_Player?.UnequipItem(a_Item as EquipableItem);
                        m_Player?.Inventory.AddItem(a_Item);
                    }
                    EquipmentOptions();
                    break;
                case 2:
                    m_Player?.UnequipItem(a_Item as EquipableItem);
                    EquipmentOptions();
                    break;
                case 3: EquipmentOptions(); break;
            }
        }

        private void SpellBookOptions()
        {
            byte input = byte.MinValue;

            Console.Clear();
            m_Player?.SpellBook.DrawSpellBook();

            do { GetInput(ref input, "\nEnter 0 to close your book: "); }
            while (input != 0);

            MainOptions();
        }

        // IN PROGRESS
        private void SellingItemOptions(Action a_OptionFunc)
        {
            byte input = byte.MinValue;

            Console.Clear();
            m_Player?.Inventory.DrawInventory();

            do { GetInput(ref input, "Select an item to sell: "); }
            while(input > m_Player?.Inventory.InventoryCount);

            if (input == 0)
            {
                if (a_OptionFunc != null)
                    a_OptionFunc.Invoke();
            }
            else
                SelectedItemToSell(m_Player?.Inventory.GetItem(input), a_OptionFunc);
        }

        // IN PROGRESS
        private void SelectedItemToSell(Item? a_Item, Action a_OptionFunc)
        {
            if (a_Item == null)
                return;

            byte input = byte.MinValue;
            Console.Clear();
            a_Item.DrawItem();

            Console.Write("╔═════════════════════════════╗\n" +
                          "║       ¤ Item options ¤      ║\n" +
                          "║                             ║\n" +
                          "║   1- Sell                   ║\n" +
                          "║                             ║\n" +
                          "║   2- Back                   ║\n" +
                          "║                             ║\n" +
                          "╚═════════════════════════════╝\n");

            do { GetInput(ref input, $"Do you want sell {a_Item.Name}? : "); }
            while (input > 2);

            if (input == 0)
                m_Player?.Inventory.SellItem(a_Item);
            else
                SellingItemOptions(a_OptionFunc);
        }

        private void GetInput(ref byte a_Input, string a_Text)
        {
            Console.Write(a_Text);
            try { a_Input = byte.Parse(Console.ReadLine()); }
            catch { a_Input = 99; }// Just a random number to make sure it will be refused, to prompt user again.
        }

        public void CreateCharacter(string a_Name, EClass a_Class)
        {
            EquipableItem[] items = GameData.GetStartingItems();

            switch (a_Class)
            {
                case EClass.Warrior:
                    m_Player = new Player(a_Name, a_Class, new int[]
                    { 150, 50, 10, 30, 20, 0, 0 });
                    m_Player.Equipment.EquipItems(new EquipableItem[] { items[0],items[1] } );
                    break;
                case EClass.Archer:
                    m_Player = new Player(a_Name, a_Class, new int[]
                    { 100, 100, 10, 15, 30, 0, 0 });
                    m_Player.Equipment.EquipItems(new EquipableItem[] { items[2], items[3] });
                    break;
                case EClass.Sorcerer:
                    m_Player = new Player(a_Name, a_Class, new int[]
                    { 75, 200, 10, 10, 10, 0, 0 });
                    m_Player.Equipment.EquipItems(new EquipableItem[] { items[4], items[5] });
                    break;
            }
            // Starting gold
            m_Player?.Inventory.AddGold(50);

            MainOptions();
        }
    }
}