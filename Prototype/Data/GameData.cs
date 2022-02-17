namespace Prototype
{
    public static class GameData
    {
        public static EquipableItem[] GetStartingItems()
        {
            List<EquipableItem> equipableItems = new List<EquipableItem>();
            string fileName = "StartingEquipableData.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Json\", fileName);
            string json = File.ReadAllText(path);
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<List<EquipableObject>>>(json);
            for (int i = 0; i < items?.Count; i++)
            {
                for (int j = 0; j < items[i].Count; j++)
                {
                    equipableItems.Add(new EquipableItem(items[i][j].Name, (EQuality)items[i][j].Quality, (EEquipableType)items[i][j].EquipableType, 
                        new int[] { items[i][j].Armor, items[i][j].Strength, items[i][j].Dexterity, items[i][j].Vitality, items[i][j].Energy, 
                            items[i][j].MinDamage, items[i][j].MaxDamage }, (EWeaponType)items[i][j].WeaponType));
                }
            }
            return equipableItems.ToArray();
        }

        public static Product[] GetAlchemistItems(string a_FileName)
        {
            List<Product> consumableItems = new List<Product>();
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Json\", a_FileName);
            string json = File.ReadAllText(path);
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConsumableObject>>(json);
            for (int i = 0; i < items?.Count; i++)
            {
                consumableItems.Add(new Product(items[i].Price, new ConsumableItem
                    (items[i].Name, (EQuality)items[i].Quality, items[i].HealthAmount, items[i].ManaAmount)));
            }
            return consumableItems.ToArray();
        }

        public static Product[] GetMarketProducts(string a_FileName)
        {
            List<Product> equipableItems = new List<Product>();
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Json\", a_FileName);
            string json = File.ReadAllText(path);
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EquipableObject>>(json);
            for (int i = 0; i < items?.Count; i++)
            {
                equipableItems.Add(new Product(items[i].Price, new EquipableItem(items[i].Name, (EQuality)items[i].Quality, (EEquipableType)items[i].EquipableType,
                    new int[] { items[i].Armor, items[i].Strength, items[i].Dexterity, items[i].Vitality, items[i].Energy,
                                items[i].MinDamage, items[i].MaxDamage }, (EWeaponType)items[i].WeaponType)));
            }
            return equipableItems.ToArray();
        }

        public static Spell[] GetWizardSpells(string a_FileName)
        {
            List<Spell> wizardSpells = new List<Spell>();
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Json\", a_FileName);
            string json = File.ReadAllText(path);
            var spells = Newtonsoft.Json.JsonConvert.DeserializeObject<SpellObject[]>(json);
            for (int i = 0; i < spells?.Length; i++)
            {
                wizardSpells.Add(new Spell(spells[i].Price, spells[i].Name, (ESpellType)spells[i].Type, new int[]
                    { spells[i].ArmorBuff, spells[i].StrengthBuff, spells[i].DexterityBuff, spells[i].VitalityBuff, spells[i].EnergyBuff,
                    spells[i].MinDamage, spells[i].MaxDamage, spells[i].HealAmount, spells[i].ManaAmount, spells[i].ManaCost}));
            }
            return wizardSpells.ToArray();
        }
    }
}