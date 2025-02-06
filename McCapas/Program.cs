using McCapas.Data;
using McCapas.Services;
using McCapas.ServicesLogin;
using McCapas.ServicesLogin.SenhaService;
using McCapas.ServicesLogin.SessaoService;
using McCapas.ServicesMaterial;
using McCapas.ServicesTapete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IloginInterface, LoginService>();
builder.Services.AddScoped<ISenhaInterface, SenhasService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();    
builder.Services.AddScoped<ItapeteServices, TapeteServices>();
builder.Services.AddScoped<ICapaService, CapaServices>();
builder.Services.AddScoped<ISessaoInterface, SessaoService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
