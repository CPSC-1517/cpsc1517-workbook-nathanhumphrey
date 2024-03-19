using WestWindWebApp.Components;

// Required namespaces
using WestWindSystem;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure our backend system
var connectionString = builder.Configuration.GetConnectionString("WWDB");
builder.Services.WWBackendDependencies(options =>
	options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
