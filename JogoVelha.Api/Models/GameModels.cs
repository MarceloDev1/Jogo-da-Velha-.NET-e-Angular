namespace JogoVelha.Api.Models;
public enum Player { X, O }
public enum GameStatus { InProgress, Draw, XWins, OWins }

public class GameState
{
    public string PlayerXName { get; set; } = string.Empty;
    public string PlayerOName { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public char[] Board { get; set; } = Enumerable.Repeat(' ', 9).ToArray();
    public Player CurrentPlayer { get; set; } = Player.X;
    public GameStatus Status { get; set; } = GameStatus.InProgress;
}

public class MoveRequest
{
    public int Index { get; set; }
}