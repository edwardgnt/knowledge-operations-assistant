using KnowledgeAssistant.Core.AI;
using KnowledgeAssistant.Infrastructure.AI;
using OpenAI.Chat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var openAiApiKey = builder.Configuration["OpenAI:ApiKey"]
    ?? throw new InvalidOperationException(
        "OpenAI API key is not configured.");

var openAiModel = builder.Configuration["OpenAI:Model"]
    ?? throw new InvalidOperationException(
        "OpenAI model is not configured.");

builder.Services.AddSingleton(new ChatClient(openAiModel, openAiApiKey));

builder.Services.AddScoped<IChatService, OpenAIChatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
