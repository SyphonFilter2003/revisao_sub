public class Tarefa
{
    public int TarefaId { get; set; }
    public required string Descricao { get; set; }
    public bool Concluida { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }  // Tornar a propriedade Categoria anulável
}
