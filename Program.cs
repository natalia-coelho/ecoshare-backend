using ecoshare_backend;
using ecoshare_backend.Data;
using ecoshare_backend.Models;
using ecoshare_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure middleware
ConfigureMiddleware(app);
app.MapControllers();
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Configure database contexts
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

    // Add app services
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddScoped<UsuarioService>();
    services.AddScoped<TokenService>();

    // Configure Identity services
    services
        .AddIdentity<Usuario, IdentityRole>()
        .AddEntityFrameworkStores<UserDbContext>() // Database communication
        .AddDefaultTokenProviders();               // Authentication configuration

    services.Configure<IdentityOptions>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });


    // Configure JWT authentication

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uSx3FNPdJMC_0vE9vrlQDHMcO45J_gwSr4e4eow4I8o")),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero,
        };
    });

    // Configure Authorization policies
    services.AddAuthorization(options =>
    {
        options.AddPolicy("SupplierPolicy", policy => policy.RequireRole(RoleManager.GetRoleName(UserRole.SUPPLIER)));
        options.AddPolicy("ClientPolicy", policy => policy.RequireRole(RoleManager.GetRoleName(UserRole.CLIENT)));
    });

    // Add CORS policy
    // TODO: Get this URL from Configuration
    //     var frontendUrl = "http://localhost:4200";
    //var frontendUrl = builder.Configuration.GetSection("Integrations:FrontendUrl").Value;
    var frontendUrl = "http://localhost:4200"; // or use configuration

    services.AddCors(options => options.AddPolicy("FrontEnd", policy =>
    {
        policy.WithOrigins(frontendUrl).AllowAnyMethod().AllowAnyHeader();
    }));

    // Add controllers and JSON configuration
    services.AddControllers().AddNewtonsoftJson();

    // Configure Swagger/OpenAPI
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void ConfigureMiddleware(WebApplication app)
{
    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("FrontEnd");
    }

    app.UseHttpsRedirection();

    // Enable authentication and authorization middleware
    app.UseAuthentication();
    app.UseAuthorization();

    // Enable CORS for frontend
    app.UseCors("FrontEnd");

    // Map controllers
    app.MapControllers();
}
