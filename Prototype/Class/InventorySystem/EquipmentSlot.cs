namespace Prototype
{
    public class EquipmentSlot
    {
        private EEquipableType m_EquipableType;
        private EquipableItem? m_EquipableItem = null;

        public EEquipableType EquipableType { get => m_EquipableType; }
        public EquipableItem? EquipableItem { get => m_EquipableItem; }

        public EquipmentSlot(EEquipableType a_Type)
        {
            m_EquipableType = a_Type;
        }

        public void Equip(EquipableItem a_Item)
        {
            m_EquipableItem = a_Item;
        }

        public void Unequip()
        {
            m_EquipableItem = null;
            // Add to inventory
        }
    }
}