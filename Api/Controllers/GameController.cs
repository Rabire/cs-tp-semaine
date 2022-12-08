using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static List<Game> games = new List<Game>
        {
            new Game { Id = 1, GameTime = DateTime.Now, PlayerMove = Move.Paper, ComputerMove = Move.Rock, Result = Result.Win },
            new Game { Id = 2, GameTime = DateTime.Now, PlayerMove = Move.Paper, ComputerMove = Move.Scissors, Result = Result.Loose }
        };

        [HttpPost]
        [Route("Play")]
        public async Task<ActionResult<List<Game>>> PlayGame([FromBody] Move playerMove)
        {
            Random random = new();
            Move computerMove = (Move)random.Next(3);

            Result gameResult = getResult(playerMove, computerMove);

            Game game = new Game { PlayerMove = playerMove, ComputerMove = computerMove, Result = gameResult };

            games.Add(game);

            return Ok(games);
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
