using Dapper.Contrib.Extensions;
using MinimalAPIDapper.Data;
using static MinimalAPIDapper.Data.TarefaContext;

namespace MinimalAPIDapper.Endpoints;

public static class TarefasEndpoints
{
    public static void MapTarefasEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => $"Aplicação Inicializada!");
        
        app.MapGet("/tarefas", async(GetConnection connectionGetter) =>
        {
            using var connection = await connectionGetter();
            var tarefas = connection.GetAll<Tarefa>();

            if (tarefas is null)
                return Results.NotFound();

            return Results.Ok(tarefas);
        });
        
        app.MapGet("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
        {
            using var connection = await connectionGetter();
            var tarefa = connection.Get<Tarefa>(id);

            if (tarefa is null)
                return Results.NotFound("Tarefa não encontrada!");

            return Results.Ok(tarefa);

        });

        app.MapPost("/tarefas", async (GetConnection connectionGetter, Tarefa tarefa) =>
        {
            using var connection = await connectionGetter();
            var tarefaId = connection.Insert(tarefa);

            return Results.Created($"/tarefas/{tarefaId}", tarefa);
        });
    }
}

