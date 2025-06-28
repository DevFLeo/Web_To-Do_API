using System.ComponentModel.DataAnnotations;
using TodoApiPortfolio.Models;

// Essa parte é responsavel por criar um DTO (Data Transfer Object) para a criação de uma tarefa.

namespace TodoApiPortfolio.DTOs
{
    public class CriarTarefaDto 
    {
        [Required] 
        [MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;
        public Prioridade Prioridade { get; set; } = Prioridade.Media;
        public DateTime? DataDeVencimento { get; set; }
    }
}
