using ControlInventario.Components;
using ControlInventario.Data;
using ControlInventario.Middleware;
using ControlInventario.Services;
using Microsoft.EntityFrameworkCore;
using ControlInventario.Data.Seeders;

var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
        npgsql.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null))
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>(
        name: "postgresql",
        tags: ["db", "ready"]);

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();   

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            })
        };

        await context.Response.WriteAsync(
            System.Text.Json.JsonSerializer.Serialize(result));
    }
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var context = await db.CreateDbContextAsync();

    await context.Database.MigrateAsync();

    await CategoriaSeeder.SeedAsync(context);
    await ProveedorSeeder.SeedAsync(context);
}

app.Run();