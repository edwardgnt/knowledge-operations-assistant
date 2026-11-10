using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Core.Knowledge;

namespace KnowledgeAssistant.Infrastructure.Documents
{
    public sealed class MarkdownDocumentReader : IDocumentReader
    {
        public async Task<KnowledgeDocument> ReadAsync(string path, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(path);

            if (!string.Equals(
                Path.GetExtension(path),
                ".md",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "Only Markdown files are supported.",
                    nameof(path));
            }

            string content = await File.ReadAllTextAsync(path, cancellationToken);

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidDataException(
                    "The document is empty.");
            }

            return new KnowledgeDocument(
                Source: Path.GetFileName(path),
                Content: content);
        }
    }
}