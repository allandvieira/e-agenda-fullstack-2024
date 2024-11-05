using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Orm.Compartilhado;

namespace eAgenda.WebApi.Controllers;

[Route("api/db")]
public class DatabaseController : Controller
{
    private readonly EAgendaDbContext _context;
    private readonly IWebHostEnvironment _env;

    public DatabaseController(EAgendaDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // Este endpoint só funciona em ambiente de desenvolvimento
    [HttpDelete("limpar")]
    public async Task<IActionResult> LimparBancoDeDados()
    {
        if (!_env.IsDevelopment())
            return Unauthorized("Esta operação só é possível em um ambiente de desenvolvimento");
        
        try
        {
            var compromissos = _context.Set<Compromisso>();            
            var contatos = _context.Set<Contato>();            
            var despesas = _context.Set<Despesa>();            
            var categorias = _context.Set<Categoria>();            
            var itensTarefa = _context.Set<ItemTarefa>();            
            var tarefas = _context.Set<Tarefa>();
            var usuarios = _context.Set<Usuario>();          
            
            compromissos.RemoveRange(compromissos);
            contatos.RemoveRange(contatos);
            despesas.RemoveRange(despesas);
            categorias.RemoveRange(categorias);
            itensTarefa.RemoveRange(itensTarefa);
            tarefas.RemoveRange(tarefas);
            usuarios.RemoveRange(usuarios);

            await _context.SaveChangesAsync();

            return Ok("O banco de dados foi limpo com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao tentar limpar o banco de dados: {ex.Message}");
        }
    }
}