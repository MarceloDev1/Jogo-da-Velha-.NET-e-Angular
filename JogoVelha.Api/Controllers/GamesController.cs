using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JogoVelha.Api.Models;
using JogoVelha.Api.Services;
using JogoVelha.Api.Data;

namespace JogoVelha.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _svc;
    private readonly AppDbContext _db;

    public GamesController(IGameService svc, AppDbContext db)
    {
        _svc = svc;
        _db = db;
    }

    [HttpPost]
    public ActionResult<GameState> Create() => _svc.Create();

    [HttpGet("{id:guid}")]
    public ActionResult<GameState> Get(Guid id)
        => _svc.Get(id) is { } g ? Ok(g) : NotFound();

    [HttpPost("{id:guid}/move")]
    public async Task<ActionResult<GameState>> Move(Guid id, [FromBody] MoveRequest req)
    {
        var g = _svc.Move(id, req.Index);
        if (g is null) return NotFound();

        var vencedor = g.Status switch
        {
            GameStatus.XWins => "X",
            GameStatus.OWins => "O",
            GameStatus.Draw => "E",
            _ => null
        };

        return Ok(g);
    }

    public record CreateGameResultRequest(string Vencedor, DateTime? DataHora);

    [HttpPost("results")]
    public async Task<ActionResult<GameResult>> CreateResult([FromBody] CreateGameResultRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Vencedor)) return BadRequest("Informe Vencedor: 'X', 'O' ou 'E'.");
        var v = req.Vencedor.Trim().ToUpperInvariant();
        if (v is not ("X" or "O" or "E")) return BadRequest("Vencedor deve ser 'X', 'O' ou 'E' (E = empate).");

        var entity = new GameResult
        {
            Vencedor = v,
            DataHora = req.DataHora ?? DateTime.Now
        };

        _db.GameResults.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetResultById), new { id = entity.Id }, entity);
    }

    [HttpGet("results")]
    public async Task<ActionResult<IEnumerable<GameResult>>> GetResults()
        => Ok(await _db.GameResults
                      .OrderByDescending(r => r.DataHora)
                      .Take(10)
                      .ToListAsync());

    [HttpGet("results/{id:int}")]
    public async Task<ActionResult<GameResult>> GetResultById(int id)
    {
        var r = await _db.GameResults.FindAsync(id);
        return r is null ? NotFound() : Ok(r);
    }
}

