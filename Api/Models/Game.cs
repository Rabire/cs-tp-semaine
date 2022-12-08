namespace Api
{
    public class Game
    {
        public int Id { get; set; }

        public DateTime GameTime { get; set; } = DateTime.Now;

        public Move PlayerMove { get; set; }

        public Move ComputerMove { get; set; }

        public Result Result { get; set; }
    }
}
