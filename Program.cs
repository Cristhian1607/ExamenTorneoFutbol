var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Inicializa el cliente supabase
var supaUrl = builder.Configuration["Supabase:Url"];
var supaKey = builder.Configuration["Supabase:Key"];
TorneosFutbolMVC.Services.SupabClient.Initialize(supaUrl, supaKey);

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Torneos}/{action=Index}/{id?}");
app.Run();