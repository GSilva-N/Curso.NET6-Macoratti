using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyContext>(opt => 
    opt.UseInMemoryDatabase("TarefasDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

/*app.MapGet("frases", async () =>
    await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes"));*/

app.MapGet("/tarefas", async (MyContext context) =>
{
    var listatarefas = await context.Tarefas.ToListAsync();

    if (listatarefas != null && listatarefas.Count > 0)
        return Results.Ok(listatarefas);

    return Results.NoContent();
});
    

app.MapGet("/tarefas/{id}", async (int id, MyContext context) =>
    await context.Tarefas.FindAsync(id) is Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound());

app.MapGet("/tarefas_concluidas", async (MyContext context) => await context.Tarefas.Where(tarefa => tarefa.EstaConcluida).ToListAsync());


app.MapPost("/tarefas", async (Tarefa tarefa, MyContext context) =>
{
    context.Tarefas.Add(tarefa);
    await context.SaveChangesAsync();
    return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
});

app.MapPut("/tarefas/{id}", async (int id, Tarefa tarefa, MyContext context) =>
{
    var retorno = await context.Tarefas.FindAsync(id);
    
    if (retorno is null)
        return Results.NotFound();

    retorno.Nome = tarefa.Nome;
    retorno.EstaConcluida = tarefa.EstaConcluida;

    await context.SaveChangesAsync();
    
    return Results.Ok(retorno);
});

app.MapDelete("/tarefas/{id}", async (int id, MyContext context) =>
{
    var retorno = await context.Tarefas.FindAsync(id);

    if (retorno is null)
        return Results.NotFound();

    context.Tarefas.Remove(retorno);
    await context.SaveChangesAsync();

    return Results.Ok();
});

app.UseHttpsRedirection();
app.Run();

class Tarefa
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public bool EstaConcluida { get; set; }
}

class MyContext : DbContext
{
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    
    public MyContext(DbContextOptions<MyContext> options): base(options)
    {       
    }
}