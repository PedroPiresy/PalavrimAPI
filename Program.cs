using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PalavrimAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar o serviço como Singleton para reaproveitar a mesma instância
builder.Services.AddSingleton(new PalavrimService(5));

// Defina as origens permitidas para cada ambiente
string[] allowedOriginsLocal = new[] { "http://localhost:5173", "http://localhost:5134" };
string[] allowedOriginsProd = new[] { "https://palavrim.vercel.app" };

//string[] allowedOrigins = allowedOriginsLocal;
string[] allowedOrigins = allowedOriginsProd;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseStaticFiles();

// Aplica o CORS antes dos endpoints
app.UseCors("AllowFrontend");


app.MapGet("/", () => Results.Redirect("/index.html"));

app.MapGet("/tamanho", (PalavrimService service) =>
{
    return Results.Ok(service.TamanhoPalavra);
});

app.MapGet("/palavra-do-dia", (PalavrimService service) =>
{
    return Results.Ok(service.PalavraDoDia());
});

app.MapGet("/palavra-aleatoria", (PalavrimService service) =>
{
    return Results.Ok(service.PalavraAleatoria());
});

app.MapGet("/valida/{palavra}", (string palavra, PalavrimService service) =>
{
    return Results.Ok(service.PalavraValida(palavra));
});

app.MapGet("/prefixo/{prefixo}", (string prefixo, PalavrimService service) =>
{
    return Results.Ok(service.BuscarPorPrefixo(prefixo));
});

app.MapGet("/sufixo/{sufixo}", (string sufixo, PalavrimService service) =>
{
    return Results.Ok(service.BuscarPorSufixo(sufixo));
});

app.MapGet("/todas", (PalavrimService service) =>
{
    return Results.Ok(service.TodasPalavras());
});

app.Run();
