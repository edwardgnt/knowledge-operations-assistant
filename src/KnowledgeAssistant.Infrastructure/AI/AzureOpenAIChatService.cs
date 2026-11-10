using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Core.AI;
using OpenAI.Chat;


namespace KnowledgeAssistant.Infrastructure.AI
{
    public sealed class AzureOpenAIChatService : IChatService
    {
        private readonly ChatClient _chatClient;

        public AzureOpenAIChatService(ChatClient chatClient)
        {
            _chatClient = chatClient;
        }
        public async Task<string> GetResponseAsync(string question, CancellationToken cancellationToken = default)
        {
            ChatCompletion completion = await _chatClient.CompleteChatAsync(
                [
                    new UserChatMessage(question)
                ],
                cancellationToken: cancellationToken);

            return string.Concat(
              completion.Content
                  .Where(part => part.Text is not null)
                  .Select(part => part.Text));
        }
    }
}