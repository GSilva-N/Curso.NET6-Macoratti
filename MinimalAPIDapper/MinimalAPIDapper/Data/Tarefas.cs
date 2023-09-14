using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPIDapper.Data;

[Table("Tarefas")]
public record Tarefa(int Id, string Atividade, string Status);
/*public class Tarefa
{
    [Key]
    public int Id { get; set; }
    public string Atividade { get; set;}
    public string Status { get; set;}
}*/

