using Microsoft.AspNetCore.Mvc;
using BackendApp.Models;
using BackendApp.Services;

namespace BackendApp.Controllers;

    [ApiController]
[Route("api/[controller]")]
public class HighscoreController : ControllerBase
{
    private readonly HighscoreServices _HighscoreService;

    public HighscoreController(HighscoreServices highscoreService) =>
        _HighscoreService = highscoreService;

    [HttpGet]
    public async Task<List<Highscore>> Get() => await _HighscoreService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Highscore>> Get(string id)
    {
        var highscore = await _HighscoreService.GetAsync(id);

        if (highscore is null)
        {
            return NotFound();
        }

        return highscore;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Highscore newHighscore)
    {
        await _HighscoreService.CreateAsync(newHighscore);

        return CreatedAtAction(nameof(Get), new { id = newHighscore.Id }, newHighscore);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Highscore updatedHighscore)
    {
        var highscore = await _HighscoreService.GetAsync(id);

        if (highscore is null)
        {
            return NotFound();
        }

        updatedHighscore.Id = highscore.Id;

        await _HighscoreService.UpdateAsync(id, updatedHighscore);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var highscore = await _HighscoreService.GetAsync(id);

        if (highscore is null)
        {
            return NotFound();
        }

        await _HighscoreService.RemoveAsync(highscore.Id);

        return NoContent();
    }
}

