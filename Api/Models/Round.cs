using System.ComponentModel.DataAnnotations.Schema;

namespace Api
{
    public class Round
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public DateTime GameTime { get; set; } = DateTime.Now;

        public Move PlayerMove { get; set; }

        public Move ComputerMove { get; set; }

        public Result Result { get; set; }
    }
}
