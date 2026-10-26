using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Core.AI;
using OpenAI.Chat;

namespace KnowledgeAssistant.Infrastructure.AI;

public sealed class OpenAIChatService : IChatService
{
    private readonly ChatClient _chatClient;

    public OpenAIChatService(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<string> GetResponseAsync(
        string question,
        CancellationToken cancellationToken = default)
    {
        ChatCompletion completion = await _chatClient.CompleteChatAsync(
            [
                new UserChatMessage(question)
            ],
            cancellationToken: cancellationToken);

        return completion.Content[0].Text;
    }
}