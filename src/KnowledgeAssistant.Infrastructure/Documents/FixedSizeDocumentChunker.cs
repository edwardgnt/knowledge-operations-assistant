using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Core.Knowledge;

namespace KnowledgeAssistant.Infrastructure.Documents
{
    public sealed class FixedSizeDocumentChunker : IDocumentChunker
    {
        public IReadOnlyList<KnowledgeChunk> Chunk(
            KnowledgeDocument document,
            int chunkSize = 500,
            int overlap = 100)
        {
            ArgumentNullException.ThrowIfNull(document);

            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize));
            }

            if (overlap < 0 || overlap >= chunkSize)
            {
                throw new ArgumentOutOfRangeException(nameof(overlap));
            }

            var chunks = new List<KnowledgeChunk>();

            if (string.IsNullOrEmpty(document.Content))
            {
                return chunks;
            }

            int start = 0;

            while (start < document.Content.Length)
            {
                int length = Math.Min(
                    chunkSize,
                    document.Content.Length - start);

                string content = document.Content.Substring(start, length);

                chunks.Add(new KnowledgeChunk(
                    Source: document.Source,
                    Index: chunks.Count,
                    StartOffset: start,
                    Content: content));

                if (start + length >= document.Content.Length)
                {
                    break;
                }

                start += chunkSize - overlap;
            }

            return chunks;
        }
    }
}