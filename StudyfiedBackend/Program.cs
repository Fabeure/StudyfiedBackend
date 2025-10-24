using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using DotnetGeminiSDK;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using StudyfiedBackend.Controllers.Authentication;
using StudyfiedBackend.Controllers.FlashCards;
using StudyfiedBackend.Controllers.Resumes;
using StudyfiedBackend.Controllers.Quize;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.Models;
using System.Text;
using StudyfiedBackend.Controllers.Users;
using StudyfiedBackend.Controllers.ChatBot;
using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Client;
using DotnetGeminiSDK.Config;

var builder = WebApplication.CreateBuilder(args);

string[] origins = { "https://fabeure.github.io", "https://localhost:5173", "https://saber-azouzi.github.io", "http://localhost:4200" };
// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = "mongodb+srv://Fabeure:Fabeure@fabeure.tsdcgd5.mongodb.net/?retryWrites=true&w=majority&appName=Fabeure",
        DatabaseName = "Studyfied"
    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireLowercase = false;

        //lockout
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 5;

        options.User.RequireUniqueEmail = true;

    }

};

builder.Services.ConfigureMongoDbIdentityUserOnly<ApplicationUser, Guid>(mongoDbIdentityConfig)
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiJ9.ew0KICAic3ViIjogIjEyMzQ1Njc4OTAiLA0KICAibmFtZSI6ICJBbmlzaCBOYXRoIiwNCiAgImlhdCI6IDE1MTYyMzkwMjINCn0.EH_mTmtyJ-Heekz3O6FzVVeDxFt9-UT_5D4oom0tyc0")),
        ClockSkew = TimeSpan.Zero
    };
});

// Add depencies here when adding a new service

builder.Services.AddScoped<IFlashCardsService, FlashCardsService>();
builder.Services.AddScoped<IResumesService, ResumesService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IChatBotService, ChatBotService>();
builder.Services.AddSingleton<IMongoContext, MongoContext>();


builder.Services.AddSingleton<IGeminiClient>(provider =>
{
    var apiKey = builder.Configuration.GetValue<string>("GeminiApiKey");
    var config = new GoogleGeminiConfig
    {
        ApiKey = apiKey,
        DefaultTextModel = "gemini-2.5-flash-lite-preview-09-2025",

        // CRITICAL: Override ALL base URLs to match your model
        TextBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite-preview-09-2025",
        ImageBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite-preview-09-2025",
        ModelBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models",
        EmbeddingBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models",

        DefaultEmbeddingModel = "models/embedding-001"
    };
    return new GeminiClient(config);
});

builder.Services.AddCors(options =>
    options.AddPolicy("AllowSpecificOrigin",
    builder => builder
    .AllowAnyHeader()
    .WithOrigins(origins)
    .AllowAnyMethod()
    .AllowCredentials()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// lweh ? manedrouch 
// to specifically target this class when building the fake client in unit tests
public partial class Program { }
