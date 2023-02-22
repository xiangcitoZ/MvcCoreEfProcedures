using Microsoft.EntityFrameworkCore;
using MvcCoreEfProcedures.Data;
using MvcCoreEfProcedures.Repository;

var builder = WebApplication.CreateBuilder(args);


string connectionString =
    builder.Configuration.GetConnectionString("SqlHospital");

builder.Services.AddTransient<RepositoryTrabajadores>();
builder.Services.AddTransient<RepositoryDoctores>();
builder.Services.AddTransient<RepositoryVistaEmpleados>();
builder.Services.AddTransient<RepositoryEnfermos>();
builder.Services.AddDbContext<HospitalContext>
     (options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Enfermos}/{action=Index}/{id?}");

app.Run();
