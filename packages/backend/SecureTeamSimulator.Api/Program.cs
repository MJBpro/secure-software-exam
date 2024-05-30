using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SecureTamSimulator.Api.Security;
using SecureTamSimulator.Api.Security.Policies.Roles;
using SecureTamSimulator.Api.Security.Policies.Scopes;
using SecureTeamSimulator.Application.Services;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Infrastructure.Database;
using SecureTeamSimulator.Core.Gdpr;
using SecureTeamSimulator.Core.Security.Outgoing;

var builder = WebApplication.CreateBuilder(args);

var domain = builder.Configuration["Auth0:Domain"];
var audience = builder.Configuration["Auth0:Audience"];
var managementToken = builder.Configuration["Auth0:ManagementApiToken"];
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = audience;
    
    if (builder.Environment.IsDevelopment())
    {
        options.RequireHttpsMetadata = false;
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = domain,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyScopes.ReadClaims, policy =>
        policy.Requirements.Add(new ScopeRequirement(PolicyScopes.ReadClaims)));
    options.AddPolicy(PolicyScopes.WriteSignup, policy =>
        policy.Requirements.Add(new ScopeRequirement(PolicyScopes.WriteSignup)));
    options.AddPolicy(PolicyScopes.ReadsUserTermsContext, policy =>
        policy.Requirements.Add(new ScopeRequirement(PolicyScopes.ReadsUserTermsContext)));

    options.AddPolicy(PolicyRoles.Admin, policy =>
        policy.Requirements.Add(new RoleRequirement(new[] { "Admin" })));
    options.AddPolicy(PolicyRoles.Member, policy =>
        policy.Requirements.Add(new RoleRequirement(new[] { "Member" })));
});


builder.Services.AddHttpClient();

builder.Services.AddSingleton<IAuthorizationHandler, RoleHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ScopeHandler>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.Configure<EncryptionSettings>(builder.Configuration.GetSection("EncryptionSettings"));
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IAuth0ManagementService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new Auth0ManagementService();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Auth0 API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Must be lowercase
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors((cors) =>
{
    cors.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
