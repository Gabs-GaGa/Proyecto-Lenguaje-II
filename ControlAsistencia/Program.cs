using ControlAsistencia.Components;
using ControlAsistencia.Data;
using ControlAsistencia.Middleware;
using Microsoft.EntityFrameworkCore;

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
    .AddNpgsql(
        connectionString!,
        name: "postgresql",
        tags: ["db", "ready"]);

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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
