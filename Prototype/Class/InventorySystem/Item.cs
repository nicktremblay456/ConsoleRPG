namespace Prototype
{
    public abstract class Item
    {
        protected string p_Id = "";
        protected string p_Name = "";
        protected EQuality p_Quality = EQuality.Poor;

        public string Id { get => p_Id; }
        public string Name { get => p_Name; }
        public EQuality Quality { get => p_Quality; }

        public Item(string a_ItemName, EQuality a_Quality)
        {
            p_Id = Guid.NewGuid().ToString("N");
            p_Name = a_ItemName;
            p_Quality = a_Quality;
        }

        public abstract void DrawItem();
    }
}