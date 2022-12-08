using System.ComponentModel.DataAnnotations.Schema;

namespace Api
{
    public class Game
    {
        public int Id { get; set; }
        public Result Result { get; set; }
        public ICollection<Round> Rounds { get; set; }

    }
}
