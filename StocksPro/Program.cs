using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
