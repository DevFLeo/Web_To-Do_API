namespace TodoApiPortfolio.Models
{
    public class Tarefa
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public bool Feito { get; set; }
        public Prioridade Prioridade { get; set; }
        public DateTime? DataDeValidade { get; set; }
    }
}