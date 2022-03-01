namespace Prototype
{
    public sealed class Player : Character
    {
        private Equipment m_Equipment;
        private Inventory m_Inventory;
        private SpellBook m_SpellBook;

        private int m_CurrentHealth;
        private int m_CurrentMana;

        private bool m_IsDead = false;
        public bool IsDead { get => m_IsDead; }
        public Equipment Equipment { get => m_Equipment; }
        public Inventory Inventory { get => m_Inventory;}
        public SpellBook SpellBook { get => m_SpellBook;}
        public int CurrentHealth { get => m_CurrentHealth;}
        public int CurrentMana { get => m_CurrentMana;}

        public Player(string a_Name, EClass a_Class, int[] a_Stats) : base(a_Name, a_Stats)
        {
            m_Inventory = new Inventory();
            m_Equipment = new Equipment(ref m_Inventory);
            m_SpellBook = new SpellBook();
            
            m_CurrentHealth = p_Health;
            m_CurrentMana = p_Mana;
            p_Class = a_Class;

            // Test items
            EquipableItem item = new EquipableItem(1000, "Enigma", EQuality.Epic, EEquipableType.Chest, 
            new int[] { 1200, 225, 175, 500, 250, 0, 0 });
            EquipableItem item2 = new EquipableItem(1000, "Breath Of The Dying", EQuality.Legendary, EEquipableType.MainHand,
            new int[] { 0, 500, 350, 250, 250, 75, 235 }, EWeaponType.MELEE);

            m_Inventory.AddItems(new Item[] { item, item2 });
        }

        public void CastSpell(Spell? a_Spell)
        {
            if (a_Spell == null) return;
            if (m_CurrentMana >= a_Spell.ManaCost)
                m_CurrentMana -= a_Spell.ManaCost;
        }

        public int GetSpellDamage(Spell? a_Spell)
        {
            if (a_Spell == null) return 0;
            Random r = new Random();
            int minDamage = a_Spell.MinDamage + (int)(p_Energy * 0.5f), maxDamage = a_Spell.MaxDamage + (int)(p_Energy * 0.5f);

            return r.Next(minDamage, maxDamage + 1);
        }

        public int GetDamage()
        {
            if (m_Equipment.GetEquipedWeapon() == null) return 1;
            Random r = new Random();
            EquipableItem? weapon = m_Equipment.GetEquipedWeapon();
            int minDamage = weapon.MinDamage, maxDamage = weapon.MaxDamage;
            
            if (m_Equipment.GetEquipedWeapon() != null)
            {
                // WORK IN PROGRESS, need to remake this part since we have more than 3 weapon type.
                switch(weapon.WeaponType)
                {
                    case EWeaponType.MAGICAL: 
                    case EWeaponType.MELEE:
                        minDamage += (int)(p_Strength * 0.5f);
                        maxDamage += (int)(p_Strength * 0.5f);
                        break;
                    case EWeaponType.RANGED:
                        minDamage += (int)(p_Dexterity * 0.5f);
                        maxDamage += (int)(p_Dexterity * 0.5f);
                        break;
                }
            }

            return r.Next(minDamage, maxDamage + 1);
        }

        public int[] Stats(EquipableItem a_Item)
        {
            return new int[] 
            { a_Item.Armor, a_Item.Strength, a_Item.Dexterity, a_Item.Vitality, a_Item.Energy, a_Item.MinDamage, a_Item.MaxDamage };
        }

        public void EquipItem(EquipableItem? a_Item)
        {
            if (a_Item == null)
                return;

            if (a_Item.WeaponType != EWeaponType.NONE)
            {
                switch (p_Class)
                {
                    case EClass.Warrior:
                        if (a_Item.WeaponType != EWeaponType.MELEE)
                            return;
                        break;
                    case EClass.Archer:
                        if (a_Item.WeaponType != EWeaponType.RANGED)
                            return;
                        break;
                    case EClass.Sorcerer:
                        if (a_Item.WeaponType != EWeaponType.MAGICAL)
                            return;
                        break;
                }
            }
            AudioManager.PlaySoundEffect(ESoundEffect.Item);
            m_Equipment.EquipItem(a_Item);
            m_Inventory.RemoveItem(a_Item);
            AddStats(Stats(a_Item));
            m_CurrentHealth = p_Health;
            m_CurrentMana = p_Mana;
        }

        public override void GainExp(int a_Amount)
        {
            base.GainExp(a_Amount);
            m_CurrentHealth = p_Health;
            m_CurrentMana = p_Mana;
        }

        public void UnequipItem(EquipableItem? a_Item)
        {
            if (a_Item == null)
                return;
            AudioManager.PlaySoundEffect(ESoundEffect.Item);
            m_Equipment.UnequipItem(a_Item);
            RemoveStats(Stats(a_Item));
            m_CurrentHealth = p_Health;
            m_CurrentMana = p_Mana;
        }

        #region Abstract Methods
        public override void TakeDamage(int a_Amount)
        {
            float damage = a_Amount - (p_Armor * 0.5f);
            m_CurrentHealth -= (int)damage;
            if (m_CurrentHealth <= 0)
            {
                m_IsDead = true;
                m_CurrentHealth = 0;
                // TO DO
            }
        }

        public override void Regen(int a_HealthAmount = 0, int a_ManaAmount = 0)
        {
            if (a_HealthAmount > 0)
            {
                m_CurrentHealth += a_HealthAmount;
                if (m_CurrentHealth > p_Health)
                    m_CurrentHealth = p_Health;
            }
            if (a_ManaAmount > 0)
            {
                m_CurrentMana += a_ManaAmount;
                if (m_CurrentMana > p_Mana)
                    m_CurrentMana = p_Mana;
            }
        }

        public override void DrawStats()
        {
            Console.WriteLine("¤═════════════════════════════¤");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Name: {p_Name}\n" +
                          $"Level: {p_Level}\n" +
                          $"Class: {p_Class}\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Health: {m_CurrentHealth}\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"Mana: {m_CurrentMana}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"Armor: {p_Armor}\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Strength: {p_Strength}\n" +
                          $"Dexterity: {p_Dexterity}\n" +
                          $"Vitality: {p_Vitality}\n" +
                          $"Energy: {p_Energy}\n\n");

            Console.Write($"Exp: {p_Exp} / {p_MaxExp}\n\n");
            Console.ResetColor();
            Console.WriteLine("¤═════════════════════════════¤");
        }
        #endregion
    }
}