public class Categoria
{
    public int CategoriaId { get; set; }
    public required string Nome { get; set; }
    public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();  // Relacionamento 1:N
}
