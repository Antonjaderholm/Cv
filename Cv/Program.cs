using Cv.Components.Account;
using Cv.Components;
using Cv.Data;
using Cv.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using CV.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        ConfigureMiddleware(app);
    }
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Core Services
        services.AddRazorComponents()
         .AddInteractiveServerComponents()
         .AddCircuitOptions(options =>
         {
             options.DetailedErrors = true;
         });
        // MongoDB Configuration
        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(configuration.GetConnectionString("MONGODB_URI")));
        services.AddScoped<MongoDbContext>();

        // CV Services
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IProjectService, ProjectService>();

        // HTTP Client Configuration
        services.AddHttpClient<Labb3CVClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7160/");
        });
        services.AddScoped<Labb3CVClient>();

        // API Controllers Configuration
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

        // CORS Configuration
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Identity and Authentication
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        // Database Context
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Identity Configuration
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        // Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies();

        // Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("ADMIN"));
        });

        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        services.AddEndpointsApiExplorer();
    }
   
    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapControllers();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();  // Remove CircuitOptions here
        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }
}
