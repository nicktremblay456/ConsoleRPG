namespace Prototype
{
    public class Inventory
    {
        private const int MAX_SPACE = 25;

        private List<Item> m_Items;
        private List<ConsumableItem> m_Consumables = new List<ConsumableItem>();
        private int m_Gold = 0;// Start game with 50 gold

        public int Gold { get => m_Gold; }
        public int InventoryCount { get => m_Items.Count; }
        public int ConsumableCount { get => m_Consumables.Count; }


        public Inventory()
        {
            m_Items = new List<Item>();
        }

        public void AddGold(int a_Amount)
        {
            m_Gold += a_Amount;
        }

        public void BuyItem(int a_Price)
        {
            if (m_Gold >= a_Price)
            {
                m_Gold -= a_Price;
                AudioManager.PlaySoundEffect(ESoundEffect.BuyItem);
            }
        }

        public void SellItem(Item a_Item)
        {
            m_Gold += a_Item.Price;
            RemoveItem(a_Item);
        }

        public void AddItem(Item a_Item)
        {
            if (m_Items.Count == MAX_SPACE)
            {
                Console.WriteLine($"Inventory is full... {a_Item.Name} is destroyed");
                return;// Inventory is full
            }

            m_Items.Add(a_Item);
        }

        public void AddItems(Item[] a_Items)
        {
            foreach(Item item in a_Items)
            {
                if (m_Items.Count == MAX_SPACE)
                {
                    Console.WriteLine($"Inventory is full... {item.Name} is destroyed");
                    continue;// Inventory is full
                }

                m_Items.Add(item);
            }
        }

        public void RemoveItem(Item a_Item)
        {
            m_Items.Remove(a_Item);
        }

        public void RemoveItems(Item[] a_Items)
        {
            foreach (Item item in a_Items)
            {
                m_Items.Remove(item);
            }
        }

        public Item? GetItem(int a_Index)
        {
            return m_Items[a_Index];
        }

        public ConsumableItem? GetConsumable(int a_Index)
        {
            return m_Consumables[a_Index];
        }

        public void DrawConsumable()
        {
            m_Consumables = new List<ConsumableItem>();
            foreach (Item item in m_Items)
            {
                if (item is ConsumableItem)
                    m_Consumables.Add(item as ConsumableItem);
            }
            Console.WriteLine("¤═════════════════════════════¤\n" +
                              "         ¤ Consumable ¤        \n");
            Console.Write("0: Back\n\n");
            for (int i = 0; i < m_Consumables.Count; i++)
            {
                Console.Write($"{i + 1}: {m_Consumables[i].Name}\n");
            }
            Console.WriteLine("\n¤═════════════════════════════¤");
        }

        public void DrawInventory()
        {
            Console.WriteLine("¤═════════════════════════════¤\n" +
                              "         ¤ Inventory ¤         \n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Gold: {m_Gold}\n\n");
            Console.ResetColor();

            Console.Write("0: Back\n\n");
            for (int i = 0; i < m_Items.Count; i++)
            {
                Console.Write($"{i + 1}: {m_Items[i].Name}\n");
            }
            Console.WriteLine("\n¤═════════════════════════════¤");
        }
    }
}