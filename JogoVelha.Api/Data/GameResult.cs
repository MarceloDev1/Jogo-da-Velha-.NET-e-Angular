using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JogoVelha.Api.Data;

public class GameResult
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(1)")]
    public string Vencedor { get; set; } = string.Empty;

    public DateTime DataHora { get; set; } = DateTime.Now;
}
