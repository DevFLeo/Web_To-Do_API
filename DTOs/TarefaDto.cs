using TodoApiPortfolio.Models;

// Essa parte do DTO é responsavel por representar uma tarefa já existente, ou seja, quando a tarefa já foi criada e está sendo retornada para o usuário.

namespace TodoApiPortfolio.DTOs
{
    public class TarefaDto
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool EstaConcluida { get; set; }
        public Prioridade Prioridade { get; set; }
        public DateTime? DataDeVencimento { get; set; }
    }
}