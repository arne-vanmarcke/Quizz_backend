using Microsoft.EntityFrameworkCore;

namespace BackendApp.Models
{
    public class HighscoreContext : DbContext
    {

        public HighscoreContext(DbContextOptions<HighscoreContext> options): base(options)
        {
        }

        public DbSet<Highscore> Highscores { get; set; } = null!;
    }
}
