using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Core.AI;

namespace KnowledgeAssistant.Infrastructure.AI
{
    public sealed class BaselineChatService : IChatService
    {
        public Task<string> GetResponseAsync(string question, CancellationToken cancellationToken = default)
        {
            var response = $"Baseline response received for: {question}";

            return Task.FromResult(response);
        }
    }
}