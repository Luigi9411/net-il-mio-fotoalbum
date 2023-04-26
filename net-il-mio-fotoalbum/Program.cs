using net_il_mio_fotoalbum.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer<AlbumContext>("Data Source=localhost;Initial Catalog=Photo.Db;Integrated Security=True;TrustServerCertificate=True");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Photo}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
using (var ctx = scope.ServiceProvider.GetService<AlbumContext>())
{
    ctx!.Seed();
}

app.Run();
