using Microsoft.EntityFrameworkCore;
using TodoApiPortfolio.Interfaces;
using TodoApiPortfolio.Models;

//  Repositorio funcionando com o Entity Framework Core
//  Este repositório é responsável por todas as operações
//  e interações com a base de dados para as tarefas.

namespace TodoApiPortfolio.Repositories
{
    //  Esta classe implementa a interface ITarefaRepository.
    public class TarefaRepository : ITarefaRepository
    {
        //  Declaramos como "private readonly" para garantir que só é usado dentro desta classe
        private readonly TodoContext _context;
        public TarefaRepository(TodoContext context) => _context = context;
        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            //  Usa o Entity Framework para ir à tabela de Tarefas e devolve todas, de forma assíncrona.
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa?> GetByIdAsync(long id)
        {
            //  Procura na tabela de Tarefas por uma tarefa com o 'id' fornecido.
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task AddAsync(Tarefa tarefa)
        {
            //  A tarefa ainda não foi guardada na base de dados, está apenas "em memória".
            await _context.Tarefas.AddAsync(tarefa);
        }

        public void Update(Tarefa tarefa)
        {
            //  O EF é inteligente. Não precisamos de um comando de update.
            //  Quando o SaveChangesAsync for chamado, o EF irá gerar o comando SQL de UPDATE correto.
            _context.Entry(tarefa).State = EntityState.Modified;
        }

        public void Delete(Tarefa tarefa)
        {
            //  Tal como o Update, apenas informamos ao contexto que esta entidade deve ser removida.
            _context.Tarefas.Remove(tarefa);
        }

        public async Task<bool> SaveChangesAsync()
        {
            //  Devolve 'true' se pelo menos uma linha foi alterada na base de dados.
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

//  Este repositório é usado pelos controladores para realizar operações CRUD (Criar, Ler, Atualizar, Excluir) nas tarefas.