using FluentValidation;
using TodoApiPortfolio.DTOs;

//  Esse ficheiro é responsável por validar o DTO de criação de tarefas.
//  Ele utiliza a biblioteca FluentValidation para definir regras de validação que serão aplicadas quando o usuário tentar criar uma nova tarefa.

namespace TodoApiPortfolio.Validators
{
    public class CriarTarefaDtoValidator : AbstractValidator<CriarTarefaDto>
    {
        public CriarTarefaDtoValidator()
        {
            //  -> Regra 1: O Título não pode ser vazio e deve ter no máximo 100 caracteres.
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

            //  -> Regra 2: A Prioridade deve estar dentro dos valores válidos da nossa enumeração.
            RuleFor(x => x.Prioridade)
                .IsInEnum().WithMessage("O valor da prioridade é inválido.");

            //  -> Regra 3: Se uma Data de Vencimento for fornecida, ela não pode ser no passado.
            RuleFor(x => x.DataDeVencimento)
                .Must(BeAValidDate).When(x => x.DataDeVencimento.HasValue)
                .WithMessage("A data de vencimento não pode ser no passado.");
        }

        //  -> Método auxiliar para a nossa regra customizada de data
        private bool BeAValidDate(DateTime? date)
        {
            //  -> Consideramos a data válida se for hoje ou no futuro.
            return date.Value.Date >= DateTime.Now.Date;
        }
    }
}