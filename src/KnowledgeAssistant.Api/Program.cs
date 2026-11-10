using KnowledgeAssistant.Core.AI;
using KnowledgeAssistant.Infrastructure.AI;
using OpenAI.Chat;
using System.ClientModel;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var aiProvider = builder.Configuration["AI:Provider"] ?? "OpenAI";

switch (aiProvider.ToLowerInvariant())
{
    case "azureopenai":
        {
            var endpoint = builder.Configuration["AzureOpenAI:Endpoint"]
                ?? throw new InvalidOperationException(
                    "Azure OpenAI endpoint is not configured.");

            var deploymentName = builder.Configuration["AzureOpenAI:DeploymentName"]
                ?? throw new InvalidOperationException(
                    "Azure OpenAI deployment name is not configured.");

            var apiKey = builder.Configuration["AzureOpenAI:ApiKey"]
                ?? throw new InvalidOperationException(
                    "Azure OpenAI API key is not configured.");

            builder.Services.AddSingleton<ChatClient>(_ =>
            {
                var options = new OpenAIClientOptions
                {
                    Endpoint = new Uri(
                        $"{endpoint.TrimEnd('/')}/openai/v1/")
                };

                var client = new OpenAIClient(
                    new ApiKeyCredential(apiKey),
                    options);

                return client.GetChatClient(deploymentName);
            });

            builder.Services.AddScoped<IChatService, AzureOpenAIChatService>();

            break;
        }

    case "openai":
        {
            var apiKey = builder.Configuration["OpenAI:ApiKey"]
                ?? throw new InvalidOperationException(
                    "OpenAI API key is not configured.");

            var model = builder.Configuration["OpenAI:Model"]
                ?? throw new InvalidOperationException(
                    "OpenAI model is not configured.");

            builder.Services.AddSingleton(
                new ChatClient(model, apiKey));

            builder.Services.AddScoped<IChatService, OpenAIChatService>();

            break;
        }

    default:
        throw new InvalidOperationException(
            $"Unsupported AI provider: {aiProvider}");
}

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
