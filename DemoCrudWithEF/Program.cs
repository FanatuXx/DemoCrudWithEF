using DemoCrudWithEF.Domain;
using DemoCrudWithEF.Domain.Repositories;
using DemoCrudWithEF.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddDbContext<MusicDbContext>(o =>
{
    o.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MusicDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
});

builder.Services.AddScoped<IGroupeRepository, GroupeService>();
builder.Services.AddScoped<IAlbumRepository, AlbumService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapBlazorHub();

app.Run();
