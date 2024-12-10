using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

    // POST: api/tarefa/cadastrar
        [HttpPost("cadastrar")]
    public async Task<ActionResult<Tarefa>> CadastrarTarefa([FromBody] Tarefa tarefa)
    {
        if (tarefa == null)
        {
            return BadRequest("A tarefa é obrigatória.");
        }

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ListarTarefas), new { id = tarefa.TarefaId }, tarefa);
    }

    // GET: api/tarefa/listar
    [HttpGet("listar")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ListarTarefas()
    {
        return await _context.Tarefas.Include(t => t.Categoria).ToListAsync();
    }

    // PUT: api/tarefa/alterar
    [HttpPut("alterar/{id}")]
    public async Task<IActionResult> AlterarTarefa(int id, Tarefa tarefa)
    {
    if (id != tarefa.TarefaId)
    {
        return BadRequest("O ID da tarefa não corresponde.");
    }

    // Verifica se a tarefa existe
    var tarefaExistente = await _context.Tarefas.FindAsync(id);
    if (tarefaExistente == null)
    {
        return NotFound("Tarefa não encontrada.");
    }

    // Atualiza a tarefa com os novos dados
    tarefaExistente.Descricao = tarefa.Descricao;
    tarefaExistente.Concluida = tarefa.Concluida;
    tarefaExistente.CategoriaId = tarefa.CategoriaId;

    // Salva as alterações no banco de dados
    await _context.SaveChangesAsync();

    // Retorna a tarefa atualizada
    return Ok(tarefaExistente);  // Retorna a tarefa atualizada
}

    // GET: api/tarefa/naoconcluidas
    [HttpGet("naoconcluidas")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> TarefasNaoConcluidas()
    {
        return await _context.Tarefas.Where(t => !t.Concluida).ToListAsync();
    }

    // GET: api/tarefa/concluidas
    [HttpGet("concluidas")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> TarefasConcluidas()
    {
        return await _context.Tarefas.Where(t => t.Concluida).ToListAsync();
    }
}
