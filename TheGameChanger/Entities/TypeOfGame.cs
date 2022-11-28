namespace TheGameChanger.Entities
{
    public class TypeOfGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
