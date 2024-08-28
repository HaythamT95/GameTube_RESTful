using GameTube_RESTful.Models;
using GameTube_RESTful.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTube_RESTful.Services
{
    public class GameService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _context.Game.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetGamesByCategoryAsync(int categoryId)
        {
            return await _context.GameCategories
                .Where(gc => gc.CategoryId == categoryId)
                .Select(gc => gc.Game)
                .ToListAsync();
        }

        public async Task AddGameAsync(Game game)
        {
            _context.Game.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game != null)
            {
                _context.Game.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
