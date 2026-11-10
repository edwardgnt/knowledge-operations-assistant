using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Infrastructure.Documents;

namespace KnowledgeAssistant.Tests.Documents
{
    public sealed class MarkdownDocumentReaderTests
    {
        [Fact]
        public async Task ReadAsync_ValidMarkdown_ReturnsSourceAndContent()
        {
            // Arrange
            string path = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid():N}.md");

            const string content = "# Refund Policy\n\nRefunds within 30 days.";

            try
            {
                await File.WriteAllTextAsync(path, content);

                var reader = new MarkdownDocumentReader();

                // Act
                var document = await reader.ReadAsync(path);

                // Assert
                Assert.Equal(Path.GetFileName(path), document.Source);
                Assert.Equal(content, document.Content);
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        [Fact]
        public async Task ReadAsync_EmptyMarkdown_ThrowsInvalidDataException()
        {
            // Arrange
            string path = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid():N}.md");

            try
            {
                await File.WriteAllTextAsync(path, string.Empty);

                var reader = new MarkdownDocumentReader();

                // Act & Assert
                await Assert.ThrowsAsync<InvalidDataException>(
                    () => reader.ReadAsync(path));
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }


        [Fact]
        public async Task ReadAsync_UnsupportedExtension_ThrowsArgumentException()
        {
            // Arrange
            string path = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid():N}.txt");

            var reader = new MarkdownDocumentReader();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => reader.ReadAsync(path));

            Assert.Equal("path", exception.ParamName);
        }
    }
}