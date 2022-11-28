namespace TheGameChanger.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? PositiveCounter { get; set; } = 0;
        public int? NegativeCounter { get; set; } = 0;

        public int TypeOfGameId { get; set; }
        public virtual TypeOfGame TypeOfGame { get; set; }
    }
}
