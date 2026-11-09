using GestorDuctos.Components;
using Microsoft.EntityFrameworkCore;
using GestorDuctos.Data;
using GestorDuctos.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a SQLite
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Configurar el DbContext para usar PostgreSQL en lugar de SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Habilitar ASP.NET Core Identity con soporte para Roles (SE MANTIENE INTEGRO)
// ... tu código anterior
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 6;            // Solo exige 6 caracteres
        options.Password.RequireDigit = false;          // No exige números obligatorios
        options.Password.RequireNonAlphanumeric = false;// No exige símbolos especiales (*, #, @)
        options.Password.RequireUppercase = false;      // No exige mayúsculas
        options.Password.RequireLowercase = false;      // No exige minúsculas
        options.Password.RequiredUniqueChars = 0;       // Permite repetir el mismo carácter
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/login";
    options.LogoutPath = "/api/auth/logout";
});
// Registrar servicios de negocio del sistema de ductos
builder.Services.AddScoped<OficioService>();
builder.Services.AddScoped<PlanoService>();
builder.Services.AddSingleton<AuditoriaService>();
builder.Services.AddScoped<ProcedimientoService>();
builder.Services.AddScoped<IProcedimientosService, ProcedimientoService>();
builder.Services.AddScoped<ProyectoService>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAntiforgery();

var app = builder.Build();

// Crear automáticamente el rol de Administrador y asegurar el usuario y contraseña por defecto al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Esto aplica las migraciones y crea las tablas si no existen
        context.Database.Migrate(); 

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        if (!await roleManager.RoleExistsAsync("Administrador"))
        {
            await roleManager.CreateAsync(new IdentityRole("Administrador"));
        }

        string adminEmail = "admin@gestorductos.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var nuevoAdmin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var resultado = await userManager.CreateAsync(nuevoAdmin, "Admin123*");
            if (resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(nuevoAdmin, "Administrador");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar la base de datos.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapPost("/api/auth/login", async (HttpContext context, SignInManager<IdentityUser> signInManager) =>
{
    var form = await context.Request.ReadFormAsync();
    string email = form["email"]!;
    string password = form["password"]!;

    var result = await signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        context.Response.Redirect("/");
    }
    else
    {
        context.Response.Redirect("/login?error=InvalidCredentials");
    }
}).DisableAntiforgery();

app.MapPost("/api/auth/logout", async (HttpContext context, SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    context.Response.Redirect("/login");
}).DisableAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones.");
    }
}

app.Run();