// --- 1. Usings Necess�rios para a API funcionar direito ---
using Microsoft.EntityFrameworkCore;
using TodoApiPortfolio.Interfaces;
using TodoApiPortfolio.Models;
using TodoApiPortfolio.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

//  Como o Serilog � uma biblioteca de logging avan�ada,
//  ela permite que voc� registre logs de forma estruturada
//  e com diferentes n�veis de severidade (Informa��o, Aviso, Erro, etc.).

namespace TodoApiPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // --- Configura��o inicial do Serilog ---
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) 
                .CreateLogger();

            try
            {
                Log.Information("A aplica��o est� a iniciar...");

                var builder = WebApplication.CreateBuilder(args);

                //  Dizemos ao builder para usar o Serilog em vez do logger padr�o da Microsoft
                builder.Host.UseSerilog();

                // --- 2. �rea de Configura��o de Servi�os  ---

                builder.Services.AddDbContext<TodoContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("TodoDB")));

                //  Regista o reposit�rio de tarefas como um servi�o que pode ser injetado em controladores e outros servi�os.

                builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
                builder.Services.AddAutoMapper(typeof(Program));

                //  Configura��o do FluentValidation ---

                builder.Services.AddFluentValidationAutoValidation();
                builder.Services.AddValidatorsFromAssemblyContaining<Program>();

                //  Registo de Controladores e Swagger ---

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    //  Obt�m o assembly (o ficheiro .dll compilado) que est� a ser executado.
                    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    var xmlFilename = $"{assembly.GetName().Name}.xml";
                    var xmlPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location), xmlFilename);

                    options.IncludeXmlComments(xmlPath);
                });

                // --- 3. Constru��o da Aplica��o ---
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
                Log.Fatal(ex, "A aplica��o falhou ao iniciar.");
            }
            finally
            {
                //  Garante que todos os logs s�o escritos antes de a aplica��o fechar.
                Log.CloseAndFlush();
            }
        }
    }
}