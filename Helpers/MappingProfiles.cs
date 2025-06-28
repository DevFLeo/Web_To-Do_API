using AutoMapper;
using TodoApiPortfolio.DTOs;
using TodoApiPortfolio.Models;

namespace TodoApiPortfolio.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Mapeia da nossa entidade Tarefa para o nosso TarefaDto
            CreateMap<Tarefa, TarefaDto>();

            // Mapeia do nosso DTO de criação para a nossa entidade Tarefa
            CreateMap<CriarTarefaDto, Tarefa>();
        }
    }
}