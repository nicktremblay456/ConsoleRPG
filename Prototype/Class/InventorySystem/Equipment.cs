namespace Prototype
{
    public class Equipment
    {
        private List<EquipmentSlot> m_EquipmentSlots = new List<EquipmentSlot>();
        private Inventory m_Inventory;


        public Equipment(ref Inventory a_Inventory)
        {
            m_Inventory = a_Inventory;
            // Create a slot for each part. Head, Chest, Legs, etc...
            for (int i = 0; i < (int)EEquipableType.Count; i++)
            {
                m_EquipmentSlots.Add(new EquipmentSlot((EEquipableType)i));
            }
        }

        public void EquipItem(EquipableItem a_Item)
        {
            foreach (EquipmentSlot slot in m_EquipmentSlots)
            {
                if (slot.EquipableType == a_Item.EquipableType)
                {
                    if (slot.EquipableItem != null)
                        m_Inventory.AddItem(slot.EquipableItem);
                    slot.Equip(a_Item);
                    break;// Don't need to look for other slot since we already found the one we needed.
                }
            }
        }

        public void EquipItems(EquipableItem[] a_Items)
        {
            foreach (EquipmentSlot slot in m_EquipmentSlots)
            {
                for (int i = 0; i < a_Items.Length; i++)
                {
                    if (slot.EquipableType == a_Items[i].EquipableType)
                    {
                        if (slot.EquipableItem != null)
                            m_Inventory.AddItem(slot.EquipableItem);
                        slot.Equip(a_Items[i]);
                    }
                }
            }
        }

        public void UnequipItem(EquipableItem a_Item)
        {
            foreach (EquipmentSlot slot in m_EquipmentSlots)
            {
                if (slot.EquipableType == a_Item.EquipableType)
                {
                    slot.Unequip();
                }
            }
        }

        public EquipableItem? GetItem(int a_Index)
        {
            return m_EquipmentSlots[a_Index].EquipableItem;
        }

        public EquipableItem? GetEquipedWeapon()
        {
            foreach (EquipmentSlot slot in m_EquipmentSlots)
            {
                if (slot.EquipableType == EEquipableType.MainHand)
                    return slot.EquipableItem;
            }

            return null;
        }

        public void DrawEquipment()
        {
            for (int i = 0; i < m_EquipmentSlots.Count; i++)
            {
                if (m_EquipmentSlots[i].EquipableItem != null)
                {
                    Console.WriteLine($"{i}: ");
                    m_EquipmentSlots[i].EquipableItem?.DrawItem();
                }
                else
                    Console.WriteLine($"\n{i}: ({m_EquipmentSlots[i].EquipableType}) Empty\n");
            }
            Console.Write($"\n{m_EquipmentSlots.Count}: Back\n");
        }
    }
}