using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("NewGame")]
        public async Task<ActionResult<List<Game>>> CreateGame([FromBody] Move playerMove)
        {

            List<Game> dbGames = await _context.Games.ToListAsync();

            Game newGame = new Game { Id = dbGames.Last().Id + 1 };

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", newGame);
        }

        [HttpPost]
        [Route("Play")]
        public async Task<ActionResult<List<Game>>> Play([FromBody] PlayRound body)
        {

            Game currentGame = _context.Games.Find((Game g) => g.Id == body.GameId);

            Console.WriteLine(currentGame);

            Random random = new();
            Move computerMove = (Move)random.Next(3);

            Result gameResult = getResult(body.playerMove, computerMove);

            List<Round> dbRounds = await _context.Rounds.ToListAsync();

            Round round = new Round { Id = dbRounds.Last().Id + 1, GameId = body.GameId, PlayerMove = body.playerMove, ComputerMove = computerMove, Result = gameResult };

            _context.Rounds.Add(round);
            await _context.SaveChangesAsync();

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
