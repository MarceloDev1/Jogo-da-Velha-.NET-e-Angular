using System.Collections.Concurrent;
using JogoVelha.Api.Models;

namespace JogoVelha.Api.Services;
public interface IGameService
{
    GameState Create();
    GameState? Get(Guid id);
    GameState? Move(Guid id, int index);
}

public class GameService : IGameService
{
    private static readonly int[][] Wins = new[]
    {
        new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
        new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
        new[]{0,4,8}, new[]{2,4,6}
    };

    private readonly ConcurrentDictionary<Guid, GameState> _games = new();

    public GameState Create()
    {
        var g = new GameState();
        _games[g.Id] = g;
        return g;
    }

    public GameState? Get(Guid id) => _games.TryGetValue(id, out var g) ? g : null;

    public GameState? Move(Guid id, int index)
    {
        if (!_games.TryGetValue(id, out var g)) return null;
        if (g.Status != GameStatus.InProgress) return g;
        if (index < 0 || index > 8) return g;
        if (g.Board[index] != ' ') return g;

        var mark = g.CurrentPlayer == Player.X ? 'X' : 'O';
        g.Board[index] = mark;

        if (Wins.Any(w => w.All(i => g.Board[i] == mark)))
        {
            g.Status = g.CurrentPlayer == Player.X ? GameStatus.XWins : GameStatus.OWins;
            return g;
        }

        if (g.Board.All(c => c != ' '))
        {
            g.Status = GameStatus.Draw;
            return g;
        }

        g.CurrentPlayer = g.CurrentPlayer == Player.X ? Player.O : Player.X;
        return g;
    }
}