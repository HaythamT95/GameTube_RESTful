using GameTube_RESTful.Models;
using GameTube_RESTful.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTube_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(GameService gameService) : ControllerBase
    {
        private readonly GameService _gameService = gameService;

        [HttpGet]
        //[Authorize] //Adds Bearer token authorization
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        // GET: api/game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        // GET: api/game/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategory(int categoryId)
        {
            var games = await _gameService.GetGamesByCategoryAsync(categoryId);
            if (games == null || !games.Any())
            {
                return NotFound();
            }
            return Ok(games);
        }

        // POST: api/game
        [HttpPost]
        public async Task<ActionResult<Game>> AddGame(Game game)
        {
            await _gameService.AddGameAsync(game);
            return CreatedAtAction(nameof(GetGameById), new { id = game.GameId }, game);
        }

        // PUT: api/game/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, Game game)
        {
            if (id != game.GameId)
            {
                return BadRequest();
            }

            if (!_gameService.GameExists(id))
            {
                return NotFound();
            }

            await _gameService.UpdateGameAsync(game);
            return NoContent();
        }

        // DELETE: api/game/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            if (!_gameService.GameExists(id))
            {
                return NotFound();
            }

            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }
    }
}
