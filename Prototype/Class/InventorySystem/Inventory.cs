namespace Prototype
{
    public class Inventory
    {
        private const int MAX_SPACE = 25;

        private List<Item> m_Items;
        private int m_Gold = 0;// Start game with 50 gold

        public int Gold { get => m_Gold; }
        public int InventoryCount { get => m_Items.Count; }


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
                m_Gold -= a_Price;
        }

        public void SellItem(Product a_Product)
        {
            m_Gold += a_Product.Price;
            RemoveItem(a_Product.Item);
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