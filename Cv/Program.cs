using Cv.Components.Account;
using Cv.Components;
using Cv.Data;
using Cv.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Net.Sockets;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // CORS Configuration
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazor", policy =>
            {
                policy.WithOrigins("https://localhost:7296")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // MongoDB Configuration
        builder.Services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(builder.Configuration.GetConnectionString("MONGODB_URI")));
        builder.Services.AddScoped<MongoDbContext>();

        // Client Services for Labb3-CV frontend
        builder.Services.AddHttpClient<Labb3CVClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7160/");
        });

        builder.Services.AddScoped<Labb3CVClient>();

        // Services for Skills and Projects
        builder.Services.AddScoped<ISkillService, SkillService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();

        // Identity and Authentication
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        // Database Context
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Identity Configuration
        builder.Services.AddIdentityCore<ApplicationUser>(options =>
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

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies();

        // Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("ADMIN"));
        });

        // Services
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<ISkillService, SkillService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline
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
        app.UseCors("AllowBlazor");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        // Add the endpoints configuration here
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
        });

        app.MapAdditionalIdentityEndpoints();
        app.Run();
    }
} 