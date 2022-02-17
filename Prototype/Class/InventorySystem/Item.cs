namespace Prototype
{
    public abstract class Item
    {
        //protected string p_Id = "";
        protected int p_Price = 0;
        protected string p_Name = "";
        protected EQuality p_Quality = EQuality.Poor;

        //public string Id { get => p_Id; }
        public int Price { get => p_Price; }
        public string Name { get => p_Name; }
        public EQuality Quality { get => p_Quality; }

        public Item(int a_Price, string a_ItemName, EQuality a_Quality)
        {
            //p_Id = Guid.NewGuid().ToString("N");
            p_Price = a_Price;
            p_Name = a_ItemName;
            p_Quality = a_Quality;
        }

        public abstract void DrawItem();
    }
}