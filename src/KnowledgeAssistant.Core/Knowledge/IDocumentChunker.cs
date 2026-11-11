using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAssistant.Core.Knowledge
{
    public interface IDocumentChunker
    {
        IReadOnlyList<KnowledgeChunk> Chunk(
            KnowledgeDocument document,
            int chunkSize = 500,
            int overlap = 100);
    }
}