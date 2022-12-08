using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static List<Game> games = new List<Game>
        {
            new Game { Id = 1, Result = Result.Draw }
        };

        private static List<Round> rounds = new List<Round>
        {
            new Round { Id = 1, GameId = 1, GameTime = DateTime.Now, PlayerMove = Move.Paper, ComputerMove = Move.Rock, Result = Result.Win },
            new Round { Id = 2, GameId = 1, GameTime = DateTime.Now, PlayerMove = Move.Paper, ComputerMove = Move.Scissors, Result = Result.Loose },
            new Round { Id = 3, GameId = 1, GameTime = DateTime.Now, PlayerMove = Move.Scissors, ComputerMove = Move.Scissors, Result = Result.Draw }
        };

        [HttpPost]
        [Route("NewGame")]
        public async Task<ActionResult<List<Game>>> CreateGame([FromBody] Move playerMove)
        {
            Game newGame = new Game { Id = games.Last().Id + 1 };

            return Ok(newGame);
        }

        [HttpPost]
        [Route("Play")]
        public async Task<ActionResult<List<Game>>> Play([FromBody] PlayRound body)
        {
            Game currentGame = games.Find(g => g.Id == body.GameId);

            Console.WriteLine(currentGame);

            Random random = new();
            Move computerMove = (Move)random.Next(3);

            Result gameResult = getResult(body.playerMove, computerMove);

            Round round = new Round { Id = rounds.Last().Id + 1, GameId = body.GameId, PlayerMove = body.playerMove, ComputerMove = computerMove, Result = gameResult };

            rounds.Add(round);

            return Ok(currentGame);
        }

        private Result getResult(Move playerMove, Move computerMove)
        {
            switch (playerMove, computerMove)
            {
                case (Move.Rock, Move.Paper):
                case (Move.Paper, Move.Scissors):
                case (Move.Scissors, Move.Rock):
                    return Result.Loose;
                case (Move.Rock, Move.Scissors):
                case (Move.Paper, Move.Rock):
                case (Move.Scissors, Move.Paper):
                    return Result.Win;
                default:
                    return Result.Draw;
            }
        }
    }
}
