using TodoApiPortfolio.Models;

// Define um repositório de tarefas que será usado para acessar e manipular dados de tarefas no banco de dados.

namespace TodoApiPortfolio.Interfaces
{
    // Ele define todos os métodos que uma classe de repositório de tarefas deve ter.
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetAllAsync();
        Task<Tarefa?> GetByIdAsync(long id);
        Task AddAsync(Tarefa tarefa);
        void Update(Tarefa tarefa);
        void Delete(Tarefa tarefa);
        Task<bool> SaveChangesAsync();
    }
}