// --- 1. Usings Necessários para a API funcionar direito ---
using Microsoft.EntityFrameworkCore;
using TodoApiPortfolio.Interfaces;
using TodoApiPortfolio.Models;
using TodoApiPortfolio.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

//  Como o Serilog é uma biblioteca de logging avançada,
//  ela permite que você registre logs de forma estruturada
//  e com diferentes níveis de severidade (Informação, Aviso, Erro, etc.).

namespace TodoApiPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // --- Configuração inicial do Serilog ---
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) 
                .CreateLogger();

            try
            {
                Log.Information("A aplicação está a iniciar...");

                var builder = WebApplication.CreateBuilder(args);

                //  Dizemos ao builder para usar o Serilog em vez do logger padrão da Microsoft
                builder.Host.UseSerilog();

                // --- 2. Área de Configuração de Serviços  ---

                builder.Services.AddDbContext<TodoContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("TodoDB")));

                //  Regista o repositório de tarefas como um serviço que pode ser injetado em controladores e outros serviços.

                builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
                builder.Services.AddAutoMapper(typeof(Program));

                //  Configuração do FluentValidation ---

                builder.Services.AddFluentValidationAutoValidation();
                builder.Services.AddValidatorsFromAssemblyContaining<Program>();

                //  Registo de Controladores e Swagger ---

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    //  Obtém o assembly (o ficheiro .dll compilado) que está a ser executado.
                    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    var xmlFilename = $"{assembly.GetName().Name}.xml";
                    var xmlPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location), xmlFilename);

                    options.IncludeXmlComments(xmlPath);
                });

                // --- 3. Construção da Aplicação ---
                var app = builder.Build();
 
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseStaticFiles();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "A aplicação falhou ao iniciar.");
            }
            finally
            {
                //  Garante que todos os logs são escritos antes de a aplicação fechar.
                Log.CloseAndFlush();
            }
        }
    }
}