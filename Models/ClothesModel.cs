namespace Practice_01._02_KazanovAlexandr.Models
{
    [Serializable]
    public class ClothesModel
    {
        public long Id { get; set; }
        public byte Size { get; set; }
        public ushort Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ClothesModel(ushort price, string? name, string? description)
        {
            Price = price;
            Name = name;
            Description = description;
            DateTime centuryBegin = new DateTime(2022, 1, 1);
            Id = DateTime.Now.Ticks - centuryBegin.Ticks;
        }

    }
}
