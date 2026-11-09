using GestorDuctos.Components;
using Microsoft.EntityFrameworkCore;
using GestorDuctos.Data;
using GestorDuctos.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=gestorductos.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Habilitar ASP.NET Core Identity con soporte para Roles (Sin DefaultUI)
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Registrar servicios de negocio del sistema de ductos
builder.Services.AddScoped<OficioService>();
builder.Services.AddScoped<PlanoService>();
builder.Services.AddScoped<ProcedimientoService>();
builder.Services.AddScoped<IProcedimientosService, ProcedimientoService>(); // <-- Corrección aquí
builder.Services.AddScoped<ProyectoService>();
builder.Services.AddSingleton<AuditoriaService>();
builder.Services.AddScoped<ProcedimientoService>();
builder.Services.AddScoped<IProcedimientosService, ProcedimientoService>();
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
        else
        {
            string nuevaPassword = "Admin123*";
            var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
            await userManager.ResetPasswordAsync(adminUser, token, nuevaPassword);

            if (!await userManager.IsInRoleAsync(adminUser, "Administrador"))
            {
                await userManager.AddToRoleAsync(adminUser, "Administrador");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar los roles y usuarios predeterminados.");
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

app.Run();