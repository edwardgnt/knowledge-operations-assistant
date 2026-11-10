using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAssistant.Core.Knowledge
{
    public interface IDocumentReader
    {
        Task<KnowledgeDocument> ReadAsync(string path, CancellationToken cancellationToken = default);
    }
}