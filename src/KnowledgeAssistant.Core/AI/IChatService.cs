using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAssistant.Core.AI
{
    public interface IChatService
    {
        Task<string> GetResponseAsync(string question, CancellationToken cancellationToken = default);
    }
}